using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using Cite.DomainAuthentication;
using System.Text;
using MMRQueueInterfacer;
public partial class myVideos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.

        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        UserAccount account = (UserAccount)Session["account"];
        try
        {
            IEnumerable<Video> videos = null;

            //Admin is visible to only admins,faculty users,staff users
            if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                ad.Visible = false;
            }

            String groupID = (String)Request.QueryString["groupID"];

            DBDataContext db = DBDataContext.CreateInstance();

            // The user didn't navigate here from a group page
            if (String.IsNullOrEmpty(groupID))
            {
                videos = db.Videos.Where(i => i.Username.ToLower() == account.Username.ToLower());
                // Top Navigation 
                groupVideos.Visible = false;
                userVideos.Visible = true;

                myMediaIcon.Visible = true;
                groupsIcon.Visible = false;
            }
            // The user navigated here from a group page: include the group id in all video URLs
            else
            {
                videos = db.Videos.Where(i => i.GroupID == int.Parse(groupID));
                videoList.DisplayVideoOwner = true;

                ////to include the group name in navigation
                var groupName = (from g in db.StudentGroups
                                 where g.GroupID == int.Parse(groupID)
                                 select g.GroupName);
                groupname.Attributes["href"] = @"GroupMedia.aspx?groupID=" + groupID;
                // Hide media selected icon, because we're on a group page
                myMediaIcon.Visible = false;
                // Show the groups page
                groupsIcon.Visible = true;
                lblgroupname.Text = groupName.FirstOrDefault().ToString();
                groupVideos.Visible = true;
                userVideos.Visible = false;
                videoList.IncludeGroupIDInURL = true;
            }


            if (videos.Count() > 0)
            {
                videoList.Visible = true;
                noVideosLabel.Visible = false;
                videoList.Videos = videos;



            }
            else
            {
                videoList.Visible = false;
                noVideosLabel.Visible = true;
            }
        }
        catch (Exception ex)
        {
        }

    }


}

