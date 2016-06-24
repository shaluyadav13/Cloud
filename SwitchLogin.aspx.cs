using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SwitchLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        // Make sure the user is an admin.
        if (!((UserAccount)Session["account"]).Admin)
            Response.Redirect("~/MyMedia.aspx", true);

        UserAccount account = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        errorLabel.Text = "";


    }
    protected void loginButton_Click(object sender, EventArgs e)
    {
        try
        {
            String username = usernameTextBox.Text;
            if (!String.IsNullOrEmpty(username))
            {
                UserAccount account = new UserAccount(username);
                if (account.CanLogin)
                {
                    ApplicationLogger.LogItem((UserAccount)Session["account"], String.Format("Switching login to {0} {1}, {2}.",
                                                                                             account.Username,
                                                                                             account.LastName,
                                                                                             account.FirstName));
                    Session.Clear();
                    Session["account"] = account;
                    ApplicationLogger.LogItem(account, "Successful login using administrative account switching.");
                    Response.Redirect("MyMedia.aspx");
                }
                else if (!account.Exists)
                {
                    throw new ApplicationException("Specified user does not exist.");
                }
                else
                {
                    throw new ApplicationException("That user does not have the permissions to login to Northwest Video.");
                }
            }

        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

 
}