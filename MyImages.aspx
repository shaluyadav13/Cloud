<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyImages.aspx.cs" Inherits="MyImages" %>
<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leftNav" Runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
            <img id="myMediaIcon" runat="server" src="images/myVideosIconBlue.png" alt="myMediaIcon" style="float: left" />
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups" runat="server" clientidmode="static">
            <img id="groupsIcon" runat="server" src="images/groupsIconBlue.png" alt="groupsIcon" style="float: left" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div class="fontPlacement" id="userImages" runat="server">
            <a href="myMedia.aspx" style="text-decoration: none;">MyMedia </a>> My Images
        </div>
        <div class="fontPlacement" id="groupImages" runat="server">
            <a href="myGroups_Student.aspx" style="text-decoration: none;">My Groups&nbsp> </a><a style="text-decoration: none;" runat="server" ID="groupname"><asp:Label ID="lblgroupname" runat="server"></asp:Label></a><span> > Images</span>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <h2>
        <asp:Label ID="welcomeNote" runat="server" />
    </h2>

    <br />
    <div>
        <div style="margin-left: 20px">
            <asp:Label ID="noImagesLabel" runat="server" Text="You do not have any images." />
        </div>
        <local:ImageList ID="imageList" runat="server" DisplayAudioOwner="False" EnablePaging="True"
            ItemsPerPage="10" PageDisplayCount="15" EnableSorting="true" />

    </div>

</asp:Content>