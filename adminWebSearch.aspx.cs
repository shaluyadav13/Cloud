using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;

public partial class adminWebSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Redirect if not logged in.
            if (Session["account"] == null)
                Response.Redirect("login.aspx", true);

            // Redirect to MyMedia if the user is not an administrator.
            if (!((UserAccount)Session["account"]).Admin)
                Response.Redirect("MyMedia.aspx", true);

            UserAccount account = (UserAccount)Session["account"];

            //sNumber.InnerHtml = account.Username;

            errorLabel.Text = "";
            resultsLabel.Text = "";
        }
        catch (Exception ex)
        {

        }
    }
    protected void searchButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(searchBox.Text))
            {
                DBDataContext db = DBDataContext.CreateInstance();

                DomainAccount account = (DomainAccount)Session["account"];

                // Search all websites
                var searchedweb = WebSearcher.SearchAllWebsites(searchBox.Text);

                // Output results.
                if (searchedweb.Count() > 0)
                {
                    webList.Visible = true;
                    webList.Websites = searchedweb;
                }
                else
                {
                    webList.Visible = false;
                    errorLabel.Text = "<strong>No results.</strong>";
                }
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
}