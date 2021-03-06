﻿using System;
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
    /// This is a custom web control designed to display websites in an orderly manner. It does several things.
    /// 
    /// The main feature is rendering the websites into a nice list.
    /// It also supports paging and sorting.
    /// 
    /// INTERNAL IMPLEMENTATION DETAILS:
    /// It uses a hidden field to track the currently selected page and uses client-side JavaScript on
    /// some paging buttons to change the value of the hidden field, then force a postback. The server-side
    /// code can see the changes in the hidden field's value and change the current page accordingly.
    /// </summary>
    public class WebsiteList : WebControl
    {
        private LinkButton firstButton;
        private LinkButton lastButton;
        private LinkButton prevButton;
        private LinkButton nextButton;

        //private RadioButton sortByTitleRadio;
        //private RadioButton sortByDateAscendingRadio;
        //private RadioButton sortByDateDescendingRadio;
        //private RadioButton sortByNameRadio;

        private DropDownList sortByDropDownList;

        private HiddenField selectedPageField;

        public WebsiteList()
        {

            initializeChildControls();

            ItemsPerPage = 10;
            listItems = new List<WebsitesListItem>();
            EnablePaging = true;
            EnableSorting = true;
            SelectedPage = 1;
            PageDisplayCount = 10;
            DisplayAudioOwner = false;
            IncludeGroupIDInURL = false;

        }

        private void initializeChildControls()
        {
            selectedPageField = new HiddenField();
            selectedPageField.ID = "selectedPageWebsite";
            Controls.Add(selectedPageField);

            firstButton = new LinkButton();
            firstButton.Text = "&lt;&lt;";
            firstButton.ID = "firstButtonWebsite";
            firstButton.Click += new EventHandler(firstButton_Click);
            Controls.Add(firstButton);

            prevButton = new LinkButton();
            prevButton.Text = "&lt;";
            prevButton.ID = "prevButtonWebsite";
            prevButton.Click += new EventHandler(prevButton_Click);
            Controls.Add(prevButton);

            nextButton = new LinkButton();
            nextButton.Text = "&gt;";
            nextButton.ID = "nextButtonWebsite";
            nextButton.Click += new EventHandler(nextButton_Click);
            Controls.Add(nextButton);

            lastButton = new LinkButton();
            lastButton.Text = "&gt;&gt;";
            lastButton.ID = "lastButtonWebsite";
            lastButton.Click += new EventHandler(lastButton_Click);
            Controls.Add(lastButton);


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
            //sortByDateDescendingRadio.Checked = true;
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

            // Sort by dropdown
            ListItem title = new ListItem("Title", "title");
            ListItem newestFirst = new ListItem("Newest First", "newestFirst");
            ListItem oldestFirst = new ListItem("Oldest First", "oldestFirst");
            ListItem owner = new ListItem("Owner Last Name", "owner");
            sortByDropDownList = new DropDownList();
            sortByDropDownList.Items.Add(title);
            sortByDropDownList.Items.Add(newestFirst);
            sortByDropDownList.Items.Add(oldestFirst);
            sortByDropDownList.Items.Add(owner);
            sortByDropDownList.AutoPostBack = true;
            // Set the defualt to "Newest First"
            sortByDropDownList.SelectedIndex = 1;
            Controls.Add(sortByDropDownList);



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
            IEnumerable<WebsitesListItem> items = listItems;

            if (EnableSorting && items.Count() > 0)
            {
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

                // Render the sorting RadioButtons.

                writer.WriteLine("<div>");
                writer.WriteLine("<strong>&nbsp;&nbsp;Sort by:</strong>&nbsp;");

                //sortByTitleRadio.RenderControl(writer);

                //writer.WriteLine("&nbsp;&nbsp;");
                //sortByDateDescendingRadio.RenderControl(writer);
                //writer.WriteLine("&nbsp;&nbsp;");
                //sortByDateAscendingRadio.RenderControl(writer);
                //writer.WriteLine("&nbsp;&nbsp;");


                

                // Only allow user to sort by owner if we are actually displaying owner information.
                if (DisplayAudioOwner)
                {
                    //sortByNameRadio.RenderControl(writer);
                    
                }
                else
                {
                    // Remove the owner list item from the dropdown
                    sortByDropDownList.Items.RemoveAt(3);
                }
                //writer.WriteLine("");
                sortByDropDownList.RenderControl(writer);
                writer.WriteLine("</div>");



            }


            // Clip our items collection to the items for the current page.
            items = items.Skip((SelectedPage - 1) * ItemsPerPage).Take(ItemsPerPage);

            //VIDEOLISTBREAK
            //writer.WriteLine("<div class=\"videoListBreak\">");
            //writer.WriteLine("</div>");
            writer.WriteLine("<br />");


            // Render the websites.
            writer.WriteLine("<ul id=\"websiteList\">");
            foreach (var item in items)
                item.Render(writer);
            writer.WriteLine("</ul>");

            // Render the hidden field which keeps track of the current page number.
            selectedPageField.RenderControl(writer);

            if (EnablePaging && listItems.Count > ItemsPerPage)
            {
                // Clip SelectedPage if it is outside the legal bounds.
                if (SelectedPage < 1)
                    SelectedPage = 1;
                if (SelectedPage > Pages)
                    SelectedPage = Pages;

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
        }

        /// <summary>
        /// Gets or sets a value indicating whether this websiteList should support paging.
        /// </summary>
        public bool EnablePaging
        {
            get { return (bool)ViewState["enablePaging"]; }
            set { ViewState["enablePaging"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how many websites this websiteList should display per page.
        /// </summary>
        public int ItemsPerPage
        {
            get { return (int)ViewState["itemsPerPage"]; }
            set { ViewState["itemsPerPage"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating which page this websiteList is currently displaying. This
        /// has bounds-checking, so values smaller than 1 get set to 1 and values larger than the
        /// number of pages get set to the highest page number.
        /// </summary>
        public int SelectedPage
        {
            get { return int.Parse(selectedPageField.Value); }
            set { selectedPageField.Value = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets a value indicating how many pages of data this websiteList has. This is a private
        /// field used internally.
        /// </summary>
        private int Pages
        {
            get { return (int)ViewState["pages"]; }
            set { ViewState["pages"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how many page links this websiteList should have. They will be
        /// centered on SelectedPage.
        /// </summary>
        public int PageDisplayCount
        {
            get { return (int)ViewState["pageDisplayCount"]; }
            set { ViewState["pageDisplayCount"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this websiteList should display who posted the website
        /// within the website's data.
        /// </summary>
        public bool DisplayAudioOwner
        {
            get { return (bool)ViewState["DisplayAudioOwner"]; }
            set { ViewState["DisplayAudioOwner"] = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the links to videos will include a query parameter for
        /// the group ID
        /// </summary>
        public bool IncludeGroupIDInURL
        {
            get { return (bool)ViewState["includeGroupIDInURL"]; }
            set { ViewState["includeGroupIDInURL"] = value; }

        }

        /// <summary>
        /// Gets or sets a value indicating whether this websiteList should display radio buttons to allow
        /// the user to sort its websites.
        /// </summary>
        public bool EnableSorting
        {
            get { return (bool)ViewState["enableSorting"]; }
            set { ViewState["enableSorting"] = value; }
        }

        /// <summary>
        /// Sets a collection of website to be the data source for this website list.
        /// </summary>
        public IEnumerable<Websites> Websites
        {
            set
            {
                var items = listItems;
                items.Clear();
                items.AddRange(value.Select(i => new WebsitesListItem(i, DisplayAudioOwner, IncludeGroupIDInURL)));
                listItems = items;

                Pages = (int)Math.Ceiling(value.Count() / (double)ItemsPerPage);
            }
        }

        /// <summary>
        /// Gets or sets a collection of websiteListItems used internally for rendering.
        /// </summary>
        private List<WebsitesListItem> listItems
        {
            get { return (List<WebsitesListItem>)ViewState["listItems"]; }
            set { ViewState["listItems"] = value; }
        }

        /// <summary>
        /// Private class containing data used by the websiteList to render each audio.
        /// </summary>
        [Serializable]
        private class WebsitesListItem
        {
            public String title;
            public String description;
            public DateTime datePosted;
            public String webID;
            public int numOfHits;
            public String Username;
            // public string Uploadedby;
            public String lastHit;
            // public String facultyOwnerId;
            public String copyRight;
            public int? groupId;
            public bool includeGroupIDInURL;

            private String _firstName;
            private String _lastName;

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

            public WebsitesListItem(Websites wid, bool displayPostedBy, bool includeGroupIDInURL)
            {
                title = wid.Title;

                description = wid.Description.Replace("\n", "<br />");
                webID = wid.WebID;
                numOfHits = wid.Views;
                datePosted = wid.DatePosted;
                Username = wid.Username;
                //Uploadedby = vid.UploadedBy;
              //  copyRight = wid.Copyright;
                this.includeGroupIDInURL = includeGroupIDInURL;

                if (wid.GroupID != null)
                {
                    groupId = wid.GroupID;
                }
                else
                {
                    groupId = null;
                }
                if (wid.StudentGroup != null)
                {
                    //facultyOwnerId = vid.StudentGroup.FacultyOwner;
                }

                this.displayPostedBy = displayPostedBy;
                if (wid.LastView != null)
                {
                    lastHit = wid.LastView.ToString();
                }
                else
                {
                    lastHit = "No views";
                }
            }

            public void Render(HtmlTextWriter writer)
            {


                var server = HttpContext.Current.Server;
                string file = server.MapPath("convertedWeb\\") + webID;
                bool WebConverted = File.Exists(file);

                //temporary until iis can be configured to access file system
                WebConverted = true;


                writer.WriteLine("<li>");


                //if (WebConverted)
                //{

                    String plural = "";
                    if (numOfHits != 1)
                        plural = "s";
                    // Include the group id the url params
                    if (this.includeGroupIDInURL)
                    {
                        writer.WriteLine("<a href=\"OpenWebsite.aspx?groupID={0}&wid={1}\"><img class=\"listThumbnail\" src=\"/images/largeIcons/websiteIconLarge.png\"/></a>", groupId, webID);
                        writer.WriteLine("<p>");
                        writer.WriteLine(String.Format("<a href=\"OpenWebsite.aspx?groupID=" + this.groupId + "&wid={0}\">{1}</a> - {2} ({3} view{4})",
                            webID,
                            title,
                            datePosted.ToShortDateString(),
                            numOfHits,
                            plural));
                    }
                    else
                    {
                        writer.WriteLine("<a href=\"OpenWebsite.aspx?wid={0}\"><img class=\"listThumbnail\" src=\"/images/largeIcons/websiteIconLarge.png\"/></a>", webID);
                        writer.WriteLine("<p>");
                        writer.WriteLine(String.Format("<a href=\"OpenWebsite.aspx?wid={0}\">{1}</a> - {2} ({3} view{4})",
                            webID,
                            title,
                            datePosted.ToShortDateString(),
                            numOfHits,
                            plural));
                    }
                //}
                //else
                //{
                //    writer.WriteLine("<p>");
                //    String plural = "";
                //    if (numOfHits != 1)
                //        plural = "s";
                //    writer.WriteLine(String.Format("{1}- {2} ({3} view{4})",
                //        webID,
                //        title,
                //        datePosted.ToShortDateString(),
                //        numOfHits,
                //        plural));
                //}

                if (displayPostedBy)
                {
                    writer.WriteLine(String.Format("<br /><b>Owned by:</b> {0} {1} ({2})",
                                                FirstName,
                                                LastName, Username));
                    //if (!string.IsNullOrEmpty(Uploadedby))
                    //{
                    //    if (Username != Uploadedby)
                    //    {
                    //        DomainAccount acc = new DomainAccount(Uploadedby);
                    //        writer.WriteLine(String.Format("<br /><b>Uploaded by:</b> {0} {1}",
                    //                                     acc.FirstName,
                    //                                      acc.LastName));
                    //    }
                    //}

                }
                else
                {
                    //if (!string.IsNullOrEmpty(Uploadedby))
                    //{
                    //    if (Username != Uploadedby)
                    //    {
                    //        DomainAccount acc = new DomainAccount(Uploadedby);
                    //        writer.WriteLine(String.Format("<br /><b>Uploaded by:</b> {0} {1}",
                    //                                     acc.FirstName,
                    //                                      acc.LastName));
                    //    }
                    //}
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

                if (WebConverted)
                {
                    writer.WriteLine(String.Format("<br /><b>Description:</b> {0}",
                                              description));
                    writer.WriteLine(String.Format("<br /><b>Last View:</b> {0}",
                                                lastHit));
               

                    // student can edit thier website only. 'Edit' link is visible only to the faculty.
                    DomainAccount account = (DomainAccount)HttpContext.Current.Session["account"];
                    if (account.Username.ToLower() == Username.ToLower() || !(account.OU.Equals(OrganizationalUnit.StudentUsers)))
                    {
                        writer.WriteLine(String.Format("<br /><a href=\"EditWebsite.aspx?wid={0}\">Edit</a>",
                                            webID));
                    }
                }
                else
                {
                    writer.WriteLine("<br /><b>This website is currently being converted.</b>");
                }





                writer.WriteLine("</p><div class=\"clear\" />");
                writer.WriteLine("</li>");
            }
        }
    }
}