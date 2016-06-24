using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class status : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect to the login page if the user is not signed in.
        if (Session["account"] == null)
            Response.Redirect("~/Login.aspx", false);

        // Redirect to MyVideos if the user is not an administrator.
        if (!((UserAccount)Session["account"]).Admin)
        {
            Session["error"] = "Ah ah ah... you're not allowed to do that.";
            Response.Redirect("Error.aspx", false);
        }
        UserAccount account = (UserAccount)Session["account"];
        //Admin is visible to only admins,faculty users,staff users
        if (!account.Admin && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        errorLabel.Text = "";

        fillStatusLabels();
    }


    private void fillStatusLabels()
    {
        try
        {
            // Load the information that can come straight from the database.
            DBDataContext db = DBDataContext.CreateInstance();
           

            int activeUsers = 0;
            long cumulativesize = 0;
            long cumulativeuser = 0;
            long cumulativeitems = 0;
            long cumulativeviews = 0;
            long totalSize = 0;
            double avgSize = 0;
            double avgPerUser = 0;
            int clicks = 0;


            #region audio
            int totalAudio = db.Audios.Count();
            cumulativeitems += totalAudio;
            if (totalAudio != 0)
            {
                IList<string> objusername = db.Audios.Select(X => X.Username).ToList();
                activeUsers = objusername.Distinct().Count();
                cumulativeuser += activeUsers;
                
                string path = Server.MapPath(AppSettings.AudioConvertedFolder);
                List<String> allFiles = Directory.GetFiles(path).ToList();
                totalSize = allFiles.Sum(x => new FileInfo(x).Length);

                cumulativesize += totalSize;
                avgSize = totalSize / (double)totalAudio;
                avgPerUser = totalAudio / (double)activeUsers;
                clicks = db.Audios.Select(x => x.NumOfHits).Sum();
                cumulativeviews += clicks;

            }

            audioItems.Text = totalAudio.ToString();
            audioActiveUsers.Text = activeUsers.ToString();
            audioAverageItems.Text = Math.Round(avgPerUser, 1).ToString();
            audioSize.Text = Utility.formatBytesToString(totalSize);
            audioAverageSize.Text = Utility.formatBytesToString((long)avgSize);
            audioViews.Text = clicks.ToString();
            #endregion     


            #region documents
            int totaldocuments = db.Files.Count();
            cumulativeitems += totaldocuments;
            if (totaldocuments != 0)
            {
                IList<string> objusername = db.Files.Select(X => X.Username).ToList();
                activeUsers = objusername.Distinct().Count();
                cumulativeuser += activeUsers;

                string path = Server.MapPath(AppSettings.DocumentSavedFolder);
                List<String> allFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories).ToList();
                totalSize = allFiles.Sum(x => new FileInfo(x).Length);

                cumulativesize += totalSize;
                avgSize = totalSize / (double)totaldocuments;
                avgPerUser = totaldocuments / (double)activeUsers;
                clicks = db.Files.Select(x => x.Views).Sum();
                cumulativeviews += clicks;
            }

            documentsItems.Text = totaldocuments.ToString();
            documentsActiveUsers.Text = activeUsers.ToString();
            documentsAverageItems.Text = Math.Round(avgPerUser, 1).ToString();
            documentsSize.Text = Utility.formatBytesToString(totalSize);
            documentsAverageSize.Text = Utility.formatBytesToString((long)avgSize);
            documentsViews.Text = clicks.ToString();
            #endregion   

            #region images
            int totalimages = db.Images.Count();
            cumulativeitems += totalimages;
            if (totalimages != 0)
            {
                IList<string> objusername = db.Images.Select(X => X.Username).ToList();
                activeUsers = objusername.Distinct().Count();
                cumulativeuser += activeUsers;

                string path = Server.MapPath(AppSettings.ImagesSavedFolder);
                List<String> allFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories).ToList();
                totalSize = allFiles.Sum(x => new FileInfo(x).Length);

                cumulativesize += totalSize;
                avgSize = totalSize / (double)totalimages;
                avgPerUser = totalimages / (double)activeUsers;
                clicks = db.Images.Select(x => x.Views).Sum();
                cumulativeviews += clicks;
            }

            imagesItems.Text = totalimages.ToString();
            imagesActiveUsers.Text = activeUsers.ToString();
            imagesAverageItems.Text = Math.Round(avgPerUser, 1).ToString();
            imagesSize.Text = Utility.formatBytesToString(totalSize);
            imagesAverageSize.Text = Utility.formatBytesToString((long)avgSize);
            imagesViews.Text = clicks.ToString();
            #endregion   

            #region website
            int totalwebsite = db.Websites.Count();
            cumulativeitems += totalwebsite;
           
            if (totalwebsite != 0)
            {
                IList<string> objusername = db.Websites.Select(X => X.Username).ToList();
                activeUsers = objusername.Distinct().Count();
                cumulativeuser += activeUsers;

                string path = Server.MapPath(AppSettings.WebConvertedFolder);
                List<String> allFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories).ToList();
                totalSize = allFiles.Sum(x => new FileInfo(x).Length);

                cumulativesize += totalSize;
                avgSize = totalSize / (double)totalwebsite;
                avgPerUser = totalwebsite / (double)activeUsers;
                clicks = db.Websites.Select(x => x.Views).Sum();
                cumulativeviews += clicks;
            }

            websiteItems.Text = totalwebsite.ToString();
            websiteActiveUsers.Text = activeUsers.ToString();
            websiteAverageItems.Text = Math.Round(avgPerUser, 1).ToString();
            websiteSize.Text = Utility.formatBytesToString(totalSize);
            websiteAverageSize.Text = Utility.formatBytesToString((long)avgSize);
            websiteViews.Text = clicks.ToString();
            #endregion   


            #region video
            int totalVideos = db.Videos.Count();
            cumulativeitems += totalVideos;
          
            if (totalVideos != 0)
            {
                IList<string> objusername = db.Videos.Select(X => X.Username).ToList();
                activeUsers = objusername.Distinct().Count();
                cumulativeuser += activeUsers;

                string path = Server.MapPath(AppSettings.VideoConvertedFolder);
                List<String> allFiles = Directory.GetFiles(path).ToList();
                totalSize = allFiles.Sum(x => new FileInfo(x).Length);
                
                avgSize = totalSize / (double)totalVideos;
                cumulativesize += totalSize;
                avgPerUser = totalVideos / (double)activeUsers;
                clicks = db.Videos.Select(x => x.Views).Sum();
                cumulativeviews += clicks;
            }

            videoItems.Text = totalVideos.ToString();
            videoActiveUsers.Text = activeUsers.ToString();
            videoAverageItems.Text = Math.Round(avgPerUser, 1).ToString();
            videoSize.Text = Utility.formatBytesToString(totalSize);
            videoAverageSize.Text = Utility.formatBytesToString((long)avgSize);
            videoViews.Text = clicks.ToString();
            #endregion         
            

            totalSizelbl.Text = Utility.formatBytesToString(cumulativesize);
            totalActiveUsers.Text = "--";
            totalItems.Text = cumulativeitems.ToString();
            totalViews.Text = cumulativeviews.ToString();

            freeDiskSpace.Text = "Free Disk Space: " + Utility.getFreeDiskSpaceString() + " (" + Utility.getFreeDiskSpacePercentage() + ")";
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }
    }
}