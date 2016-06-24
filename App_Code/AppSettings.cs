using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// This class maps to application settings in web.config and stores miscellaneous settings.
/// </summary>
public static class AppSettings
{
    // Set this value to true when deploying to the live server. Set to false if deploying
    // to the development server. This value is used to change references to application settings.
    // This setting will affect HTTPS secure settings, database connection strings, and web service paths.
    private static bool isProductionServer = true;

    /// <summary>
    /// Gets a value indicating whether this build is for the production server or for the development server.
    /// </summary>
    public static bool IsProductionServer
    {
        get { return isProductionServer; }
    }

    //----------Commented out by Lawrence Foley on 03/13/2015, flash media server is no longer used-------//

    /// <summary>
    /// Gets the fully-qualified web-relative path to the Flash Media Server.
    /// </summary>
    //public static String FlashMediaServerPath
    //{
    //    get 
    //    {
    //        if (isProductionServer)
    //            return getAppValue("flashMediaServer");
    //        else
    //            return getAppValue("flashMediaServerTest");
    //    }

    //}

    //----------Commented out by Lawrence Foley on 03/13/2015, flash media server is no longer used-------//

    /// <summary>
    /// Gets the TCP port number used to contact the file transfer service on the Flash
    /// Media Server.
    /// </summary>
    //public static int FileTransferPort
    //{
    //    get 
    //    {
    //        if (isProductionServer)
    //            return int.Parse(getAppValue("flashMediaPort"));
    //        else
    //            return int.Parse(getAppValue("flashMediaPortTest"));
    //    }
    //}

    /// <summary>
    /// Gets the bytes of free space threshold
    /// </summary>
    public static long getFreeDiskSpaceThreshold
    {
        get { return long.Parse(getAppValue("freeDiskSpaceThreshold")); }
    }
    /// <summary>
    /// Gets the SMTP email server to use when sending emails.
    /// </summary>
    public static String SMTPServerPath
    {
        get { return getAppValue("smtpServer"); }
    }

    /// <summary>
    /// Gets the email address the application should send emails from.
    /// </summary>
    public static String AppEmailAddress
    {
        get { return getAppValue("appEmailAddress"); }
    }
    public static String AppEmailPassword
    {
        get { return getAppValue("appEmailPassword"); }
    }
    public static int AppEmailPort
    {
        get { return int.Parse(getAppValue("appEmailPort"));  }
    }

    /// <summary>
    /// Gets the location the application should use as a temporary folder for
    /// storing video files during the conversion to .mp4 and .webm.
    /// </summary>
    public static String VideoConversionFolder
    {
        get { return getAppValue("videoTempPath"); }
    }

    public static String AudioConversionFolder
    {
        get { return getAppValue("audioTempPath"); }
    }
    public static String WebConversionFolder
    {
        get { return getAppValue("webTempPath"); }
    }
    public static String DocumentSavedFolder
    {
        get { return getAppValue("filePath"); }
    }
    public static String ImagesSavedFolder
    {
        get { return getAppValue("imagePath"); }
    }

    /// <summary>
    /// Gets the location the application should use for
    /// storing converted video files.
    /// </summary>
    public static String VideoConvertedFolder
    {
        get { return getAppValue("videoConvertedPath"); }
    }

    public static String AudioConvertedFolder
    {
        get { return getAppValue("audioConvertedPath"); }
    }
    public static String WebConvertedFolder
    {
        get { return getAppValue("webConvertedPath"); }
    }

    /// <summary>
    /// Gets the location for storeing thumbnails
    /// </summary>
    public static String ThumbnailFolder
    {
        get { return getAppValue("thumbnailPath"); }
    }


    /// <summary>
    /// Gets the fully-qualified path to this web application, minus the protocol (http or https).
    /// i.e. "cite.nwmissouri.edu/NWVideo"
    /// </summary>
    public static String AppWebPath
    {
        get
        {
            if (isProductionServer)
                return getAppValue("webPath");
            else
                return getAppValue("webPathTest");
        }
    }

    /// <summary>
    /// Gets the first part of the url to use for websites that are uploaded.
    /// </summary>
    public static String websitesBaseURL
    {
        get
        {
            if(IsProductionServer)
            {
                return getAppValue("websitesBaseURL");
            }
            else
            {
                return "http://localhost/";
            }
        }
    }

    public static String AdministratorEmailAddress
    {
        get
        {
            return getAppValue("administratorEmailAddress");
        }
    }
    public static String DatabaseConnectionString
    {
        get
        {
            if (isProductionServer)
                return getAppValue("ProductionConnectionString");
            else
                return getAppValue("TestConnectionString");
        }
    }

    //---------- Commented out on 4/9/2015 as these settings are not even used ---------//

    ///// <summary>
    ///// Gets the resolution FFMPEG should use when converting videos.
    ///// </summary>
    //public static String UploadResolution
    //{
    //    get { return getAppValue("uploadResolution"); }
    //}

    ///// <summary>
    ///// Gets the video bitrate FFMPEG should use when converting videos.
    ///// </summary>
    //public static String UploadBitRate
    //{
    //    get { return getAppValue("uploadBitRate"); }
    //}

    ///// <summary>
    ///// Gets the audio sampling rate FFMPEG should use when converting videos.
    ///// </summary>
    //public static String UploadAudioSamplingRate
    //{
    //    get { return getAppValue("uploadAudioRate"); }
    //}

    ///// <summary>
    ///// Gets the audio bit rate FFMPEG should use when converting videos.
    ///// </summary>
    //public static String UploadAudioBitRate
    //{
    //    get { return getAppValue("uploadAudioBitRate"); }
    //}

    /// <summary>
    /// Gets the resolution FFMPEG should use when taking still images out of a video.
    /// </summary>
    public static String StillImageResolution
    {
        get { return getAppValue("imageResolution"); }
    }

    /// <summary>
    /// Gets the number of still images FFMPEG should capture from a video.
    /// </summary>
    public static int StillImageCaptureCount
    {
        get { return int.Parse(getAppValue("imagesToCapture")); }
    }

    /// <summary>
    /// Gets the number of still images per second FFMPEG should capture from a video.
    /// </summary>
    public static double StillImageCaptureRate
    {
        get { return double.Parse(getAppValue("imageRatePerSecond")); }
    }

    /// <summary>
    /// Indicates whether the login page requires SSL encryption over the HTTPS protocol.
    /// </summary>
    public static bool Require_SSL
    {
        get
        {
            return false;
        }
    }

    // Gets a String array containing all of the acceptable file extensions for videos being uploaded
    // to Northwest Video.
    public static String[] AcceptableVideoFileFormats
    {
        get
        {
            return new String[] { ".avi", ".wmv", ".mp4", ".mpg", ".mpeg", ".asf", ".rm", ".flv", ".rv", ".mov", ".m4v" };
        }
    }
    public static String[] AcceptableAudioFileFormats
    {
        get
        {
            return new String[] { ".mp3", ".wav", ".ogg", ".wma"};
        }
    }
    public static String[] AcceptableWebFileFormats
    {
        get
        {
            return new String[] { ".zip"};
        }
    }
    public static String[] AcceptableFileFormats
    {
        get
        {
            // Added .back .rvc .kmz .kml
            return new String[] { ".pdf",".docx",".doc",".txt",".xls",".xlsx", ".ppt", ".pptx", ".rtf", ".back", ".rvc", ".kmz", ".musx", ".kml"};
        }
    }
    public static String[] AcceptableTranscriptFileFormats
    {
        get
        {
            return new String[] { ".srt", ".txt" };
        }
    }
    public static String[] AcceptableImageFormats
    {
        get
        {
            return new String[] { ".jpg", ".png",".jpeg",".gif",".tif",".tiff",".jpe",".jfif",".bmp",".dib"};
        }
    }

    private static String getAppValue(String name)
    {
        return ConfigurationManager.AppSettings.Get(name);
    }
}
