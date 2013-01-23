using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace Azure.MediaAssets
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CloudMediaContext("andrewmyhremedia", "dvs2Em1bYBpihqgtX4W5yCfRbHwL4PF/pKK7a5VEV3Y=");
            string waitMessage = "Building the list. This may take a few "
        + "seconds to a few minutes depending on how many assets "
        + "you have."
        + Environment.NewLine + Environment.NewLine
        + "Please wait..."
        + Environment.NewLine;
            Console.Write(waitMessage);

            Console.WriteLine(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Access policies:");
            foreach (IAccessPolicy policy in context.AccessPolicies)
            {
                Console.WriteLine("");
                Console.WriteLine("Name:  " + policy.Name);
                Console.WriteLine("ID:  " + policy.Id);
                Console.WriteLine("Permissions: " + policy.Permissions);
                Console.WriteLine("==============");

            }

            var accessPolicy = context.AccessPolicies.FirstOrDefault();

            foreach (IAsset asset in context.Assets)
            {
                // Display the collection of assets.
                Console.WriteLine("");
                Console.WriteLine("******ASSET******");
                Console.WriteLine("Asset ID: " + asset.Id);
                Console.WriteLine("Name: " + asset.Name);
                Console.WriteLine("==============");
                Console.WriteLine("******LOCATORS******");
                if (!asset.Locators.Any())
                {
                    Console.WriteLine("No locators, creating...");
                    ILocator locator = context.Locators.CreateLocator(LocatorType.Sas, asset,
                    accessPolicy,
                    DateTime.UtcNow.AddMinutes(-5));
                    asset.Update();
                } 
                foreach(var loc in asset.Locators)
                {
                    Console.WriteLine("***********");
                    Console.WriteLine("Locator Id: " + loc.Id);
                    Console.WriteLine("Locator access policy Id: " + loc.AccessPolicyId);
                    Console.WriteLine("Access policy permissions: " + loc.AccessPolicy.Permissions);
                    Console.WriteLine("Locator expiration: " + loc.ExpirationDateTime);
                    // The locator path is the base or parent path (with included permissions) to access  
                    // the media content of an asset. To create a full URL to a specific media file, take 
                    // the locator path and then append a file name and info as needed.  
                    Console.WriteLine("Locator base path: " + loc.Path);
                    Console.WriteLine("");

                    List<String> sasUrlList = GetAssetSasUrlList(asset, loc);
                    Console.WriteLine("***********");
                    Console.WriteLine("********URLS********");
                    foreach(var url in sasUrlList)
                    {
                        Console.WriteLine("Url: " + url);
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("==============");
                Console.WriteLine("******ASSET FILES******");

                // Display the files associated with each asset. 
                foreach (IAssetFile fileItem in asset.AssetFiles)
                {
                    Console.WriteLine("Name: " + fileItem.Name);
                    Console.WriteLine("Size: " + fileItem.ContentFileSize);
                    Console.WriteLine("==============");
                }


            }

            Console.WriteLine(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Locators:");
            foreach (ILocator loc in context.Locators)
            {
                Console.WriteLine("***********");
                Console.WriteLine("Locator Id: " + loc.Id);
                Console.WriteLine("Locator asset Id: " + loc.AssetId);
                Console.WriteLine("Locator access policy Id: " + loc.AccessPolicyId);
                Console.WriteLine("Access policy permissions: " + loc.AccessPolicy.Permissions);
                Console.WriteLine("Locator expiration: " + loc.ExpirationDateTime);
                // The locator path is the base or parent path (with included permissions) to access  
                // the media content of an asset. To create a full URL to a specific media file, take 
                // the locator path and then append a file name and info as needed.  
                Console.WriteLine("Locator base path: " + loc.Path);
                Console.WriteLine("");
            }
        }

        static List<String> GetAssetSasUrlList(IAsset asset, ILocator locator)
        {
            // Declare a list to contain all the SAS URLs.
            List<String> fileSasUrlList = new List<String>();

            // If the asset has files, build a list of URLs to 
            // each file in the asset and return. 
            foreach (IAssetFile file in asset.AssetFiles)
            {
                string sasUrl = BuildFileSasUrl(file, locator);
                fileSasUrlList.Add(sasUrl);
            }

            // Return the list of SAS URLs.
            return fileSasUrlList;
        }

        // Create and return a SAS URL to a single file in an asset. 
        static string BuildFileSasUrl(IAssetFile file, ILocator locator)
        {
            // Take the locator path, add the file name, and build 
            // a full SAS URL to access this file. This is the only 
            // code required to build the full URL.
            var uriBuilder = new UriBuilder(locator.Path);
            uriBuilder.Path += "/" + file.Name;

            // Optional:  print the locator.Path to the asset, and 
            // the full SAS URL to the file
            Console.WriteLine("Locator path: ");
            Console.WriteLine(locator.Path);
            Console.WriteLine();
            Console.WriteLine("Full URL to file: ");
            Console.WriteLine(uriBuilder.Uri.AbsoluteUri);
            Console.WriteLine();


            //Return the SAS URL.
            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
