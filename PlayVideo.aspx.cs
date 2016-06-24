using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Reflection;

public partial class PlayVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccount account = (UserAccount)Session["account"];

            //Admin is visible to only admins,faculty users,staff users
            if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                ad.Visible = false;
            }


            //sNumber.InnerHtml = account.Username;

            // Search the database for this video.
            DBDataContext db = DBDataContext.CreateInstance();

            Video vid;

            string videoID = (string)Request.QueryString["vid"];
            vid = (from v in db.Videos
                   where v.VideoID == videoID
                   select v).Single();
            // They arrived at this page through a group, so show the group breadcrumbs instead of MyMedia
            if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
            {
                int groupID = int.Parse(Request.QueryString["groupID"]);
                var groupName = (from g in db.StudentGroups
                                 where g.GroupID == groupID
                                 select g.GroupName);
                breadcrumbs.InnerHtml = "<a href=\"myGroups_Student.aspx\" style=\"text-decoration: none;\">My Groups&nbsp;&gt; </a><a href=\"GroupMedia.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + groupName.FirstOrDefault().ToString() + "</span></a>&nbsp>&nbsp<a href=\"MyVideos.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + "Videos" + "</span></a>&nbsp>&nbsp" + vid.Title.ToString();
                myMediaIcon.Visible = false;
                groupsIcon.Visible = true;
            }
            else
            {
                myMediaIcon.Visible = true;
                groupsIcon.Visible = false;
            }



            if (videoID.Equals(null))
            {
                lblVideoName.Visible = false;
            }

            this.Title = vid.Title + " - Northwest Cloud";
            lblVideoName.Text = vid.Title;
            //set the source and poster of the video tag
            source1.Attributes["src"] = ((AppSettings.VideoConvertedFolder + "\\") + videoID + ".mp4");
            videoPlayer.Attributes["poster"] = ((AppSettings.ThumbnailFolder + "\\") + videoID + ".png");

            //Display an error message if the file hasn't been converted yet
            FileInfo videoFile = new FileInfo(Server.MapPath(AppSettings.VideoConvertedFolder + "\\") + "\\" + videoID + ".mp4");
            if (!File.Exists(videoFile.FullName))
            {
                videoPlayer.Visible = false;
                videoErrorContainer.Visible = true;
                videoErrorThumbnail.Visible = true;
                videoErrorThumbnail.Attributes["src"] = ((AppSettings.ThumbnailFolder + "\\") + videoID + ".png");
            }
            else
            {
                videoPlayer.Visible = true;
                videoErrorContainer.Visible = false;
                videoErrorThumbnail.Visible = false;
            }
          
            //Link to the transcript file
            if (vid.Transcript)
            {
                transcriptLink.Attributes["href"] = (@"Transcripts/" + vid.VideoID + ".txt" );
                transcriptLink.Attributes["target"] = "_new";

            }
            else
            {
                lblName.Visible = false;
                transcript.Visible = false;
            }
            //update last viewed date with current date and increase number of views by 1 whenever user open the video link
            int numberOfViews = vid.Views;
            vid.LastView = DateTime.Now;
            vid.Views = numberOfViews + 1;
            db.SubmitChanges();

            
            String facultyowner = vid.GroupID.HasValue ? db.StudentGroups.Where(x => x.GroupID == vid.GroupID).Select(i => i.FacultyOwner).Single() : vid.Username;
            // Now check to see if the user is logged in and is the owner of this video, or is an admin,
            // or owns the student group this video belongs to.
            if (Session["account"] != null &&
                (((UserAccount)Session["account"]).Username.ToLower() == vid.Username.ToLower()                                     // They own the item
                    || ((UserAccount)Session["account"]).Admin)                                                                     // They are an admin
                    || (vid.GroupID.HasValue && facultyowner.ToLower() == ((UserAccount)Session["account"]).Username.ToLower())     // They are the group owner
                    || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers            // They are faculty
                    || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.StaffUsers              // They are staff
                )
            {
                // This user is the owner or an admin, show them the embed code panel.
                ownerPanel.Visible = true;

                //----------------old code that 
                //String code = "<script language=javascript>";
                //code += "window.location.href =";
                //code += "\"http://" + ConfigurationManager.AppSettings.Get("webPath");
                //code += "\"http://cite1.nwmissouri.edu/NWCloudTest/";
                //code += "PlayVid.aspx?vid=" + vid.VideoID;
                //code += "\"</script>";

                //Gets the current URL to use for links and embedd code
                string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
                thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
                string videosURL = thisURL + (AppSettings.VideoConvertedFolder + "\\");

                if (ddlVideoSize.SelectedValue == "560*315")
                {
                    String code = "<iframe src=\"" + thisURL + "playVid.aspx?vid=" + vid.VideoID + "&width=545&height=280\" width=\"560\" height=\"320\" frameborder=\"0\" allowfullscreen></iframe>"; 
                    codeLabel.Text = Server.HtmlEncode(code);
                }

                videoLinkLabel.Text = "Direct Link: " + thisURL + "PlayVid.aspx?vid=" + vid.VideoID;

                //videoLinkLabel.Text   += "<br/><br/>"+Server.HtmlEncode(String.Format("HTML Link: <a href=\"http://cite.nwmissouri.edu/NWCloud/PlayVid.aspx?vid={0}\" target=\"_new\">{1}</a>",
                //                                        vid.VideoID,
                //                                        vid.Title));

               videoLinkLabel.Text += "<br><br>" + Server.HtmlEncode("HTML Link: <a href=\"" + thisURL + "PlayVid.aspx?vid=" + vid.VideoID + "\" target=\"_new\">" + vid.Title + "</a>");
                
                if (vid.Transcript)
                {
                    //lblTranscript.Text = Server.HtmlEncode(String.Format("Transcript Link: <a href=\"http://cite.nwmissouri.edu/NWCloud/Transcripts/{0}\" target=\"_new\">{1}</a>",
                    //                                   Session["transcriptFile"],
                    //                                   vid.Title+" Transcript"));

                    lblTranscript.Text = Server.HtmlEncode("Transcript Link: <a href=\"" + thisURL + "/Transcripts/" + vid.VideoID + ".txt" + " target=\"_new\">" + vid.Title + "</a>");
                }

            }
            else
                ownerPanel.Visible = false;
        }
        catch(Exception ex)
        {

        }
    }

    protected void ddlVideoSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] separators = { "*" };
        String[] size = ddlVideoSize.SelectedValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        String width = size[0];
        String height = size[1];

        DBDataContext db = DBDataContext.CreateInstance();

        Video vid;

        string videoID = (string)Request.QueryString["vid"];
        if (videoID.Equals(null))
        {
            welcomeNote.Visible = false;
        }
        vid = (from v in db.Videos
               where v.VideoID == videoID
               select v).Single();

        //Gets the current URL to use for links and embedd code
        string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
        thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
        string videosURL = thisURL + (AppSettings.VideoConvertedFolder + "\\");

        // The video size is a certain amount less than the iframe size becuase the html video player has a border
        String code = "<iframe src=\"" + thisURL + "playVid.aspx?vid=" + vid.VideoID + "&width=" + (int.Parse(width) - 15).ToString() + "&height=" + (int.Parse(height) - 40).ToString() + "\" width=\"" + width + "\" height=\"" + height + "\" frameborder=\"0\" allowfullscreen></iframe>";
        codeLabel.Text = Server.HtmlEncode(code);

        codeLabel.Text = Server.HtmlEncode(code);

    }
}