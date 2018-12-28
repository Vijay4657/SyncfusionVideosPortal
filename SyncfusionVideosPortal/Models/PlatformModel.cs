namespace SyncfusionVideosPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SyncfusionVideosPortal.Entity;
    using System.Text;

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
        /// Property for Platform's list
        /// </summary>
        public List<Hackathon_Platform> PlatformList;

        /// <summary>
        /// Property for Video's list
        /// </summary>
        public List<Hackathon_Videos> VideosDetails;

        /// <summary>
        /// Method to get the platform List
        /// </summary>
        /// <returns>returns the platform List</returns>
        public static List<Hackathon_Videos> GetPlatformVideoList()
        {
            List<Hackathon_Videos> platformVideosList = new List<Hackathon_Videos>();
            try
            {
                var entity = new devsyncdbEntities();
                platformVideosList = (from video in entity.Hackathon_Videos
                                      where video.IsActive
                                      orderby video.CreatedDate descending
                                      select video).ToList();
            }
            catch (Exception ex)
            {

            }

            return platformVideosList;
        }

        public static List<Hackathon_Platform> getPlatformList()
        {
            List<Hackathon_Platform> platformList = new List<Hackathon_Platform>();
            try
            {
                var entity = new devsyncdbEntities();
                platformList = (from platform in entity.Hackathon_Platform
                                      where platform.IsActive
                                      select platform).ToList();
            }
            catch (Exception ex)
            {

            }

            return platformList;
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
                var entity = new devsyncdbEntities();
                string slugTitle = string.Empty;
                slugTitle = GetSlugTitle(title);
                Hackathon_Videos videosEntry = new Hackathon_Videos();
                videosEntry.ControlId = 0;
                videosEntry.CreatedDate = DateTime.Now;
                videosEntry.Description = description;
                videosEntry.IsActive = true;
                videosEntry.IsFeature = true;
                videosEntry.IsLatest = IsLatest;
                videosEntry.PlatformId = GetPlatformId(platform);
                videosEntry.SlugTitle = slugTitle;
                videosEntry.ThumbnailLink = thumbnailLink;
                videosEntry.Title = title;
                videosEntry.VideoLink = youTubeLink;
                entity.Hackathon_Videos.Add(videosEntry);
                entity.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return isSuccess;
        }

        /// <summary>
        /// Method to Update the viewcount of the 
        /// </summary>
        /// <param name="slugTitle">slug title</param>
        public void AddViewCount(string slugTitle)
        {
            try
            {
                int existingCount;
                using (var entity = new devsyncdbEntities())
                {
                    var videoDetail = (from video in entity.Hackathon_Videos
                                     where video.SlugTitle == slugTitle && video.IsActive
                                     select video).FirstOrDefault();
                    if (videoDetail != null)
                    {
                        existingCount = (int)videoDetail.ViewCount;
                        int newCount = existingCount + 1;
                        videoDetail.ViewCount = newCount;
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Method to Update the like count of the 
        /// </summary>
        /// <param name="videoId">Video Id</param>
        /// <returns>Returns the new like count</returns>
        public int AddLikeCount(int videoId)
        {
            int newCount = 0;
            try
            {
                int existingCount;
                using (var entity = new devsyncdbEntities())
                {
                    var videoDetail = (from video in entity.Hackathon_Videos
                                       where video.VideoId == videoId && video.IsActive
                                       select video).FirstOrDefault();
                    if (videoDetail != null)
                    {
                        existingCount = (int)videoDetail.LikeCount;
                        newCount = existingCount + 1;
                        videoDetail.LikeCount = newCount;
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return newCount;
        }

        /// <summary>
        /// Get the comment details for the video details page
        /// </summary>
        /// <param name="platform">platform value</param>
        /// <param name="slugTitle">slug title</param>
        /// <returns>returns the comment details page</returns>
        public List<Hackathon_Comments> GetCommentDetails(string platform, string slugTitle)
        {
            List<Hackathon_Comments> commentDetails = new List<Hackathon_Comments>();
            try
            {
                int platformId = GetPlatformIdFromShortName(platform);
                using (var entity = new devsyncdbEntities())
                {
                    commentDetails = (from video in entity.Hackathon_Videos
                                      join comment in entity.Hackathon_Comments on video.VideoId equals comment.VideoId
                                      where video.PlatformId == platformId && video.SlugTitle == slugTitle
                                      select comment).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return commentDetails;
        }

        /// <summary>
        /// Add new comment for video
        /// </summary>
        /// <param name="videoId">video Id</param>
        /// <param name="comment">comment Details</param>
        /// <returns>adds the new comment for video</returns>
        public bool AddNewComment(int videoId, string comment)
        {
            bool isSuccess = false;
            try
            {
                using (var entity = new devsyncdbEntities())
                {
                    Hackathon_Comments commentDetails = new Hackathon_Comments();
                    commentDetails.VideoId = videoId;
                    commentDetails.Comment = comment;
                    commentDetails.CreatedDate = DateTime.Now;
                    commentDetails.IsActive = true;
                    entity.Hackathon_Comments.Add(commentDetails);
                    entity.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isSuccess;
        }

        /// <summary>
        /// Method to get the platform display name
        /// </summary>
        /// <param name="shortName">short Name</param>
        /// <returns>returns the platform display name from short name</returns>
        public string GetPlatformDisplayName(string shortName)
        {
            string platformName = string.Empty;
            try
            {
                using (var entity = new devsyncdbEntities())
                {
                    platformName = (from platform in entity.Hackathon_Platform
                                    where platform.ShortName == shortName && platform.IsActive
                                    select platform.PlatformName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return platformName;
        }

        /// <summary>
        /// Method to get the platform Id
        /// </summary>
        /// <param name="platformName">platform name</param>
        /// <returns>returns the platform Id of the platform</returns>
        public int GetPlatformId(string platformName)
        {
            int platformId = 0;
            try
            {
                var entity = new devsyncdbEntities();
                platformId = (from platform in entity.Hackathon_Platform
                              where platform.PlatformName == platformName && platform.IsActive
                              select platform.PlatformId).First();
            }
            catch (Exception ex)
            {

            }

            return platformId;
        }

        /// <summary>
        /// Method to get the platform Id
        /// </summary>
        /// <param name="platformName">platform name</param>
        /// <returns>returns the platform Id from the platform short name</returns>
        public int GetPlatformIdFromShortName(string platformShortName)
        {
            int platformId = 0;
            try
            {
                var entity = new devsyncdbEntities();
                platformId = (from platform in entity.Hackathon_Platform
                              where platform.ShortName == platformShortName && platform.IsActive
                              select platform.PlatformId).First();
            }
            catch (Exception ex)
            {

            }

            return platformId;
        }

        /// <summary>
        /// Method to get the video details
        /// </summary>
        /// <param name="platformShortName">platform short name</param>
        /// <param name="slugTitle">slug title</param>
        /// <returns>returns the video details based on the shortname and title</returns>
        public Hackathon_Videos GetVideoDetails(string platformShortName, string slugTitle)
        {
            Hackathon_Videos videoDetails = new Hackathon_Videos();
            int platformId = GetPlatformIdFromShortName(platformShortName);
            try
            {
                using (var entity = new devsyncdbEntities())
                {
                    videoDetails = (from video in entity.Hackathon_Videos
                                    where video.PlatformId == platformId && video.SlugTitle == slugTitle && video.IsActive
                                    select video).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            //// To Add the video view count while visiting the page
            AddViewCount(slugTitle);
            return videoDetails;
        }

        /// <summary>
        /// Format the title to use in the URL
        /// </summary>
        /// <param name="title">article title</param>
        /// <returns>return slug title</returns>
        public static string GetSlugTitle(string title)
        {
            try
            {
                title = string.IsNullOrEmpty(title) ? string.Empty : title.Trim().ToLower();
                var titleSlug = new StringBuilder();
                var isPreviousDash = false;
                foreach (char character in title)
                {
                    if (titleSlug.Length > 101)
                    {
                        break;
                    }
                    else if ((character >= '0' && character <= '9') || (character >= 'a' && character <= 'z'))
                    {
                        titleSlug.Append(character);
                        isPreviousDash = false;
                    }
                    else if (character == ' ' || character == ',' || character == '.' || character == '/' || character == '\\' || character == '-' || character == '_' || character == '=' || character == '“' || character == '”')
                    {
                        if (!isPreviousDash)
                        {
                            titleSlug.Append('-');
                            isPreviousDash = true;
                        }
                    }
                    else if ((int)character > 128)
                    {
                        isPreviousDash = false;
                        if ("àåáâäãåą".Contains(character))
                        {
                            titleSlug.Append('a');
                        }
                        else if ("èéêëę".Contains(character))
                        {
                            titleSlug.Append('e');
                        }
                        else if ("ìíîïı".Contains(character))
                        {
                            titleSlug.Append('i');
                        }
                        else if ("òóôõöøőð".Contains(character))
                        {
                            titleSlug.Append('o');
                        }
                        else if ("ùúûüŭů".Contains(character))
                        {
                            titleSlug.Append('u');
                        }
                        else if ("çćčĉ".Contains(character))
                        {
                            titleSlug.Append('c');
                        }
                        else if ("żźž".Contains(character))
                        {
                            titleSlug.Append('z');
                        }
                        else if ("śşšŝ".Contains(character))
                        {
                            titleSlug.Append('s');
                        }
                        else if ("ñń".Contains(character))
                        {
                            titleSlug.Append('n');
                        }
                        else if ("ýÿ".Contains(character))
                        {
                            titleSlug.Append('y');
                        }
                        else if ("ğĝ".Contains(character))
                        {
                            titleSlug.Append('g');
                        }
                        else if (character == 'ř')
                        {
                            titleSlug.Append('r');
                        }
                        else if (character == 'ł')
                        {
                            titleSlug.Append('l');
                        }
                        else if (character == 'đ')
                        {
                            titleSlug.Append('d');
                        }
                        else if (character == 'ß')
                        {
                            titleSlug.Append("ss");
                        }
                        else if (character == 'Þ')
                        {
                            titleSlug.Append("th");
                        }
                        else if (character == 'ĥ')
                        {
                            titleSlug.Append('h');
                        }
                        else if (character == 'ĵ')
                        {
                            titleSlug.Append('j');
                        }
                        else
                        {
                            titleSlug.Append(string.Empty);
                        }
                    }
                }

                return titleSlug.Length > 101 ? titleSlug.ToString().Substring(0, titleSlug.ToString().LastIndexOf('-')) : titleSlug.ToString().TrimEnd('-');
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}