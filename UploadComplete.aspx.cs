using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_UploadComplete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        UserAccount account1 = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account1.Admin && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }

        string type = Request.QueryString["type"];
        //to display the success upload message based on the type of media uploaded
        if (type == "audio")
        {
            video.Visible = false;
            website.Visible = false;
            file.Visible = false;
           images.Visible=false;
        }
        else if (type == "website")
        {
            video.Visible = false;
            audio.Visible = false;
            file.Visible = false;
            images.Visible=false;
        }
        else if (type == "files")
        {
            video.Visible = false;
            website.Visible = false;
            audio.Visible = false;
            images.Visible=false;
        }
        else if (type == "images")
        {
          
            file.Visible = false;
            video.Visible = false;
            audio.Visible = false;
            website.Visible = false;
        }
        else
        {
            file.Visible = false;
            website.Visible = false;
            audio.Visible = false;
            images.Visible = false;
        }
    }

}