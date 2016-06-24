//-----------------Commented out by Lawrence Foley on 03/13/2015, this converted is not used; it has been replaced by MMRQueue------------//


//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Net.Mail;
//using System.Threading;
//using VideoTransfer.Common;

///// <summary>
///// Summary description for VideoConverter
///// </summary>
//public class VideoConverter
//{
//    private String videoPath;
//    private String thumbnailPath;
//    private String ffmpegPath;
//    private Video video;

//    private UserAccount user;
//    private bool email;

//    private Thread worker;

//    private int retries;
//    private const int MAX_RETRIES = 2;

//    /// <summary>
//    /// Creates a new VideoConverter but does not start the conversion process.
//    /// </summary>
//    /// <param name="videoPath">File path to the video to be converted.</param>
//    /// <param name="thumbnailPath">File path to the thumbnail image for the video.</param>
//    /// <param name="ffmpegPath">File path to FFMPEG.EXE</param>
//    /// <param name="video">A Video object for this video.</param>
//    public VideoConverter(String videoPath, String thumbnailPath, String ffmpegPath, Video video, UserAccount user, bool email)
//    {
//        retries = 0;
//        this.videoPath = videoPath;
//        this.thumbnailPath = thumbnailPath;
//        this.ffmpegPath = ffmpegPath;
//        this.video = video;
//        this.user = user;
//        this.email = email;
//        worker = new Thread(convertVideo);
//    }

//    /// <summary>
//    /// Begins asynchronously converting the video.
//    /// </summary>
//    public void Start()
//    {
//        worker.Start();
//    }

//    private void convertVideo()
//    {
//        bool retry = true;

//        while (retry)
//        {
//            retry = false;
//            try
//            {
//                ApplicationLogger.LogItem(user, "VideoConverter: Beginning video conversion on worker thread.", video.VideoID);

//                // Run ffmpeg over the video and convert it to a Flash video file.
//                ProcessStartInfo info = new ProcessStartInfo();
//                info.FileName = ffmpegPath;
//                info.UseShellExecute = false;
//                info.CreateNoWindow = true;

//                // Create the new file path.
//                String newFileName = Path.ChangeExtension(videoPath, ".flv");

//                // Change the original file's name if it was also a flash video.
//                // When the conversion is completed, the name is the same and the only difference is the
//                // extension, so if the original file is a flash video there will be a naming collision.
//                if (newFileName == videoPath)
//                {
//                    String original = videoPath.Substring(0, videoPath.LastIndexOf(".")) + "_original.flv";
//                    File.Move(videoPath, original);
//                    videoPath = original;
//                }
//                ApplicationLogger.LogItem(user, "VideoConverter: The new file will be saved at '" + newFileName + "'.", video.VideoID);

//                // Quality settings are taken from web.config.
//                String resolution = AppSettings.UploadResolution;
//                String videoBitRate = AppSettings.UploadBitRate;
//                String audioRate = AppSettings.UploadAudioSamplingRate;
//                String audioBitRate = AppSettings.UploadAudioBitRate;

//                info.Arguments = String.Format("-i \"{0}\" -s {1} -b {2} -ar {3} -ab {4} \"{5}\"",
//                    videoPath,
//                    resolution,
//                    videoBitRate,
//                    audioRate,
//                    audioBitRate,
//                    newFileName);

//                ApplicationLogger.LogItem(user, "VideoConverter: Launching ffmpeg.", video.VideoID);

//                // Launch ffmpeg and wait for it to finish.
//                Process p = Process.Start(info);
//                p.WaitForExit();
//                p.Close();
//                ApplicationLogger.LogItem(user, "VideoConverter: ffmpeg conversion completed, checking status.", video.VideoID);

//                // Check the output file's size. If it is 0 bytes then the conversion failed and we should not
//                // send the video to the FMS.
//                long vidSize = (new FileInfo(newFileName)).Length;

//                if (vidSize == 0)
//                {
//                    ApplicationLogger.LogItem(user, "VideoConverter: Video conversion failed on attempt # " + (retries + 1) + ".", video.VideoID);
//                    File.Delete(newFileName);
//                    throw new VideoConversionException("Video conversion failed.");
//                }

//                // The file is converted, so try to send it to the flash media server.
//                ApplicationLogger.LogItem(user, "VideoConverter: ffmpeg conversion completed successfully, connecting to file transfer service.", video.VideoID);
//                FileTransfer f = new FileTransfer();
//                f.Connect();
//                f.VideoID = video.VideoID;

//                // Send an add request.
//                f.User = user;
//                ResponseType response = f.SendAddRequest(newFileName);

//                if (response == ResponseType.Successful)
//                {
//                    ApplicationLogger.LogItem(user, "VideoConverter: 'Successful' response received from file transfer service.", video.VideoID);

//                    // Now finally update the database.
//                    DBDataContext db = DBDataContext.CreateInstance();
//                    video.DatePosted = DateTime.Now;

//                    // Read in the thumbnail image and post it within the database record.
//                    byte[] thumbnail = File.ReadAllBytes(thumbnailPath);
//                    video.ThumbnailBytes = thumbnail;

//                    // Set the video's posted date and size properties.
//                    video.DatePosted = DateTime.Now;
//                    video.Size = (new FileInfo(newFileName)).Length;

//                    ApplicationLogger.LogItem(user, "VideoConverter: Inserting Video object into database.", video.VideoID);
//                    db.Videos.InsertOnSubmit(video);
//                    db.SubmitChanges();
//                    ApplicationLogger.LogItem(user, "VideoConverter: Video object inserted, database saved.", video.VideoID);
//                }
//                else
//                {
//                    // Eventually do something interesting if the transfer fails.
//                    ApplicationLogger.LogItem(user, "VideoConverter: Unable to transfer using file transfer service, aborting.", video.VideoID);
//                    AppCleanUp.RemoveTempImages(video.VideoID, Path.GetDirectoryName(thumbnailPath));
//                    throw new FileTransferException("File transfer service closed the connection unexpectedly.");
//                }

//                // Now do cleanup.
//                ApplicationLogger.LogItem(user, "VideoConverter: Cleaning up temporary files.", video.VideoID);
//                AppCleanUp.RemoveTempFiles(video.VideoID, Path.GetDirectoryName(thumbnailPath));

//                // Send an email to the user who uploaded, if necessary.
//                if (email)
//                {
//                    ApplicationLogger.LogItem(user, "VideoConverter: Emailing the user.", video.VideoID);
//                    String fromAddress = AppSettings.AppEmailAddress;
//                    String smtpServer = AppSettings.SMTPServerPath;

//                    String webPath = AppSettings.AppWebPath;

//                    String protocol;
//                    if (AppSettings.Require_SSL)
//                        protocol = "https://";
//                    else
//                        protocol = "http://";

//                    String message =
//                        String.Format("Your recently uploaded video, \"{0}\", has been converted and is now available.\n\n",
//                                      video.Title);

//                    message += String.Format("You can login to Northwest Video at {0}{1}login.aspx or watch the video at http://{1}playvideo.aspx?vid={2}.",
//                                             protocol,
//                                             webPath,
//                                             video.VideoID);

//                    SmtpClient smtp = new SmtpClient(smtpServer);

//                    smtp.Send(fromAddress, user.Email, "Northwest Video: Conversion completed", message);

//                }


//                // Done!
//            }
//            catch (FileTransferException)
//            {
//                ApplicationLogger.LogItem(user, "VideoConverter: The file transfer operation experienced an error.", video.VideoID);
//            }
//            catch (VideoConversionException)
//            {
//                retries++;
//                if (retries <= MAX_RETRIES)
//                {
//                    ApplicationLogger.LogItem(user, "VideoConverter: Conversion failed, retrying.", video.VideoID);
//                    retry = true;
//                }
//                else
//                {
//                    retry = false;

//                    // We've failed several times.

//                    // Delete the still images for this video.
//                    AppCleanUp.RemoveTempImages(video.VideoID, Path.GetDirectoryName(thumbnailPath));

//                    // Send an email informing the user and the CITE office.
//                    ApplicationLogger.LogItem(user, "VideoConverter: Conversion failed, max retry counter hit.", video.VideoID);
//                    String fromAddress = AppSettings.AppEmailAddress;
//                    String smtpServer = AppSettings.SMTPServerPath;

//                    // Send to the user.
//                    {
//                        ApplicationLogger.LogItem(user, "VideoConverter: Emailing the user about the error.", video.VideoID);
//                        String message =
//                            String.Format("Your recently uploaded video, \"{0}\", has encountered a problem and has not been made available on Northwest Video. The CITE Office is aware of the problem and will examine the issue.",
//                                          video.Title);
//                        SmtpClient smtp = new SmtpClient(smtpServer);
//                        smtp.Send(fromAddress, user.Email, "Northwest Video: Conversion failed", message);
//                    }

//                    // Send to CITE.
//                    {
//                        ApplicationLogger.LogItem(user, "VideoConverter: Emailing CITE about the error.", video.VideoID);
//                        String message =
//                            String.Format("A video conversion has failed.\n\nDetails:\nUser: {0}, {1} ({2})\nVideo Title: {3}\nDate: {4}\nVideo ID: {5}",
//                                          user.LastName,
//                                          user.FirstName,
//                                          user.Username,
//                                          video.Title,
//                                          DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt"),
//                                          video.VideoID);

//                        SmtpClient smtp = new SmtpClient(smtpServer);
//                        smtp.Send(fromAddress, AppSettings.AdministratorEmailAddress, "Northwest Video: Conversion Failure", message);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                ApplicationLogger.LogItem(user, "VideoConverter: An exception has occurred.\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message, video.VideoID);
//            }
//        } // End of retry while loop
//    } // End of convertVideo()
//} // End of class VideoConverter

//public class VideoConversionException : ApplicationException
//{
//    public VideoConversionException(String message)
//        : base(message)
//    { }
//}

//public class FileTransferException : ApplicationException
//{
//    public FileTransferException(String message)
//        : base(message)
//    {

//    }
//}