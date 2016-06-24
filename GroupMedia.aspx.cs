using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GroupMedia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);
        UserAccount account = (UserAccount)Session["account"];

        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }
        String groupID = (String)Request.QueryString["groupID"];
        using (DBDataContext db = DBDataContext.CreateInstance())
        {
            bool isOwner = (from gr in db.StudentGroups
                            where gr.GroupID == int.Parse(groupID) && gr.FacultyOwner == account.Username
                            select gr).Count() != 0;
            bool isAuthorisedStudent = false;
            if (!isOwner)
            {
                isAuthorisedStudent = (from ag in db.AuthorizedStudents
                                       join gro in db.StudentGroups
                                       on ag.GroupID equals gro.GroupID
                                       where ag.GroupID == int.Parse(groupID) && ag.Username == account.Username && gro.ShowGroup == true
                                       select ag).Count() != 0;
            }
            if (isOwner || isAuthorisedStudent)
            {
                //Session["groupID"] = groupID;
                if (!String.IsNullOrEmpty(groupID))
                {
                    video.Attributes["href"] = "MyVideos.aspx?groupID=" + groupID;
                    audio.Attributes["href"] = "myAudio.aspx?groupID=" + groupID;
                    webpage.Attributes["href"] = "MyWebpages.aspx?groupID=" + groupID;
                    files.Attributes["href"] = "MyFiles.aspx?groupID=" + groupID;
                    images.Attributes["href"] = "MyImages.aspx?groupID=" + groupID;

                }
                else
                {
                    video.Attributes["href"] = "MyVideos.aspx";
                    audio.Attributes["href"] = "myAudio.aspx";
                    webpage.Attributes["href"] = "MyWebpages.aspx";
                    files.Attributes["href"] = "MyFiles.aspx";
                    images.Attributes["href"] = "MyImages.aspx";
                }
                string groupName = (from st in db.StudentGroups
                                    where st.GroupID == int.Parse(groupID)
                                    select st.GroupName).First();
                lblgroupname.Text = groupName;
            }
            else
            {
                Session["error"] = "You don't have access to this group.";
                Response.Redirect("Error.aspx", false);
            }
        }

    }
}