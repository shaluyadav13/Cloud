using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);

            if(Session["error"] != null)
                errorMessage.Text = Session["error"].ToString();

            Session["error"] = null;

            UserAccount account = (UserAccount)Session["account"];
            //Admin is visible to only admins,faculty users,staff users
            if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                ad.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }

    }
}