<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myGroups_Student.aspx.cs" Inherits="pages_myGroup_Student" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />

    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <link href='http://fonts.googleapis.com/css?family=Lato' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="js/master.js"></script>

    <style>
        .grpMessage {
            padding-left:1%;
        }
    </style>
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
    <a href="Help/help.aspx">
        <div id="help"></div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin"></div>
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
        <div class="fontPlacement">
            <a href="myGroups_Student.aspx" style="text-decoration: none;">My Groups</a><a style="text-decoration: none;" runat="server" ID="groupname"><asp:Label ID="lblgroupname" runat="server"></asp:Label></a>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>
        <asp:Label ID="welcomeNote" runat="server" />
    </h2>

    <br />

    <div>

        <asp:Label ID="MyGroupsHeader" runat="server" />

        <asp:Label ID="Dispalylist" runat="server" />
        <asp:Label ID="lblCreatedGroups" runat="server" />
        <asp:Label ID="lblDisplayList" runat="server" />

        <local:VideoList ID="videoList" runat="server" IncludeGroupIDInURL="True" DisplayVideoOwner="True" EnablePaging="True"
            EnableSorting="True" ItemsPerPage="10" PageDisplayCount="10" SelectedPage="1" />
        <asp:Label ID="grpMessage"  runat="server" Text="Create and edit groups under 'Admin'" Font-Bold="True" CssClass="grpMessage" ></asp:Label>

    </div>

</asp:Content>
