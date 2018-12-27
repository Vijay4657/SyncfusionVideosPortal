namespace SyncfusionVideosPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SyncfusionVideosPortal.Entity;

    /// <summary>
    /// Class for platform model
    /// </summary>
    public class PlatformModel
    {
        /// <summary>
        /// Property for platform name
        /// </summary>
        public string PlatformName;

        /// <summary>
        /// Property for Platform Id
        /// </summary>
        public int PlatformId;

        /// <summary>
        /// Property for Title
        /// </summary>
        public string Title;

        /// <summary>
        /// Property for Description
        /// </summary>
        public string Description;

        /// <summary>
        /// Property for SlugTitle
        /// </summary>
        public string SlugTitle;

        /// <summary>
        /// Property for Control name
        /// </summary>
        public string ControlName;

        /// <summary>
        /// Property for Control Id
        /// </summary>
        public string ControlId;

        /// <summary>
        /// Property for knowing the Latest Video
        /// </summary>
        public bool IsLatest;

        /// <summary>
        /// Property for knowing the status of the Video
        /// </summary>
        public bool IsActive;

        /// <summary>
        /// Property for Youtube Link
        /// </summary>
        public string YoutubeLink;

        /// <summary>
        /// Property for Video thumb nail URL
        /// </summary>
        public string ThumbNailLink;

        /// <summary>
        /// Method to get the platform List
        /// </summary>
        /// <returns>returns the platform List</returns>
        public static List<Hackathon_Platform> GetPlatformVideoList()
        {
            List<Hackathon_Platform> platformVideosList = new List<Hackathon_Platform>();
            try
            {
                var entity = new Hackathon_Platform();
                platformVideosList = (from platform in entity.Hackathon_Videos
                                      where platform.IsActive).ToList();
            }
            catch (Exception ex)
            {

            }

            return platformVideosList;
        }

        /// <summary>
        /// Method to upload the video details
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="platform"></param>
        /// <param name="isLatest"></param>
        /// <param name="youTubeLink"></param>
        /// <param name="thumbnailLink"></param>
        /// <returns>Puts the database entry for the video link entries</returns>
        public bool NewVideoUpload(string title, string description, string platform, bool isLatest, string youTubeLink, string thumbnailLink)
        {
            bool isSuccess = false;
            try
            {
                var entity = new Hackathon_Videos();

            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}