using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PlayVid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Make sure a video was specified.
        if (Request.QueryString["vid"] == null || Request.QueryString["vid"].Trim() == "")
        {
            Response.Redirect("InvalidVideo.aspx", true);
        }

        // Get the video id from the URL
        String id = Request.QueryString["vid"];
        String videoWidth = (string)Request.QueryString["width"];
        String videoHeight = (string)Request.QueryString["height"];
        // Search the database for this video.
        DBDataContext db = DBDataContext.CreateInstance();

        Video vid;

        try
        {
            vid = (from v in db.Videos
                   where v.VideoID == id
                   select v).Single();

            Page.Title = vid.Title + " - Northwest Cloud";

            //set the source and poster of the video tag
            videoTag.Attributes["width"] = videoWidth;
            videoTag.Attributes["height"] = videoHeight;
            source1.Attributes["src"] = ((AppSettings.VideoConvertedFolder + "/") + id + ".mp4");
            videoTag.Attributes["poster"] = ((AppSettings.ThumbnailFolder + "/") + vid.VideoID + ".png");

            // Show transcript link if there is a transcript
            if (vid.Transcript)
            {
                transcriptLink.HRef = (@"Transcripts/" + vid.VideoID + ".txt");
            }
            else
            {
                transcriptLink.Visible = false;
            }
        }
        catch (Exception)
        {
            Response.Redirect("InvalidVideo.aspx", true);
        }


        // Handle the web counter image (used to track page hits, browser and platform statistics.

        // Change the link depending on whether this is being deployed to the live or the production server.
        if (AppSettings.IsProductionServer)
        {
            // Link for the live web counter.
            //webCounterLink.ImageUrl = "http://cite.nwmissouri.edu/webcounter/72/AliceBlue/AliceBlue/Hidden.png";
        }
        else
        {
            // Disabled. Notice the "n" after the 72 (the site's unique ID within the web counter). The "n"
            // means that it will not increment the page hits when the image is loaded. Use this for debugging
            // or when deploying to the test server.
          //  webCounterLink.ImageUrl = "http://cite.nwmissouri.edu/webcounter/72n/AliceBlue/AliceBlue/Hidden.png";
        }


    }
}