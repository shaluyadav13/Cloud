using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        UserAccount account = (UserAccount)Session["account"];

        errorReportLink.HRef = ("~/ErrorReport.aspx");
        userAgreementLink.HRef = ("~/Agreement.aspx");

        //if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers))
        //{
        //    ad.Visible = false;
        //}

        sNumber.InnerHtml = account.Username;

    }
    protected void searchButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(searchBox.Text))
        {
            Response.Redirect("Search.aspx?SearchText=" + searchBox.Text);
        }
    }
    protected void logOut_Click(object sender, EventArgs e)
    {
        Session["account"] = null;
        Response.Redirect("~/Login.aspx", true);
        System.Diagnostics.Debug.WriteLine("test");

    }
    
}
