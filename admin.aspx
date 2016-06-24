<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin" MasterPageFile="~/MasterPage.master" %>





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
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
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
                <img src="images/adminIconBlue.png" alt="myVideosIcon" style="float: left" />
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
        Administrator
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="body2">
        <asp:Panel ID="pnladmin" runat="server">
            <a href="allVideos.aspx">
                <p>Browse All Videos</p>
            </a>
            <a href="adminSearch.aspx">
                <p>Search All Videos</p>
            </a>
            <a href="adminAudioSearch.aspx">
                <p>Search All Audios</p>
            </a>
            <a href="adminWebSearch.aspx">
                <p>Search All Websites</p>
            </a>
            <a href="adminFileSearch.aspx">
                <p>Search All Documents</p>
            </a>
            <a href="AdminImageSearch.aspx">
                <p>Search All Images</p>
            </a>
            <a href="Administrators.aspx">
                <p>Administrators</p>
            </a>
            <a href="SwitchLogin.aspx">
                <p>Switch Login</p>
            </a>

            <a href="EditStudentGroup.aspx">
                <p>Create & Edit Groups</p>
            </a>
            <a href="status.aspx">
                <p>Status</p>
            </a>
            <a href="errorLog.aspx">
                <p>Error Log</p>
            </a>
        </asp:Panel>
        <asp:Panel ID="pnlfaculty" runat="server">
            <div>

                <a href="EditStudentGroup.aspx">
                  <p>Create & Edit Groups</p>
                </a>

            </div>
        </asp:Panel>
    </div>


</asp:Content>
