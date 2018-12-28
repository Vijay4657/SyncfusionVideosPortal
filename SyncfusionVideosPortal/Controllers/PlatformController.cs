namespace SyncfusionVideosPortal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SyncfusionVideosPortal.Models;
    using SyncfusionVideosPortal.Entity;

    /// <summary>
    /// Controller for platform details page
    /// </summary>
    public class PlatformController : Controller
    {
        /// <summary>
        /// Action for showing the Video Links based on platform
        /// </summary>
        /// <returns>returns the video links based on platform</returns>
        public ActionResult PlatformVideos()
        {
            PlatformModel platform = new PlatformModel();
            platform.PlatformList = PlatformModel.getPlatformList();
            platform.VideosDetails = PlatformModel.GetPlatformVideoList();
            return View(platform);
        }

        /// <summary>
        /// Action for showing the video details page
        /// </summary>
        /// <returns>shows the video details page</returns>
        public ActionResult VideoDetailsPage(string platform, string slugTitle)
        {
            PlatformModel platformModel = new PlatformModel();
            Hackathon_Videos videoDetails = platformModel.GetVideoDetails(platform, slugTitle);
            return View(videoDetails);
        }
    }
}