<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminSearch.aspx.cs" Inherits="adminSearch" MasterPageFile="~/MasterPage.master" %>

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
        Video Search
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            To search all Videos within Northwest Cloud, enter some search terms.
        Title, description, and username will be searched.
        </p>
        <asp:TextBox ID="searchBox" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="searchButton_Click" />
        <br />
        <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        <br />
        <div>
            <asp:Label ID="resultsLabel" runat="server" />
            <local:VideoList ID="videoList" runat="server" EnablePaging="true" EnableSorting="false" DisplayVideoOwner="True" ItemsPerPage="10" PageDisplayCount="10" SelectedPage="1" />
        </div>
    </div>
</asp:Content>
