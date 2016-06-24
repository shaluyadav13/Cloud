using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;

public partial class Agreement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);

            UserAccount account1 = (UserAccount)Session["account"];
            //Admin is visible to only admins,faculty users,staff users
            if (!account1.Admin && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
                ad.Visible = false;
            }
            using (DBDataContext db = new DBDataContext())
            {
                bool userSigned = (from agr in db.UserAgreements
                                   where agr.Username == account1.Username
                                   select agr).Count() != 0;
                if (userSigned)
                {
                    signedPNL.Visible = false;

                }
                else
                {
                    signedPNL.Visible = true;
                }
            }
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (agreed.Checked)
            {
                using (DBDataContext db = new DBDataContext())
                {
                    UserAccount account1 = (UserAccount)Session["account"];
                    UserAgreement agreedUser = new UserAgreement();
                    agreedUser.Username = account1.Username;
                    agreedUser.SignedDate = DateTime.Now;
                    db.UserAgreements.InsertOnSubmit(agreedUser);
                    db.SubmitChanges();
                    Response.Redirect("UploadMedia.aspx", false);
                }

            }
            else
            {
                error.Text = "You cannot upload any media without signing the user agreement";
            }
        }
        catch (Exception ex)
        {
        }
    }
}