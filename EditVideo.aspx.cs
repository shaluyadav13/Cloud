using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;
using VideoTransfer.Common;
using MMRQueueInterfacer;
using System.Diagnostics;

public partial class EditVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Redirect to the login page if the user is not signed in.
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);

            errorLabel.Text = "";

            UserAccount account = (UserAccount)Session["account"];
            //Admin is visible to only admins,faculty users,staff users
            if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                ad.Visible = false;
            }
            //sNumber.InnerHtml = account.Username;

            lbl_ClaimMessage.Visible = false;

            if (!IsPostBack)
                loadVideoInfo();
        }
        catch (Exception ex)
        {

        }

    }

    private void loadVideoInfo()
    {
        // Get the video ID from the query string.
        String vid = (String)Request.QueryString["vid"];

        try
        {
            UserAccount account = (UserAccount)Session["account"];

            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Looking up student groups.");
            // Find out if the student belongs to more than one student group.
            DBDataContext db = DBDataContext.CreateInstance();

            var stuGroups = from i in db.StudentGroups select i;
            List<StudentGroup> objstuGroups = stuGroups.AsQueryable().ToList();
            foreach (var item in stuGroups.ToList())
            {
                bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                if (!yes) { objstuGroups.Remove(item); }
            }
            List<StudentGroup> objmemGroups = objstuGroups;
            if (account.OU == OrganizationalUnit.StudentUsers)
            {
                objstuGroups = objstuGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();

            }
            else if (account.Admin || account.OU == OrganizationalUnit.StaffUsers || account.OU == OrganizationalUnit.FacultyUsers)
            {

                objmemGroups = objstuGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();
                objstuGroups = objstuGroups.Where(x => x.FacultyOwner.ToLower() == account.Username.ToLower()).OrderBy(x => x.GroupName).ToList();
                objstuGroups.AddRange(objmemGroups);
            }
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User belongs to " + stuGroups.Count() + " student groups.");

            // Search for the video.
            var videos = from i in db.Videos
                         where i.VideoID == vid
                         select i;

            if (videos.Count() == 0) // If the video doesn't exist, redirect.
                Response.Redirect("MyVideos.aspx", true);

            Video v = videos.First();

            // Make sure the user has permission to edit this video.
            //UserAccount account = (UserAccount)Session["account"];
            bool admin = account.Admin;
            String username = account.Username.ToLower();
            int groupid = 0;
            String groupowner = username;
            if (admin && v.GroupID != null)
            {
                groupid = (int)db.Videos.Where(x => x.VideoID == vid).Select(x => x.GroupID).FirstOrDefault();
                groupowner = db.StudentGroups.Where(x => x.GroupID == groupid).FirstOrDefault().FacultyOwner;

            }


            // The user can always edit their own videos. Admins may edit any videos.
            if (admin || username == v.Username.ToLower() || groupowner.ToLower() == account.Username.ToLower())
            {
                //if (account.OU == OrganizationalUnit.StudentUsers)
                //{
                //    //for student always share their video to one of the groups they enrolled in                   
                //    rbtn2.Enabled = false;
                //}
                if (stuGroups.Count() >= 1)
                {
                    if (!(account.OU == OrganizationalUnit.StudentUsers))
                    {
                        if (username == v.Username.ToLower())
                        {
                            studentGroupListBox.Items.Add(new ListItem("My profile", "admin_profile"));
                        }
                        else if (groupowner.ToLower() == account.Username.ToLower())
                        {
                            btnClaim.Visible = true;
                        }
                    }
                    foreach (var g in objstuGroups.ToList())
                    {
                        studentGroupListBox.Items.Add(new ListItem(g.GroupName, g.GroupID.ToString()));
                    }

                }
                else
                {
                    studentGroupPanel.Visible = false;
                }
                titleTextBox.Text = v.Title;
                descriptionTextBox.Text = v.Description;

                if (v.GroupID != null)
                {
                    studentGroupListBox.SelectedValue = v.GroupID.ToString();
                }
                else
                {
                    studentGroupListBox.SelectedValue = studentGroupListBox.Items.FindByText("My profile").Value;
                }
                if (v.AutoDeleteDate.HasValue)
                {
                    autoRemoveDateTextBox.Text = v.AutoDeleteDate.Value.ToString(@"MM/dd/yyyy");
                }

                if (!String.IsNullOrEmpty(v.Author))
                {
                    authorTextBox.Text = v.Author;
                }


            }
            else
            {
                //// Check to see if the user is editing one of their students' videos.
                //if (!(v.GroupID.HasValue && groupowner.ToLower() == account.Username.ToLower()))
                //{
                // User can't edit this video, redirect.
                Response.Redirect("MyVideos.aspx", true);
                // }
            }
        }
        catch (ApplicationException ex)
        {
            errorLabel.Text = ex.Message;
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
    protected void removeVideoButton_Click(object sender, EventArgs e)
    {
        // Remove the video.
        // Get the video ID from the query string.
        String vid = (String)Request.QueryString["vid"];

        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            // Search for the video.
            var v = (from i in db.Videos
                     where i.VideoID == vid
                     select i).Single();

            // See if the user owns this video.
            UserAccount account = (UserAccount)Session["account"];
            bool wasOwner = account.Username.ToLower() == v.Username.ToLower();
            String groupowner = v.Username;
            if (v.GroupID != null)
            {
                groupowner = db.StudentGroups.Where(x => x.GroupID == v.GroupID).FirstOrDefault().FacultyOwner;
            }
            bool wasFacultyOwner = v.GroupID.HasValue && groupowner == account.Username.ToLower();

            // Delete the video(s)
            AppCleanUp.RemoveVideo(v.VideoID);

            db.Videos.DeleteOnSubmit(v);
            db.SubmitChanges();
            if (wasOwner)
                returnFromEdit(ReturnFromEditValue.Owner);
            else if (wasFacultyOwner)
                returnFromEdit(ReturnFromEditValue.Student);
            else
                returnFromEdit(ReturnFromEditValue.Admin);

        }
        catch (ApplicationException ex)
        {
            errorLabel.Text = ex.Message;
        }
        catch (System.IO.IOException)
        {
            errorLabel.Text = "This file is currently being processed and cannot be deleted. Please try again later.";
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    private enum ReturnFromEditValue { Owner, Admin, Student };
    private void returnFromEdit(ReturnFromEditValue value)
    {
        // If this video belongs to the user, return to MyVideos.
        if (value == ReturnFromEditValue.Owner)
            Response.Redirect("MyVideos.aspx", true);
        // If faculty was editing their students' videos.
        else if (value == ReturnFromEditValue.Student)
            Response.Redirect("MyStudentsVideos.aspx", true);
        // Otherwise the user is an admin editing someone else's video, return to AllVideos.
        else
            Response.Redirect("AllVideos.aspx", true);
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        // Get the video ID from the query string.
        String vid = (String)Request.QueryString["vid"];

        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            // Search for the video's owner.
            var v = (from i in db.Videos
                     where i.VideoID == vid
                     select i).Single();

            UserAccount account = (UserAccount)Session["account"];
            // See if the user owns this video, used for redirection when finished.
            bool wasOwner = account.Username.ToLower() == v.Username.ToLower();
            String groupowner = "";
            if (v.GroupID == null)
            {
                groupowner = v.Username;
            }
            else
            {
                groupowner = db.StudentGroups.Where(x => x.GroupID == v.GroupID).FirstOrDefault().FacultyOwner;
            }
            bool wasFacultyOwner = groupowner.ToLower() == account.Username.ToLower();

            if (wasOwner)
                returnFromEdit(ReturnFromEditValue.Owner);
            else if (wasFacultyOwner)
                returnFromEdit(ReturnFromEditValue.Student);
            else
                returnFromEdit(ReturnFromEditValue.Admin);
        }
        catch (ApplicationException ex)
        {
            errorLabel.Text = ex.Message;
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // Get the video ID from the query string.
            String vid = (String)Request.QueryString["vid"];
            UserAccount account = (UserAccount)Session["account"];
            Upload upload = new Upload();
            try
            {
                DBDataContext db = DBDataContext.CreateInstance();

                // Search for the video.
                var v = (from i in db.Videos
                         where i.VideoID == vid
                         select i).Single();

                var stuGroups = from i in db.StudentGroups select i;
                List<StudentGroup> objstuGroups = stuGroups.AsQueryable().ToList();
                foreach (var item in stuGroups.ToList())
                {
                    bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                    if (!yes) { objstuGroups.Remove(item); }
                }
                List<StudentGroup> objmemGroups = objstuGroups;
                if (account.OU == OrganizationalUnit.StudentUsers)
                {
                    objstuGroups = objstuGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();
                }
                else if (account.Admin || account.OU == OrganizationalUnit.StaffUsers || account.OU == OrganizationalUnit.FacultyUsers)
                {
                    objmemGroups = objstuGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();
                    objstuGroups = objstuGroups.Where(x => x.FacultyOwner.ToLower() == account.Username.ToLower()).OrderBy(x => x.GroupName).ToList();
                    objstuGroups.AddRange(objmemGroups);
                }

                // If we have more than one student group, or at least one and user is faculty, staff, or admin
                if (stuGroups.Count() >= 1)
                //|| (stuGroups.Count() > 0 &&
                //    (account.Admin
                //    || account.OU == OrganizationalUnit.FacultyUsers
                //    || account.OU == OrganizationalUnit.StaffUsers)
                //    )
                //)
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
                            int groupID = int.Parse(selectedGroup);
                            upload.GroupID = groupID;
                            v.GroupID = groupID;
                            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User is posting to group " + groupID + ".");
                        }
                        else
                        {
                            v.GroupID = null;
                            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User is posting to own profile.");
                        }
                    }
                }
                //else if (stuGroups.Count() == 1)
                //{
                //    // Only one group. Use that one.
                //    var group = stuGroups.Single();
                //    upload.GroupID = group.GroupID;
                //    v.GroupID = group.GroupID;
                //    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User only belongs to one group, automatically selecting group " + group.GroupID + ".");
                //}
                else
                {
                    // No code needed here. Posting to own profile.
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User is an admin, faculty, or staff not in any student groups. Posting to own profile.");
                }


                // See if the user owns this video, used for redirection when finished.
                bool wasOwner = account.Username.ToLower() == v.Username.ToLower();
                String groupowner = v.Username;
                if (v.GroupID != null)
                {
                    groupowner = db.StudentGroups.Where(x => x.GroupID == v.GroupID).FirstOrDefault().FacultyOwner;
                }
                bool wasFacultyOwner = v.GroupID.HasValue && groupowner == account.Username.ToLower();

                // Update the video.
                v.Title = titleTextBox.Text;
                v.Description = descriptionTextBox.Text;

                // Check the auto-delete date.
                DateTime autoDelete;
                if (DateTime.TryParse(autoRemoveDateTextBox.Text, out autoDelete))
                {
                    if (autoDelete <= DateTime.Now.Date)
                    {
                        throw new ApplicationException("Auto-Removal date must come after today's date.");

                    }
                    v.AutoDeleteDate = autoDelete;
                }

                if (!String.IsNullOrEmpty(authorTextBox.Text.Trim()))
                {
                    v.Author = authorTextBox.Text;

                }
                //Replace Transcript
                if (transcript.FileName != "")
                {
                    string transcriptFileNameFull = transcript.FileName;

                    // Check the file name, make sure the extension is allowed.
                    string TranscriptExt = Path.GetExtension(transcriptFileNameFull);

                    if (!AppSettings.AcceptableTranscriptFileFormats.Contains(TranscriptExt.ToLower()))
                    {
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: Input Transcript was an unaccepted format, aborting.");
                        throw new ApplicationException("Invalid Transcript format.");
                    }

                    string transcriptFile = (String)Request.QueryString["vid"] + TranscriptExt;
                    transcript.PostedFile.SaveAs(Server.MapPath("~\\Transcripts\\") + transcriptFile);
                    v.Transcript = true;
                    //Session["transcriptFile"] = transcriptFile;
                }


                // Replace file if there is one selected
                if (FileToUpload.FileName != "")
                {
                    String tempDirName;
                    String fullFilePath;
                    FileStream fs;
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: Replacing media, about to begin upload.");
                    String fileNameFull = FileToUpload.FileName;

                    String ext = Path.GetExtension(fileNameFull);

                    // Create an Upload entity object for this (so far) incompleted upload.
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: Creating database objects of upload, audio and websites.");


                    // Get the actual file contents, store them in a byte array.
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: Receiving video data.");
                    byte[] fileBytes = FileToUpload.FileBytes;

                    String fileName = Path.GetFileName(fileNameFull);
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: Media filename is '" + fileName + "'.");

                    if (AppSettings.AcceptableVideoFileFormats.Contains(ext.ToLower()))
                    {
                        // Use the same image ID since we are replacing the file
                        String VideoID = (String)Request.QueryString["vid"];

                        //This has been changed from using the "Web.config" value to the relative path of the program
                        //String fileTempDir = AppSettings.ImagesSavedFolder;
                        String fileTempDir = AppSettings.VideoConversionFolder;


                        tempDirName = fileTempDir + VideoID;
                        // Now create the temp directory.
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: Creating temporary directory '" + tempDirName + "'.", VideoID);

                        fullFilePath = tempDirName + "\\" + VideoID + ext;
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: Saving file to '" + fullFilePath + "'.", VideoID);

                        Directory.CreateDirectory(tempDirName);

                        fs = File.Create(fullFilePath);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditVideo.aspx: file saved successfully.", VideoID);

                        //set the file destination
                        string fileDestination = Server.MapPath(AppSettings.VideoConvertedFolder + "\\") + vid;
                        // Convert the file and move it to the converted videos section
                        // Create an interfacer to interact with the MMRQueue service
                        Interfacer MMRInterfacer = new Interfacer();
                        string email;
                        if (account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StudentUsers))
                        {
                            email = account.Email.Split('@')[0] + "@mail.nwmissouri.edu";
                        }
                        else
                        {
                            email = account.Email;
                        }
                
                        MMRInterfacer.ProcessVideo(fullFilePath, fileDestination, email);

                        // Finish the image object's properties.
                        //images.DatePosted = DateTime.Now;
                        //images.Size = (new FileInfo(fullFilePath)).Length;

                        
                    }
                    else
                    {
                        throw new ApplicationException("The file you choose to upload isn't a supported video type");
                    }
                }


                db.SubmitChanges();
                Response.Redirect("UploadComplete.aspx?type=video", true);
                if (wasOwner)
                    returnFromEdit(ReturnFromEditValue.Owner);
                else if (wasFacultyOwner)
                    returnFromEdit(ReturnFromEditValue.Student);
                else
                    returnFromEdit(ReturnFromEditValue.Admin);
            }
            catch (ApplicationException ex)
            {
                errorLabel.Text = ex.Message;
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
            }
        }
    }
    protected void btnClaim_Click(object sender, EventArgs e)
    {

        String vid = (String)Request.QueryString["vid"];
        UserAccount account = (UserAccount)Session["account"];

        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            // Search for the video.
            var v = (from i in db.Videos
                     where i.VideoID == vid
                     select i).Single();

            v.Username = account.Username;
            v.GroupID = null;
            db.SubmitChanges();

            lbl_ClaimMessage.Visible = true;
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;

        }
    }
}