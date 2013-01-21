using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AndrewMyhre.com.Web
{
    public class AzureAssetUrlGenerator
    {
        public VideoAsset[] Assets { get; private set; }
        private CloudMediaContext _context;
        private static StringBuilder _log = new StringBuilder();
        public static StringBuilder Log { get { return _log; } }

        public VideoAsset[] Generate()
        {
            var mediaUrls = new List<VideoAsset>();

            _context = new CloudMediaContext("andrewmyhremedia", "dvs2Em1bYBpihqgtX4W5yCfRbHwL4PF/pKK7a5VEV3Y=");
            Log.Clear();
            Debug("");
            DebugFormat("***** Generating media urls at {0} *****", DateTime.Now);

            IAccessPolicy accessPolicy = null;

            foreach (var policy in _context.AccessPolicies)
            {
                if (policy.Permissions == AccessPermissions.Read)
                {
                    accessPolicy = policy;
                    break;
                }
            }

            if (accessPolicy == null)
            {
                accessPolicy = _context.AccessPolicies.Create("andrewmyhre.com media access policy",
                    TimeSpan.FromDays(1),
                    AccessPermissions.Read);
                Debug("Created a new access policy " + accessPolicy.Id + " at " + accessPolicy.Created);
            } else
            {
                Debug("Using existing access policy from " + accessPolicy.Created);
            }

            foreach (IAsset asset in _context.Assets)
            {
                DebugFormat("Asset {0} state = {1}", asset.Name, asset.State);

                var theManifest =
                        from f in asset.AssetFiles
                        //where f.Name.EndsWith(".ism")
                        where f.Name.EndsWith(".mp4")
                        select f;

                if (theManifest.Count()==0) continue;
                IAssetFile manifestFile = theManifest.First();

                //if (asset.State != AssetState.Published) continue;
                ILocator locator = null;
                var validLocators = from l in asset.Locators
                                    where l.AccessPolicy.Permissions == AccessPermissions.Read
                                          && l.ExpirationDateTime > DateTime.Now
                                          && l.Type == LocatorType.OnDemandOrigin
                                    select l;

                if (validLocators.Count() > 0)
                {
                    locator = validLocators.First();
                }
                else
                {
                    if (asset.Locators.Count() == 5)
                    {
                        asset.Locators.First().Delete();
                    }

                    locator = _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, asset, accessPolicy,
                                                                DateTime.UtcNow.AddMinutes(-5));
                }
                Debug("Created locator " + locator.Id);
                //string urlForClientStreaming = locator.Path + manifestFile.Name + "/manifest";
                var uriBuilder = new UriBuilder(locator.Path);
                uriBuilder.Path += manifestFile.Name;
                string urlForClientStreaming = uriBuilder.Uri.AbsoluteUri;
                mediaUrls.Add(new VideoAsset() { ContentUrl = urlForClientStreaming ,
                                                 Type = (asset.Name.StartsWith("flicker") || asset.Name.StartsWith("stock_vhs")) ? "flicker"
                                                    : asset.Name.StartsWith("v") ? "wallpaper" : "content"
                });
            }

            this.Assets = mediaUrls.ToArray();

            Debug("*************");
            Debug("GENERATED URLS");
            Debug("*************");
            foreach(var url in mediaUrls)
            {
                DebugFormat("{0}:{1}", url.Type, url.ContentUrl);
            }

            return mediaUrls.ToArray();
        }

        private void Debug(string message)
        {
            Log.AppendFormat("<div>{0}</div>", message);
        }
        private void DebugFormat(string messageFormat, params object[] args)
        {
            Log.AppendFormat("<div>{0}</div>", string.Format(messageFormat, args));
        }

        static IEnumerable<VideoAsset> GetAssetSasUrlList(IAsset asset, ILocator locator)
        {
            // Declare a list to contain all the SAS URLs.
            var fileSasUrlList = new List<VideoAsset>();

            // If the asset has files, build a list of URLs to 
            // each file in the asset and return. 
            foreach (IAssetFile file in asset.AssetFiles)
            {
                var content = BuildFileSasUrl(file, locator);
                fileSasUrlList.Add(content);
            }

            // Return the list of SAS URLs.
            return fileSasUrlList;
        }

        // Create and return a SAS URL to a single file in an asset. 
        static VideoAsset BuildFileSasUrl(IAssetFile file, ILocator locator)
        {
            // Take the locator path, add the file name, and build 
            // a full SAS URL to access this file. This is the only 
            // code required to build the full URL.
            var uriBuilder = new UriBuilder(locator.Path);
            uriBuilder.Path += "/" + file.Name;

            //Return the SAS URL.
            return new VideoAsset() { 
                        ContentUrl = uriBuilder.Uri.AbsoluteUri,
                        Type = (file.Name.StartsWith("flicker") || file.Name.StartsWith("stock_vhs")) ? "flicker"
                            : file.Name.StartsWith("v") ? "wallpaper" : "content"};
        }
    }

    public class VideoAsset
    {
        public string ContentUrl { get; set; }
        public string Type { get; set; }
    }
}