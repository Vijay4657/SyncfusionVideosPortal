namespace SyncfusionVideosPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

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
    }
}