<%@ Page Title="Search - Northwest Video" Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
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
            <img src="images/myVideosIconBlue.png" alt="myMediaIcon" style="float: left" />
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
        <div class="fontPlacement" style="font-weight: bold;">
            
            <asp:Label ID="searchLabel" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Label ID="welcomeNote" runat="server"></asp:Label>
    </h2>


    <div>
        <br />
        <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        <local:MediaList ID="mediaList" DisplayMediaOwner="true" EnablePaging="true" EnableSorting="true"
            SelectedPage="1" ItemsPerPage="10" PageDisplayCount="5" runat="server"></local:MediaList>

<%--        <local:VideoList ID="videoList" DisplayVideoOwner="True" EnablePaging="True"
            EnableSorting="False" runat="server" SelectedPage="1" ItemsPerPage="10" PageDisplayCount="3" />--%>

    </div>
</asp:Content>
