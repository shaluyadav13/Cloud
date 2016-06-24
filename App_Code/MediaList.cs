using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using Cite.DomainAuthentication;
using System.Web.UI;
using System.IO;


namespace NorthwestVideo.Controls
{
    /// <summary>
    /// This is a custom web control designed to display media items in an orderly manner. It does several things.
    /// 
    /// The main feature is rendering the media items into a nice list.
    /// It also supports paging and sorting.
    /// 
    /// INTERNAL IMPLEMENTATION DETAILS:
    /// It uses a hidden field to track the currently selected page and uses client-side JavaScript on
    /// some paging buttons to change the value of the hidden field, then force a postback. The server-side
    /// code can see the changes in the hidden field's value and change the current page accordingly.
    /// </summary>
    public class MediaList : WebControl
    {
        private LinkButton firstButton;
        private LinkButton lastButton;
        private LinkButton prevButton;
        private LinkButton nextButton;

        //private RadioButton sortBySearchRelevanceRadio;
        //private RadioButton sortByTitleRadio;
        //private RadioButton sortByDateAscendingRadio;
        //private RadioButton sortByDateDescendingRadio;
        //private RadioButton sortByNameRadio;

        private DropDownList sortByDropDownList;

        private DropDownList filterByMediaType;

        private HiddenField selectedPageField;

        public MediaList()
        {

            initializeChildControls();

            ItemsPerPage = 10;
            listItems = new List<MediaListItem>();
            EnablePaging = true;
            EnableSorting = true;
            SelectedPage = 1;
            PageDisplayCount = 10;
            DisplayMediaOwner = false;
            IncludeGroupIDInURL = false;
        }

        private void initializeChildControls()
        {
            selectedPageField = new HiddenField();
            selectedPageField.ID = "selectedPageMedia";
            Controls.Add(selectedPageField);

            firstButton = new LinkButton();
            firstButton.Text = "&lt;&lt;";
            firstButton.ID = "firstButtonMedia";
            firstButton.Click += new EventHandler(firstButton_Click);
            Controls.Add(firstButton);

            prevButton = new LinkButton();
            prevButton.Text = "&lt;";
            prevButton.ID = "prevButtonMedia";
            prevButton.Click += new EventHandler(prevButton_Click);
            Controls.Add(prevButton);

            nextButton = new LinkButton();
            nextButton.Text = "&gt;";
            nextButton.ID = "nextButtonMedia";
            nextButton.Click += new EventHandler(nextButton_Click);
            Controls.Add(nextButton);

            lastButton = new LinkButton();
            lastButton.Text = "&gt;&gt;";
            lastButton.ID = "lastButtonMedia";
            lastButton.Click += new EventHandler(lastButton_Click);
            Controls.Add(lastButton);



            //sortBySearchRelevanceRadio = new RadioButton();
            //sortBySearchRelevanceRadio.Text = "Relevance";
            //sortBySearchRelevanceRadio.GroupName = "sortGroup";
            //sortBySearchRelevanceRadio.Checked = true;
            //sortBySearchRelevanceRadio.AutoPostBack = true;
            //Controls.Add(sortBySearchRelevanceRadio);

            //sortByDateAscendingRadio = new RadioButton();
            //sortByDateAscendingRadio.Text = "Oldest First";
            //sortByDateAscendingRadio.GroupName = "sortGroup";
            ////sortByDateAscendingRadio.ID = this.ID + "_sortByDateAscendingRadio" + Guid.NewGuid().ToString("N");
            //sortByDateAscendingRadio.AutoPostBack = true;
            //Controls.Add(sortByDateAscendingRadio);





            //sortByDateDescendingRadio = new RadioButton();
            //sortByDateDescendingRadio.Text = "Newest First";
            //sortByDateDescendingRadio.GroupName = "sortGroup";
            ////sortByDateDescendingRadio.ID = this.ID + "_sortByDateDescendingRadio" + Guid.NewGuid().ToString("N");
            ////sortByDateDescendingRadio.Checked = true;
            //sortByDateDescendingRadio.AutoPostBack = true;
            //Controls.Add(sortByDateDescendingRadio);


            //sortByTitleRadio = new RadioButton();
            //sortByTitleRadio.Text = "Title";
            //sortByTitleRadio.GroupName = "sortGroup";
            ////sortByTitleRadio.ID = this.ID + "_sortByTitleRadio" + Guid.NewGuid().ToString("N");
            //sortByTitleRadio.AutoPostBack = true;
            //Controls.Add(sortByTitleRadio);


            //sortByNameRadio = new RadioButton();
            //sortByNameRadio.Text = "Owner";
            //sortByNameRadio.GroupName = "sortGroup";
            ////sortByNameRadio.ID = this.ID + "_sortByNameRadio" + Guid.NewGuid().ToString("N");
            //sortByNameRadio.AutoPostBack = true;
            //Controls.Add(sortByNameRadio);

            ListItem relevance = new ListItem("Relevance", "relevance");
            ListItem title = new ListItem("Title", "title");
            ListItem newestFirst = new ListItem("Newest First", "newestFirst");
            ListItem oldestFirst = new ListItem("Oldest First", "oldestFirst");
            ListItem owner = new ListItem("Owner Last Name", "owner");
            sortByDropDownList = new DropDownList();
            sortByDropDownList.Items.Add(relevance);
            sortByDropDownList.Items.Add(title);
            sortByDropDownList.Items.Add(newestFirst);
            sortByDropDownList.Items.Add(oldestFirst);
            sortByDropDownList.Items.Add(owner);
            sortByDropDownList.AutoPostBack = true;
            Controls.Add(sortByDropDownList);
            

            ListItem all = new ListItem("All", "all");
            ListItem video = new ListItem("Video", "video");
            ListItem audio = new ListItem("Audio", "audio");
            ListItem website = new ListItem("Website", "website");
            ListItem document = new ListItem("Document", "file");
            ListItem image = new ListItem("Image", "image");
            filterByMediaType = new DropDownList();
            filterByMediaType.Items.Add(all);
            filterByMediaType.Items.Add(video);
            filterByMediaType.Items.Add(audio);
            filterByMediaType.Items.Add(website);
            filterByMediaType.Items.Add(document);
            filterByMediaType.Items.Add(image);
            filterByMediaType.AutoPostBack = true;
            Controls.Add(filterByMediaType);

            //sortByDropDownList.Items

        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            SelectedPage = Pages;
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            SelectedPage = Math.Min(Pages, SelectedPage + 1);
        }
        private void prevButton_Click(object sender, EventArgs e)
        {
            SelectedPage = Math.Max(1, SelectedPage - 1);
        }
        private void firstButton_Click(object sender, EventArgs e)
        {
            SelectedPage = 1;
        }

        /// <summary>
        /// This method is responsible for generating HTML output which will represent
        /// this WebControl.
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            IEnumerable<MediaListItem> items = listItems;

            if (EnableSorting && items.Count() > 0)
            {
                // Sort media items
                if (sortByDropDownList.SelectedItem.Value == "title")
                {
                    items = items.OrderBy(i => i.title);

                }
                else if (sortByDropDownList.SelectedItem.Value == "owner")
                {
                    items = items.OrderBy(i => i.LastName);

                }
                else if (sortByDropDownList.SelectedItem.Value == "oldestFirst")
                {
                    items = items.OrderBy(i => i.datePosted);

                }
                else if (sortByDropDownList.SelectedItem.Value == "newestFirst")
                {
                    items = items.OrderBy(i => i.datePosted).Reverse();
                }


                // Filter by media type
                if (filterByMediaType.SelectedValue == "all")
                {
                    // Do nothing, return all results
                }
                if (filterByMediaType.SelectedValue == "video")
                {
                    items = items.Where(x => x.mediaType == "video");
                }
                else if (filterByMediaType.SelectedValue == "audio")
                {
                    items = items.Where(x => x.mediaType == "audio");
                }
                else if (filterByMediaType.SelectedValue == "website")
                {
                    items = items.Where(x => x.mediaType == "website");
                }
                else if (filterByMediaType.SelectedValue == "file")
                {
                    items = items.Where(x => x.mediaType == "file");
                }
                else if (filterByMediaType.SelectedValue == "image")
                {
                    items = items.Where(x => x.mediaType == "image");
                }
                else
                {
                    // Do nothing, return all results
                }

                // Render the sorting RadioButtons.
                writer.WriteLine("<div>");
                writer.WriteLine("<strong>&nbsp;&nbsp;Sort by:&nbsp;</strong>");
                
                //sortBySearchRelevanceRadio.RenderControl(writer);
                //writer.WriteLine("&nbsp;&nbsp;");
                //sortByTitleRadio.RenderControl(writer);

                //writer.WriteLine("&nbsp;&nbsp;");
                //sortByDateDescendingRadio.RenderControl(writer);
                //writer.WriteLine("&nbsp;&nbsp;");
                //sortByDateAscendingRadio.RenderControl(writer);
                //writer.WriteLine("&nbsp;&nbsp;");

                //// Only allow user to sort by owner if we are actually displaying owner information.
                //if (DisplayMediaOwner)
                //{
                //    sortByNameRadio.RenderControl(writer);
                //    writer.WriteLine("&nbsp;&nbsp;");
                //}

                // Render the dropdown
                sortByDropDownList.RenderControl(writer);

                writer.WriteLine("<strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media Type:&nbsp;</strong>");
                filterByMediaType.RenderControl(writer);
                writer.WriteLine("</div>");

            }

            // Get the number of total items with the filters selected
            int numberOfResults = items.Count();

            // Render the hidden field which keeps track of the current page number.
            selectedPageField.RenderControl(writer);

            if (EnablePaging && numberOfResults > ItemsPerPage)
            {
                Pages = (int)Math.Ceiling(numberOfResults / (double)ItemsPerPage);
                // Clip SelectedPage if it is outside the legal bounds.
                if (SelectedPage < 1)
                    SelectedPage = 1;
                if (SelectedPage > Pages)
                    SelectedPage = Pages;

                // Clip our items collection to the items for the current page.
                items = items.Skip((SelectedPage - 1) * ItemsPerPage).Take(ItemsPerPage);

                writer.WriteLine("<br />");

                // Render the media items
                writer.WriteLine("<ul id=\"mediaList\">");
                foreach (var item in items)
                    item.Render(writer);
                writer.WriteLine("</ul>");




                // Determine which pages we want to display links for.
                int minPage = Math.Max(1, SelectedPage - (PageDisplayCount / 2));
                int maxPage = Math.Min(Pages, SelectedPage + (PageDisplayCount / 2));

                // Begin outputting the paging links.
                writer.WriteLine("<div class=\"PaginationControls\">");
                writer.WriteLine("&nbsp;&nbsp;");
                //firstButton.RenderControl(writer);
                writer.WriteLine("&nbsp;&nbsp;&nbsp;");
                prevButton.RenderControl(writer);
                writer.WriteLine("&nbsp;&nbsp;&nbsp;");

                // Output the dynamically-generated paging links.
                for (int i = minPage; i <= maxPage; i++)
                {
                    if (i == SelectedPage)
                    {
                        // Simply render the page number if this is the current page. Otherwise we'll
                        // generate a hyperlink.
                        writer.WriteLine(i + "&nbsp;&nbsp;&nbsp;");
                    }
                    else
                    {
                        // This JavaScript snippet looks for the HiddenField we've called selectedPageField here and sets
                        // its value to the page number for this button. After doing so it causes a PostBack, which will
                        // force this VideoList to re-render itself with the new paging selection.
                        writer.WriteLine("<a href=\"javascript:document.getElementById('{0}').value='{1}';{2};\">{1}</a>&nbsp;&nbsp;&nbsp;",
                                         selectedPageField.ClientID,
                                         i,
                                         Page.GetPostBackEventReference(this));
                    }
                }

                nextButton.RenderControl(writer);
                writer.WriteLine("&nbsp;&nbsp;&nbsp;");
                //lastButton.RenderControl(writer);
                writer.WriteLine("</div>");
                writer.WriteLine("<br/><br/><br/><br/>");

            }
            else
            {
                writer.WriteLine("<br />");

                // Render the media items
                writer.WriteLine("<ul id=\"mediaList\">");
                foreach (var item in items)
                    item.Render(writer);
                writer.WriteLine("</ul>");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this MediaList should support paging.
        /// </summary>
        public bool EnablePaging
        {
            get { return (bool)ViewState["enablePaging"]; }
            set { ViewState["enablePaging"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how many audios this MediaList should display per page.
        /// </summary>
        public int ItemsPerPage
        {
            get { return (int)ViewState["itemsPerPage"]; }
            set { ViewState["itemsPerPage"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating which page this MediaList is currently displaying. This
        /// has bounds-checking, so values smaller than 1 get set to 1 and values larger than the
        /// number of pages get set to the highest page number.
        /// </summary>
        public int SelectedPage
        {
            get { return int.Parse(selectedPageField.Value); }
            set { selectedPageField.Value = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets a value indicating how many pages of data this MediaList has. This is a private
        /// field used internally.
        /// </summary>
        private int Pages
        {
            get { return (int)ViewState["pages"]; }
            set { ViewState["pages"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how many page links this MediaList should have. They will be
        /// centered on SelectedPage.
        /// </summary>
        public int PageDisplayCount
        {
            get { return (int)ViewState["pageDisplayCount"]; }
            set { ViewState["pageDisplayCount"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this MediaList should display who posted the video
        /// within the media's data.
        /// </summary>
        public bool DisplayMediaOwner
        {
            get { return (bool)ViewState["DisplayMediaOwner"]; }
            set { ViewState["DisplayMediaOwner"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the links to media items will include a query parameter for
        /// the group ID
        /// </summary>
        public bool IncludeGroupIDInURL
        {
            get { return (bool)ViewState["includeGroupIDInURL"]; }
            set { ViewState["includeGroupIDInURL"] = value; }

        }

        /// <summary>
        /// Gets or sets a value indicating whether this MediaoList should display radio buttons to allow
        /// the user to sort its media items.
        /// </summary>
        public bool EnableSorting
        {
            get { return (bool)ViewState["enableSorting"]; }
            set { ViewState["enableSorting"] = value; }
        }

        /// <summary>
        /// Sets a collection of media items to be the data source for this media list.
        /// </summary>
        public IEnumerable<MediaItem> Medias
        {
            set
            {
                var items = listItems;
                items.Clear();
                items.AddRange(value.Select(i => new MediaListItem(i, DisplayMediaOwner, IncludeGroupIDInURL)));
                listItems = items;

                Pages = (int)Math.Ceiling(value.Count() / (double)ItemsPerPage);
            }
        }

        /// <summary>
        /// Gets or sets a collection of MediaListItems used internally for rendering.
        /// </summary>
        private List<MediaListItem> listItems
        {
            get { return (List<MediaListItem>)ViewState["listItems"]; }
            set { ViewState["listItems"] = value; }
        }

        /// <summary>
        /// Private class containing data used by the AudioList to render each audio.
        /// </summary>
        [Serializable]
        private class MediaListItem
        {
            public String title;
            public String description;
            public DateTime datePosted;
            public String id;
            public int numOfHits;
            public String Username;
            public String lastHit;
            public String copyRight;
            public int? groupId;
            public bool includeGroupIDInURL;
            public string mediaType;

            private String _firstName;
            private String _lastName;
            private bool transcript;
            public String FirstName
            {
                get
                {
                    // Don't look up name info until it's actually needed.
                    if (String.IsNullOrEmpty(_firstName))
                    {
                        DomainAccount acc = new DomainAccount(Username);
                        _firstName = acc.FirstName;
                        _lastName = acc.LastName;
                    }
                    return _firstName;
                }
            }

            public String LastName
            {
                get
                {
                    // Don't look up name info until it's actually needed.
                    if (String.IsNullOrEmpty(_lastName))
                    {
                        DomainAccount acc = new DomainAccount(Username);
                        _firstName = acc.FirstName;
                        _lastName = acc.LastName;
                    }
                    return _lastName;
                }
            }

            private bool displayPostedBy;

            public MediaListItem(MediaItem mi, bool displayPostedBy, bool includeGroupIDInURL)
            {
                title = mi.Title;

                description = mi.Description.Replace("\n", "<br />");
                id = mi.ID;
                numOfHits = mi.NumOfHits;
                datePosted = mi.DatePosted;
                Username = mi.Username;
                this.displayPostedBy = displayPostedBy;
                this.includeGroupIDInURL = includeGroupIDInURL;
                this.mediaType = mi.MediaType;

                if (mi.GroupID != null)
                {
                    groupId = mi.GroupID;
                }
                else
                {
                    groupId = null;
                }

                this.displayPostedBy = displayPostedBy;
                if (mi.LastHit != null)
                {
                    lastHit = mi.LastHit.ToString();
                }
                else
                {
                    lastHit = "No views";
                }
            }

            public void Render(HtmlTextWriter writer)
            {
                string iconPath = "images/largeIcons/" + this.mediaType + "IconLarge" + ".png";
                 //imageTag = "<a href=\"PlayVideo.aspx?vid=" + VideoID + "\">" + "<img class=\"listThumbnail\" src=\"" + ThumbnailPath + "\"/>" + "</a>";

                //var encoded = HttpUtility.HtmlEncode(imageTag);


                //-----------------------------------------------------------
                
                String plural = "";
                if (this.numOfHits != 1)
                    plural = "s";

                // Check what type of media it is and the give it the appropriate url
                string baseLink;
                string imageTag = "";
                switch(this.mediaType)
                {
                    case "video":
                        baseLink = String.Format("<a href=\"PlayVideo.aspx?vid={0}\">{1}</a> - {2} ({3} view{4})",
                        id,
                        title,
                        datePosted.ToShortDateString(),
                        numOfHits,
                        plural);
                        imageTag = String.Format("<a href=\"PlayVideo.aspx?vid={0}\">" + "<img class=\"listThumbnail\" src=\"" + iconPath + "\"/>" + "</a>", this.id);
                        break;
                    case "audio":
                        baseLink = String.Format("<a href=\"playAudio.aspx?aid={0}\">{1}</a> - {2} ({3} view{4})",
                        id,
                        title,
                        datePosted.ToShortDateString(),
                        numOfHits,
                        plural);
                        imageTag = String.Format("<a href=\"playAudio.aspx?aid={0}\">" + "<img class=\"listThumbnail\" src=\"" + iconPath + "\"/>" + "</a>", this.id);
                        break;
                    case "website":
                        baseLink = String.Format("<a href=\"OpenWebsite.aspx?wid={0}\">{1}</a> - {2} ({3} view{4})",
                        id,
                        title,
                        datePosted.ToShortDateString(),
                        numOfHits,
                        plural);
                        imageTag = String.Format("<a href=\"OpenWebsite.aspx?wid={0}\">" + "<img class=\"listThumbnail\" src=\"" + iconPath + "\"/>" + "</a>", this.id);
                        break;
                    case "file":
                        baseLink = String.Format("<a href=\"OpenFile.aspx?fid={0}\">{1}</a> - {2} ({3} view{4})",
                        id,
                        title,
                        datePosted.ToShortDateString(),
                        numOfHits,
                        plural);
                        imageTag = String.Format("<a href=\"OpenFile.aspx?fid={0}\">" + "<img class=\"listThumbnail\" src=\"" + iconPath + "\"/>" + "</a>", this.id);
                        break;
                    case "image":
                        baseLink = String.Format("<a href=\"OpenImage.aspx?imageid={0}\">{1}</a> - {2} ({3} view{4})",
                        id,
                        title,
                        datePosted.ToShortDateString(),
                        numOfHits,
                        plural);
                        imageTag = String.Format("<a href=\"OpenImage.aspx?imageid={0}\">" + "<img class=\"listThumbnail\" src=\"" + iconPath + "\"/>" + "</a>", this.id);
                        break;
                    default:
                        baseLink = "There was an error loading the link.";
                        break;
                        
                }

                writer.WriteLine("<li>");
                writer.WriteLine(imageTag);
                writer.WriteLine("<p>");


                writer.WriteLine(baseLink);

                writer.WriteLine(String.Format("<br /><b>Type:</b> {0}", char.ToUpper(this.mediaType[0]) + this.mediaType.Substring(1)));

                if (displayPostedBy)
                {
                    writer.WriteLine(String.Format("<br /><b>Owned by:</b> {0} {1}",
                                                FirstName,
                                                LastName));
                }

                if (groupId == null)
                {
                    writer.WriteLine(String.Format("<br /><b>Group:</b> {0}",
                                               "No Group"));
                }
                else
                {
                    DBDataContext db = DBDataContext.CreateInstance();
                    var stuGroup = (from i in db.StudentGroups
                                    where i.GroupID == groupId
                                    select i).Single();
                    writer.WriteLine(String.Format("<br /><b>Group:</b> {0}",
                                               stuGroup.GroupName));
                }


                    writer.WriteLine(String.Format("<br /><b>Description:</b> {0}",
                                              description));
                    writer.WriteLine(String.Format("<br /><b>Last Listened on:</b> {0}",
                                                lastHit));

                    // Display edit link if they own the video or if they are faculty or staff
                    DomainAccount account = (DomainAccount)HttpContext.Current.Session["account"];
                    if (account.Username.ToLower() == Username.ToLower() || !(account.OU.Equals(OrganizationalUnit.StudentUsers)))
                    {
                        // Check what type of media it is and the give it the appropriate edit link
                        string editMediaLink;
                        switch(this.mediaType)
                        {
                            case "video":
                                editMediaLink = String.Format("<a href=\"EditVideo.aspx?vid={0}\">Edit</a>", this.id);
                                break;
                            case "audio":
                                editMediaLink = String.Format("<a href=\"EditAudio.aspx?aid={0}\">Edit</a>", this.id);
                                break;
                            case "website":
                                editMediaLink = String.Format("<a href=\"EditWebsite.aspx?id={0}\">Edit</a>", this.id);
                                break;
                            case "file":
                                editMediaLink = String.Format("<a href=\"EditFile.aspx?fid={0}\">Edit</a>", this.id);
                                break;
                            case "image":
                                editMediaLink = String.Format("<a href=\"EditImage.aspx?imageid={0}\">Edit</a>", this.id);
                                break;
                            default:
                                editMediaLink = "There was an error loading the link.";
                                break;
                        
                        }
                        writer.WriteLine("<br />");
                        writer.WriteLine(editMediaLink);
                    }
                writer.WriteLine("</p><div class=\"clear\" />");
                writer.WriteLine("</li>");
                // End of media item
            }
        }
    }
}