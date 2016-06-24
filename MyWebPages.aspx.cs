using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyWebPages : System.Web.UI.Page
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


        try
        {
            IEnumerable<Websites> websites = null;
            String groupID = (String)Request.QueryString["groupID"];

            DBDataContext db = DBDataContext.CreateInstance();


            //gets the websites of logged in user
            if (String.IsNullOrEmpty(groupID))
            {
                websites = db.Websites.Where(i => i.Username.ToLower() == account.Username.ToLower());

                // Top Navigation 
                groupWebsites.Visible = false;
                userWebsites.Visible = true;

                myMediaIcon.Visible = true;
                groupsIcon.Visible = false;
            }
            //gets the websites of a group
            else
            {
                websites = db.Websites.Where(i => i.GroupID == int.Parse(groupID));
                websiteList.DisplayAudioOwner = true;
                websiteList.IncludeGroupIDInURL = true;
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
                groupWebsites.Visible = true;
                userWebsites.Visible = false;
            }
            if (websites.Count() > 0)
            {
                websiteList.Visible = true;
                noWebLabel.Visible = false;
                websiteList.Websites = websites;
            }
            else
            {
                websiteList.Visible = false;
                noWebLabel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            welcomeNote.Text = ex.Message;
        }
    }
}