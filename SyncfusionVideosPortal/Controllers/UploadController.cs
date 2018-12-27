namespace SyncfusionVideosPortal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SyncfusionVideosPortal.Models;

    /// <summary>
    /// Class for Upload Controller
    /// </summary>
    public class UploadController : Controller
    {
        /// <summary>
        /// Upload Controller View
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            return View();
        }
        
        /// <summary>
        /// Method to upload the video details
        /// </summary>
        /// <param name="title">title value</param>
        /// <param name="description">description value</param>
        /// <param name="platform">platform value</param>
        /// <param name="isLatest">latest video</param>
        /// <param name="youTubeLink">youtube link</param>
        /// <param name="thumbnailLink">thumb nail link</param>
        /// <returns>Puts the database entry for the video link entries</returns>
        [HttpPost]
        public ActionResult UploadVideo(string title, string description, string platform, bool isLatest, string youTubeLink, string thumbnailLink)
        {
            bool isSuccess = true;
            try
            {
                PlatformModel platformModel = new PlatformModel();
                isSuccess = platformModel.NewVideoUpload(title, description, platform, isLatest, youTubeLink, thumbnailLink);
                return Json(new { result = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}