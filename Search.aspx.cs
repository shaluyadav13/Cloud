using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Cite.DomainAuthentication;
using System.Text;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Redirect if not logged in.
        if (Session["account"] == null)
            Response.Redirect("login.aspx", true);

        UserAccount account1 = (UserAccount)Session["account"];

        //Admin is visible to only admins,faculty users,staff users
        if (!account1.Admin && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.FacultyUsers) && !account1.OU.Equals(Cite.DomainAuthentication.OrganizationalUnit.StaffUsers))
        {
            ad.Visible = false;
        }


        errorLabel.Text = "";
        //lblVideos.Text = "<b>Videos</b><br/>";
        string searchText = Request.QueryString["SearchText"];
        searchLabel.Text = "Search results for \"" + searchText + "\"";

        try
        {
            if (!String.IsNullOrEmpty(searchText))
            {
                //DBDataContext db = DBDataContext.CreateInstance();
                DomainAccount account = (DomainAccount)Session["account"];


                //---- OLD CODE ----//
                //// Get all of the user's audios and their students' audios.
                //List<Audio> audios = (from i in db.Audios
                //                      where i.Username.ToLower() == account.Username.ToLower()
                //                      || (i.GroupID.HasValue && i.StudentGroup.FacultyOwner.ToLower() == account.Username.ToLower())
                //                      select i).ToList();
                //// Get all of the user's websites and their students' websites.
                //List<Websites> websites = (from i in db.Websites
                //                      where i.Username.ToLower() == account.Username.ToLower()
                //                      || (i.GroupID.HasValue && i.StudentGroup.FacultyOwner.ToLower() == account.Username.ToLower())
                //                      select i).ToList();
                //// Get all of the user's files and their students' files.
                //List<Files> files = (from i in db.Files
                //                           where i.Username.ToLower() == account.Username.ToLower()
                //                           || (i.GroupID.HasValue && i.StudentGroup.FacultyOwner.ToLower() == account.Username.ToLower())
                //                           select i).ToList();
                //// Get all of the user's images and their students' images.
                //List<Images> images = (from i in db.Images
                //                     where i.Username.ToLower() == account.Username.ToLower()
                //                     || (i.GroupID.HasValue && i.StudentGroup.FacultyOwner.ToLower() == account.Username.ToLower())
                //                     select i).ToList();
                //---- END OLD CODE ----//
                
                
                
                //----Search----//
                //var searchedVideos = VideoSearcher.SearchVideos(videos, searchText);
                //var searchedAudios = AudioSearcher.SearchAudios(audios, searchText);
                //var searchedWebsites = WebSearcher.SearchWebsites(websites, searchText);
                //var searchedFiles = FileSearcher.SearchFiles(files, searchText);
                //var searchedImages = ImageSearcher.SearchImages(images, searchText);

                var searchedMediaItems = MediaSearcher.SearchAllMedia(searchText, "relevance" , false, account);
                if (searchedMediaItems.Count() > 0)
                {
                    mediaList.Visible = true;
                    mediaList.Medias = searchedMediaItems;
                    //searchLabel.Text = searchedMediaItems.Count().ToString() + " results for \"" + searchText + "\"";

                }
                else
                {
                    mediaList.Visible = false;
                    errorLabel.ForeColor = System.Drawing.Color.Black;
                    errorLabel.Text = "Sorry there were no results for your search.";
                }
                ((TextBox)Master.FindControl("searchBox")).Attributes["value"] = searchText;
                // Set the text in the search bar to the text that was searched

                

                // Output results
                //if (searchedVideos.Count() > 0)
                //{
                    
                //    videoList.Videos = searchedVideos;
                //    videoList.Visible = true;
                //}
                //else
                //{
                //    videoList.Visible = false;
                //    errorVideos.Text = "<strong>No results.</strong>";
                //}
                //if (searchedAudios.Count() > 0)
                //{

                //    audioList.Audios = searchedAudios;
                //    audioList.Visible = true;
                //}
                //else
                //{
                //    audioList.Visible = false;
                //    errorAudios.Text = "<strong>No results.</strong>";
                //}
                //if (searchedWebsites.Count() > 0)
                //{

                //    webList.Websites = searchedWebsites;
                //    webList.Visible = true;
                //}
                //else
                //{
                //    webList.Visible = false;
                //    errorWebsites.Text = "<strong>No results.</strong>";
                //}
                //if (searchedFiles.Count() > 0)
                //{

                //    fileList.Files = searchedFiles;
                //    fileList.Visible = true;
                //}
                //else
                //{
                //    fileList.Visible = false;
                //    errorFiles.Text = "<strong>No results.</strong>";
                //}
                //if (searchedImages.Count() > 0)
                //{
                //    imageList.Images = searchedImages;
                //    imageList.Visible = true;
                //}
                //else
                //{
                //    imageList.Visible = false;
                //    errorImages.Text = "<strong>No results.</strong>";
                //}
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }


    }

}
