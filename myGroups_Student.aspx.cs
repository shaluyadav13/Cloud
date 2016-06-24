using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;
using System.Data.Objects;

public partial class pages_myGroup_Student : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", true);

        UserAccount account1 = (UserAccount)Session["account"];
        Session["groupID"] = "";
        //Admin is visible to only admins,faculty users,staff users
        if (!account1.Admin && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }

        groupname.Visible = false;
        DBDataContext db = DBDataContext.CreateInstance();
        DomainAccount account = (DomainAccount)Session["Account"];
        if (!String.IsNullOrEmpty(Request.QueryString["groupID"]))
        {
            getStudentGroupVideos(int.Parse(Request.QueryString["groupID"]));
            grpMessage.Visible = false;
        }
        else
        {
            //displays groups created and the groups in for faculty and staff users
            if (account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) || account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
            {
               

            var createdGroups= from sg in db.StudentGroups
                                   where sg.FacultyOwner == account.Username //&& sg.EndDate >= DateTime.Now.Date 
                                    select sg;
            List<StudentGroup> objcreatedGroups = createdGroups.AsQueryable().ToList();
            foreach (var item in createdGroups.ToList())
            {
                bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                if (!yes) { objcreatedGroups.Remove(item); }
            }

            var stuGroups = from sg in db.StudentGroups
                            join us in db.AuthorizedStudents on sg.GroupID equals us.GroupID
                            where us.Username == account.Username //&& sg.EndDate >= DateTime.Now.Date //&& sg.ShowGroup == true
                            //select new { sg.GroupID, sg.GroupName, sg.StartDate, sg.EndDate, sg.Videos };
                            select sg;

            List<StudentGroup> objstuGroups = stuGroups.AsQueryable().ToList();
            foreach (var item in stuGroups.ToList())
            {
                bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                if (!yes) { objstuGroups.Remove(item); }
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder createdGrp = new StringBuilder();


            MyGroupsHeader.Text = "<p><strong>&nbsp;&nbsp;&nbsp;Groups In</strong></p>";
            lblCreatedGroups.Text = "<p><strong>&nbsp;&nbsp;&nbsp;Groups Owned</strong>";
            if (stuGroups.Count() == 0)
            {
                MyGroupsHeader.Text = "<p><strong>You are not a member of any groups.</strong></p>";
            }
            else
            {
                sb.AppendLine("<ul>");
                foreach (var g in objstuGroups)
                {
                    if (!g.ShowGroup)
                    {
                        sb.AppendLine(String.Format("<li><a onclick=this.removeAttribute('href');this.className='disabled'>{1}, {2} - {3}, {4} videos, {5} audios,{6} websites,{7} documents</a></li>",
                            //sb.AppendLine(String.Format("<li><span>{1}, {2} - {3}, {4} videos</span></li>",
                                                g.GroupID,
                                                g.GroupName,
                                                g.StartDate.ToShortDateString(),
                                                g.EndDate.ToShortDateString(),
                                                g.Videos.Count,
                                                g.Audios.Count,
                                                g.Websites.Count,
                                                g.Files.Count));
                        sb.AppendLine("</br>");
                    }
                    else
                    {
                        sb.AppendLine(String.Format("<li><a href='GroupMedia.aspx?groupID={0}'>{1}, {2} - {3}, {4} videos, {5} audios,{6} websites,{7} documents</a></li>",
                                                g.GroupID,
                                                g.GroupName,
                                                g.StartDate.ToShortDateString(),
                                                g.EndDate.ToShortDateString(),
                                                g.Videos.Count,
                                                g.Audios.Count,
                                                g.Websites.Count,
                                                g.Files.Count));
                        sb.AppendLine("</br>");
                    }
                }
                sb.AppendLine("</ul>");
            }
          
            Dispalylist.Text = sb.ToString();
            if (createdGroups.Count() == 0)
            {
                lblCreatedGroups.Text = "<p><strong>You have not created any groups.</strong></p>";
            }
            else
            {
                createdGrp.AppendLine("<ul>");
                foreach (var g in objcreatedGroups)
                {
                  
                        createdGrp.AppendLine(String.Format("<li><a href='GroupMedia.aspx?groupID={0}'>{1}, {2} - {3}, {4} videos, {5} audios,{6} websites,{7} documents</a></li>",
                                                g.GroupID,
                                                g.GroupName,
                                                g.StartDate.ToShortDateString(),
                                                g.EndDate.ToShortDateString(),
                                                g.Videos.Count,
                                                g.Audios.Count,
                                                g.Websites.Count,
                                                g.Files.Count));
                        createdGrp.AppendLine("</br>");
                    
                }
                createdGrp.AppendLine("</ul>");
            }
            lblDisplayList.Text = createdGrp.ToString();
            }
                //displays groups in for student users
            else if (account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StudentUsers))
            {

             
               var stuGroups1 =  from sg in db.StudentGroups
                               join us in db.AuthorizedStudents on  sg.GroupID   equals us.GroupID
                              where us.Username == account.Username //&& sg.EndDate >= DateTime.Now  //&& sg.ShowGroup == true
                               ////select new { sg.GroupID, sg.GroupName, sg.StartDate, sg.EndDate, sg.Videos };
                               select sg;
               List<StudentGroup>  stuGroups = stuGroups1.ToList();
               foreach (var item in stuGroups1.ToList())
               {
                   bool yes = DateTime.Parse(item.EndDate.ToString("MM-dd-yyyy")).Date > DateTime.Now.Date;
                   if (!yes) { stuGroups.Remove(item); }
               }
              // var groups = db.AuthorizedStudents.Where(x => x.Username == account.Username).Select(x => x.GroupID).ToList();
          
              
                StringBuilder sb = new StringBuilder();
                StringBuilder createdGrp = new StringBuilder();


                MyGroupsHeader.Text = "<p><strong>&nbsp;&nbsp;&nbsp;Groups in</strong></p>";
                
                if (stuGroups.Count() == 0)
                {
                    MyGroupsHeader.Text = "<p><strong>You do not have any student groups.</strong></p>";
                }
                else
                {
                    sb.AppendLine("<ul>");
                    foreach (var g in stuGroups.ToList())
                    {
                        if (!g.ShowGroup)
                        {
                            sb.AppendLine(String.Format("<li><a onclick=this.removeAttribute('href');this.className='disabled'>{1}, {2} - {3}, {4} videos, {5} audios,{6} websites,{7} documents</a></li>",
                                //sb.AppendLine(String.Format("<li><span>{1}, {2} - {3}, {4} videos</span></li>",
                                                    g.GroupID,
                                                    g.GroupName,
                                                    g.StartDate.ToShortDateString(),
                                                    g.EndDate.ToShortDateString(),
                                                    g.Videos.Count,
                                                    g.Audios.Count,
                                                    g.Websites.Count,
                                                    g.Files.Count));
                            sb.AppendLine("</br>");
                        }
                        else
                        {
                            sb.AppendLine(String.Format("<li><a href='GroupMedia.aspx?groupID={0}'>{1}, {2} - {3}, {4} videos, {5} audios,{6} websites,{7} documents</a></li>",
                                                    g.GroupID,
                                                    g.GroupName,
                                                    g.StartDate.ToShortDateString(),
                                                    g.EndDate.ToShortDateString(),
                                                    g.Videos.Count,
                                                    g.Audios.Count,
                                                    g.Websites.Count,
                                                    g.Files.Count));
                            sb.AppendLine("</br>");
                        }
                    }
                    sb.AppendLine("</ul>");
                }
                Dispalylist.Text = sb.ToString();
            }
        }
    }

    private void getStudentGroupVideos(int groupID)
    {
        DBDataContext db = DBDataContext.CreateInstance();
        DomainAccount account = (DomainAccount)Session["account"];
        var videos = (from i in db.Videos
                      where i.GroupID == groupID
                      select i);
        var groupName = (from g in db.StudentGroups
                         where g.GroupID == groupID
                         select g.GroupName);
        StringBuilder sb = new StringBuilder();

        groupname.Visible = true;
        groupname.Attributes["href"] = @"GroupMedia.aspx?groupID=" + groupID;
       
        lblgroupname.Text = groupName.FirstOrDefault().ToString();

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

}