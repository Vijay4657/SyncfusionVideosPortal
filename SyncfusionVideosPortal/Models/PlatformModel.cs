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
        /// Method to get the platform List
        /// </summary>
        /// <returns>returns the platform List</returns>
        public static List<Hackathon_Platform> GetPlatformVideoList()
        {
            List<Hackathon_Platform> platformVideosList = new List<Hackathon_Platform>();
            try
            {
                var entity = new devsyncdbEntities();
                platformVideosList = (from platform in entity.Hackathon_Platform
                                      where platform.IsActive
                                      select platform).ToList();
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
                              where platform.PlatformName == PlatformName && platform.IsActive
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