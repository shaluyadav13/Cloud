using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpenWebsite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        UserAccount account = (UserAccount)Session["account"];

        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }

        // Search the database for this website.
        DBDataContext db = DBDataContext.CreateInstance();

        Websites wid;

        string webid = (string)Request.QueryString["wid"];

        wid = (from a in db.Websites
               where a.WebID == webid
               select a).Single();


        // They arrived at this page through a group, so show the group breadcrumbs instead of MyMedia
        if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
        {
            int groupID = int.Parse(Request.QueryString["groupID"]);
            var groupName = (from g in db.StudentGroups
                             where g.GroupID == groupID
                             select g.GroupName);
            breadcrumbs.InnerHtml = "<a href=\"myGroups_Student.aspx\" style=\"text-decoration: none;\">My Groups&nbsp;&gt; </a><a href=\"GroupMedia.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + groupName.FirstOrDefault().ToString() + "</span></a>&nbsp>&nbsp<a href=\"MyWebpages.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + "WebPages" + "</span></a>&nbsp>&nbsp" + wid.Title.ToString();
            myMediaIcon.Visible = false;
            groupsIcon.Visible = true;


        }
        else
        {
            breadcrumbs.InnerHtml = "<a href=\"myMedia.aspx\" style=\"text-decoration: none;\">MyMedia&nbsp;&gt; </a><a href=\"MyWebpages.aspx\" style=\"text-decoration: none;\"><span>" + "My Webpages" + "</span></a>&nbsp>&nbsp" + wid.Title.ToString();
            myMediaIcon.Visible = true;
            groupsIcon.Visible = false;
        }

        this.Title = wid.Title + " - Northwest Website";

        //set the href and target of the a tag
        //webLink.Attributes["href"] = ("http://cite.nwmissouri.edu/" + wid.Title + "_" + wid.WebID);

        webLink.Attributes["href"] = (AppSettings.websitesBaseURL + wid.WebID);

        
        webLink.Attributes["target"]=("_blank");
        lblName.Text = wid.Title;

        //update last viewed date with current date and increase number of views by 1 whenever user open the video link
        int numberOfViews = wid.Views;
        wid.LastView = DateTime.Now;
        wid.Views = numberOfViews + 1;
        db.SubmitChanges();


        String facultyowner = wid.GroupID.HasValue ? db.StudentGroups.Where(x => x.GroupID == wid.GroupID).Select(i => i.FacultyOwner).Single() : wid.Username;
        // Now check to see if the user is logged in and is the owner of this website, or is an admin,
        // or owns the student group this video belongs to.
        if (Session["account"] != null &&
                (((UserAccount)Session["account"]).Username.ToLower() == wid.Username.ToLower()                             // They own the item
                || ((UserAccount)Session["account"]).Admin)                                                                 // They are an admin
                || (wid.GroupID.HasValue && facultyowner.ToLower() == ((UserAccount)Session["account"]).Username.ToLower()) // They are the group owner
                || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers        // They are faculty
                || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.StaffUsers          // They are staff
            )
        {
            // This user is the owner or an admin, show them the embed code panel.
            ownerPanel.Visible = true;

            //Gets the current URL to use for links and embedd code
            string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
            thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
            string webURL = thisURL + "convertedWeb/";

            if (ddlWebSize.SelectedValue == "Default")
            {
                String code = "<iframe";
                code += " src=\"" + AppSettings.websitesBaseURL + wid.WebID + "/" + "\"";
                code+=" width='100%' height='100%'>";
                code += "</iframe>";

                codeLabel.Text = Server.HtmlEncode(code);
            }
            webLinkLabel.Text = "Direct Link: " + AppSettings.websitesBaseURL + wid.WebID;

            webLinkLabel.Text += "<br><br>" + Server.HtmlEncode("HTML Link: <a href=\"" + AppSettings.websitesBaseURL + wid.WebID + "\" target=\"_new\">" + wid.Title + "</a>");
           

        }
        else
            ownerPanel.Visible = false;
    }
    protected void ddlWebSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBDataContext db = DBDataContext.CreateInstance();
        string[] separators = { "*" };
        string webid = (string)Request.QueryString["wid"];
        Websites wid;
        wid = (from a in db.Websites
               where a.WebID == webid
               select a).Single();
        
        //Gets the current URL to use for links and embedd code
        string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
        thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
        string webURL = thisURL + "convertedWeb/";

        if (ddlWebSize.SelectedValue != "Default")
        {
            String[] size = ddlWebSize.SelectedValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            String width = size[0];
            String height = size[1];
            String code = "<iframe";
            code += " src=\"" + AppSettings.websitesBaseURL + wid.WebID + "\"";
            code += " width=" + "\"" + width + "\"";
            code += " height=" + "\"" + height + "\"";
            code += " >";
            code += "</iframe>";

            codeLabel.Text = Server.HtmlEncode(code);
        }


    }
}