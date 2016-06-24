using System;
using System.Collections.Generic;
using System.Linq;
using Cite.DomainAuthentication;

/// <summary>
/// Summary description for MediaSearcher
/// </summary>
public static class MediaSearcher
{
    /// <summary>
    /// Search all media items (video, audio, web, file, image)
    /// </summary>
    /// <param name="query"></param>
    /// <param name="userAccount">The user that is searching. If one isn't provided everything is searched.</param>
    /// <returns></returns>
    public static IEnumerable<MediaItem> SearchAllMedia(string query, string sortBy = null, bool descendingOrder = false, DomainAccount account = null)
    {
        // Instance of the database class
        DBDataContext db = DBDataContext.CreateInstance();

        // Split the query into tokens
        string[] queryTokens = query.ToLower().Split(new string[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        List<Audio> audios = new List<Audio>();
        List<Video> videos = new List<Video>();
        List<Websites> websites = new List<Websites>();
        List<Files> files = new List<Files>();
        List<Images> images = new List<Images>();
        
        // Get all media items from the database which this user account has permission to access
        //-----Videos-----//
        // Get all of the videos where the user is the owner of the group the videos are in
        List<Video> groupVideos = (from v in db.Videos
                                   join g in db.StudentGroups
                                   on v.GroupID equals g.GroupID
                                   where (g.FacultyOwner.ToLower() == account.Username.ToLower())
                                   select v).ToList();
        // Get all the videos where the user is the owner of the video
        List<Video> userVideos = (from v in db.Videos
                                  where (v.Username.ToLower() == account.Username.ToLower())
                                  select v).ToList();
        // Combine the video lists, removing duplicates
        videos = groupVideos.Union(userVideos).ToList();

        //-----Audios-----//
        // Get all of the audios where the user is the owner of the group the audios are in
        List<Audio> groupAudios = (from a in db.Audios
                                   join g in db.StudentGroups
                                   on a.GroupID equals g.GroupID
                                   where (g.FacultyOwner.ToLower() == account.Username.ToLower())
                                   select a).ToList();
        // Get all the videos where the user is the owner of the video
        List<Audio> userAudios = (from a in db.Audios
                                  where (a.Username.ToLower() == account.Username.ToLower())
                                  select a).ToList();
        // Combine the video lists, removing duplicates
        audios = groupAudios.Union(userAudios).ToList();

        //-----Websites-----//
        // Get all of the websites where the user is the owner of the group the websites are in
        List<Websites> groupWebsites = (from w in db.Websites
                                        join g in db.StudentGroups
                                        on w.GroupID equals g.GroupID
                                        where (g.FacultyOwner.ToLower() == account.Username.ToLower())
                                        select w).ToList();
        // Get all the websites where the user is the owner of the website
        List<Websites> userWebsites = (from w in db.Websites
                                       where (w.Username.ToLower() == account.Username.ToLower())
                                       select w).ToList();
        // Combine the website lists, removing duplicates
        websites = groupWebsites.Union(userWebsites).ToList();

        //-----Files-----//
        // Get all of the files where the user is the owner of the group the files are in
        List<Files> groupFiles = (from f in db.Files
                                  join g in db.StudentGroups
                                  on f.GroupID equals g.GroupID
                                  where (g.FacultyOwner.ToLower() == account.Username.ToLower())
                                  select f).ToList();
        // Get all the files where the user is the owner of the file
        List<Files> userFiles = (from f in db.Files
                                 where (f.Username.ToLower() == account.Username.ToLower())
                                 select f).ToList();
        // Combine the file lists, removing duplicates
        files = groupFiles.Union(userFiles).ToList();

        //-----Images-----//
        // Get all of the images where the user is the owner of the group the images are in
        List<Images> groupImages = (from i in db.Images
                                    join g in db.StudentGroups
                                    on i.GroupID equals g.GroupID
                                    where (g.FacultyOwner.ToLower() == account.Username.ToLower())
                                    select i).ToList();
        // Get all the images where the user is the owner of the image
        List<Images> userImages = (from i in db.Images
                                   where (i.Username.ToLower() == account.Username.ToLower())
                                   select i).ToList();
        // Combine the image lists, removing duplicates
        images = groupImages.Union(userImages).ToList();


        // Get all the media items from the media lists that contain at least one of the search tokens in their title, description or author name
        foreach (String token in queryTokens)
        {
            audios = (audios.AsEnumerable().Where(x => x.Username.ToLower().Contains(token) || 
                                                                x.Title.ToLower().Contains(token) ||
                                                                x.Description.ToLower().Contains(token))).ToList();

            videos = (videos.AsEnumerable().Where(x => x.Username.ToLower().Contains(token) ||
                                                                x.Title.ToLower().Contains(token) ||
                                                                x.Description.ToLower().Contains(token))).ToList();

            websites = (websites.AsEnumerable().Where(x => x.Username.ToLower().Contains(token) ||
                                                                x.Title.ToLower().Contains(token) ||
                                                                x.Description.ToLower().Contains(token))).ToList();

            files = (files.AsEnumerable().Where(x => x.Username.ToLower().Contains(token) ||
                                                                x.Title.ToLower().Contains(token) ||
                                                                x.Description.ToLower().Contains(token))).ToList();

            images = (images.AsEnumerable().Where(x => x.Username.ToLower().Contains(token) ||
                                                                x.Title.ToLower().Contains(token) ||
                                                                x.Description.ToLower().Contains(token))).ToList();
        }


        
        // Create a list of media items by combining all the other lists
        List<MediaItem> mediaItems = new List<MediaItem>();

        foreach (Audio audio in audios)
        {
            MediaItem mi = new MediaItem();
            mi.SearchWeight = 0;
            mi.MediaType = "audio";
            mi.ID = audio.AudioID;
            mi.Title = audio.Title;
            mi.Description = audio.Description;
            mi.DatePosted = audio.DatePosted;
            mi.Username = audio.Username;
            mi.Size = audio.Size;
            mi.NumOfHits = audio.NumOfHits;
            mi.LastHit = audio.LastHit.HasValue ? (System.DateTime)audio.LastHit : (System.Nullable<System.DateTime>)null;
            mi.AutoDeleteDate = audio.AutoDeleteDate.HasValue ? (System.DateTime)audio.AutoDeleteDate : (System.Nullable<System.DateTime>)null;
            mi.Author = audio.Author;
            mi.ShareStatus = audio.ShareStatus;
            mi.GroupID = audio.GroupID.HasValue ? (int)audio.GroupID : (System.Nullable<int>)null;

            mediaItems.Add(mi);
        }

        foreach (Video video in videos)
        {
            MediaItem mi = new MediaItem();
            mi.SearchWeight = 0;
            mi.MediaType = "video";
            mi.ID = video.VideoID;
            mi.Title = video.Title;
            mi.Description = video.Description;
            mi.DatePosted = video.DatePosted;
            mi.Username = video.Username;
            mi.Size = video.Size;
            mi.NumOfHits = video.Views;
            mi.LastHit = video.LastView.HasValue ? (System.DateTime)video.LastView : (System.Nullable<System.DateTime>)null;
            mi.AutoDeleteDate = video.AutoDeleteDate.HasValue ? (System.DateTime)video.AutoDeleteDate : (System.Nullable<System.DateTime>)null;
            mi.Author = video.Author;
            mi.ShareStatus = video.ShowStatus;
            mi.GroupID = video.GroupID.HasValue ? (int)video.GroupID : (System.Nullable<int>)null;

            mediaItems.Add(mi);
        }

        foreach (Websites website in websites)
        {
            MediaItem mi = new MediaItem();
            mi.SearchWeight = 0;
            mi.MediaType = "website";
            mi.ID = website.WebID;
            mi.Title = website.Title;
            mi.Description = website.Description;
            mi.DatePosted = website.DatePosted;
            mi.Username = website.Username;
            mi.Size = website.Size;
            mi.NumOfHits = website.Views;
            mi.LastHit = website.LastView.HasValue ? (System.DateTime)website.LastView : (System.Nullable<System.DateTime>)null;
            mi.AutoDeleteDate = website.AutoDeleteDate.HasValue ? (System.DateTime)website.AutoDeleteDate : (System.Nullable<System.DateTime>)null;
            mi.Author = website.Author;
            mi.ShareStatus = website.ShowStatus;
            mi.GroupID = website.GroupID.HasValue ? (int)website.GroupID : (System.Nullable<int>)null;

            mediaItems.Add(mi);
        }

        foreach (Files file in files)
        {
            MediaItem mi = new MediaItem();
            mi.SearchWeight = 0;
            mi.MediaType = "file";
            mi.ID = file.FileID;
            mi.Title = file.Title;
            mi.Description = file.Description;
            mi.DatePosted = file.DatePosted;
            mi.Username = file.Username;
            mi.Size = file.Size;
            mi.NumOfHits = file.Views;
            mi.LastHit = file.LastView.HasValue ? (System.DateTime)file.LastView : (System.Nullable<System.DateTime>)null;
            mi.AutoDeleteDate = file.AutoDeleteDate.HasValue ? (System.DateTime)file.AutoDeleteDate : (System.Nullable<System.DateTime>)null;
            mi.Author = file.Author;
            mi.ShareStatus = file.ShowStatus;
            mi.GroupID = file.GroupID.HasValue ? (int)file.GroupID : (System.Nullable<int>)null;

            mediaItems.Add(mi);
        }

        foreach (Images image in images)
        {
            MediaItem mi = new MediaItem();
            mi.SearchWeight = 0;
            mi.MediaType = "image";
            mi.ID = image.ImageID;
            mi.Title = image.Title;
            mi.Description = image.Description;
            mi.DatePosted = image.DatePosted;
            mi.Username = image.Username;
            mi.Size = image.Size;
            mi.NumOfHits = image.Views;
            mi.LastHit = image.LastView.HasValue ? (System.DateTime)image.LastView : (System.Nullable<System.DateTime>)null;
            mi.AutoDeleteDate = image.AutoDeleteDate.HasValue ? (System.DateTime)image.AutoDeleteDate : (System.Nullable<System.DateTime>)null;
            mi.Author = image.Author;
            mi.ShareStatus = image.ShowStatus;
            mi.GroupID = image.GroupID.HasValue ? (int)image.GroupID : (System.Nullable<int>)null;

            mediaItems.Add(mi);
        }

        if (sortBy == null || sortBy == "relevance")
            return SortByRelevance(mediaItems, query);
        else
            return mediaItems;
        //else if (sortBy == "title")
        //    return SortByTitle(mediaItems, descendingOrder);
        //else if (sortBy == "date")
        //    return SortByDatePosted(mediaItems, descendingOrder);
        //else if (sortBy == "author")
        //    return SortByOwner(mediaItems, descendingOrder);
        //else
        //    return SortByRelevance(mediaItems, query);
    }

    /// <summary>
    /// Sorts a List of MediaItems by relevance (how similar they are to the query string)
    /// </summary>
    /// <param name="mediaItems"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    private static IEnumerable<MediaItem> SortByRelevance(IEnumerable<MediaItem> mediaItems, string query)
    {
        List<MediaItem> SearchResults = new List<MediaItem>();

        string[] queryTokens = query.ToLower().Split(new string[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        foreach(MediaItem mediaItem in mediaItems)
        {
            // Get the owner of the mediaItem
            DomainAccount userAccount = new DomainAccount(mediaItem.Username);

            // Split attributes into tokens
            string[] titleTokens = mediaItem.Title.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            string[] descriptionTokens = mediaItem.Description.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            //Break author into string arrays.
            string[] authorTokens = null;
            if (!string.IsNullOrEmpty(mediaItem.Author))
                authorTokens = mediaItem.Author.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);


            // Compare each token in the query to each token in the title, description and author
            for (int i = 0; i <  queryTokens.Length; i++)
            {
                for (int k = 0; k < titleTokens.Length; k++)
                {
                    if (titleTokens[k].Contains(queryTokens[i]))
                    {
                        // First word matches. Begin comparing next words for as long as possible.
                        // We will weight matching sequences of words higher than single word matches.
                        int length = 1;
                        int tempi = i;
                        int tempk = k;
                        while (++tempi < queryTokens.Length && ++tempk < titleTokens.Length && queryTokens[tempi] == titleTokens[tempk])
                        {
                            length++;
                        }
                        // Title is weighted more than description.
                        // Square length to put a lot of emphasis on longer matching sequences.
                        mediaItem.SearchWeight += length * length * 2;
                    }
                } // End of title inner loop.

                for (int k = 0; k < descriptionTokens.Length; k++) // Inner loop over description words.
                {
                    if (descriptionTokens[k].Contains(queryTokens[i]))
                    {
                        // First word matches. Begin comparing next words for as long as possible.
                        // We will weight matching sequences of words higher than single word matches.
                        int length = 1;
                        int tempi = i;
                        int tempk = k;
                        while (++tempi < queryTokens.Length && ++tempk < descriptionTokens.Length && queryTokens[tempi] == descriptionTokens[tempk])
                        {
                            length++;
                        }
                        // Square length to put a lot of emphasis on longer matching sequences.
                        mediaItem.SearchWeight += length * length;
                    }
                } // End of description inner loop.

                //author
                if (authorTokens != null)
                {
                    for (int k = 0; k < authorTokens.Length; k++) // Inner loop over author words.
                    {
                        if (authorTokens[k].Contains(queryTokens[i]))
                        {
                            // First word matches. Begin comparing next words for as long as possible.
                            // We will weight matching sequences of words higher than single word matches.
                            int length = 1;
                            int tempi = i;
                            int tempk = k;
                            while (++tempi < queryTokens.Length && ++tempk < authorTokens.Length && queryTokens[tempi] == authorTokens[tempk])
                            {
                                length++;
                            }
                            // Author is weighted more than description.
                            // Square length to put a lot of emphasis on longer matching sequences.
                            mediaItem.SearchWeight += length * length * 2;
                        }
                    }
                } // End of author inner loop.

                // Now compare to the owner of the video.
                if (queryTokens[i] == userAccount.Username.ToLower())
                {
                    mediaItem.SearchWeight += 2;
                }
                if (queryTokens[i] == userAccount.FirstName.ToLower())
                {
                    mediaItem.SearchWeight += 2;
                }
                if (queryTokens[i] == userAccount.LastName.ToLower())
                {
                    mediaItem.SearchWeight += 2;
                }

            } // End of outer loop.


            if (mediaItem.SearchWeight > 0)
            {
                SearchResults.Add(mediaItem);
            }
        } // End of foreach website loop.

        // Sort by the search values
        SearchResults = SearchResults.OrderBy(i => -i.SearchWeight).ToList();
        return SearchResults;
    }

    ///// <summary>
    ///// Sorts a List of MediaItems alphabetically by the title
    ///// </summary>
    ///// <param name="mediaItems"></param>
    ///// <returns></returns>
    //private static IEnumerable<MediaItem> SortByTitle(IEnumerable<MediaItem> mediaItems, bool descendingOrder)
    //{
    //    if(descendingOrder)
    //        mediaItems = mediaItems.OrderBy(i => i.Title).Reverse().ToList();
    //    else
    //        mediaItems = mediaItems.OrderBy(i => i.Title).ToList();

    //    return mediaItems;
    //}

    ///// <summary>
    ///// Sorts a List of MediaItems by date posted
    ///// </summary>
    ///// <param name="mediaItems"></param>
    ///// <param name="descendingOrder"></param>
    ///// <returns></returns>
    //private static IEnumerable<MediaItem> SortByDatePosted(IEnumerable<MediaItem> mediaItems, bool descendingOrder)
    //{
    //    if(descendingOrder)
    //        mediaItems = mediaItems.OrderBy(i => i.DatePosted).Reverse().ToList();
    //    else
    //        mediaItems = mediaItems.OrderBy(i => i.Author).ToList();

    //    return mediaItems;
    //}

    ///// <summary>
    ///// Sorts a List of MediaItems alphabetically by the authors name
    ///// </summary>
    ///// <param name="mediaItems"></param>
    ///// <param name="descendingOrder"></param>
    ///// <returns></returns>
    //private static IEnumerable<MediaItem> SortByOwner(IEnumerable<MediaItem> mediaItems, bool descendingOrder)
    //{
    //    if (descendingOrder)
    //        mediaItems = mediaItems.OrderBy(i => i.Author).Reverse().ToList();
    //    else
    //        mediaItems = mediaItems.OrderBy(i => i.Author).ToList();

    //    return mediaItems;
    //}
}