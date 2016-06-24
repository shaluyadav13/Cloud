<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectThumbnail.aspx.cs" Inherits="pages_SelectThumbnail" MasterPageFile="~/MasterPage.master" %>


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
            <img src="images/myVideosIconBlue.png" alt="myMediaIcon" style="float: left" />
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups" >
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
    </asp:Panel>
        <a href="Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="navText">
        Select a thumbnail
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="body2">
        Please select a thumbnail image to represent your video and then click &#39;Finish&#39;.<br />
        <br />
        <asp:RadioButtonList ID="imageRadioButtonList" runat="server" CellPadding="10"
            CellSpacing="10" RepeatColumns="3" Style="margin-bottom: 0px"
            RepeatDirection="Horizontal">
        </asp:RadioButtonList>
        <p>
            When you click finish, the video will be converted and prepared for publishing. <br />
            The preparation may take several minutes or longer. 
        </p>

       
        <br />
        <asp:Button ID="finishButton" runat="server" OnClick="finishButton_Click"
            Text="Finish" /> <asp:Label runat="server" ID="lblerror" Text="Please select a thumbnail" Visible="false" ForeColor="Red" />
        <br />
        <br />

    </div>
</asp:Content>
