<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="myVideos.aspx.cs" Inherits="myVideos" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div class="fontPlacement" id="userVideos" runat="server">
            <a href="myMedia.aspx" style="text-decoration: none;">MyMedia </a>> My Videos
        </div>
        <div class="fontPlacement" id="groupVideos" runat="server">
            <a href="myGroups_Student.aspx" style="text-decoration: none;">My Groups&nbsp> </a><a style="text-decoration: none;" runat="server" id="groupname">
                <asp:Label ID="lblgroupname" runat="server"></asp:Label></a><span> > Videos</span>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>
        <asp:Label ID="welcomeNote" runat="server" />
    </h2>

    <br />
    <div>
        <div style="margin-left: 20px">
            <asp:Label ID="noVideosLabel" runat="server" Text="You do not have any videos." />
        </div>
        <local:VideoList ID="videoList" runat="server" DisplayVideoOwner="false" EnablePaging="True"
            ItemsPerPage="10" PageDisplayCount="15" EnableSorting="true" />

    </div>

</asp:Content>





