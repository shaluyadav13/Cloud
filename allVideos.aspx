<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allVideos.aspx.cs" Inherits="allVideos" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>

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
   <a href="Help/help.aspx">
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
        Browse Videos
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <h2>
            <asp:Label ID="HeaderLabel" runat="server" /></h2>
        <br />
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label" />
            <br />
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </div>
    </div>
</asp:Content>
