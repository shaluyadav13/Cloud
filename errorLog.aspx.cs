using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class errorLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e){
        UserAccount account = (UserAccount)Session["account"];

        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers))
        {
            ad.Visible = false;
        }
    }


}