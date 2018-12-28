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
            ViewBag.CommentDetails = platformModel.GetCommentDetails(platform, slugTitle);
            return View(videoDetails);
        }

        /// <summary>
        /// Action for updating comments
        /// </summary>
        /// <param name="videoId">video Id</param>
        /// <returns>action for updating the comments</returns>
        public ActionResult AddLikeCount(int videoId)
        {
            PlatformModel platformModel = new PlatformModel();
            bool isSuccess = platformModel.AddLikeCount(videoId);
            return Json(new { result = isSuccess }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action for updating comments
        /// </summary>
        /// <param name="videoId">video Id</param>
        /// <param name="comment">comment</param>
        /// <returns>action for updating the comments</returns>
        public ActionResult UpdateComments(int videoId, string comment)
        {
            PlatformModel platformModel = new PlatformModel();
            bool isSuccess = platformModel.AddNewComment(videoId, comment);
            return Json(new { result = isSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}