using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorReport : System.Web.UI.Page
{
    UserAccount account1;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        account1 = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account1.Admin && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }
    }
    protected void submit_button_Click(object sender, EventArgs e)
    {
        try
        {
            MailAddress to = new MailAddress("cite@nwmissouri.edu", "CITE");
            MailAddress from = new MailAddress("citedev@nwmissouri.edu", "Northwest Cloud");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "NW Cloud - User Error Report";
            message.Body = userMessage.Text + "\n Sent from account: \"" + account1.Email + "\"";

            SmtpClient smtp = new SmtpClient("email.nwmissouri.edu");
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;

            string pass = "15FAdevlop";
            NetworkCredential cred = new NetworkCredential(from.Address, pass);
            smtp.Credentials = cred;
            smtp.Timeout = 20000;
            smtp.Send(message);

            errorReportText.Visible = false;
            userMessage.Visible = false;
            submit_button.Visible = false;
            successMessage.Text = "Your message has been sent! Thank you for your feedback.";

        }
        catch (Exception ex)
        {
            successMessage.Text = "There was an error.";
        }
    }
}