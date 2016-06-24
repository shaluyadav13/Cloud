<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="myMedia.aspx.cs" Inherits="myMedia" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <link href='http://fonts.googleapis.com/css?family=Lato' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="js/master.js"></script>
    <title>Northwest Cloud</title>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
            <img src="images/myVideosIconBlue.png" alt="myVideosIcon" style="float: left" />
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload"></div>
    </a>
   <a href="Help/help.aspx">
        <div id="help"></div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin">
            </div>
        </a>
    </asp:Panel>
    <a href="Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div class="fontPlacement" style="color: #243E77; font-weight: bold;">
            MyMedia
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Label ID="welcomeNote" runat="server" />
    </h2>

    <br />
    <div>
        <table style="margin-left: 8px;">
            <tr>
                <td>
                    <a href="myVideos.aspx" class="mymedia">
                        <img src="images/videoIcon2.png" alt="folerIcon" /></a>
                </td>
                <td>
                    <a href="myVideos.aspx" class="mymedia">My Video</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="myAudio.aspx" class="mymedia">
                        <img src="images/audioIcon.png" alt="folerIcon" /></a>
                </td>
                <td>
                    <a href="myAudio.aspx" class="mymedia">My Audio</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="MyWebPages.aspx" class="mymedia">
                        <img src="images/webIcon.png" alt="folerIcon" /></a>
                </td>
                <td>
                    <a href="MyWebPages.aspx" class="mymedia">My WebPages</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="MyFiles.aspx" class="mymedia">
                        <img src="images/documentIcon.png" alt="folerIcon" /></a>
                </td>
                <td>
                    <a href="MyFiles.aspx" class="mymedia">My Documents</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="MyImages.aspx" class="mymedia">
                        <img src="images/pictureIcon.png" alt="folerIcon" /></a>
                </td>
                <td>
                    <a href="MyImages.aspx" class="mymedia">My Images</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

