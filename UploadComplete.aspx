<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadComplete.aspx.cs" Inherits="pages_UploadComplete" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>

    <title>Northwest Cloud</title>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups" >
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload">
            <img src="images/uploadIconBlue.png" alt="upload" style="float: left" />
        </div>
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
    <div id="navText">
        Uploading
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <h3 id="video" runat="server">Your video has been uploaded to our system and is now being converted. It should 
        become available shortly. It will be avaliable in the "My Media" tab.</h3>
          <h3 id="audio" runat="server">Your audio has been uploaded to our system and is now being converted. It should 
        become available shortly. It will be avaliable in the "My Media" tab.</h3>
          <h3 id="website" runat="server">Your website has been uploaded to our system and is now being converted. It should 
        become available shortly. It will be avaliable in the "My Media" tab.</h3>
          <h3 id="file" runat="server">Your file has been uploaded to our system. It should 
        become available shortly. It will be avaliable in the "My Media" tab.</h3>
        <h3 id="images" runat="server">Your image has been uploaded to our system. It should 
        become available shortly. It will be avaliable in the "My Media" tab.</h3>
    </div>
</asp:Content>
