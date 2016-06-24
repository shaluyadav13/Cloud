using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Web.Administration;
using System.DirectoryServices;


/// <summary>
/// Summary description for AppCleanUp
/// </summary>
public static class AppCleanUp
{
    /// <summary>
    ///  Deletes all temp files and folders associated with the video referenced by videoID.
    /// </summary>
    /// <param name="videoID"></param>
    /// <param name="imagesPath"></param>
    public static void RemoveTempFiles(String videoID, String imagesPath)
    {
        RemoveTempVideo(videoID);
        RemoveTempImages(videoID, imagesPath);
    }


    public static void RemoveTempVideo(String videoID)
    {
        ApplicationLogger.LogItem(null, "AppCleanUp: Deleting video temporary files.", videoID);
        // Find the video temp path.
        String appTempPath = ConfigurationManager.AppSettings.Get("VideoTempPath");
        String videoTempPath = appTempPath + videoID;

        // Delete the temp folder.
        if (Directory.Exists(videoTempPath))
            Directory.Delete(videoTempPath, true);
    }

    public static void RemoveTempImages(String videoID, String imagesPath)
    {
        ApplicationLogger.LogItem(null, "AppCleanUp: Deleting video image files.", videoID);
        // Delete any still images associated with the video.
        var images = from i in Directory.GetFiles(imagesPath)
                     where i.Contains(videoID)
                     select i;

        foreach (String s in images)
        {
            File.Delete(s);
        }
    }

    /// <summary>
    ///  Deletes specified video files from the file system along with the thumbnail image
    /// </summary>
    /// <param name="videoID">The ID of the video to be deleted</param>
    /// <param name="removeDatabaseEntry">Remove the video entry from the database also</param>
    public static void RemoveVideo(String videoID)
    {
        ApplicationLogger.LogItem(null, "AppCleanUp: Deleting video files.", videoID);
        String basePath = HttpRuntime.AppDomainAppPath;
        String mp4FilePath = basePath + AppSettings.VideoConvertedFolder + "\\" + videoID + ".mp4";
        String webmFilePath = basePath + AppSettings.VideoConvertedFolder + "\\" + videoID + ".webm";
        String thumbnailPath = basePath + AppSettings.ThumbnailFolder + "\\" + videoID + ".png";

        if (File.Exists(mp4FilePath))
            File.Delete(mp4FilePath);
        if(File.Exists(webmFilePath))
            File.Delete(webmFilePath);
        if (File.Exists(thumbnailPath))
            File.Delete(thumbnailPath);
    }

    /// <summary>
    ///  Deletes the specified audio files from the file system
    /// </summary>
    /// <param name="audioID">The ID of the audio file to be deleted</param>
    public static void RemoveAudio(String audioID)
    {
        try
        {
            ApplicationLogger.LogItem(null, "AppCleanUp: Deleting audio files.", audioID);

            String basePath = HttpRuntime.AppDomainAppPath;
            String mp3FilePath = basePath + AppSettings.AudioConvertedFolder + "\\" + audioID + ".mp3";
            String oggFilePath = basePath + AppSettings.AudioConvertedFolder + "\\" + audioID + ".ogg";
            if (File.Exists(mp3FilePath))
                File.Delete(mp3FilePath);
            if (File.Exists(oggFilePath))
                File.Delete(oggFilePath);
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    ///  Deletes the specified website from the file system
    /// </summary>
    /// <param name="websiteID">The ID of the website to be deleted</param>
    /// /// <param name="websiteTitle">The title, or name, of the website to be deleted</param>
    public static void RemoveWebsite(String websiteID, String websiteTitle)
    {
        ApplicationLogger.LogItem(null, "AppCleanUp: Deleting website files.", websiteID);
        String basePath = HttpRuntime.AppDomainAppPath;
        String webPath = basePath + AppSettings.WebConvertedFolder + "\\" + websiteID + "\\";
        if (Directory.Exists(webPath))
            Directory.Delete(webPath, true);

        //This method is to remove virtual directory from IIS while deleting website
        // Note - this will NOT work when running in Visual Studio because it will be using IISExpress instead of IIS
        try
        {
            ServerManager serverManager = new ServerManager();

            // "nwcloudweb" is the name of the site where the websites' virtual directories are made 
            Application ap = serverManager.Sites["nwcloudweb"].Applications[0];

            //string appPath = @"/" + websiteTitle + "_" + websiteID;
            string appPath = @"/" + websiteID;
            VirtualDirectory v = ap.VirtualDirectories[appPath];

            ap.VirtualDirectories.Remove(v);
            serverManager.CommitChanges();
        }
        catch (Exception ex)
        {
        }
    }


    /// <summary>
    ///  Deletes the specified document file from the file system
    /// </summary>
    /// <param name="documentID">The ID of the document file to be deleted</param>
    public static void RemoveDocument(String documentID)
    {
        String basePath = HttpRuntime.AppDomainAppPath;

        ApplicationLogger.LogItem(null, "AppCleanUp: Deleting document file.", documentID);
        String documentFilePath = basePath + AppSettings.DocumentSavedFolder + "\\" + documentID;
        if (Directory.Exists(documentFilePath))
            Directory.Delete(documentFilePath, true);
    }

    /// <summary>
    ///  Deletes the specified image from the file system
    /// </summary>
    /// <param name="imageID">The ID of the image file to be deleted</param>
    public static void RemoveImage(String imageID)
    {
        String basePath = HttpRuntime.AppDomainAppPath;

        ApplicationLogger.LogItem(null, "AppCleanUp: Deleting image file.", imageID);
        String imageFilePath = basePath + AppSettings.ImagesSavedFolder  + "\\" + imageID;
        if (Directory.Exists(imageFilePath))
            Directory.Delete(imageFilePath, true);
    }
}

