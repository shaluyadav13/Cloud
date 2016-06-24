using System;
using System.Linq;
using System.Web;
using System.IO;
//using System.

public partial class OpenImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.

        if (Session["account"] == null)
        {
            Response.Redirect("~/Login.aspx", true);
        }

        UserAccount account = (UserAccount)Session["account"];

        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        // Search the database for this image.
        DBDataContext db = DBDataContext.CreateInstance();

        Images image;

        string imageid = (string)Request.QueryString["imageid"];
        image = (from a in db.Images
                  where a.ImageID == imageid
               select a).Single();

        // They arrived at this page through a group, so show the group breadcrumbs instead of MyMedia
        if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
        {
            int groupID = int.Parse(Request.QueryString["groupID"]);
            var groupName = (from g in db.StudentGroups
                             where g.GroupID == groupID
                             select g.GroupName);
            breadcrumbs.InnerHtml = "<a href=\"myGroups_Student.aspx\" style=\"text-decoration: none;\">My Groups&nbsp;&gt; </a><a href=\"GroupMedia.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + groupName.FirstOrDefault().ToString() + "</span></a>&nbsp>&nbsp<a href=\"MyImages.aspx?groupID=" + groupID.ToString() + "\" style=\"text-decoration: none;\"><span>" + "Images" + "</span></a>&nbsp>&nbsp" + image.Title.ToString();
            myMediaIcon.Visible = false;
            groupsIcon.Visible = true;
        }
        else
        {
            breadcrumbs.InnerHtml = "<a href=\"myMedia.aspx\" style=\"text-decoration: none;\">MyMedia</a></a>&nbsp>&nbsp<a href=\"MyImages.aspx\" style=\"text-decoration: none;\"><span>" + "My Images" + "</span></a>&nbsp>&nbsp" + image.Title.ToString();
            myMediaIcon.Visible = true;
            groupsIcon.Visible = false;
        }

        this.Title = image.Title + " - Northwest Website";
        //to get the format of the image
        var server = HttpContext.Current.Server;
        bool fileConverted;
        String[] fileFormats = AppSettings.AcceptableImageFormats;
        String fileFormat = "";
        for (int i = 0; i < fileFormats.Length; i++)
        {
            string file = server.MapPath("userimages\\") + imageid + "\\" + imageid + fileFormats[i];
            fileConverted = File.Exists(file);
            if (fileConverted)
            {
                fileFormat = fileFormats[i];
            }
        }

        //Gets the current URL to use for links and embedd code
        string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
        thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
        string userimagesURL = thisURL + "userimages/";

        imageLink.Attributes["src"] = (@"userimages/" + imageid + "/" + imageid + fileFormat);
        lblName.Text = image.Title + "<br>";
        fullSizeImageLink.Attributes["href"] = userimagesURL + image.ImageID + "/" + image.ImageID + fileFormat;

        //update last viewed date with current date and increase number of views by 1 whenever user open the image link
        int numberOfViews = image.Views;
        image.LastView = DateTime.Now;
        image.Views = numberOfViews + 1;
        db.SubmitChanges();

        String facultyowner = image.GroupID.HasValue ? db.StudentGroups.Where(x => x.GroupID == image.GroupID).Select(i => i.FacultyOwner).Single() : image.Username;

        // Now check to see if the user is logged in and is the owner of this image, or is an admin,
        // or owns the student group this image belongs to.
        if (Session["account"] != null &&
                (((UserAccount)Session["account"]).Username.ToLower() == image.Username.ToLower()                               // They own the item
                || ((UserAccount)Session["account"]).Admin)                                                                     // They are an admin
                || (image.GroupID.HasValue && facultyowner.ToLower() == ((UserAccount)Session["account"]).Username.ToLower())   // They are the group owner
                || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers            // They are faculty
                || ((UserAccount)Session["account"]).OU == Cite.DomainAuthentication.OrganizationalUnit.StaffUsers              // They are staff
            )
        {
            // This user is the owner or an admin, show them the embed code panel.
            ownerPanel.Visible = true;



                String code = "<img";
                code += " src=\"" + userimagesURL + image.ImageID + "/" + image.ImageID + fileFormat + "\"";
                code += " alt=" + "\"" + image.Description + "\""; 
                code += " >";
                code += "</img>";

                codeLabel.Text = Server.HtmlEncode(code);

                fileLinkLabel.Text = "Direct Link: " + userimagesURL + image.ImageID + "/" + image.ImageID + fileFormat;

                fileLinkLabel.Text += "<br><br>" + Server.HtmlEncode("HTML Link: <a href=\"" + userimagesURL + image.ImageID + "/" + image.ImageID + fileFormat + "\" target=\"_new\">" + image.Title + "</a>");
            

        }
        else
            ownerPanel.Visible = false;
    }
    protected void ddlImageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] separators = { "*" };
         
        if (ddlFileSize.SelectedValue != "Original")
        {
            String[] size = ddlFileSize.SelectedValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            String width = size[0];
            String height = size[1];
            // Search the database for this video.
            DBDataContext db = DBDataContext.CreateInstance();

            Images images;

            string imageId = (string)Request.QueryString["imageid"];

            images = (from a in db.Images
                      where a.ImageID == imageId
                      select a).Single();

            var server = HttpContext.Current.Server;
            bool fileConverted;
            String[] fileFormats = AppSettings.AcceptableImageFormats;
            String fileFormat = "";
            for (int i = 0; i < fileFormats.Length; i++)
            {
                string file = server.MapPath("userimages\\") + imageId + "\\" + imageId + fileFormats[i];
                fileConverted = File.Exists(file);
                if (fileConverted)
                {
                    fileFormat = fileFormats[i];
                }
            }

            //Gets the current URL to use for links and embedd code
            string thisURL = HttpContext.Current.Request.Url.AbsoluteUri;
            thisURL = thisURL.Remove(thisURL.LastIndexOf('/') + 1);
            string userimagesURL = thisURL + "userimages/";

            String code = "<img";
            code += " src=\"" + userimagesURL + images.ImageID + "/" + images.ImageID + fileFormat + "\"";
            code += " width=" + "\"" + width + "\"";
            code += " height=" + "\"" + height + "\"";
            code += " alt=" + "\"" + images.Description + "\""; 
            code += " >";
            code += "</img>";

            codeLabel.Text = Server.HtmlEncode(code);
        }

       
     
    }
}