using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoTransfer.Common;

public partial class StudentGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        UserAccount account = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }

      

        // Make sure the user is an admin or faculty.
        if (account.Admin && account.OU.Equals("FacultyUsers"))
        {
            Response.Redirect("~/myMedia.aspx", true);
        }

        errorLabel.Text = "";
        if (!IsPostBack)
            fillGroupsListBox();
    }

    private void fillGroupsListBox()
    {
        try
        {
            studentGroupsListBox.Items.Clear();

            DBDataContext db = DBDataContext.CreateInstance();
            IEnumerable<StudentGroup> groups;
      

            UserAccount account = (UserAccount)Session["account"];
            if (showAllCheckBox.Checked)
            {

                // This condition shows all the groups of the faculty or the staff users only.
                if (account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) || account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
                {


                    groups = from i in db.StudentGroups
                             where i.FacultyOwner == account.Username
                             orderby i.GroupName
                             select i;

                }
                // dispalys all the groups if its an admin
                else
                {
                    groups = from i in db.StudentGroups
                             orderby i.GroupName
                             select i;
                }
            }
            else
            {
                // UserAccount account = (UserAccount)Session["account"];
                //allow faculty/staff to view and create student groups
                if (account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) || account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
                {


                    groups = from i in db.StudentGroups
                             where i.FacultyOwner == account.Username // && i.EndDate >= DateTime.Now
                             orderby i.GroupName
                             select i;

                }
                else
                {
                    groups = from i in db.StudentGroups   orderby i.GroupName  select i;
                }
               
            }

            foreach (var g in  groups.ToList())
            {
                String display = String.Format("{0} ({1} - {2}, {3} members)",
                                               g.GroupName,
                                               g.StartDate.ToShortDateString(),
                                               g.EndDate.ToShortDateString(),
                                               g.AuthorizedStudents.Count);
                studentGroupsListBox.Items.Add(new ListItem(display, g.GroupID.ToString()));
            }

            if (groups.Count() > 0)
            {
                studentGroupsListBox.Visible = true;
                noGroupsLabel.Visible = false;
            }
            else
            {
                studentGroupsListBox.Visible = false;
                noGroupsLabel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
    protected void editGroupButton_Click(object sender, EventArgs e)
    {
        if (studentGroupsListBox.SelectedIndex >= 0)
        {
            Response.Redirect("EditStudentGroup.aspx?id=" + studentGroupsListBox.SelectedValue);
        }
    }

    protected void newGroupButton_Click(object sender, EventArgs e)
    {
        try
        {
            DBDataContext db = DBDataContext.CreateInstance();
            int maxId = 0;
            int groupCount = 0;
            StudentGroup g = new StudentGroup();
            g.GroupName = "New Group";
            g.Description = "New Group created on " + DateTime.Now.ToShortDateString();
            g.StartDate = DateTime.Now.Date;
            g.EndDate = DateTime.Now.Date.AddDays(120);
            g.FacultyOwner = ((UserAccount)Session["account"]).Username;
            groupCount = (from st in db.StudentGroups
                         select st).Count();
            if (groupCount > 0)
            {
                maxId = (from s in db.StudentGroups select s.GroupID).Max();
            }
            maxId++;
            db.StudentGroups.InsertOnSubmit(g);
            db.SubmitChanges();
            Response.Redirect("EditStudentGroup.aspx?id=" + g.GroupID, false);
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
    protected void deleteGroupButton_Click(object sender, EventArgs e)
    {
        if (studentGroupsListBox.SelectedIndex >= 0)
        {
            try
            {
                int id;
                if (int.TryParse(studentGroupsListBox.SelectedValue, out id))
                {
                    DBDataContext db = DBDataContext.CreateInstance();
                    var groupToDelete = (from i in db.StudentGroups
                                         where i.GroupID == id
                                         select i).Single();


                    // Get all videos for the group
                    var VideosToDelete = from i in db.Videos
                                         where i.GroupID == id
                                         select i;
                    // Delete all videos for the group
                    foreach (var video in VideosToDelete)
                    {
                        AppCleanUp.RemoveVideo(video.VideoID.ToString());
                    }
                    // Remove entries from database on submit
                    db.Videos.DeleteAllOnSubmit(VideosToDelete);


                    // Get all audio files for the group
                    var AudiosToDelete = from i in db.Audios
                                         where i.GroupID == id
                                         select i;
                    // Delete all audios for the group
                    foreach (var audio in AudiosToDelete)
                    {
                        AppCleanUp.RemoveAudio(audio.AudioID.ToString());
                    }
                    // Remove entries from database on submit
                    db.Audios.DeleteAllOnSubmit(AudiosToDelete);


                    // Get all websites for the group
                    var WebsitesToDelete = from i in db.Websites
                                         where i.GroupID == id
                                         select i;
                    // Delete all websites for the group
                    foreach (var website in WebsitesToDelete)
                    {
                        AppCleanUp.RemoveWebsite(website.WebID.ToString(), website.Title);
                    }
                    db.Websites.DeleteAllOnSubmit(WebsitesToDelete);


                    // Get all images for the group
                    var ImagesToDelete = from i in db.Images
                                           where i.GroupID == id
                                           select i;
                    // Delete all images for the group
                    foreach (var image in ImagesToDelete)
                    {
                        AppCleanUp.RemoveImage(image.ImageID);
                    }
                    db.Images.DeleteAllOnSubmit(ImagesToDelete);


                    // Get all documents for the group
                    var DocumentsToDelete = from i in db.Files
                                           where i.GroupID == id
                                           select i;
                    // Delete all documents for the group
                    foreach (var document in DocumentsToDelete)
                    {
                        AppCleanUp.RemoveDocument(document.FileID);
                    }
                    db.Files.DeleteAllOnSubmit(DocumentsToDelete);


                    //--------Commented out by Lawrence Foley on 03/13/2015, needs to be replaced to delete videos from file system, not flash media server
                    //--------This also needs to delete all media types and not just videos

                    //foreach (var vid in VideostoDelete)
                    //{
                    //    FileTransfer t = new FileTransfer();
                    //    t.Connect();
                    //    FileResponse response = t.SendDeleteRequest(vid.VideoID);
                    //    if (response.ResponseType == ResponseType.Successful)
                    //    {
                    //        // Delete the video if the flash server successfully deleted it.
                    //        db.Videos.DeleteOnSubmit(vid);
                    //        db.SubmitChanges();
                    //        ApplicationLogger.LogItem(null, "Video deleted", vid.VideoID);

                    //    }
                    //}



                    db.StudentGroups.DeleteOnSubmit(groupToDelete);
                    db.SubmitChanges();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('The Group is successfully deleted !');", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    fillGroupsListBox();
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
            }
        }
    }
    protected void showAllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        fillGroupsListBox();
    }


}