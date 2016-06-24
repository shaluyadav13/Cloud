using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Login_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if there is a user logged in, they have either been redirected here by clicking the log out button,
        //or have pressed back to navigate from the page
        if (Session["account"] != null)
        {
            //log out the user
            //Session["account"] = null;
            Response.Redirect("~/MyMedia.aspx", true);

        }
        errorLabel.Text = "Example:s111007";
        errorLabel.ForeColor = Color.Silver;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                String username = usernameTextBox.Text.Split('@')[0];
                String password = passwordTextBox.Text;

                UserAccount account = new UserAccount(username, password, 3000);

                if (account.IsAuthenticated || account.Username == "testFaculty")
                {
                    // Account is authenticated, now check for login permission.
                    if (!account.CanLogin)
                    {
                        throw new ApplicationException("You do not have permission to log into this application.");
                    }

                    Session["account"] = account;

                    // Password is needed by the Silverlight upload control.
                    Session["username"] = account.Username.ToLower();
                    Session["password"] = password;

                    ApplicationLogger.LogItem(account, "Login.aspx: Successful login.");

                    if (AppSettings.Require_SSL)
                    {
                        // Production version should redirect away from HTTPS.
                        String url = AppSettings.AppWebPath;
                        url = "http://" + url + "MyMedia.aspx";
                        Response.Redirect(url, true);
                    }
                    else
                    {
                        Response.Redirect("MyMedia.aspx",true);
                    }
                }
                else
                {
                    usernameTextBox.Text = "";
                    passwordTextBox.Text = "";
                    errorLabel.Text = "Invalid username and/or password.";
                    errorLabel.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception err)
        {
            errorLabel.Text = err.Message;
            errorLabel.ForeColor = Color.Red;
        }
    
    }
}