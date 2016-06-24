using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;

public partial class Administrators : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Redirect to the login page if the user is not signed in.
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);

            // Make sure the user is an admin.
            if (!((UserAccount)Session["account"]).Admin)
                Response.Redirect("~/MyVideos.aspx", true);

            UserAccount account = (UserAccount)Session["account"];

            if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers))
            {
                ad.Visible = false;
            }
            errorLabel.Text = "";
            if (!IsPostBack)
                fillAdminListBox();
        }
        catch (Exception ex)
        {

        }
    }

    private void fillAdminListBox()
    {
        try
        {
            adminListBox.Items.Clear();
            DBDataContext db = DBDataContext.CreateInstance();
            var users = from b in db.Admins
                        orderby b.Username
                        select new DomainAccount(b.Username);

            foreach (DomainAccount a in users)
            {
                String name = String.Format("{0}, {1} {2}",
                                            a.Username.ToLower(),
                                            a.FirstName,
                                            a.LastName);
                adminListBox.Items.Add(new ListItem(name, a.Username));
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void removeAdminButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (adminListBox.SelectedIndex != -1)
            {
                // Get the admin's username from the list box.
                String username = adminListBox.SelectedValue;

                // Make sure the admin isn't removing himself.
                if (username.ToLower() == ((UserAccount)Session["account"]).Username.ToLower())
                    throw new ApplicationException("You cannot remove yourself from the admin list.");

                DBDataContext db = DBDataContext.CreateInstance();

                // Remove the admin.
                Admin a = (from i in db.Admins
                           where i.Username == username
                           select i).Single();
                db.Admins.DeleteOnSubmit(a);
                db.SubmitChanges();
                fillAdminListBox();
            }
        }
        catch (ApplicationException ex)
        {
            errorLabel.Text = ex.Message;
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
    protected void addAdminButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (usernameTextBox.Text.Trim() == "")
                throw new ApplicationException("Please enter a username.");

            String username = usernameTextBox.Text;
            usernameTextBox.Text = "";

            // Look the user up in ActiveDirectory.
            UserAccount newAdmin = new UserAccount(username);

            if (!newAdmin.Exists)
                throw new ApplicationException("That user does not exist in the Northwest domain.");

            // Finally add the admin.
            Admin a = new Admin();
            a.Username = newAdmin.Username;
            DBDataContext db = DBDataContext.CreateInstance();
            db.Admins.InsertOnSubmit(a);
            db.SubmitChanges();
            fillAdminListBox();
        }
        catch (ApplicationException ex)
        {
            errorLabel.Text = ex.Message;
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }


}