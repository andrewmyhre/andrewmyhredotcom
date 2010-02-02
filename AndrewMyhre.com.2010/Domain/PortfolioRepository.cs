using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using AndrewMyhre.com._2010.ViewModels;

namespace AndrewMyhre.com._2010
{
    public class PortfolioRepositoryFake : AndrewMyhre.com._2010.IPortfolioRepository
    {
        public IEnumerable<PortfolioViewModel> All()
        {
            return new PortfolioViewModel[] {
                new PortfolioViewModel() {
                    Project="Digital Wall 1.0",
                    Client="Tequila",
                    Description=@"The first incarnation of the 'Digital Wall' allowed any number of people to collaboratively create graffiti on a physical wall.

Anyone on a computer with an internet connection could go to the website, draw on the screen and see their contribution beamed via projector onto the wall. The feedback was provided by a webcam feed of the wall itself.
                    
The drawing data is sent from the Silverlight application to a SOAP web service on the server where it is stored in a database. A WPF application running on the machine plugged into the projector checks for new drawing data on a seperate thread, and adds it to the canvas as it comes in. 
                    
It also provides input via a Wii remote so participants can draw directly on the wall in a more natural way. I chose the approach of showing a video feed of the wall rather than rendering the aggregate drawing data directly on screen because I wanted to convey to the user that they really are affecting physical space somewhere in the world.
                    
Technologies used: ASP.Net 3.5, LINQ, Silverlight 2.0, WCF, WPF, WiimoteLib",
                    VideoFilename = "digitalwall.mp4"
                },

                new PortfolioViewModel() {
                    Project="Digital Wall 2.0",
                    Client="Tequila",
                    Description=@"The second incarnation of the Digital Wall takes a slightly different approach in that participants can not longer see a video feed of a real wall. Instead they can contribute discrete, personal canvases to a continuous feed in the style of Twitter. A user can also take another previous contribution and re-hash it into something new.
                        
The application was rebuilt from the ground-up with a more sophisticated drawing upload implementation and more creation options with images and text.
                        
Content can also be tagged and discovered via the tag cloud, as well as by using the search filters. Users can read and post comments for creations. Finally, creations are uploaded to a TwitPic account where they can are broadcast via Twitter and stored indefinitely.
                        
Technologies used: ASP.Net 3.5, ASP.Net MVC, WCF, Silverlight 2.0",
                    ImageFilenames = new string[] { "dw2_1.png", "dw2_2.png", "dw2_3.png", "dw2_4.png", "dw2_5.png", "dw2_6.png" }
                },

                new PortfolioViewModel() {
                    Project="Translation Tool",
                    Client="Canon Europe",
                    Description=@"Translation Tool allows Canon to easily translate content for their online product configurators.

Because Canon target many European markets and update their configurators regularly, this application was built to ease the localisation process. A Flash front-end runs on top of a SOAP layer built using WCF. The localised data is stored as XML files on the server.
                
The application was extended in 2009 to allow publishing the XML translation files to any website via a SOAP call, to support localisation management for the Canon Create site.
                        
Technologies used: ASP.Net 2.0",
                    ImageFilenames = new string[] { "translation_1.png", "translation_2.png", "translation_3.png", "translation_4.png" }
                },

                new PortfolioViewModel() {
                    Project="Create*",
                    Client="Canon Europe",
                    Description=@"Canon Create* allows Canon partners to download up-to-date promotional material and easily customise it. Canon representatives can create document templates in the application and define editable content, which partners can then tailor to their own specific markets.
                
The application employs a sophisticated role management system based on the ASP.Net 2.0 Membership system which makes it simple for Canon to configure the permissions given to site users.

In 2008 the Create* site was readied for expansion into several European countries by implementing a localisation system. As the long-term requirements for localisation and content updates were unclear we abstracted the storage logic using the Provider Model design pattern. This allowed us to build an implementation using Xml files as a repository, and in the future swap this repository with another, such as SQL. Indeed this component was reused in several projects and extended to become a mature internal project.
                        
Technologies used: ASP.Net 2.0

Design Patterns: Provider Model, Dependency Injection, Factory Method, IoC",
                    ImageFilenames = new string[] { "create_1.png", "create_2.png" }
                },
                                
                new PortfolioViewModel() {
                Project = "What A Find",
                Client = "What A Find",
                Description = @"What A Find is an eCommerce service offering factory seconds direct to consumers.

The website features seasonal themes, a large selection of products and employs a sophisticated 'XML Package' system for authoring page templates.

Technologies used: ASP.Net 2.0, ASP.Net Storefront, XSLT, XML",
                ImageFilenames = new string[] { "waf_1.png", "waf_2.png" }
            },

            new PortfolioViewModel() {
                Project = "Army/Camouflage Web Surveys",
                Client = "The Army",
                Description = @"These websites were a survey built for the Army to support a data collection campaign.

Flyers distributed with Army publications as well as direct mail drove contacts to these websites via personalised Urls, by offering a free t-shirt. 

Visitors were then asked to provide up-to-date address details and the Camouflage stream (the Army's school-age interest group) were also required to provide some information about tertiary study plans.

The websites were implemented as a single ASP.Net MVC web application which, through the use of design patterns, easily provided a way to offer both streams the correct respective user journey while keeping application logic robust and testable.

Technologies used: ASP.Net 3.5, ASP.Net MVC 1.0",
                ImageFilenames = new string[] { "army_1.png", "army_2.png", "army_3.png", "army_4.png", "army_5.png", "army_6.png", "army_7.png" }
            },

            new PortfolioViewModel() {
                Project = "Carbon Labelling",
                Client = "The Carbon Trust",
                Description = @"Carbon Labelling is the Carbon Trust's business standards arm. The initiative aims to provide a set of standards by which businesses can be graded on their carbon footprint and have their products labelled appropriately. The Carbon Trust also provide assistance with lowering a business' carbon footprint.

The website is a full ASP.Net MVC implementation with all website copy stored in an Xml file, allowing for frequent updates to news articles and case studies in a simple, cost-effective way.

Technologies used: ASP.Net 3.5, ASP.Net MVC 1.0",
                ImageFilenames = new string[] { "cl_1.png", "cl_2.png", "cl_3.png", "cl_4.png" }
            }

            };


        }
    }
}
