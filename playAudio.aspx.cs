using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class playAudio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccount account = (UserAccount)Session["account"];
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);
        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        // Search the database for this audio.
        DBDataContext db = DBDataContext.CreateInstance();

        Audio aid;

        string audioId = (string)Request.QueryString["aid"];
        if (audioId.Equals(null))
        {
            lblAudioName.Visible = false;
        }
        aid = (from a in db.Audios
               where a.AudioID == audioId
               select a).Single();

        // They arrived at this page through a group, so show the group breadcrumbs instead of MyMedia
        if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
        {
            int groupID = int.Parse(Request.QueryString["groupID"]);
            var groupName = (from g in db.StudentGroups
                             where g.GroupID == groupID
                             select g.GroupName);
            breadcrumbs.InnerHtml = "<a href=\"myGroups_Student.aspx\" style=\"text-decoration: none;\">My Groups&nbsp;&gt; </a><a href=\"GroupMedia.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + groupName.FirstOrDefault().ToString() + "</span></a>&nbsp>&nbsp<a href=\"myAudio.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + "Audios" + "</span></a>&nbsp>&nbsp" + aid.Title.ToString();
            myMediaIcon.Visible = false;
            groupsIcon.Visible = true;
            
            
        }
        else
        {
            myMediaIcon.Visible = true;
            groupsIcon.Visible = false;
        }



        this.Title = aid.Title + " - Northwest Audio";
        lblAudioName.Text = aid.Title;
        //set the source and poster of the audio tag
        audioTag.Attributes["src"] = (@"convertedAudios/" + audioId + ".mp3");
        if (aid.Transcript)
        {
            transcriptLink.Attributes["href"] = (@"Transcripts/" + aid.AudioID + ".txt");
            transcriptLink.Attributes["target"] = "_new";
        }
        else
        {
            lblName.Visible = false;
            transcript.Visible = false;
        }

        //Display an error message if the file hasn't been converted yet
        FileInfo audioFile = new FileInfo(Server.MapPath(AppSettings.AudioConvertedFolder + "\\") + audioId + ".mp3");
        if (!File.Exists(audioFile.FullName))
        {
            audioTag.Visible = false;
            audioErrorContainer.Visible = true;
        }
        else
        {
            audioTag.Visible = true;
            audioErrorContainer.Visible = false;
        }

        //update last viewed date with current date and increase number of views by 1 whenever user open the audio link
        int numberOfViews =aid.NumOfHits;
        aid.LastHit = DateTime.Now;
        aid.NumOfHits = numberOfViews + 1;
        db.SubmitChanges();

        String facultyowner = aid.GroupID.HasValue ? db.StudentGroups.Where(x => x.GroupID == aid.GroupID).Select(i => i.FacultyOwner).Single() : aid.Username;

        // Now check to see if the user is logged in and is the owner of this audio, or is an admin,
        // or owns the student group this audio belongs to.
        if (Session["account"] != null &&
            (((UserAccount)Session["account"]).Username.ToLower() == aid.Username.ToLower()                                 // They own the item
                || ((UserAccount)Session["account"]).Admin)                                                                 // They are an admin
                || (aid.GroupID.HasValue && facultyowner.ToLower() == ((UserAccount)Session["account"]).Username.ToLower()) // They are the group owner
                || ((UserAccount)Session["account"]).OU ==  Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers       // They are faculty
                || ((UserAccount)Session["account"]).OU ==  Cite.DomainAuthentication.OrganizationalUnit.StaffUsers         // They are staff
            )
        {
            // This user is the owner or an admin, show them the embed code panel.
            ownerPanel.Visible = true;

            //Gets the current URL to use for links and embedd code
            string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
            thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
            string audioURL = thisURL + "convertedAudios/";

            String code = "<iframe src=\"" + thisURL + "playAid.aspx?aid=" + aid.AudioID + "\" width=\"570\" height=\"115\" frameborder=\"0\"></iframe>";

            codeLabel.Text = Server.HtmlEncode(code);


            audioLinkLabel.Text = "Direct Link: " + thisURL + "playAid.aspx?aid=" + aid.AudioID;

            audioLinkLabel.Text += "<br><br>" + Server.HtmlEncode("HTML Link: <a href=\"" + thisURL + "playAid.aspx?aid=" + aid.AudioID + ">" + aid.Title + "</a>");
 
            if (aid.Transcript)
            {
                //Old - this is still here becaues there have been issues with transcripts
                //lblTranscript.Text = Server.HtmlEncode(String.Format("Transcript Link: <a href=\"http://cite.nwmissouri.edu/NWCloud/Transcripts/{0}\" target=\"_new\">{1}</a>",
                //                                   Session["transcriptFile"],
                //                                   aid.Title + " Transcript"));

                lblTranscript.Text = Server.HtmlEncode("Transcript Link: <a href=\"" + thisURL + "Transcripts/" + aid.AudioID + ".txt" + ">" + aid.Title + "</a>");
            }

        }
        else
            ownerPanel.Visible = false;
    }
}