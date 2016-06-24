using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Redirect to the login page if the user is not signed in.
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);

            UserAccount account = (UserAccount)Session["account"];
            pnlfaculty.Visible = false;
            pnladmin.Visible = false;
            // User is a student, so they do not have access to this page.
            if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                Response.Redirect("~/myMedia.aspx", true);
            }
            // User is a faculty or staff member, so show the groups settings.
            else if (account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) || account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                pnlfaculty.Visible = true;
            }

            // User is also admin, so show the admin options.
            if(account.Admin)
            {
                pnladmin.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
       
    }

 
}