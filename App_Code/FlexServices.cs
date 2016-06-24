using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for FlexServices
/// </summary>
[WebService(Namespace = "http://cite.nwmissouri.edu/NWVideoFlex")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FlexServices : System.Web.Services.WebService
{

    public FlexServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public struct VideoData
    {
        public String Title;
        public String Description;
        public String DatePosted;
        public String Owner;
        public String Author;
    }

    [WebMethod]
    public VideoData GetVideoMetadata(String videoID)
    {
        try
        {
            DBDataContext db = DBDataContext.CreateInstance();
            Video vid = (from i in db.Videos
                         where i.VideoID == videoID
                         select i).Single();

            // Look up the owner's full name.
            UserAccount account = new UserAccount(vid.Username);

            VideoData data = new VideoData()
            {
                Title = vid.Title,
                Description = vid.Description,
                DatePosted = vid.DatePosted.ToShortDateString(),
                Owner = account.FirstName + " " + account.LastName
            };

            if (!String.IsNullOrEmpty(vid.Author))
            {
                data.Author = vid.Author;
            }
            else
            {
                data.Author = "";
            }
            return data;
        }
        catch (Exception)
        {
            return new VideoData();
        }
    }

    [WebMethod]
    public void IncrementVideoViews(String videoID)
    {
        try
        {
            // Look up the video, set its last view date to now, and increment the view counter.
            DBDataContext db = DBDataContext.CreateInstance();
            Video vid = (from i in db.Videos
                         where i.VideoID == videoID
                         select i).Single();
            vid.LastView = DateTime.Now;
            vid.Views++;
            db.SubmitChanges();
        }
        catch (Exception)
        { }
    }

}

