using System;
using System.Linq;
using System.Web;
using System.IO;
public partial class OpenFile : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
    {
        UserAccount account = (UserAccount)Session["account"];

        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        // Search the database for this file.
        DBDataContext db = DBDataContext.CreateInstance();

        Files fid;
        string fileId = (string)Request.QueryString["fid"];
        fid = (from a in db.Files
               where a.FileID == fileId
               select a).Single();

        // They arrived at this page through a group, so show the group breadcrumbs instead of MyMedia
        if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
        {
            int groupID = int.Parse(Request.QueryString["groupID"]);
            var groupName = (from g in db.StudentGroups
                             where g.GroupID == groupID
                             select g.GroupName);
            breadcrumbs.InnerHtml = "<a href=\"myGroups_Student.aspx\" style=\"text-decoration: none;\">My Groups&nbsp;&gt; </a><a href=\"GroupMedia.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + groupName.FirstOrDefault().ToString() + "</span></a>&nbsp>&nbsp<a href=\"MyFiles.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + "Documents" + "</span></a>&nbsp>&nbsp" + fid.Title.ToString();
            myMediaIcon.Visible = false;
            groupsIcon.Visible = true;
        }
        else
        {
            breadcrumbs.InnerHtml = "<a href=\"myMedia.aspx\" style=\"text-decoration: none;\">MyMedia</a>&nbsp>&nbsp<a href=\"MyFiles.aspx\" style=\"text-decoration: none;\"><span>" + "My Documents" + "</span></a>&nbsp>&nbsp" + fid.Title.ToString();
                myMediaIcon.Visible = true;
                groupsIcon.Visible = false;
        }
        this.Title = fid.Title + " - Northwest Website";

        //to get the format of the document
        var server = HttpContext.Current.Server;
        bool fileConverted;
        String[] fileFormats = AppSettings.AcceptableFileFormats;
        String fileFormat="";
        for (int i = 0; i < fileFormats.Length; i++)
        {
            string file = server.MapPath("documents\\") + fileId + "\\" + fileId + fileFormats[i];
            fileConverted = File.Exists(file);
            if (fileConverted)
            {
                fileFormat = fileFormats[i];
            }
        }
      
        fileLink.Attributes["href"] = (@"documents/" +fileId+ "/"+ fileId + fileFormat);
        // Signals to the browser that this file should be downloaded
        fileLink.Attributes.Add("download", "");
        lblName.Text = fid.Title;

        //update last viewed date with current date and increase number of views by 1 whenever user open the file link
        int numberOfViews = fid.Views;
        fid.LastView = DateTime.Now;
        fid.Views = numberOfViews + 1;
        db.SubmitChanges();



        // Now check to see if the user is logged in and is the owner of this file, or is an admin,
        // or owns the student group this file belongs to.


        String facultyowner = fid.GroupID.HasValue ? db.StudentGroups.Where(x => x.GroupID == fid.GroupID).Select(i => i.FacultyOwner).Single() : fid.Username;


        if (Session["account"] != null &&
            (((UserAccount)Session["account"]).Username.ToLower() == fid.Username.ToLower()                                 // The own the item
                || ((UserAccount)Session["account"]).Admin)                                                                 // They are an admin
                || (fid.GroupID.HasValue && facultyowner.ToLower() == ((UserAccount)Session["account"]).Username.ToLower()) // They are the group owner
                || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers        // They are faculty
                || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.StaffUsers          // They are staff
            )
        {
            // This user is the owner or an admin, show them the embed code panel.
            ownerPanel.Visible = true;

            //Gets the current URL to use for links and embedd code
            string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
            thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
            string documentsURL = thisURL + "documents/";

            //Only pdf files can be embed into websites; word,text,excel files cannot be embed in other websites
            if (fileFormat == ".pdf")
            {
                String code = "<iframe";
                code += " src=\"" + documentsURL + fid.FileID + "/" + fid.FileID + fileFormat + "\"";
                code += "width='560' height='315'>";
                code += "</iframe>";

                codeLabel.Text = Server.HtmlEncode(code);
            }
            //hiding dropdown list 
            else
            {
                heading.Visible = false;
                fileWidth.Visible = false;
            }
            fileLinkLabel.Text = "Direct Link: " + documentsURL + fid.FileID + "/" + fid.FileID + fileFormat;

            fileLinkLabel.Text += "<br><br>" + Server.HtmlEncode("HTML Link: <a href=\"" + documentsURL + fid.FileID + "/" + fid.FileID + fileFormat + "\" target=\"_new\">" + fid.Title + "</a>");

        }
        else
            ownerPanel.Visible = false;
    }
    protected void ddlFileSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] separators = { "*" };
        String[] size = ddlFileSize.SelectedValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        String width = size[0];
        String height = size[1];

        // Search the database for this video.
        DBDataContext db = DBDataContext.CreateInstance();

        Files fid;

        string fileId = (string)Request.QueryString["fid"];

        fid = (from a in db.Files
               where a.FileID == fileId
               select a).Single();

        var server = HttpContext.Current.Server;
        bool fileConverted;
        String[] fileFormats = AppSettings.AcceptableFileFormats;
        String fileFormat = "";
        for (int i = 0; i < fileFormats.Length; i++)
        {
            string file = server.MapPath("documents\\") + fileId + "\\" + fileId + fileFormats[i];
            fileConverted = File.Exists(file);
            if (fileConverted)
            {
                fileFormat = fileFormats[i];
            }
        }
        //Gets the current URL to use for links and embedd code
        string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
        thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
        string documentsURL = thisURL + "documents/";

        //Only pdf files can be embed into websites; word,text,excel files cannot be embed in other websites
        if (fileFormat == ".pdf")
        {
            String code = "<iframe";
            code += " src=\"" + documentsURL + fid.FileID + "/" + fid.FileID + fileFormat + "\"";
            code += "width='"+ width + "' height='" + height + "'>";
            code += "</iframe>";

            codeLabel.Text = Server.HtmlEncode(code);
        }
        else
        {
            heading.Visible = false;
            fileWidth.Visible = false;
        }
    }
}