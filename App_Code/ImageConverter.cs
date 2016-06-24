
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

/// <summary>
/// Summary description for ImageConverter
/// </summary>
public static class ImageConverter
{
    public struct ConvertImagesResult
    {
        public bool Successful;
        public String[] Images;
    }
    public static ConvertImagesResult ConvertImages(String videoID, String fileName, String imagePath, String ffmpegPath)
    {
        // Now run ffmpeg over the input video and have it pull several stills out
        // for use as a thumbnail.
        ProcessStartInfo info = new ProcessStartInfo();
        info.CreateNoWindow = true;
        info.UseShellExecute = false;
        info.FileName = ffmpegPath + "/ffmpeg.exe";
        String captureFileName = imagePath + "/" + videoID + "-image-%02d.png";
        String resolution = AppSettings.StillImageResolution;
        int count = AppSettings.StillImageCaptureCount;
        double rate = AppSettings.StillImageCaptureRate;
        info.Arguments = String.Format(" -i \"{0}\" -r {1} -s {2} -vframes {3} -f image2 \"{4}\"",
                                        fileName,
                                        rate,
                                        resolution,
                                        count,
                                        captureFileName);

        ApplicationLogger.LogItem(null, info.UserName);

        // ffmpeg runs as a separate process.
        Process p = Process.Start(info);

        // Wait for the process to terminate before continuing. This should only take a moment since we're
        // only grabbing a few still images.
        p.WaitForExit();
        p.Close();

        // Several still images for the video should now exist. If they don't, then ffmpeg likely
        // could not understand the video, meaning the video is likely corrupt or an odd format.
        // If this is the case, perform cleanup and inform the user that the file couldn't be understood.
        var images = from i in Directory.GetFiles(imagePath)
                     where i.Contains(videoID)
                     select i;

        if (images.Count() == 0)
            return new ConvertImagesResult() { Successful = false };
        else
            return new ConvertImagesResult() { Successful = true, Images = images.ToArray() };

    }
}
