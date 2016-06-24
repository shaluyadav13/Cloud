using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using VideoTransfer.Common;
using System.Diagnostics;
using System.Collections;

/// <summary>
/// This class is responsible for running a background thread that periodically checks
/// all of the videos in the system and deletes any videos that have an auto-delete date set,
/// when that date is reached.
/// 
/// There are some caveats with long-lived threads like this in an ASP.NET application. To conserve
/// computing resources, IIS periodically shuts down ASP.NET processes and threads if they are not
/// actively being used, i.e. Internet users are not on the web site. Long-lived threads like this
/// one will be terminated and may not get to run. One way of dealing with this problem is to
/// configure the application pool in IIS for this application not to shutdown worker processes after
/// being idle and not to recycle worker processes. Or, as done here, as soon as the process starts
/// again (and spawns and starts up an AutoDeleter object as seen in Global.asax) the AutoDeleter 
/// will run through the deleteVideos() method before entering its infinite while loop. If the 
/// application is shut down and a user requests a web page, it will start back up and immediately 
/// remove any videos needing auto-delete. The infinite while loop ensures that if the application 
/// is somehow still running when the date changes, it will run at the scheduled time.
/// </summary>
public static class AutoDeleter
{
    private static Thread worker;
    private static bool isRunning;

    /// <summary>
    /// Starts a background thread which will handle any auto-deletions.
    /// </summary>
    public static void Start()
    {
        if (!isRunning)
        {
            worker = new Thread(doWork);
            worker.IsBackground = true;
            isRunning = true;
            worker.Start();
        }
    }

    public static void Stop()
    {
        if (isRunning)
        {
            isRunning = false;
            worker.Abort();
        }
    }

    /// <summary>
    /// This is the method that the worker thread will run.
    /// </summary>
    private static void doWork()
    {
        try
        {

            // Run through all media types immediately after the thread starts, deleting expired items
            deleteExpiredVideos();
            deleteExpiredAudios();
            deleteExpiredWebsites();
            deleteExpiredDocuments();
            deleteExpiredImages();
            
        }
        catch (Exception)
        { }

        // Enter a while loop, deleting expired media every morning.
        while (isRunning)
        {
            try
            {
                // Auto-delete should run around one AM, it's close to the beginning of the day
                // so any items will be removed relative to their auto-delete date,
                // also the server should be under light load at that time.
                if (DateTime.Now.Hour != 1)
                {
                    sleepUntilOne();
                }
                else
                {
                    deleteExpiredVideos();
                    deleteExpiredAudios();
                    deleteExpiredWebsites();
                    deleteExpiredDocuments();
                    deleteExpiredImages();
                    sleepUntilOne();

                }
            }
            catch (ThreadAbortException)
            {
                // Application was shut down.
            }
            catch (Exception)
            {

            }
        }
    }

    private static void sleepUntilOne()
    {
        DateTime now = DateTime.Now;
        
        // Put the thread to sleep until one AM "tomorrow."
        DateTime wakeUpTime = new DateTime(now.Year, now.Month, now.Day, 1, 0, 0).AddDays(1);
        TimeSpan sleepTime = wakeUpTime - now;
        Thread.Sleep(sleepTime);
    }

    //Deletes video files that are past their auto-delete date
    private static void deleteExpiredVideos()
    {
        DBDataContext db = DBDataContext.CreateInstance();
        try
        {
            //------------Commented out by Lawrence Foley on 03/12/2015, flash media server isn't used anymore --------//

            // Don't do anything if the file transfer service on the Flash Media Server isn't running.
            //if (!FileTransfer.IsFileTransferServiceUp())
            //{
            //    return;
            //}



            // Get all items which have an autodelete date specified
            List<Video> vidsToDelete = (from i in db.Videos
                                where (i.AutoDeleteDate.HasValue)
                                select i).ToList();
            
            // Filter out the videos that have gone beyond their autodelete date
            vidsToDelete = (vidsToDelete.AsEnumerable().Where(x => x.AutoDeleteDate <= DateTime.Now).ToList());

            // Loop over the videos that need to be removed
            foreach (var vid in vidsToDelete)
            {
                //------------Commented out by Lawrence Foley on 03/12/2015, flash media server isn't used anymore --------//

                //// Create a FileTransfer object and send a delete request.
                //FileTransfer t = new FileTransfer();
                //t.Connect();
                //FileResponse response = t.SendDeleteRequest(vid.VideoID);

                //// If the Flash Media Server indicates that it was able to delete the video,
                //// remove the video data from the database.
                //if (response.ResponseType == ResponseType.Successful)
                //{
                //    // Delete the video if the flash server successfully deleted it.


                // Remove the video(s) and the thumbnail from the file system
                AppCleanUp.RemoveVideo(vid.VideoID.ToString());
                ApplicationLogger.LogItem(null, "AutoDeleter: Video has expired and has been successfully deleted.", vid.VideoID);     
                //}
            }

            // Remove the video(s) from the database
            db.Videos.DeleteAllOnSubmit(vidsToDelete);
        }
        catch (Exception ex)
        {
            // If the thread is being aborted, re-throw the exception to the caller.
            if (ex.GetType() == typeof(ThreadAbortException))
            {
                throw ex;
            }
        }

        try
        {
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
        }

    }

    //Deletes audio files that are past their auto-delete date
    private static void deleteExpiredAudios()
    {
        try
        {
            // Get all items that have an autodeletedate
            DBDataContext db = DBDataContext.CreateInstance();
            List<Audio> audiosToDelete = (from i in db.Audios
                                          where (i.AutoDeleteDate.HasValue)
                                          select i).ToList();
            
            // Filter items which have passed their autodeletedate
            audiosToDelete = audiosToDelete.AsEnumerable().Where(x => x.AutoDeleteDate <= DateTime.Now).ToList();

            // Remove all the items from the file system
            foreach (var audio in audiosToDelete)
            {
                ApplicationLogger.LogItem(null, "AutoDeleter: Audio has expired and has been successfully deleted.", audio.AudioID);
                AppCleanUp.RemoveAudio(audio.AudioID.ToString());
            }
            db.Audios.DeleteAllOnSubmit(audiosToDelete);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            // If the thread is being aborted, re-throw the exception to the caller.
            if (ex.GetType() == typeof(ThreadAbortException))
            {
                throw ex;
            }
        }
    }

    //Deletes websites that are past their auto-delete date
    private static void deleteExpiredWebsites()
    {
        DBDataContext db = DBDataContext.CreateInstance();
        try
        {
            // Connect to the database and fetch all website files that have an auto-delete date set that
            // falls on or before today.
            List<Websites> websitesToDelete = (from i in db.Websites
                                  where i.AutoDeleteDate.HasValue
                                  select i).ToList();

            websitesToDelete = websitesToDelete.AsEnumerable().Where(x => x.AutoDeleteDate <= DateTime.Now).ToList();


            // Loop over the videos that need to be removed.
            foreach (var website in websitesToDelete)
            {
                // Remove the website from the database
                db.Websites.DeleteOnSubmit(website);
                db.SubmitChanges();

                // Remove the website files and directories from the file system and also delete the virtual directories
                AppCleanUp.RemoveWebsite(website.WebID, website.Title);
                ApplicationLogger.LogItem(null, "AutoDeleter: Website has expired and has been successfully deleted.", website.WebID);
                
            }
            
        }
        catch (Exception ex)
        {
            // If the thread is being aborted, re-throw the exception to the caller.
            if (ex.GetType() == typeof(ThreadAbortException))
            {
                throw ex;
            }
        }
    }

    //Deletes documents that are past their auto-delete date
    private static void deleteExpiredDocuments()
    {
        try
        {
            // Get all items which have an autodeletedate
            DBDataContext db = DBDataContext.CreateInstance();
            List<Files> documentsToDelete = (from i in db.Files
                                    where i.AutoDeleteDate.HasValue
                                    select i).ToList();
            
            // Filter items which have passed their autodeletedate
            documentsToDelete = documentsToDelete.AsEnumerable().Where(x => x.AutoDeleteDate <= DateTime.Now).ToList();

            // Loop over the videos that need to be removed
            foreach (var document in documentsToDelete)
            {
                // Remove the document from the database
                db.Files.DeleteOnSubmit(document);
                db.SubmitChanges();

                // Remove the document from the file system
                AppCleanUp.RemoveDocument(document.FileID);
                ApplicationLogger.LogItem(null, "AutoDeleter: Document has expired and has been successfully deleted.", document.FileID);
                
            }
            
        }
        catch (Exception ex)
        {
            // If the thread is being aborted, re-throw the exception to the caller.
            if (ex.GetType() == typeof(ThreadAbortException))
            {
                throw ex;
            }
        }
    }

    //Deletes images that are past their auto-delete date
    private static void deleteExpiredImages()
    {
        try
        {
            // Connect to the database and fetch all document files that have an auto-delete date set that
            // falls on or before today.
            DBDataContext db = DBDataContext.CreateInstance();
            List<Images> imagesToDelete = (from i in db.Images
                                      where i.AutoDeleteDate.HasValue
                                      select i).ToList();

            imagesToDelete = imagesToDelete.Where(x => x.AutoDeleteDate <= DateTime.Now).ToList();
            // Loop over the videos that need to be removed.
            foreach (var image in imagesToDelete)
            {
                // Remove the image from the database
                db.Images.DeleteOnSubmit(image);
                db.SubmitChanges();

                // Remove the image from the file system
                AppCleanUp.RemoveImage(image.ImageID);
                ApplicationLogger.LogItem(null, "AutoDeleter: Image has expired and has been successfully deleted.", image.ImageID);
            }
        }
        catch (Exception ex)
        {
            // If the thread is being aborted, re-throw the exception to the caller.
            if (ex.GetType() == typeof(ThreadAbortException))
            {
                throw ex;
            }
        }
    }
    // Deletes all items belonging to a group that has expired
    private static void deleteExpiredGroups()
    {
        try
        {

            DBDataContext db = DBDataContext.CreateInstance();
            List<StudentGroup> groups = (from i in db.StudentGroups
                                         //where i.EndDate.HasValue
                                         select i).ToList();
        }
        catch (Exception ex)
        {
        }
    }
    // Send an email to users who have an item that will be delete in a week or in one day
    //private static void notifyItemOwners();
}
