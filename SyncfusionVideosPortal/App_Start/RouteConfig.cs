using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SyncfusionVideosPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UploadPage",
                url: "videos/upload",
                defaults: new { controller = "Upload", action = "Upload" }
            );

            routes.MapRoute(
                name: "PlatformPage",
                url: "videos/{platform}",
                defaults: new { controller = "Platform", action = "PlatformVideos" }
            );

            routes.MapRoute(
               name: "VideoDetailsPage",
               url: "videos/{platform}/{slugTitle}",
               defaults: new { controller = "Platform", action = "VideoDetailsPage" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
