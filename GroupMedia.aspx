<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GroupMedia.aspx.cs" Inherits="GroupMedia" %>

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
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
            <img src="images/groupsIconBlue.png" alt="groupsIcon" style="float: left" />
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload"></div>
    </a>
    <a href="Help/help.aspx" >
        <div id="help"></div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin">
            </div>
        </a>
          <a href="Logout.aspx">
        <div id="logout">
        </div>
    </a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div class="fontPlacement">
            <a href="myGroups_Student.aspx" style="text-decoration: none;">My Groups </a>>
            <asp:Label ID="lblgroupname" runat="server"></asp:Label>
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
                    <img src="images/videoIcon2.png" alt="folerIcon" />
                </td>
                <td>
                    <a runat="server" id="video" style="text-decoration:none;">Videos</a>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="images/audioIcon.png" alt="folerIcon" />
                </td>
                <td>
                    <a runat="server" id="audio" class="mymedia">Audios</a>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="images/webIcon.png" alt="folerIcon" />
                </td>
                <td>
                    <a runat="server" id="webpage" class="mymedia">WebPages</a>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="images/documentIcon.png" alt="folerIcon" />
                </td>
                <td>
                    <a runat="server" id="files" class="mymedia">Documents</a>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="images/pictureIcon.png" alt="folerIcon" />
                </td>
                <td>
                    <a runat="server" id="images" class="mymedia">Images</a>
                </td>
            </tr>
        </table>
    </div>


    <!-- Old Links Code with outdated folder icon -->
        <!-- <div>
            <a runat="server" id="A1" style="text-decoration:none;">

                <img src="images/folderIcon.png" alt="folerIcon" />Videos
            
            </a>
            <a runat="server" id="a2" class="mymedia">
                <img src="images/folderIcon.png" alt="folerIcon" />Audios</a>
            <a runat="server" id="A3" class="mymedia">
                <img src="images/folderIcon.png" alt="folerIcon" />WebPages</a>
            <a runat="server" id="A4" class="mymedia">
                <img src="images/folderIcon.png" alt="folerIcon" />Documents</a>
               <a runat="server" id="A5" class="mymedia">
                <img src="images/folderIcon.png" alt="folerIcon" />Images</a>
        </div> -->
</asp:Content>


