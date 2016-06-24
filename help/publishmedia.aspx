<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="publishmedia.aspx.cs" Inherits="help_publishmedia" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <style>
        .container .content {
            display: none;
            padding: 5px;
        }
    </style>


    <title>Northwest Cloud</title>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="../myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="../myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="../UploadMedia.aspx">
        <div id="upload"></div>
    </a>

    <a href="help.aspx">

        <div id="help">
            <img src="images/helpIconBlue.png" alt="upload" style="float: left" /></div>

    </a>


    <asp:Panel ID="ad" runat="server">
        <a href="../admin.aspx">
            <div id="admin">
            </div>
        </a>
    </asp:Panel>
    <a href="../Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <asp:Image ID="Image1" ImageUrl="~/images/folderIcon.png" runat="server" />
        </div>
        <div class="fontPlacement">
            <a href="help.aspx" style="text-decoration: none;">Help</a>
            > Publishing Media
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>


        <div id="body1" style="margin-left: 25px; margin-top: 5px; overflow-y: auto; width: 800px;">
            <p>
                One of the main reason for the development of Northwest Cloud was to give faculty a way to post multimedia content to their Northwest Online course sites easily.  Faculty can now create their multimedia content, upload to Northwest Cloud and then embed the media into their course site.  
            </p>

            <table>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia1.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>Go to ‘My Media’ to view and publish media.  Select the media type you wish to view.
                       
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia2.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>Each media type will have a page listing all the media of that type uploaded to your profile.  Each entry will be listed with its Meta data, including the title, date uploaded, number of times the media has been viewed, group designation, description, date last viewed on, transcript availability.  Below this is an ‘Edit’ link to edit this data.
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia3.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>Click the title of the media to view it.  Note the transcript link, if transcript is available, it will be below the media player.
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia4.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>Links to this media listed below on this page are for sharing, building a web page and embedding in a course site.    Copy the ‘Direct Link’ and add to an email to quickly share with someone.  If you want to put a link to the media on a web page, copy the ‘HTML Link’.  If needing to share it with a class, use the ‘Embed Code.’  Set the size desired, copy the ‘Embed Code’ link and paste the code into the ‘HTML’ mode of the Visual Editor box of a text-multimedia type content item in the Northwest Online course site.
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia5.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>Northwest Online link on the bottom of the page gives quick access to login and embed.  There are two views from Northwest Online below.  The first is in Author Mode, a Text/Multimedia content item type.  The second is the resulting page after entering and saving the embed code.
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia6.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>All we need now is a smile on this person because publishing media is easy and fun.  The publishing process of audio and images is very much the same as above.  
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia7.png" style="margin-bottom: 10px;" width="90%" />
                    </td>
                    <td>Publishing documents is a bit different.  Saving Microsoft Office documents or text documents in Northwest Cloud only allow to post links for publishing these kinds’ of documents.   By saving the Microsoft Office document as a PDF and then upload it to the cloud, then these can be embedded in a course site.
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="center">
                        <img src="helpImages/publishmedia10.png" style="margin-bottom: 50px;" width="90%" />
                    </td>
                    <td>Websites can either be shared as a link or embedded in the course site.  This is handy for Softchalk files and presentations that are in form of a website.  Give it a try and call the CITE Office if you need assistance.
                            <br />
                        <br />
                        Another benefit in using media in Northwest Cloud is if a media file is posted to several course sites and it needs to be updated, then update the media on Cloud, and it will automatically update in the course sites.
                    </td>
                </tr>

                <tr>
                    <td></td>
                </tr>
            </table>


        </div>
</asp:Content>


