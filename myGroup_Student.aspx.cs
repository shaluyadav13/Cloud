using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;

public partial class pages_myGroup_Student : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
        {
            getStudentGroupVideos(int.Parse(Request.QueryString["groupID"]));
        }
        else
        {
            DBDataContext db = DBDataContext.CreateInstance();
            DomainAccount account = (DomainAccount)Session["Account"];


            var stuGroups = from sg in db.StudentGroups
                            join us in db.AuthorizedStudents on sg.GroupID equals us.GroupID
                            where us.Username == account.Username && sg.EndDate >= DateTime.Now.Date //&& sg.ShowGroup == true
                            //select new { sg.GroupID, sg.GroupName, sg.StartDate, sg.EndDate, sg.Videos };
                            select sg;

            StringBuilder sb = new StringBuilder();



            MyGroupsHeader.Text = "<p><strong>Your Student Groups</strong></p>";

            if (stuGroups.Count() == 0)
            {
                MyGroupsHeader.Text = "<p><strong>You do not have any student groups.</strong></p>";
            }
            else
            {
                sb.AppendLine("<ul>");
                foreach (var g in stuGroups)
                {
                    if (!g.ShowGroup)
                    {
                        sb.AppendLine(String.Format("<li><a onclick=this.removeAttribute('href');this.className='disabled'>{1}, {2} - {3}, {4} videos</a></li>",
                            //sb.AppendLine(String.Format("<li><span>{1}, {2} - {3}, {4} videos</span></li>",
                                                g.GroupID,
                                                g.GroupName,
                                                g.StartDate.ToShortDateString(),
                                                g.EndDate.ToShortDateString(),
                                                g.Videos.Count));
                    }
                    else
                    {
                        sb.AppendLine(String.Format("<li><a href='MyGroups_Student.aspx?groupID={0}'>{1}, {2} - {3}, {4} videos</a></li>",
                                                g.GroupID,
                                                g.GroupName,
                                                g.StartDate.ToShortDateString(),
                                                g.EndDate.ToShortDateString(),
                                                g.Videos.Count));
                    }
                }
                sb.AppendLine("</ul>");
            }

            Dispalylist.Text = sb.ToString();
        }
    }

    private void getStudentGroupVideos(int groupID)
    {
        DBDataContext db = DBDataContext.CreateInstance();
        DomainAccount account = (DomainAccount)Session["account"];
        var videos = (from i in db.Videos
                      where i.GroupID == groupID
                      select i);

        StringBuilder sb = new StringBuilder();



        if (videos.Count() == 0)
        {
            sb.AppendLine("<p><strong>This group does not have videos.</strong></p>");
        }
        else
        {
            videoList.Visible = true;
            videoList.Videos = videos;
        }
        Dispalylist.Text = sb.ToString();

    }


    protected void searchButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(searchBox.Text))
        {
            Response.Redirect("Search.aspx?SearchText=" + searchBox.Text);
        }
    }



}