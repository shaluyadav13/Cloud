using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;
using MMRQueueInterfacer;
using System.Windows.Forms;
using System.Collections.Generic;

public partial class pages_UploadMedia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        UserAccount account1 = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account1.Admin && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Loading UploadMedia.aspx.");

        try
        {
            if (!Utility.canUpload())
            {
                uploadFormContainer.Visible = false;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Not enough server space for upload!");
                Utility.SendEmail(AppSettings.AdministratorEmailAddress, "NW Cloud", "The Northwest Cloud server has reached it's free space threshold and has " +
                                                                            "stopped accepting new uploads. The current free space on the server is " + Utility.getFreeDiskSpaceString() + ".");
                throw new ApplicationException("Northwest Cloud is not currently accepting uploads");

            }
        }
        catch (ApplicationException ex)
        {
            errorLabelContainer.Visible = true;
            errorLabel.Text = ex.Message;
        }
        // Put the allowed video file extensions into fileFormatLabel.
        // Note that FFMPEG may be able to handle more file types than those listed here. The ones listed
        // have been tested and are known to work, other formats are implicitly denied.


        if (!IsPostBack)
        {
            // Find out if the user is a student belonging to a StudentGroup. The student panel would need to be visible then.
            // Note that any OU can be a student, faculty and staff can take classes at Northwest.
            UserAccount account = (UserAccount)Session["account"];


            //sNumber.InnerHtml = account.Username;

            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Looking up student groups.");
            // Find out if the student belongs to more than one student group.
            DBDataContext db = DBDataContext.CreateInstance();
            //to check if the user signed agreement or not
            bool userSigned = (from agr in db.UserAgreements
                               where agr.Username == account.Username
                               select agr).Count() == 0;

            if (userSigned)
            {
                Response.Redirect("Agreement.aspx", false);
            }
            else
            {
                var stuGroups = from i in db.StudentGroups select i;


                List<StudentGroup> objstuGroups = stuGroups.AsQueryable().ToList();

                foreach (var item in stuGroups.ToList())
                {
                    bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                    if (!yes) { objstuGroups.Remove(item); }
                }

                var memberGroups = from i in db.StudentGroups select i;
                if (account.OU == OrganizationalUnit.StudentUsers)
                {                  
                   
                    objstuGroups = objstuGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();
                 
                }
                else if (account.Admin || account.OU == OrganizationalUnit.StaffUsers || account.OU == OrganizationalUnit.FacultyUsers)
                {
                  
                    objstuGroups = objstuGroups.Where(s => s.FacultyOwner.ToLower() == account.Username.ToLower()).OrderBy(x => x.GroupName).ToList();
                    List<StudentGroup> objmemGroups = db.StudentGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();
                    memberGroups = db.StudentGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName);
                    foreach (var item in memberGroups.ToList())
                    {
                        bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                        if (!yes) { objmemGroups.Remove(item); }
                    }

                    if (objstuGroups.Count() > 0 && objmemGroups.Count() > 0)
                    {
                        foreach (StudentGroup item in objmemGroups)
                        {
                            objstuGroups.Add(item);

                        }

                    }
                    else if (objstuGroups.Count() >= 0 && objmemGroups.Count() > 0)
                    {
                        objstuGroups = objmemGroups;
                    }

                    //stuGroups = (stuGroups.ToArray().Length > 0 && memberGroups.ToArray().Length > 0) ? stuGroups.Concat(memberGroups) : memberGroups;
                }
                //Commented by sarat
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User belongs to " + stuGroups.ToArray().Length + " student groups.");

                // If the account is faculty or staff, show option to upload to own profile
                if (account.OU == OrganizationalUnit.FacultyUsers || account.OU == OrganizationalUnit.StaffUsers)
                {
                    studentGroupListBox.Items.Add(new ListItem("My profile", "admin_profile", true));
                }

                //adding "No group, just post to my profile" list item to group list if faculty has at least one group
                try
                {

                    //
                    if (objstuGroups.Count() >= 1)
                    {
                        //stuGroups.OrderBy(StudentGroup => StudentGroup.GroupName);
                        foreach (var g in objstuGroups)
                        {
                            if (g.FacultyOwner.ToLower() == account.Username.ToLower())
                            {
                                studentGroupListBox.Items.Add(new ListItem(g.GroupName + " - Owner", g.GroupID.ToString()));
                                

                            }
                            else
                            {
                                studentGroupListBox.Items.Add(new ListItem(g.GroupName + " - Member", g.GroupID.ToString()));
                            }
                        }
                        

                    }
                    else
                    {
                        studentPanel.Visible = false;
                    }
                    studentGroupListBox.SelectedIndex = 0;
                }

                catch (Exception ex)
                {

                    throw ex;
                }
                //transcriptVisibility.Visible = false;
            }
        }
        else
        {
            errorLabelContainer.Visible = false;
            errorLabel.Text = "";
            lblNogroup.Visible = false;


        }

    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyMedia.aspx");
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        #region start upload media
        try
        {
            #region common Upload
            if (emailCheckBox.Checked)
            {
                Session["sendEmailToUploader"] = true;
            }
            else
            {
                Session["sendEmailToUploader"] = false;
            }
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: About to begin an upload.");

            String fileNameFull = FileUpload1.FileName;

            // Check the file name, make sure the extension is allowed.
            String ext = Path.GetExtension(fileNameFull);
            // If an auto-delete date is set, make sure it isn't today or earlier.
            DateTime autoDelete = new DateTime();
            bool hasAutoDelete = false;
            bool NameExists;
            String tempDirName;
            String fullFilePath;
            FileStream fs;
            String transcriptFileNameFull;
            String TranscriptExt;
            String transcriptFile;
            string fileDestination;
            //create an interfacer to interact with the MMRQueue service
            Interfacer MMRInterfacer = new Interfacer();
            if (DateTime.TryParse(autoDeleteDateTextBox.Text, out autoDelete))
            {
                if (autoDelete <= DateTime.Today.Date)
                {
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Validation error, user selected a bad auto-removal date, aborting.");
                    throw new ApplicationException("Auto-removal date must fall after the current date.");
                }
                hasAutoDelete = true;
            }
            DBDataContext db = DBDataContext.CreateInstance();
            // Create an Upload entity object for this (so far) incompleted upload.
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Creating database objects of upload, audio and websites.");
            Upload upload = new Upload();
            //Creating an audio Object
            Audio audio = new Audio();
            //Creating an website object
            Websites website = new Websites();
            Files files = new Files();
            Images images = new Images();

            Random rand;
            // If the user is a student, figure out which student group the video should go in.
            UserAccount account = (UserAccount)Session["account"];
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Searching database for user's student groups.");
            int? groupID = 0;
            var stuGroups = from i in db.StudentGroups select i;
            if (account.OU == OrganizationalUnit.StudentUsers)
            {
                //stuGroups = from i in db.StudentGroups
                //            where (from k in i.AuthorizedStudents
                //                   select k.Username).Contains(account.Username)
                //            && i.EndDate >= DateTime.Now.Date
                //            orderby i.GroupName
                //            select i;


                stuGroups = db.StudentGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username == account.Username)).OrderBy(x => x.GroupName);
            }
            else if (account.Admin || account.OU == OrganizationalUnit.StaffUsers || account.OU == OrganizationalUnit.FacultyUsers)
            {

                stuGroups = db.StudentGroups.Where(x => x.FacultyOwner == account.Username).OrderBy(x => x.GroupName);
                //stuGroups = from i in db.StudentGroups
                //            where i.FacultyOwner == account.Username
                //            && i.EndDate >= DateTime.Now.Date
                //            orderby i.GroupName
                //            select i;
            }

            // If we have more than one student group, or at least one and user is faculty, staff, or admin
            if (stuGroups.Count() >= 1)
            {
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Processing user's selected student group.");
                if (studentGroupListBox.SelectedIndex < 0)
                {
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Validation error, user failed to select a student group, aborting.");
                    throw new ApplicationException("Please select which group to post your video to.");
                }
                else
                {
                    String selectedGroup = studentGroupListBox.SelectedValue;
                    // If equal to admin_profile, the video should not go into a group. It will belong to the user.
                    if (selectedGroup != "admin_profile")
                    {
                        // Otherwise get the GroupID from the list box.
                        groupID = int.Parse(selectedGroup);



                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User is posting to group " + groupID + ".");
                    }
                    else
                    {
                        groupID = null;
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User is posting to own profile.");
                    }
                }
            }
            else
            {

                // Posting to own profile.
                groupID = null;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User is an admin, faculty, or staff not in any student groups. Posting to own profile.");
            }

            // Get the actual file contents, store them in a byte array.
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Receiving video data.");
            byte[] fileBytes = FileUpload1.FileBytes;

            String fileName = Path.GetFileName(fileNameFull);
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Media filename is '" + fileName + "'.");
            #endregion

            #region audio Upload
            //Check if the uploade file is audio
            if (AppSettings.AcceptableAudioFileFormats.Contains(ext.ToLower()))
            {

                // Create a temporary directory to store the file. Hash the filename
                // to a hex string and use that as the directory name.
                String audioID = hashString(fileName);
                String audioTempDir = AppSettings.AudioConversionFolder;
                // Check to see if this audio ID already exists.
                // Check the database for existing audios, also search the application's temp directory
                // in case a audio that is still being processed is using this audio ID.

                NameExists = (from aid in db.Audios
                              where aid.AudioID == audioID
                              select aid).Count() != 0
                                    || Directory.Exists(audioTempDir + audioID);


                int loops = 0;
                rand = new Random();
                while (NameExists)
                {
                    // If the name already exists, add a random number to the hash
                    // and hash it again so that it's different.
                    audioID = audioID + rand.Next().ToString();
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed name in use, trying '" + audioID + "'.");

                    // Check to see if the new audio ID is in use.
                    NameExists = (from aid in db.Audios
                                  where aid.AudioID == audioID
                                  select aid).Count() != 0
                                      || Directory.Exists(audioTempDir + audioID);

                    loops++;
                    if (loops > 200) // If this loops more than 200 times somehow, just give up.
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Unable to select an unused file name, aborting.");
                        throw new ApplicationException("An error has occurred. A filename could not be decided upon.");
                    }
                }

                tempDirName = audioTempDir + audioID;
                // Now create the temp directory.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Creating temporary directory '" + tempDirName + "'.", audioID);
                Directory.CreateDirectory(tempDirName);
                // Create a file in this directory containing the uploaded content.
                fullFilePath = tempDirName + "/" + audioID + ext;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Saving audio to '" + fullFilePath + "'.", audioID);
                fs = File.Create(fullFilePath);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Audio saved successfully.", audioID);


                // Finish the audio object's properties.
                audio.Title = titleTextBox.Text;
                audio.Author = ((UserAccount)Session["account"]).Username.ToLower();
                audio.Username = ((UserAccount)Session["account"]).Username.ToLower();
                audio.AudioID = audioID;
                audio.Description = descriptionTextBox.Text;
                audio.DatePosted = DateTime.Now;
                audio.GroupID = groupID;
                audio.Size = (new FileInfo(fullFilePath)).Length;
                if (FileUpload_transcript.HasFile)
                {
                    //The trascript for a audio
                    transcriptFileNameFull = FileUpload_transcript.FileName;

                    // Check the file name, make sure the extension is allowed.
                    TranscriptExt = Path.GetExtension(transcriptFileNameFull);

                    if (!AppSettings.AcceptableTranscriptFileFormats.Contains(TranscriptExt.ToLower()))
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Input Transcript was an unaccepted format, aborting.");
                        throw new ApplicationException("Invalid Transcript format.");
                    }

                    transcriptFile = audioID + TranscriptExt;
                    FileUpload_transcript.PostedFile.SaveAs(Server.MapPath("~\\Transcripts\\") + transcriptFile);
                    audio.Transcript = true;
                    Session["transcriptFile"] = transcriptFile;
                }
                else
                {
                    audio.Transcript = false;
                }

                if (hasAutoDelete)
                {
                    audio.AutoDeleteDate = autoDelete;
                }

                if (!String.IsNullOrEmpty(authorTextBox.Text.Trim()))
                {
                    audio.Author = authorTextBox.Text;
                }

                //set the file destination
                fileDestination = Server.MapPath("convertedAudios") + "\\" + audio.AudioID;



                //convert the file and move it to the converted audios section
                string email;
                if (account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StudentUsers))
                {
                    email = account.Email.Split('@')[0] + "@mail.nwmissouri.edu";
                }
                else
                {
                    email = account.Email;
                }
                // Don't send an email if the checkbox wass't checked
                if ((bool)Session["sendEmailToUploader"] == false)
                {
                    email = "";
                }
                Session["sendEmailToUploader"] = null;
                MMRInterfacer.ProcessAudio(fullFilePath, fileDestination, email);

                // audio.Copyright = copyrightListBox.SelectedValue;
                //groupid null --audio comes under no group(not sharing with others)
                if (audio.GroupID == null)
                {
                    audio.ShareStatus = false;
                }
                else
                {
                    audio.ShareStatus = true;
                }
                db.Audios.InsertOnSubmit(audio);
                db.SubmitChanges();
                Session["Audio"] = audio;
                Response.Redirect("UploadComplete.aspx?type=audio", true);
            }
            #endregion
            #region video Upload
            else if (AppSettings.AcceptableVideoFileFormats.Contains(ext.ToLower()))
            {
                // Create a temporary directory to store the file. Hash the filename
                // to a hex string and use that as the directory name.
                String videoID = hashString(fileName);
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed file name is '" + videoID + "'.");

                // Check the application settings to see where we are supposed to store temp data.
                String vidTempDir = AppSettings.VideoConversionFolder;


                // Check to see if this video ID already exists.
                // Check the database for existing videos, also search the application's temp directory
                // in case a video that is still being processed is using this video ID.
                NameExists = (from vid in db.Videos
                              where vid.VideoID == videoID
                              select vid).Count() != 0
                                    || Directory.Exists(vidTempDir + videoID);
                int loops = 0;
                rand = new Random();
                while (NameExists)
                {
                    // If the name already exists, add a random number to the hash
                    // and hash it again so that it's different.
                    videoID = videoID + rand.Next().ToString();
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed name in use, trying '" + videoID + "'.");

                    // Check to see if the new video ID is in use.
                    NameExists = (from vid in db.Videos
                                  where vid.VideoID == videoID
                                  select vid).Count() != 0
                                  || Directory.Exists(vidTempDir + videoID);

                    loops++;
                    if (loops > 200) // If this loops more than 200 times somehow, just give up.
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Unable to select an unused file name, aborting.");
                        throw new ApplicationException("An error has occurred. A filename could not be decided upon.");
                    }
                }

                tempDirName = vidTempDir + videoID;
                // Now create the temp directory.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Creating temporary directory '" + tempDirName + "'.", videoID);
                Directory.CreateDirectory(tempDirName);
                // Create a file in this directory containing the uploaded content.
                fullFilePath = tempDirName + "/" + videoID + ext;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Saving video to '" + fullFilePath + "'.", videoID);
                fs = File.Create(fullFilePath);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Video saved successfully.", videoID);
                //add the full file path to session
                Session["FullFilePath"] = fullFilePath;

                // Now run ffmpeg over the input video and have it pull several stills out
                // for use as a thumbnail.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Taking still images from video using ffmpeg.", videoID);
                ProcessStartInfo info = new ProcessStartInfo();
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                info.FileName = Server.MapPath("ffmpeg") + "\\ffmpeg.exe";
                String captureFileName = Server.MapPath("TempImages") + "/" + videoID + "-image-%02d.png";
                // String captureFileName = "C:\\NWCloud\\TempImages\\" + videoID + "-image-%02d.png";
                String resolution = AppSettings.StillImageResolution;
                int count = AppSettings.StillImageCaptureCount;
                double rate = AppSettings.StillImageCaptureRate;
                info.Arguments = String.Format(" -i \"{0}\" -r {1} -s {2} -vframes {3} -f image2 \"{4}\"",
                    fullFilePath,
                    rate,
                    resolution,
                    count,
                    captureFileName);

                // ffmpeg runs as a separate process.
                Process p = Process.Start(info);
                // Wait for the process to terminate before continuing. This should only take a moment since we're
                // only grabbing a few still images.
                p.WaitForExit();
                p.Close();
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Stills taken, checking status.", videoID);

                // Several still images for the video should now exist. If they don't, then ffmpeg likely
                // could not understand the video, meaning the video is likely corrupt or an odd format.
                // If this is the case, perform cleanup and inform the user that the file couldn't be understood.
                var image = from i in Directory.GetFiles(Server.MapPath("TempImages"))
                            where i.Contains(videoID)
                            select i;

                if (image.Count() == 0)
                {
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Image capture failed, aborting.", videoID);
                    // ffmpeg failed.
                    AppCleanUp.RemoveTempFiles(videoID, Server.MapPath("Images"));
                    throw new ApplicationException("The file could not be uploaded. It may be corrupt or of a format not compatible with this website.");
                }
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Image capture successful.", videoID);
                // Finish the upload object's properties.
                upload.GroupID = groupID;
                upload.Title = titleTextBox.Text;
                upload.Author = ((UserAccount)Session["account"]).Username.ToLower();
                upload.Username = ((UserAccount)Session["account"]).Username.ToLower();
                upload.VideoID = videoID;
                upload.Description = descriptionTextBox.Text;
                upload.DateUploaded = DateTime.Now;
                upload.RawFileName = fullFilePath;
                upload.Video = 1;
                upload.Audio = 0;
                // upload.Copyright = null;
                // Find the size of the original (non-converted) upload.
                upload.Size = (new FileInfo(fullFilePath)).Length;

                if (FileUpload_transcript.HasFile)
                {
                    //The trascript for a video
                    transcriptFileNameFull = FileUpload_transcript.FileName;

                    // Check the file name, make sure the extension is allowed.
                    TranscriptExt = Path.GetExtension(transcriptFileNameFull);

                    if (!AppSettings.AcceptableTranscriptFileFormats.Contains(TranscriptExt.ToLower()))
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Input Transcript was an unaccepted format, aborting.");
                        throw new ApplicationException("Invalid Transcript format.");
                    }

                    transcriptFile = videoID + TranscriptExt;
                    FileUpload_transcript.PostedFile.SaveAs(Server.MapPath("~\\Transcripts\\") + transcriptFile);
                    upload.Transcript = true;
                    Session["transcriptFile"] = transcriptFile;
                }
                else
                {
                    upload.Transcript = false;
                }

                // Insert the upload into the database.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Inserting Upload object into database.", videoID);
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Saving Upload object to session state.", videoID);

                if (hasAutoDelete)
                {
                    upload.AutoDeleteDate = autoDelete;
                }

                if (!String.IsNullOrEmpty(authorTextBox.Text.Trim()))
                {
                    upload.Author = authorTextBox.Text;
                }


                //  upload.Copyright = copyrightListBox.SelectedValue;


                db.Uploads.InsertOnSubmit(upload);
                db.SubmitChanges();

                // Save the upload to the session state.

                Session["upload"] = upload;

                // The video has been uploaded and stills have been created. Redirect to the Select
                // Thumbnail page.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Redirecting to SelectThumbnail.aspx.", videoID);
                Response.Redirect("SelectThumbnail.aspx", false);
            }
            #endregion
            #region website upload
            else if (AppSettings.AcceptableWebFileFormats.Contains(ext.ToLower()))
            {

                // Create a temporary directory to store the file. Hash the filename
                // to a hex string and use that as the directory name.


                String webID = hashString(fileName);
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed file name is '" + webID + "'.");

                // Check the application settings to see where we are supposed to store temp data.
                String webTempDir = AppSettings.WebConversionFolder;


                // Check to see if this web ID already exists.
                // Check the database for existing websites, also search the application's temp directory
                // in case a website that is still being processed is using this web ID.
                NameExists = (from web in db.Websites
                              where web.WebID == webID
                              select web).Count() != 0
                                     || Directory.Exists(webTempDir + webID);
                int loops = 0;
                rand = new Random();
                while (NameExists)
                {
                    // If the name already exists, add a random number to the hash
                    // and hash it again so that it's different.
                    webID = webID + rand.Next().ToString();
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed name in use, trying '" + webID + "'.");

                    // Check to see if the new web ID is in use.
                    NameExists = (from web in db.Websites
                                  where web.WebID == webID
                                  select web).Count() != 0
                                  || Directory.Exists(webTempDir + webID);

                    loops++;
                    if (loops > 200) // If this loops more than 200 times somehow, just give up.
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Unable to select an unused file name, aborting.");
                        throw new ApplicationException("An error has occurred. A filename could not be decided upon.");
                    }
                }

                tempDirName = webTempDir + webID;
                // Now create the temp directory.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Creating temporary directory '" + tempDirName + "'.", webID);
                Directory.CreateDirectory(tempDirName);
                // Create a file in this directory containing the uploaded content.
                fullFilePath = tempDirName + "/" + webID + ext;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Saving website to '" + fullFilePath + "'.", webID);
                fs = File.Create(fullFilePath);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: website saved successfully.", webID);


                // Finish the website object's properties.
                website.Title = titleTextBox.Text;
                website.Username = ((UserAccount)Session["account"]).Username.ToLower();
                website.Author = ((UserAccount)Session["account"]).Username.ToLower();
                website.WebID = webID;
                website.Description = descriptionTextBox.Text;
                website.Views = 0;
                website.DatePosted = DateTime.Now;
                website.GroupID = groupID;


                website.Size = (new FileInfo(fullFilePath)).Length;


                if (hasAutoDelete)
                {
                    website.AutoDeleteDate = autoDelete;
                }

                if (!String.IsNullOrEmpty(authorTextBox.Text.Trim()))
                {
                    website.Author = authorTextBox.Text;
                }

                //set the file destination
                fileDestination = Server.MapPath("convertedWeb") + "\\" + website.WebID;

                //http://10.1.1.102/
                //convert the file and move it to the converted websites section
                string email;
                if (account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StudentUsers))
                {
                    email = account.Email.Split('@')[0] + "@mail.nwmissouri.edu";
                }
                else
                {
                    email = account.Email;
                }
                // Don't send an email if the checkbox wasn't checked
                if ((bool)Session["sendEmailToUploader"] == false)
                {
                    email = "";
                }
                Session["sendEmailToUploader"] = null;
                MMRInterfacer.ProcessWebsite(fullFilePath, website.WebID, fileDestination, email);



                // website.Copyright = copyrightListBox.SelectedValue;
                //groupid null --website comes under no group(not sharing with others)
                if (website.GroupID == null)
                {
                    website.ShowStatus = false;
                }
                else
                {
                    website.ShowStatus = true;
                }
                db.Websites.InsertOnSubmit(website);
                db.SubmitChanges();
                Session["website"] = website;
                Response.Redirect("UploadComplete.aspx?type=website");
            }
            #endregion
            #region documents Upload
            else if (AppSettings.AcceptableFileFormats.Contains(ext.ToLower()))
            {

                // Create a temporary directory to store the file. Hash the filename
                // to a hex string and use that as the directory name.
                String fileID = hashString(fileName);

                //This has been changed from using the "Web.config" value to the relative path of the program
                //String fileTempDir = AppSettings.DocumentSavedFolder;
                String fileTempDir = Server.MapPath("documents");

                // Check to see if this file ID already exists.
                // Check the database for existing documents, also search the application's  directory
                // in case a file that is still being processed is using this file ID.

                NameExists = (from aid in db.Files
                              where aid.FileID == fileID
                              select aid).Count() != 0
                                    || Directory.Exists(fileTempDir + fileID);


                int loops = 0;
                rand = new Random();
                while (NameExists)
                {
                    // If the name already exists, add a random number to the hash
                    // and hash it again so that it's different.
                    fileID = fileID + rand.Next().ToString();
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed name in use, trying '" + fileID + "'.");

                    // Check to see if the new file ID is in use.
                    NameExists = (from aid in db.Files
                                  where aid.FileID == fileID
                                  select aid).Count() != 0
                                      || Directory.Exists(fileTempDir + fileID);

                    loops++;
                    if (loops > 200) // If this loops more than 200 times somehow, just give up.
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Unable to select an unused file name, aborting.");
                        throw new ApplicationException("An error has occurred. A filename could not be decided upon.");
                    }
                }

                tempDirName = fileTempDir + "/" + fileID;
                // Now create the temp directory.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Creating temporary directory '" + tempDirName + "'.", fileID);
                Directory.CreateDirectory(tempDirName);
                // Create a file in this directory containing the uploaded content.
                fullFilePath = tempDirName + "/" + fileID + ext;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Saving file to '" + fullFilePath + "'.", fileID);
                fs = File.Create(fullFilePath);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: file saved successfully.", fileID);


                // Finish the file object's properties.
                files.Title = titleTextBox.Text;
                files.Username = ((UserAccount)Session["account"]).Username.ToLower();
                files.Author = ((UserAccount)Session["account"]).Username.ToLower();
                files.FileID = fileID;
                files.Description = descriptionTextBox.Text;
                files.DatePosted = DateTime.Now;
                files.GroupID = groupID;
                files.Size = (new FileInfo(fullFilePath)).Length;

                if (hasAutoDelete)
                {
                    files.AutoDeleteDate = autoDelete;
                }

                if (!String.IsNullOrEmpty(authorTextBox.Text.Trim()))
                {
                    files.Author = authorTextBox.Text;
                }




                // files.Copyright = copyrightListBox.SelectedValue;
                //groupid null --file comes under no group(not sharing with others)
                if (files.GroupID == null)
                {
                    files.ShowStatus = false;
                }
                else
                {
                    files.ShowStatus = true;
                }
                db.Files.InsertOnSubmit(files);
                db.SubmitChanges();
                Session["Files"] = files;
                Response.Redirect("UploadComplete.aspx?type=files", false);
            }
            #endregion
            #region Images Upload
            else if (AppSettings.AcceptableImageFormats.Contains(ext.ToLower()))
            {

                // Create a temporary directory to store the file. Hash the filename
                // to a hex string and use that as the directory name.
                String ImageID = hashString(fileName);

                //This has been changed from using the "Web.config" value to the relative path of the program
                //String fileTempDir = AppSettings.ImagesSavedFolder;
                String fileTempDir = Server.MapPath("userimages");

                // Check to see if this file ID already exists.
                // Check the database for existing documents, also search the application's  directory
                // in case a file that is still being processed is using this file ID.

                NameExists = (from fid in db.Images
                              where fid.ImageID == ImageID
                              select fid).Count() != 0
                                    || Directory.Exists(fileTempDir + ImageID);


                int loops = 0;
                rand = new Random();
                while (NameExists)
                {
                    // If the name already exists, add a random number to the hash
                    // and hash it again so that it's different.
                    ImageID = ImageID + rand.Next().ToString();
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Hashed name in use, trying '" + ImageID + "'.");

                    // Check to see if the new ImageID is in use.
                    NameExists = (from fid in db.Images
                                  where fid.ImageID == ImageID
                                  select fid).Count() != 0
                                      || Directory.Exists(fileTempDir + ImageID);

                    loops++;
                    if (loops > 200) // If this loops more than 200 times somehow, just give up.
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Unable to select an unused file name, aborting.");
                        throw new ApplicationException("An error has occurred. A filename could not be decided upon.");
                    }
                }

                tempDirName = fileTempDir + "/" + ImageID;
                // Now create the temp directory.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Creating temporary directory '" + tempDirName + "'.", ImageID);

                Directory.CreateDirectory(tempDirName);
                // Create a file in this directory containing the uploaded content.

                fullFilePath = tempDirName + "/" + ImageID + ext;
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Saving file to '" + fullFilePath + "'.", ImageID);

                fs = File.Create(fullFilePath);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: file saved successfully.", ImageID);





                // Finish the image object's properties.
                images.Title = titleTextBox.Text;
                images.Username = ((UserAccount)Session["account"]).Username.ToLower();
                images.Author = ((UserAccount)Session["account"]).Username.ToLower();
                images.ImageID = ImageID;
                images.Description = descriptionTextBox.Text;
                images.DatePosted = DateTime.Now;
                images.GroupID = groupID;
                images.Size = (new FileInfo(fullFilePath)).Length;

                if (hasAutoDelete)
                {
                    images.AutoDeleteDate = autoDelete;
                }

                if (!String.IsNullOrEmpty(authorTextBox.Text.Trim()))
                {
                    images.Author = authorTextBox.Text;
                }




                // images.Copyright = copyrightListBox.SelectedValue;
                //groupid null --file comes under no group(not sharing with others)
                if (images.GroupID == null)
                {
                    images.ShowStatus = false;
                }
                else
                {
                    images.ShowStatus = true;
                }
                db.Images.InsertOnSubmit(images);
                db.SubmitChanges();
                Session["Images"] = images;
                Response.Redirect("UploadComplete.aspx?type=images", true);
            }
            #endregion

            #region invalid file upload
            else
            {
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Input file was an unaccepted format, aborting.");
                throw new ApplicationException("Invalid file format.");
            }
            #endregion
        }
        catch (System.ServiceModel.EndpointNotFoundException ex)
        {
            errorLabelContainer.Visible = true;
            errorLabel.Text = "The conversion system is down. Sorry for the inconvenience. Please try again later.";
        }
        catch (ApplicationException ex)
        {
            errorLabelContainer.Visible = true;
            errorLabel.Text = ex.Message;
        }
        catch (Exception ex)
        {
            errorLabelContainer.Visible = true;
            errorLabel.Text = ex.Message + " --- " + ex.ToString();
        }
        #endregion
    }

    private bool tempDirExists(String path)
    {
        return Directory.Exists(path);
    }

    /// <summary>
    /// Takes a string and performs an MD5 hash on it.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private String hashString(String s)
    {
        MD5 md5 = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(s);
        byte[] hashedBytes = md5.ComputeHash(bytes);
        StringBuilder sb = new StringBuilder("");
        foreach (byte b in hashedBytes)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }


    //-------- Old code commented out by Lawrence Foley on 4/23/2015 ----///
    ////These methods shows visibility of transcript upload
    //protected void transcriptBtn_Yes_CheckedChanged(object sender, EventArgs e)
    //{
    //    showTranscipt.Visible = true;
    //    reqFileUpload.Enabled = true;


    //    transcriptVisibility.Visible = true;
    //}
    //protected void transcriptBtn_No_CheckedChanged(object sender, EventArgs e)
    //{
    //    showTranscipt.Visible = false;
    //    reqFileUpload.Enabled = false;


    //}

    //----------old code----------------//

    //This method allows transcripts upload only for Audio and Video
    //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlType.SelectedValue == "Video" || ddlType.SelectedValue == "Audio")
    //    {

    //        transcriptVisibility.Visible = true;
    //    }
    //    else
    //    {

    //        transcriptVisibility.Visible = false;
    //    }

    //}

    ////This method is for populating sub categories under creative commons
    //protected void copyrightListBox_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    descriptionDiv.Visible =true;
    //    if (copyrightListBox.SelectedValue == "CreativeCommons")
    //    {
    //        creativeCommonsBox.Visible = true;
    //        descriptionDiv.InnerHtml = "Self created video.  If sharing with others, give them permission for educational use as long as credit is given to back to the author";
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //    else if (copyrightListBox.SelectedValue == "FairUse")
    //    {
    //        creativeCommonsBox.Visible = false;
    //        descriptionDiv.InnerHtml = "The video is copyrighted and have received written permission for usage. The video is copyrighted and I am using it following the Fair Use Guidelines for Education Video is in the public domain. The author/producer of the video is allowing Creative Commons";
    //        descriptionDiv.Style.Add("margin-right","22%");
    //    }
    //    else if (copyrightListBox.SelectedValue == "PublicDomain")
    //    {
    //        creativeCommonsBox.Visible = false;
    //        descriptionDiv.InnerHtml = "Public Domain";
    //        descriptionDiv.Style.Add("margin-right", "20%");

    //    }
    //    else 
    //    {
    //        creativeCommonsBox.Visible = false;
    //        descriptionDiv.InnerHtml = "Copyrights Permission";
    //        descriptionDiv.Style.Add("margin-right", "20%");
    //    }
    //}
    ////Creative Commons Subcategories description
    //protected void creativeCommonsBox_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    String description = "";
    //    if (creativeCommonsBox.SelectedValue == "Attribution")
    //    {
    //        description = "<a rel='license' target='_blank' href='http://creativecommons.org/licenses/by/4.0/'><img alt='Creative Commons License' style='border-width:0' src='https://i.creativecommons.org/l/by/4.0/88x31.png' /></a><br/>";
    //        description += "<h4 style='height:5px;'>Description</h4>";
    //        description += "This license lets others distribute, remix, tweak, and build upon your work, even commercially, as long as they credit you for the original creation. This is the most accommodating of licenses offered, in terms of what others can do with your works licensed under Attribution.";
    //        descriptionDiv.InnerHtml = description;
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //    else if (creativeCommonsBox.SelectedValue == "Attribution-ShareAlike")
    //    {
    //        descriptionDiv.InnerHtml = "This license lets others remix, tweak, and build upon your work even for commercial reasons, as long as they credit you and license their new creations under the identical terms. This license is often compared to open source software licenses. All new works based on yours will carry the same license, so any derivatives will also allow commercial use.";
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //    else if (creativeCommonsBox.SelectedValue == "Attribution-NoDerivs")
    //    {
    //        descriptionDiv.InnerHtml = "This license allows for redistribution, commercial and non-commercial, as long as it is passed along unchanged and in whole, with credit to you.";
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //    else if (creativeCommonsBox.SelectedValue == "Attribution-NonCommercial")
    //    {
    //        descriptionDiv.InnerHtml = "This license lets others remix, tweak, and build upon your work non-commercially, and although their new works must also acknowledge you and be non-commercial, they don’t have to license their derivative works on the same terms.";
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //    else if (creativeCommonsBox.SelectedValue == "Attribution-NonCommercial-ShareAlike")
    //    {
    //        descriptionDiv.InnerHtml = "This license lets others remix, tweak, and build upon your work non-commercially, as long as they credit you and license their new creations under the identical terms. Others can download and redistribute your work just like the by-nc-nd license, but they can also translate, make remixes, and produce new stories based on your work. All new work based on yours will carry the same license, so any derivatives will also be non-commercial in nature.";
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //    else
    //    {
    //        descriptionDiv.InnerHtml = "This license is the most restrictive of our six main licenses, allowing redistribution. This license is often called the “free advertising” license because it allows others to download your works and share them with others as long as they mention you and link back to you, but they can’t change them in any way or use them commercially.";
    //        descriptionDiv.Style.Remove("margin-right");
    //    }
    //}
}