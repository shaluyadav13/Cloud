//-----------------Commented out by Lawrence Foley on 03/13/2015, this isn't used anymore. The files are uploaded in "UploadMedia.aspx.cs"------------//

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Timers;
//using System.Web.Services;
//using Cite.DomainAuthentication;

///// <summary>
///// This code page contains all of the web services the Silverlight Uploader needs in order to function.
///// </summary>
//[WebService(Namespace = "http://tempuri.org/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//// [System.Web.Script.Services.ScriptService]
//public class UploadServices : System.Web.Services.WebService
//{
//    // How many bytes does the web service expect per chunk?
//    private const long BYTES_PER_CHUNK = 1024 * 25; // 25 kilobytes per chunk.

//    private static List<SilverlightUploadSession> _silverlightSessions;
//    private static List<SilverlightUploadSession> SilverlightSessions
//    {
//        get
//        {
//            if (_silverlightSessions == null)
//                _silverlightSessions = new List<SilverlightUploadSession>();
//            return _silverlightSessions;
//        }
//    }

//    private static Timer idleOutTimer;
    
//    public UploadServices()
//    {
//        //Uncomment the following line if using designed components 
//        //InitializeComponent(); 
//        if (idleOutTimer == null)
//        {
//            idleOutTimer = new Timer(30000); // Thirty second interval.
//            idleOutTimer.Elapsed += new ElapsedEventHandler(idleOutTimer_Elapsed);
//            idleOutTimer.AutoReset = true;
//            idleOutTimer.Start();
//        }
//    }

//    private void idleOutTimer_Elapsed(object sender, ElapsedEventArgs e)
//    {
//        // Get any SilverlightUploadSessions where the keepalive has not been received for 150 seconds
//        // (ten times the keepalive interval). Delete the sessions, perform cleanup.
//        var expiredSessions = (from i in SilverlightSessions
//                               where i.LastKeepAlive < DateTime.Now - TimeSpan.FromSeconds(150)
//                               select i).ToList();


//        foreach (var s in expiredSessions)
//        {
//            ApplicationLogger.LogItem(null, "Deleting expired Silverlight session.", s.Upload.VideoID);
//            s.Close();
//            s.Delete();
//            SilverlightSessions.Remove(s);
//        }
//    }

//    public enum LoginResult { Successful, InvalidCredentials, UploadUnavailable, UnknownError };
//    /// <summary>
//    /// Verifies that the user's credentials are valid and that the user is allowed to log in.
//    /// </summary>
//    /// <param name="username"></param>
//    /// <param name="password"></param>
//    /// <returns></returns>
//    [WebMethod]
//    public LoginResult Login(String username, String password)
//    {
//        try
//        {
//            UserAccount account = new UserAccount(username, password, 3000);

//            if (account.IsAuthenticated)
//            {
//                // Account is authenticated, now check for login permission.
//                if (!account.CanLogin)
//                {
//                    ApplicationLogger.LogItem(null, "Silverlight Uploader: Invalid credentials, attempted login as " + username + ". Someone may be calling this web service outside of the Silverlight Uploader.");
//                    return LoginResult.InvalidCredentials;
//                }
//                else
//                {
//                    if (FileTransfer.IsFileTransferServiceUp())
//                    {
//                        ApplicationLogger.LogItem(account, "Silverlight Uploader: Successful login.");
//                        return LoginResult.Successful;
//                    }
//                    else
//                    {
//                        ApplicationLogger.LogItem(account, "Silverlight Uploader: Successful login, however the file transfer service on the Flash Server is down.");
//                        return LoginResult.UploadUnavailable;
//                    }
//                }
//            }
//            else
//            {
//                ApplicationLogger.LogItem(null, "Silverlight Uploader: Invalid credentials, attempted login as " + username + ". Someone may be calling this web service outside of the Silverlight Uploader.");
//                return LoginResult.InvalidCredentials;
//            }
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'Login' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message);
//            return LoginResult.UnknownError;
//        }
//    }

//    public struct Group
//    {
//        public int GroupID;
//        public String Name;
//    }
//    /// <summary>
//    /// Gets all of the groups the user belongs to, including their own profile (not technically a group)
//    /// </summary>
//    /// <param name="username"></param>
//    /// <param name="password"></param>
//    /// <returns></returns>
//    [WebMethod]
//    public Group[] GetStudentGroups(String username, String password)
//    {
//        UserAccount account = new UserAccount(username, password, 2000);
//        if (!account.CanLogin || !account.Exists)
//            return null;

//        try
//        {
//            List<Group> groups = new List<Group>();

//            DBDataContext db = DBDataContext.CreateInstance();
//            var stuGroups = from i in db.StudentGroups
//                            where (from k in i.AuthorizedStudents
//                                   select k.Username).Contains(username)
//                            && i.EndDate >= DateTime.Now.Date
//                            orderby i.GroupName
//                            select i;
//            foreach (var g in stuGroups)
//            {
//                groups.Add(new Group() { GroupID = g.GroupID, Name = g.GroupName });
//            }
//            if (account.Admin
//                || account.OU == Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers
//                || account.OU == Cite.DomainAuthentication.OrganizationalUnit.StaffUsers)
//            {
//                groups.Add(new Group() { GroupID = 0, Name = "Post to my own profile." });
//            }
//            ApplicationLogger.LogItem(account, "Silverlight Uploader: Looking up student groups. User belongs to " + groups.Count + " groups.");

//            return groups.ToArray();

//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'GetStudentGroups' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message);
//            return null;
//        }
//    }

//    /// <summary>
//    /// Gets a collection of all valid file extensions that Northwest Video accepts.
//    /// </summary>
//    /// <returns></returns>
//    [WebMethod]
//    public String[] GetAcceptableFileExtensions()
//    {
//        return AppSettings.AcceptableVideoFileFormats;
//    }

//    public struct BeginUploadResult
//    {
//        public BeginUploadStatusCode Status;
//        public String VideoID;
//        public long BytesPerChunk;
//    }
//    public enum BeginUploadStatusCode { InvalidCredentials, InvalidParameter, FileTransferServiceUnavailable, UnknownError, Okay };
//    /// <summary>
//    /// Begins an upload. On the server side, this creates a SilverlightUpload session, hashed file names, etc.
//    /// </summary>
//    /// <param name="username"></param>
//    /// <param name="password"></param>
//    /// <param name="fileName"></param>
//    /// <param name="groupID"></param>
//    /// <param name="title"></param>
//    /// <param name="description"></param>
//    /// <param name="author"></param>
//    /// <param name="autoDeleteDate"></param>
//    /// <param name="fileSize"></param>
//    /// <returns></returns>
//    [WebMethod]
//    public BeginUploadResult BeginUpload(String username, String password, String fileName, int groupID, String title, String description, String author, DateTime autoDeleteDate, long fileSize)
//    {
//        try
//        {
//            fileName = fileName.ToLower();

//            UserAccount account = new UserAccount(username, password, 2000);
//            if (!account.CanLogin || !account.Exists)
//                return new BeginUploadResult() { Status = BeginUploadStatusCode.InvalidCredentials, VideoID = "" };

//            ApplicationLogger.LogItem(account, "Silverlight Uploader: User is requesting to begin an upload.");

//            if (!FileTransfer.IsFileTransferServiceUp())
//            {
//                ApplicationLogger.LogItem(account, "Silverlight Uploader: File Transfer Service on the Flash Server is down, aborting.");
//                return new BeginUploadResult() { Status = BeginUploadStatusCode.FileTransferServiceUnavailable, VideoID = "" };
//            }

//            DBDataContext db = DBDataContext.CreateInstance();
//            if (groupID > 0)
//            {
//                if (db.AuthorizedStudents.Where(i => i.GroupID == groupID && i.Username.ToLower() == username.ToLower()).Count() != 1)
//                    return new BeginUploadResult() {Status = BeginUploadStatusCode.InvalidParameter, VideoID = "" };
//            }
//            else if (groupID == 0 && !account.Admin && account.OU != OrganizationalUnit.FacultyUsers && account.OU != OrganizationalUnit.StaffUsers)
//            {
//                return new BeginUploadResult() {Status = BeginUploadStatusCode.InvalidCredentials, VideoID = "" };
//            }

//            if (!AppSettings.AcceptableVideoFileFormats.Contains(Path.GetExtension(fileName)))
//            {
//                ApplicationLogger.LogItem(account, "Silverlight Uploader: User submitted a video with an invalid file extension, aborting.");
//                return new BeginUploadResult()
//                {
//                    Status = BeginUploadStatusCode.InvalidParameter,
//                    VideoID = ""
//                };
//            }

//            ApplicationLogger.LogItem(account, "Silverlight Uploader: Creating Upload object.");
//            Upload upload = new Upload();
//            upload.Title = title;
//            upload.Description = description;
//            upload.Size = fileSize;
//            if (author != "")
//                upload.Author = author;
//            if (autoDeleteDate != null && autoDeleteDate > DateTime.Today)
//            {
//                upload.AutoDeleteDate = autoDeleteDate;
//            }
//            if (groupID > 0)
//                upload.GroupID = groupID;
//            upload.DateUploaded = DateTime.Now;

//            // Create a temporary directory to store the file. Hash the filename
//            // to a hex string and use that as the directory name.
//            String newVideoID = fileName.ComputeMD5Hash();

//            // Check the application settings to see where we are supposed to store temp data.
//            String vidTempDir = AppSettings.VideoConversionFolder;

//            ApplicationLogger.LogItem(account, "Silverlight Uploader: Using " + newVideoID + " as video ID. Checking for availability.");

//            // Check to see if this video ID already exists.
//            // Check the database for existing videos, also search the application's temp directory
//            // in case a video that is still being processed is using this video ID.
//            bool NameExists = (from vid in db.Videos
//                               where vid.VideoID == newVideoID
//                               select vid).Count() != 0
//                               || (from u in db.Uploads
//                                   where u.VideoID == newVideoID
//                                   select u).Count() != 0
//                               || Directory.Exists(vidTempDir + newVideoID)
//                               || SilverlightSessions.Where(i => i.Upload.VideoID == newVideoID).Count() > 0;


//            int loops = 0;
//            Random rand = new Random();
//            while (NameExists)
//            {
//                // If the name already exists, add a random number to the hash
//                // and hash it again so that it's different.
//                newVideoID = (newVideoID + rand.Next().ToString()).ComputeMD5Hash();

//                ApplicationLogger.LogItem(account, "Silverlight Uploader: Video ID taken, trying " + newVideoID + ".");

//                // Check to see if the new video ID is in use.
//                NameExists = (from vid in db.Videos
//                              where vid.VideoID == newVideoID
//                              select vid).Count() != 0
//                              || (from u in db.Uploads
//                                  where u.VideoID == newVideoID
//                                  select u).Count() != 0
//                              || Directory.Exists(vidTempDir + newVideoID)
//                              || SilverlightSessions.Where(i => i.Upload.VideoID == newVideoID).Count() > 0;

//                loops++;
//                if (loops > 200) // If this loops more than 200 times somehow, just give up.
//                {
//                    ApplicationLogger.LogItem(account, "Silverlight Uploader: A strange error has occurred. The system was unable to select an unused Video ID.");
//                    return new BeginUploadResult() { Status = BeginUploadStatusCode.UnknownError, VideoID = "" };
//                }
//            }

//            String tempDirName = vidTempDir + newVideoID;
//            upload.RawFileName = tempDirName + "/" + newVideoID + Path.GetExtension(fileName);
//            upload.VideoID = newVideoID;
//            upload.Username = username;

//            ApplicationLogger.LogItem(account, "Silverlight Uploader: Saving file to '" + upload.RawFileName + "'.", upload.VideoID);

//            // Now create the temp directory.
//            Directory.CreateDirectory(tempDirName);

//            // Create a SilverlightSession for the user.
//            ApplicationLogger.LogItem(account, "Silverlight Uploader: Creating a SilverlightUploadSession for this upload.", upload.VideoID);
//            SilverlightUploadSession sus = new SilverlightUploadSession(username, password, tempDirName + "/" + newVideoID + Path.GetExtension(fileName));
//            sus.Upload = upload;
//            SilverlightSessions.Add(sus);

//            return new BeginUploadResult() { Status = BeginUploadStatusCode.Okay, VideoID = upload.VideoID, BytesPerChunk = BYTES_PER_CHUNK };
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'BeginUpload' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message);
//            return new BeginUploadResult() {Status = BeginUploadStatusCode.UnknownError, VideoID = "" };
//        }
//    }

//    public enum SendFileChunkResult { InvalidParameter, UnexpectedFileSize, VideoFormatError, UnknownError, Okay, Finished };
//    /// <summary>
//    /// This method allows the client to send a chunk of the input file to the server.
//    /// </summary>
//    /// <param name="username"></param>
//    /// <param name="password"></param>
//    /// <param name="videoID"></param>
//    /// <param name="chunk"></param>
//    /// <returns></returns>
//    [WebMethod]
//    public SendFileChunkResult SendFileChunk(String username, String password, String videoID, byte[] chunk)
//    {
//        try
//        {
//            SilverlightUploadSession sus = SilverlightSessions.Where(i => i.Username == username
//                                                                     && i.Password == password
//                                                                     && i.Upload.VideoID == videoID).Single();
//            if (sus.CurrentSize + chunk.Length > sus.Upload.Size)
//            {
//                ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Total file size exceeds earlier reported file size. Cleaning up and aborting.", sus.Upload.VideoID);
//                sus.Close();
//                sus.Delete();
//                SilverlightSessions.Remove(sus);
//                return SendFileChunkResult.UnexpectedFileSize;
//            }
//            else
//            {
//                sus.WriteChunk(chunk);
//                sus.LastKeepAlive = DateTime.Now;

//                if (sus.CurrentSize == sus.Upload.Size)
//                {
//                    ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Final chunk received, using ffmpeg to create still images.", sus.Upload.VideoID);
//                    sus.Close();

//                    var convertResult = ImageConverter.ConvertImages(videoID, sus.FileName, Server.MapPath("Images"), Server.MapPath("ffmpeg"));

//                    if (!convertResult.Successful)
//                    {
//                        ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: ffmpeg failed to create the images, cleaning up and aborting.", sus.Upload.VideoID);
//                        // ffmpeg failed.
//                        AppCleanUp.RemoveTempFiles(sus.Upload.VideoID, Server.MapPath("Images"));
//                        sus.Delete();
//                        SilverlightSessions.Remove(sus);
//                        return SendFileChunkResult.VideoFormatError;
//                    }
//                    ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Images created successfully.", sus.Upload.VideoID);
//                    var images = convertResult.Images;

//                    List<String> tempImages;

//                    // The Silverlight Uploader's image controls seem to require an absolute URI.
//                    // If this is the production server, use https for the Image URIs since Silverlight will be
//                    // running on a secure page. The user will otherwise get error messages about mixed secure 
//                    // and unsecure content.
//                    if (AppSettings.Require_SSL)
//                    {
//                        tempImages = images.Select(i => "https://" + AppSettings.AppWebPath + "/Images/" + Path.GetFileName(i)).ToList();
//                        tempImages.Insert(0, "https://" + AppSettings.AppWebPath + "/Images/N.png");
//                    }
//                    else
//                    {
//                        tempImages = images.Select(i => "http://" + AppSettings.AppWebPath + "/Images/" + Path.GetFileName(i)).ToList();
//                        tempImages.Insert(0, "http://" + AppSettings.AppWebPath + "/Images/N.png");
//                    }

//                    /* Used for debugging on a local system.
//                    var tempImages = images.Select(i => "http://localhost:1213/nwvideo/Images/" + Path.GetFileName(i)).ToList();
//                    tempImages.Insert(0, "http://localhost:1213/nwvideo/Images/N.png");*/

//                    sus.Images = tempImages.ToArray();

//                    ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Inserting Upload object into the database.", sus.Upload.VideoID);
//                    // Insert the upload into the database.
//                    DBDataContext db = DBDataContext.CreateInstance();
//                    db.Uploads.InsertOnSubmit(sus.Upload);
//                    db.SubmitChanges();

//                    ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Insertion successful.", sus.Upload.VideoID);
//                    return SendFileChunkResult.Finished;
//                }
//            }
//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Chunk received.", sus.Upload.VideoID);
//            return SendFileChunkResult.Okay;
//        }
//        catch (InvalidOperationException)
//        {
//            return SendFileChunkResult.InvalidParameter;
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'SendFileChunk' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message, videoID);
//            return SendFileChunkResult.UnknownError;
//        }
//    }

//    /// <summary>
//    /// This method returns an array of absolute URIs to the images generated by the input file.
//    /// </summary>
//    /// <param name="videoID"></param>
//    /// <returns></returns>
//    [WebMethod]
//    public String[] GetThumbnailImages(String videoID)
//    {
//        try
//        {
//            var sus = SilverlightSessions.Where(i => i.Upload.VideoID == videoID).Single();
//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Fetching still image URLS for video '" + videoID + "'.", sus.Upload.VideoID);
//            return sus.Images.ToArray();
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'GetThumbnailImages' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message, videoID);
//            return null;
//        }
//    }


//    public enum SelectThumbnailImageResult { InvalidCredentials, UnknownError, Successful };
//    /// <summary>
//    /// This method allows the user to select which image they want to be a thumbnail and then
//    /// begins the video conversion process.
//    /// </summary>
//    /// <param name="username"></param>
//    /// <param name="password"></param>
//    /// <param name="videoID"></param>
//    /// <param name="thumbnailIndex"></param>
//    /// <param name="emailWhenFinished"></param>
//    /// <returns></returns>
//    [WebMethod]
//    public SelectThumbnailImageResult SelectThumbnailImage(String username, String password, String videoID, int thumbnailIndex, bool emailWhenFinished)
//    {
//        try
//        {
//            SilverlightUploadSession sus = SilverlightSessions.Where(i => i.Username == username
//                                                                     && i.Password == password
//                                                                     && i.Upload.VideoID == videoID).Single();

//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Selecting final still image for the video.", sus.Upload.VideoID);

//            String image = sus.Images[thumbnailIndex].Substring(sus.Images[thumbnailIndex].LastIndexOf("/") + 1);
//            image = Server.MapPath("Images") + "/" + image; // Local reference to the selected image.

//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Creating Video object.", sus.Upload.VideoID);
//            Video vid = new Video();
//            vid.VideoID = sus.Upload.VideoID;
//            vid.Title = sus.Upload.Title;
//            vid.Description = sus.Upload.Description;
//            vid.DatePosted = DateTime.Now;
//            vid.Username = sus.Username.ToLower();
//            vid.Size = sus.Upload.Size;
//            vid.Views = 0;
//            if (!String.IsNullOrEmpty(sus.Upload.Author))
//                vid.Author = sus.Upload.Author;
//            if (sus.Upload.AutoDeleteDate.HasValue)
//                vid.AutoDeleteDate = sus.Upload.AutoDeleteDate;
//            if (sus.Upload.GroupID.HasValue)
//                vid.GroupID = sus.Upload.GroupID;

//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Creating VideoConverter object.", sus.Upload.VideoID);
//            VideoConverter con = 
//                new VideoConverter(sus.Upload.RawFileName, 
//                                   image, 
//                                   Server.MapPath("ffmpeg") + "/ffmpeg.exe", 
//                                   vid, 
//                                   new UserAccount(username, password), 
//                                   emailWhenFinished);

//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Starting the VideoConverter.", sus.Upload.VideoID);
//            con.Start();
//            DBDataContext db = DBDataContext.CreateInstance();
//            Upload u = db.Uploads.Where(i => i.VideoID == videoID).Single();
//            db.Uploads.DeleteOnSubmit(u);
//            db.SubmitChanges();
//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Upload object no longer needed, successfully removed from the database.", sus.Upload.VideoID);
//            SilverlightSessions.Remove(sus);
//            return SelectThumbnailImageResult.Successful;
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'SelectThumbnailImage' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message, videoID);
//            return SelectThumbnailImageResult.UnknownError;
//        }
//    }

//    [WebMethod]
//    public bool Cancel(String username, String password, String videoID)
//    {
//        try
//        {
//            SilverlightUploadSession sus = SilverlightSessions.Where(i => i.Username == username
//                                                                     && i.Password == password
//                                                                     && i.Upload.VideoID == videoID).Single();
//            sus.Close();
//            sus.Delete();
//            SilverlightSessions.Remove(sus);
//            ApplicationLogger.LogItem(sus.Account, "Silverlight Uploader: Upload cancelled.", sus.Upload.VideoID);
//            return true;
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'Cancel' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message, videoID);
//            return false;
//        }
//    }

//    [WebMethod]
//    public void KeepAlive(String username, String password, String videoID)
//    {
//        // Needed a way for the server to perform cleanup if the user somehow closes the Silverlight Uploader
//        // during an upload. This allows the SilverlightUploader to notify the server that it is still "alive."
//        // This also keeps the ASP .NET worker process alive. If it idles out the SilverlightSessions will be lost
//        // when the process is shut down.
//        try
//        {
//            SilverlightUploadSession sus = SilverlightSessions.Where(i => i.Username == username
//                                                                     && i.Password == password
//                                                                     && i.Upload.VideoID == videoID).Single();
//            sus.LastKeepAlive = DateTime.Now;
//        }
//        catch (Exception ex)
//        {
//            ApplicationLogger.LogItem(null, "Silverlight Uploader: WebMethod 'KeepAlive' has experienced an exception:\n\nType: " + ex.GetType().ToString() + "\nMessage: " + ex.Message, videoID);
//        }
//    }

//}

