using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PlayAid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Make sure a video was specified.
        if (Request.QueryString["aid"] == null || Request.QueryString["aid"].Trim() == "")
        {
            Response.Redirect("InvalidVideo.aspx", true);
        }

        // Get the video id from the URL
        String id = Request.QueryString["aid"];

        // Search the database for this video.
        DBDataContext db = DBDataContext.CreateInstance();

        Audio aid;

        try
        {
            aid = (from v in db.Audios
                   where v.AudioID == id
                   select v).Single();

            Page.Title = aid.Title + " - Northwest Cloud";

            //set the source and poster of the video tag
            audioTag.Attributes["src"] = (@"convertedAudios/" + aid.AudioID + ".mp3");
            if (aid.Transcript)
            {
                transcriptLink.HRef = (@"Transcripts/" + aid.AudioID + ".txt");
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
    }
}