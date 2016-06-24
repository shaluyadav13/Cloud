using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cite.DomainAuthentication;

public partial class allVideos : System.Web.UI.Page
{
    const String ACTION_GET_FACULTY = "GetFaculty";
    const String ACTION_GET_STUDENTS = "GetStudents";
    const String ACTION_GET_VIDEO = "GetVideo";
    const String ACTION_GET_STUDENT_GROUP = "GetStudentGroup";
    const String ACTION_GET_USER = "GetUser";

    const String VARIABLE_ACTION = "action";
    const String VARIABLE_USER_ID = "user";
    const String VARIABLE_GROUP_ID = "groupID";
    const String VARIABLE_VIDEO_ID = "vid";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Redirect to the login page if the user is not signed in.
            if (Session["account"] == null)
                Response.Redirect("~/Login.aspx", true);

            // Redirect to MyVideos if the user is not an administrator.
            if (!((UserAccount)Session["account"]).Admin)
                Response.Redirect("MyVideos.aspx", true);

            UserAccount account = (UserAccount)Session["account"];

            //sNumber.InnerHtml = account.Username;

            errorLabel.Text = "";

            Label1.Text = "";

            // Check the value of "action" in the QueryString to decide what action to take.
            if (String.IsNullOrEmpty(Request.QueryString[VARIABLE_ACTION]))
            {
                topLevel();
            }
            else if (Request.QueryString[VARIABLE_ACTION] == ACTION_GET_FACULTY)
            {
                facultyStaff();
            }
            else if (Request.QueryString[VARIABLE_ACTION] == ACTION_GET_STUDENTS)
            {
                students();
            }
            else if (Request.QueryString[VARIABLE_ACTION] == ACTION_GET_STUDENT_GROUP)
            {
                studentGroup(int.Parse(Request.QueryString[VARIABLE_GROUP_ID]));
            }
            else if (Request.QueryString[VARIABLE_ACTION] == ACTION_GET_USER)
            {
                user(Request.QueryString[VARIABLE_USER_ID]);
            }
            else if (Request.QueryString[VARIABLE_ACTION] == ACTION_GET_VIDEO)
            {
                video(Request.QueryString[VARIABLE_VIDEO_ID]);
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Default action. Display two links, one to faculty & staff videos, another to student groups.
    /// </summary>
    private void topLevel()
    {
        HeaderLabel.Text = "User Groups";

        StringBuilder sb = new StringBuilder();
        sb.Append("<ul class=\"tableOfContents\">");
        sb.Append("<li><a href='AllVideos.aspx?" + VARIABLE_ACTION + "=" + ACTION_GET_FACULTY + "'>Faculty & Staff</a></li>");
        sb.Append("<li><a href='AllVideos.aspx?" + VARIABLE_ACTION + "=" + ACTION_GET_STUDENTS + "'>Student Groups</a></li>");
        sb.Append("</ul>");
        Label1.Text = sb.ToString();
    }

    /// <summary>
    /// Outputs all student groups in the system.
    /// </summary>
    private void students()
    {
        try
        {
            HeaderLabel.Text = "Student Groups";
            DBDataContext db = DBDataContext.CreateInstance();

            // Get all student groups.
            var groups = from i in db.StudentGroups
                         orderby i.GroupName
                         select i;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul class=\"tableOfContents\">");

            foreach (var g in groups)
            {
                DomainAccount acc = new DomainAccount(g.FacultyOwner);
                sb.AppendLine(String.Format("<li><a href='AllVideos.aspx?{0}={1}&{2}={3}'>{4} ({5}, {6})</a></li>",
                                            VARIABLE_ACTION,
                                            ACTION_GET_STUDENT_GROUP,
                                            VARIABLE_GROUP_ID,
                                            g.GroupID,
                                            g.GroupName,
                                            acc.LastName,
                                            acc.FirstName));
            }

            sb.AppendLine("</ul>");
            Label1.Text = sb.ToString();

        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    /// <summary>
    /// Outputs all faculty and staff who have videos.
    /// </summary>
    private void facultyStaff()
    {
        try
        {
            HeaderLabel.Text = "Faculty & Staff & Administrators";

            // Get all users who have videos posted that are not part of a student group.

            DBDataContext db = DBDataContext.CreateInstance();

            // Don't query for distinct username, distinct kills the query performance.
            var usernames = (from i in db.Videos
                             where !i.GroupID.HasValue
                             orderby i.Username
                             select i.Username.ToLower());

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul class=\"tableOfContents\">");

            // Use DomainAccounts rather than UserAccounts throughout this page because DomainAccount is a bit faster.
            // UserAccount performs a database lookup to see if the account has admin privileges in NWVideo. We don't
            // need to know this on this page.
            Dictionary<String, DomainAccount> accounts = new Dictionary<String, DomainAccount>();

            foreach (var username in usernames)
            {
                // Get DomainAccount objects, ignoring duplicates here. Use a Dictionary because looking
                // up a key in a Dictionary is very fast.
                if (!accounts.ContainsKey(username))
                    accounts.Add(username, new DomainAccount(username));
            }

            // Turn the Dictionary into an ordered list.
            var accountList = (from i in accounts
                               orderby i.Value.LastName, i.Value.FirstName, i.Value.Username
                               select i.Value);

            foreach (DomainAccount a in accountList)
            {
                String accountType;
                switch (a.OU)
                {
                    case Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers:
                        accountType = "Faculty"; break;
                    case Cite.DomainAuthentication.OrganizationalUnit.StaffUsers:
                        accountType = "Staff"; break;
                    case Cite.DomainAuthentication.OrganizationalUnit.StudentUsers:
                        accountType = "Student"; break;
                    default:
                        accountType = "Unknown account type"; break;
                }

                int videoCount = (from i in db.Videos
                                  where i.Username.ToLower() == a.Username.ToLower()
                                  select i).Count();

                sb.AppendLine(String.Format("<li><a href='AllVideos.aspx?{0}={1}&{2}={3}'>{4}, {5} ({6}), {7}, {8} videos</a></li>",
                                        VARIABLE_ACTION,
                                        ACTION_GET_USER,
                                        VARIABLE_USER_ID,
                                        a.Username.ToLower(),
                                        a.LastName,
                                        a.FirstName,
                                        a.Username.ToLower(),
                                        accountType,
                                        videoCount));
            }

            sb.AppendLine("</ul>");

            Label1.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    /// <summary>
    /// Displays a list of videos for a certain user.
    /// </summary>
    /// <param name="username"></param>
    private void user(String username)
    {
        try
        {
            DBDataContext db = DBDataContext.CreateInstance();
            var vids = from i in db.Videos
                       where i.Username.ToLower() == username
                       orderby i.Title
                       select i;

            DomainAccount account = new DomainAccount(username);

            String accountType;
            switch (account.OU)
            {
                case Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers:
                    accountType = "Faculty"; break;
                case Cite.DomainAuthentication.OrganizationalUnit.StaffUsers:
                    accountType = "Staff"; break;
                case Cite.DomainAuthentication.OrganizationalUnit.StudentUsers:
                    accountType = "Student"; break;
                default:
                    accountType = "Unknown account type"; break;
            }

            StringBuilder sb = new StringBuilder();
            HeaderLabel.Text = String.Format("<strong>{0}, {1} ({2}), {3}, {4} videos.</strong>",
                                            account.LastName,
                                            account.FirstName,
                                            account.Username.ToLower(),
                                            accountType,
                                            vids.Count() / 1024);
            sb.AppendLine("<br /><br />");
            sb.AppendLine("<ul class=\"tableOfContents\">");

            foreach (var vid in vids)
            {
                sb.AppendLine(String.Format("<li><a href='AllVideos.aspx?{0}={1}&{2}={3}'>'{4}', {5} KB, posted {6}, {7} views</a></li>",
                                        VARIABLE_ACTION,
                                        ACTION_GET_VIDEO,
                                        VARIABLE_VIDEO_ID,
                                        vid.VideoID,
                                        vid.Title,
                                        vid.Size / 1024,
                                        vid.DatePosted.ToShortDateString(),
                                        vid.Views));
            }

            sb.AppendLine("</ul>");

            // Check to see if this user has any student groups.
            var stuGroups = from i in db.StudentGroups
                            where i.FacultyOwner.ToLower() == username
                            orderby i.GroupName
                            select i;
            if (stuGroups.Count() > 0)
            {
                sb.AppendLine("<br /><br /><h2>Student Groups</h2><ul>");

                foreach (var g in stuGroups)
                {
                    sb.AppendLine(String.Format("<li><a href='AllVideos.aspx?action={0}&groupID={1}'>{2}</a></li>",
                                                ACTION_GET_STUDENT_GROUP,
                                                g.GroupID,
                                                g.GroupName));
                }
                sb.AppendLine("</ul>");
            }

            Label1.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    /// <summary>
    /// Displays a list of videos within a specific student group.
    /// </summary>
    /// <param name="groupID"></param>
    private void studentGroup(int groupID)
    {
        try
        {
            DBDataContext db = DBDataContext.CreateInstance();

            StudentGroup g = (from i in db.StudentGroups
                              where i.GroupID == groupID
                              select i).Single();

            DomainAccount acc = new DomainAccount(g.FacultyOwner);

            HeaderLabel.Text = String.Format("{0}<br /><a href='AllVideos.aspx?{1}={2}&{3}={4}'>{5}, {6} ({7})</a><br />{8} - {9}<br />{10} students, {11} videos",
                                             g.GroupName,
                                             VARIABLE_ACTION,
                                             ACTION_GET_USER,
                                             VARIABLE_USER_ID,
                                             g.FacultyOwner.ToLower(),
                                             acc.LastName,
                                             acc.FirstName,
                                             acc.Username.ToLower(),
                                             g.StartDate.ToString("MM/dd/yyyy"),
                                             g.EndDate.ToString("MM/dd/yyyy"),
                                             g.AuthorizedStudents.Count,
                                             g.Videos.Count);

            var usernames = (from i in g.AuthorizedStudents
                             orderby i.Username
                             select i.Username.ToLower()).Distinct();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<a href='AllVideos.aspx?" + VARIABLE_ACTION + "=" + ACTION_GET_STUDENTS + "'>Back to Student Groups</a><br /><br />");
            sb.AppendLine("<ul class=\"tableOfContents\">");

            foreach (String user in usernames)
            {
                DomainAccount account = new DomainAccount(user);
                sb.AppendLine(String.Format("<li>{0}, {1} ({2})", account.LastName, account.FirstName, user));

                var videos = from i in g.Videos
                             where i.Username.ToLower() == user
                             orderby i.Title
                             select i;

                if (videos.Count() > 0)
                {
                    sb.AppendLine("<ul>");

                    foreach (var v in videos)
                    {
                        sb.AppendLine(String.Format("<li><a href='AllVideos.aspx?{0}={1}&{2}={3}'>'{4}', {5} KB, posted {6}, {7} views</a></li>",
                                                    VARIABLE_ACTION,
                                                    ACTION_GET_VIDEO,
                                                    VARIABLE_VIDEO_ID,
                                                    v.VideoID,
                                                    v.Title,
                                                    v.Size / 1024,
                                                    v.DatePosted.ToShortDateString(),
                                                    v.Views));
                    }

                    sb.AppendLine("</ul>");
                }
                sb.AppendLine("</li>");
            }

            sb.AppendLine("</ul>");
            Label1.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

    private void video(String videoID)
    {
        try
        {
            DBDataContext db = DBDataContext.CreateInstance();
            Video vid = (from i in db.Videos
                         where i.VideoID == videoID
                         select i).Single();
            DomainAccount account = new DomainAccount(vid.Username);
            HeaderLabel.Text = String.Format("'{0}'<br />Posted by <a href='AllVideos.aspx?{1}={2}&{3}={4}'>{5}, {6} ({7})</a>",
                                             vid.Title,
                                             VARIABLE_ACTION,
                                             ACTION_GET_USER,
                                             VARIABLE_VIDEO_ID,
                                             vid.Username.ToLower(),
                                             account.LastName,
                                             account.FirstName,
                                             account.Username.ToLower()); ;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<a href='PlayVid.aspx?vid=" + vid.VideoID + "' target='_new'>Go to Video</a><br />");
            sb.AppendLine("<a href='EditVideo.aspx?vid=" + vid.VideoID + "' target='_new'>Edit</a><br />");
            sb.AppendLine("<strong>Date Posted:</strong> " + vid.DatePosted.ToString("MM/dd/yyyy h:mm tt") + "<br />");
            if (vid.AutoDeleteDate.HasValue)
            {
                sb.AppendLine("<strong>Auto-Delete Date:</strong> " + vid.AutoDeleteDate.Value.ToString("MM/dd/yyyy") + "<br />");
            }
            if (!String.IsNullOrEmpty(vid.Author))
            {
                sb.AppendLine("<strong>Author:</strong> " + vid.Author + "<br />");
            }
            if (vid.GroupID.HasValue)
            {
                sb.AppendLine(String.Format("<strong>Student Group:</strong> <a href='AllVideos.aspx?action={0}&groupID={1}'>{2}</a><br />",
                                            ACTION_GET_STUDENT_GROUP,
                                            vid.GroupID.Value,
                                            vid.StudentGroup.GroupName));
            }
            sb.AppendLine("<strong>Views:</strong> " + vid.Views + "<br />");
            sb.AppendLine("<strong>Size:</strong> " + (vid.Size / 1024) + " KB<br />");
            sb.AppendLine("<strong>Last Viewed:</strong> ");
            if (vid.LastView.HasValue)
            {
                sb.AppendLine(vid.LastView.Value.ToString("MM/dd/yyyy h:mm tt") + "<br />");
            }
            else
            {
                sb.AppendLine("Never<br />");
            }
            sb.AppendLine("<strong>Description:</strong> " + vid.Description + "<br />");

            Label1.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }

   
}