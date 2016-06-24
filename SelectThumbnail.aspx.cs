using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MMRQueueInterfacer;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Cite.DomainAuthentication;

public partial class pages_SelectThumbnail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);
        lblerror.Visible = false;
        // Also redirect if the session variables required for this page don't exist.
        if (Session["upload"] == null)
            Response.Redirect("MyVideos.aspx", true);

        UserAccount account = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }

        if (!Page.IsPostBack)
        {

            // Load the upload object for this video.
            Upload upload = (Upload)Session["upload"];

            // Load all of the still images and put them in the page with a radio button for each one.

            // The first radio button will be a generic icon.
            String imageTag = "<img src='Images/N.png' width='133' height='100' />";
            String location = "\\Images\\N.png";
            imageRadioButtonList.Items.Add(new ListItem(imageTag, location));

            String directory = Server.MapPath("TempImages");
            //String directory = "C:\\NWCloud\\TempImages\\";
            String[] imageLocations = Directory.GetFiles(directory, "*.png");

            String videoID = upload.VideoID;

            foreach (String s in imageLocations)
            {
                // Only add this if the image is actually for this video, meaning it will contain
                // the video's hashed name. Other videos may be in conversion at the moment,
                // meaning unrelated images may be here.
                if (s.Contains(videoID))
                {
                    imageTag =
                        "<img src='TempImages" + "/" +
                        Path.GetFileName(s) + "' width='125' height='125' />";
                    imageRadioButtonList.Items.Add(new ListItem(imageTag, Path.GetFileName(s)));
                }
            }
        }

    }
    protected void finishButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (imageRadioButtonList.SelectedIndex != -1)
            {
                UserAccount account = (UserAccount)Session["account"];
               
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "SelectThumbnail.aspx: User clicked finish button,");
                // Load the upload object for this video.
                DBDataContext db = DBDataContext.CreateInstance();

                // Using the raw upload object from the Session state sometimes causes errors later on. So
                // just get the VideoID and look the upload up again.
                ApplicationLogger.LogItem(Session["account"] as UserAccount, "SelectThumbnail.aspx: Searching database for Upload object for this video.");
                Upload objUpload = ((Upload)Session["upload"]);
                Upload upload = db.Uploads.Where(x => x.VideoID == objUpload.VideoID).Single();
                //(from i in db.Uploads   where i.VideoID == ((Upload)Session["upload"]).VideoID select i).Single();

                String fileName = upload.RawFileName;
                String image;
                if (imageRadioButtonList.SelectedValue == "\\Images\\N.png")
                {
                    image = Server.MapPath("images\\N.png");
                    File.Copy(image, Server.MapPath("Thumbnails\\") + upload.VideoID + ".png");
                    image = Server.MapPath("Thumbnails\\") + upload.VideoID + ".png";
                }
                else
                {
                    image = Server.MapPath("TempImages\\") + imageRadioButtonList.SelectedValue;

                    File.Move(image, Server.MapPath("Thumbnails\\") + imageRadioButtonList.SelectedValue.Substring(0,
                        imageRadioButtonList.SelectedValue.IndexOf('-')) + ".png");

                    image = Server.MapPath("Thumbnails\\") + imageRadioButtonList.SelectedValue.Substring(0,
                        imageRadioButtonList.SelectedValue.IndexOf('-')) + ".png";
                }

                //after a thumnail has been selected, remove the unselected images
                String directory = Server.MapPath("TempImages");
                String[] imageLocations = Directory.GetFiles(directory, "*.png");
                String videoID = upload.VideoID;
                foreach (String s in imageLocations)
                {
                    // Make sure we only delete the thumbnails for this video and not other user's thumbnails
                    if (s.Contains(videoID))
                    {
                        FileInfo thumbnailToDelete = new FileInfo(s);
                        thumbnailToDelete.Delete();
                    }
                }

                ApplicationLogger.LogItem(Session["account"] as UserAccount, "SelectThumbnail.aspx: Creating Video object.");
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                Video vid = new Video();
                vid.VideoID = upload.VideoID;
                vid.Views = 0;
                vid.Title = upload.Title;
                vid.Description = upload.Description;
                vid.Username = upload.Username;
                vid.UploadedBy = upload.Username;
                vid.DatePosted = DateTime.Now;



                // Read in the thumbnail image and post it within the database record.
                // byte[] thumbnail = File.ReadAllBytes(image);
                // vid.ThumbnailBytes = thumbnail;

                //store the path to the image instead (in bytes for now cant figure out how to reflect changes
                //in db to DB.desginer.cs)
                vid.ThumbnailBytes = encoding.GetBytes(image);


                if (upload.AutoDeleteDate.HasValue)
                {
                    vid.AutoDeleteDate = upload.AutoDeleteDate;
                }
                if (upload.GroupID.HasValue)
                {
                    vid.GroupID = upload.GroupID;
                }

                if (!String.IsNullOrEmpty(upload.Author))
                {
                    vid.Author = upload.Author;
                }
                //groupid null --video comes under no group(not sharing with others)
                if (upload.GroupID == null)
                {
                    vid.ShowStatus = false;
                }
                else
                {
                    vid.ShowStatus = true;
                }
                //vid.Copyright = upload.Copyright;
                vid.Transcript = upload.Transcript;

                //get the full file path
                string filePath = (string)Session["FullFilePath"];

                //set the file destination
                string fileDestination = Server.MapPath(AppSettings.VideoConvertedFolder + "\\") + vid.VideoID;

                //create an interfacer to interact with the MMRQueue service
                Interfacer MMRInterfacer = new Interfacer();

                //convert the file and move it to the converted videos section
                string email;
                if(account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StudentUsers))
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
                MMRInterfacer.ProcessVideo(filePath, fileDestination, email);

                ApplicationLogger.LogItem(Session["account"] as UserAccount, "SelectThumbnail.aspx: Deleting Upload object from database.");
                db.Uploads.DeleteOnSubmit(upload);
                db.SubmitChanges();

                db.Videos.InsertOnSubmit(vid);
                db.SubmitChanges();




                // Clear the session variables that are no longer required.
                Session["upload"] = null;
                Response.Redirect("UploadComplete.aspx", true);
            }
            else
            {
                lblerror.Text = "Please select a thumbnail.";
                lblerror.Visible = true;
            }

        }
        catch (Exception ex)
        {

        }
    }

}
