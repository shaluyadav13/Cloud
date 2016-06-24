using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;
using System.Collections.Generic;
using System.Web;

public partial class EditFile : System.Web.UI.Page
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
                loadFileInfo();
        }
        catch (Exception ex)
        {

        }
    }
    private void loadFileInfo()
    {
        // Get the file ID from the query string.
        String fid = (String)Request.QueryString["fid"];

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
                //stuGroups = from i in db.StudentGroups
                //            where (from k in i.AuthorizedStudents
                //                   select k.Username).Contains(account.Username)
                //            && i.EndDate >= DateTime.Now.Date
                //            orderby i.GroupName
                //            select i;
            }
            else if (account.Admin || account.OU == OrganizationalUnit.StaffUsers || account.OU == OrganizationalUnit.FacultyUsers)
            {
                objmemGroups = objstuGroups.Where(x => x.AuthorizedStudents.Any(i => i.Username.ToLower() == account.Username.ToLower())).OrderBy(x => x.GroupName).ToList();
                objstuGroups = objstuGroups.Where(x => x.FacultyOwner.ToLower() == account.Username.ToLower()).OrderBy(x => x.GroupName.ToLower()).ToList();
                //stuGroups = from i in db.StudentGroups
                //            where i.FacultyOwner == account.Username
                //           // && i.EndDate >= DateTime.Now.Date
                //            orderby i.GroupName
                //            select i;
                //objstuGroups = stuGroups.AsQueryable().ToList();
               
                objstuGroups.AddRange(objmemGroups);
            }
            ApplicationLogger.LogItem(Session["account"] as UserAccount, "UploadMedia.aspx: User belongs to " + stuGroups.Count() + " student groups.");

            // Search for the files.
            var files = from i in db.Files
                         where i.FileID == fid
                         select i;

            if (files.Count() == 0) // If the audio doesn't exist, redirect.
                Response.Redirect("MyFiles.aspx", true);

            Files a = files.First();

            // Make sure the user has permission to edit this audio.
            //UserAccount account = (UserAccount)Session["account"];
            bool admin = account.Admin;
            String username = account.Username.ToLower();
            String groupowner = username;
            if (admin && a.GroupID != null)
            {
                groupowner = db.StudentGroups.Where(x => x.GroupID == a.GroupID).FirstOrDefault().FacultyOwner;
            }

            // The user can always edit their own videos. Admins may edit any videos.
            if (admin || username == a.Username.ToLower() || groupowner.ToLower() == account.Username.ToLower())
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
                        if (username == a.Username.ToLower())
                        {
                            studentGroupListBox.Items.Add(new ListItem("My profile", "admin_profile"));
                        }
                        else if (groupowner.ToLower() == account.Username.ToLower())
                        {
                            btnClaim.Visible = true;
                        }
                    }
                    foreach (var g in objstuGroups)
                    {
                        studentGroupListBox.Items.Add(new ListItem(g.GroupName, g.GroupID.ToString()));
                    }
               
                }
                else
                {
                    studentGroupPanel.Visible = false;
                }
                titleTextBox.Text = a.Title;
                descriptionTextBox.Text = a.Description;

                if (a.GroupID != null)
                {
                    studentGroupListBox.SelectedValue = a.GroupID.ToString();
                }
                else
                {
                    studentGroupListBox.SelectedValue = studentGroupListBox.Items.FindByText("My profile").Value;
                }
                if (a.AutoDeleteDate.HasValue)
                {
                    autoRemoveDateTextBox.Text = a.AutoDeleteDate.Value.ToString(@"MM/dd/yyyy");
                }

                if (!String.IsNullOrEmpty(a.Author))
                {
                    authorTextBox.Text = a.Author;
                }


            }
            else
            {
                //// Check to see if the user is editing one of their students' videos.
                //if (!(v.GroupID.HasValue && v.StudentGroup.FacultyOwner.ToLower() == account.Username.ToLower()))
                //{
                // User can't edit this video, redirect.
                Response.Redirect("MyFiles.aspx", true);
                // }
            }
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
    protected void removeFileButton_Click(object sender, EventArgs e)
    {
        // Remove the file.
        // Get the file ID from the query string.
        String fid = (String)Request.QueryString["fid"];

        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            // Search for the files.
            var v = (from i in db.Files
                     where i.FileID == fid
                     select i).Single();

            // See if the user owns this file.
            UserAccount account = (UserAccount)Session["account"];
            bool wasOwner = account.Username.ToLower() == v.Username.ToLower();
            String groupowner = v.Username;
            if (v.GroupID != null)
            {
                groupowner = db.StudentGroups.Where(x => x.GroupID == v.GroupID).FirstOrDefault().FacultyOwner;
            }
            bool wasFacultyOwner = groupowner.ToLower() == account.Username.ToLower();

            // Remove the document file and folder
            AppCleanUp.RemoveDocument(v.FileID);

            db.Files.DeleteOnSubmit(v);
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
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    private enum ReturnFromEditValue { Owner, Admin, Student };
    private void returnFromEdit(ReturnFromEditValue value)
    {
        // If this file belongs to the user, return to myAudio.
        if (value == ReturnFromEditValue.Owner)
            Response.Redirect("MyFiles.aspx", true);
        // If faculty was editing their students' videos.
        //else if (value == ReturnFromEditValue.Student)
        //    Response.Redirect("MyStudentsVideos.aspx", true);
        // Otherwise the user is an admin editing someone else's video, return to AllVideos.
        //else
        //    Response.Redirect("AllVideos.aspx", true);
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        // Get the file ID from the query string.
        String fid = (String)Request.QueryString["fid"];

        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            // Search for the file's owner.
            var v = (from i in db.Files
                     where i.FileID == fid
                     select i).Single();

            UserAccount account = (UserAccount)Session["account"];
            // See if the user owns this file, used for redirection when finished.
            bool wasOwner = account.Username.ToLower() == v.Username.ToLower();
            String groupowner = v.Username;
            if (v.GroupID != null)
            {
                groupowner = db.StudentGroups.Where(x => x.GroupID == v.GroupID).FirstOrDefault().FacultyOwner;
            }
            bool wasFacultyOwner = v.GroupID.HasValue && groupowner.ToLower() == account.Username.ToLower();

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
            // Get the file ID from the query string.
            String fid = (String)Request.QueryString["fid"];
            UserAccount account = (UserAccount)Session["account"];
            Upload upload = new Upload();
          
            try
            {
                DBDataContext db = DBDataContext.CreateInstance();

                // Search for the audio.
                var v = (from i in db.Files
                         where i.FileID == fid
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
                        throw new ApplicationException("Please select which group to post your audio to.");
                    }
                    else
                    {
                        String selectedGroup = studentGroupListBox.SelectedValue;
                        // If equal to admin_profile, the video should not go into a group. It will belong to the user.
                        if (selectedGroup != "admin_profile")
                        {
                            // Otherwise get the GroupID from the list box.
                            int groupID = int.Parse(selectedGroup);
                            // upload.GroupID = groupID;
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


                // See if the user owns this audio, used for redirection when finished.
                bool wasOwner = account.Username.ToLower() == v.Username.ToLower();
                String groupowner = v.Username;
                if (v.GroupID != null)
                {
                     groupowner = db.StudentGroups.Where(x => x.GroupID == v.GroupID).FirstOrDefault().FacultyOwner;
                }
                bool wasFacultyOwner = v.GroupID.HasValue && groupowner.ToLower() == account.Username.ToLower();

                // Update the audio.
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

                // Replace file if there is one selected
                if (FileToUpload.FileName != "")
                {

                    String tempDirName;
                    String fullFilePath;
                    FileStream fs;
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditFile.aspx: Replacing media, about to begin upload.");
                    String fileNameFull = FileToUpload.FileName;

                    String ext = Path.GetExtension(fileNameFull);

                    // Create an interfacer to interact with the MMRQueue service
                    //Interfacer MMRInterfacer = new Interfacer();
                    //DBDataContext db = DBDataContext.CreateInstance();
                    // Create an Upload entity object for this (so far) incompleted upload.

                    // Get the actual file contents, store them in a byte array.
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditFile.aspx: Receiving file data.");
                    byte[] fileBytes = FileToUpload.FileBytes;

                    String fileName = Path.GetFileName(fileNameFull);
                    ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditFile.aspx: Media filename is '" + fileName + "'.");

                    if (AppSettings.AcceptableFileFormats.Contains(ext.ToLower()))
                    {
                        // Use the same ID since we are replacing the file
                        String FileID = (String)Request.QueryString["fid"];

                        // Get the file extension of the current file
                        var server = HttpContext.Current.Server;
                        String[] fileFormats = AppSettings.AcceptableFileFormats;
                        String fileFormat = "";
                        for (int i = 0; i < fileFormats.Length; i++)
                        {
                            string file = server.MapPath("documents\\") + FileID + "\\" + FileID + fileFormats[i];
                            if (File.Exists(file))
                            {
                                fileFormat = fileFormats[i];
                            }
                        }
                        // If the selected file is not the same type as the old one, throw an error
                        if(fileFormat != ext)
                        {
                            throw new ApplicationException("The selected file is of a different type than the original. They original file is a \"" + fileFormat + "\" type and the selected file is a \"" + ext + " type. Please select a file of the same type.");
                        }
                        //This has been changed from using the "Web.config" value to the relative path of the program
                        //String fileTempDir = AppSettings.ImagesSavedFolder;
                        String fileTempDir = Server.MapPath(AppSettings.DocumentSavedFolder);


                        tempDirName = fileTempDir + "/" + FileID;
                        // Now create the temp directory.
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditFile.aspx: Creating temporary directory '" + tempDirName + "'.", FileID);

                        // Create a file in this directory containing the uploaded content.

                        fullFilePath = tempDirName + "/" + FileID + ext;
                        ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditFile.aspx: Saving file to '" + fullFilePath + "'.", FileID);

                        if (Directory.Exists(tempDirName))
                        {
                            // Remove previous version of file
                            String[] oldFiles = Directory.GetFiles(tempDirName);
                            foreach (String oldFile in oldFiles)
                            {
                                File.Delete(oldFile);
                            }

                            fs = File.Create(fullFilePath);
                            fs.Write(fileBytes, 0, fileBytes.Length);
                            fs.Close();
                            ApplicationLogger.LogItem(Session["account"] as UserAccount, "EditFile.aspx: file saved successfully.", FileID);
                        }
                        // Finish the image object's properties.
                        //images.DatePosted = DateTime.Now;
                        //images.Size = (new FileInfo(fullFilePath)).Length;

                        
                    }
                    else
                    {
                        throw new ApplicationException("The file you choose to upload isn't a supported document type");
                    }
                }
                db.SubmitChanges();
                Response.Redirect("UploadComplete.aspx?type=files", true);
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

        String fid = (String)Request.QueryString["fid"];
        UserAccount account = (UserAccount)Session["account"];

        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            // Search for the file.
            var v = (from i in db.Files
                     where i.FileID == fid
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