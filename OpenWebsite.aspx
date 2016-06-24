<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OpenWebsite.aspx.cs" Inherits="OpenWebsite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>

    <title>Northwest Cloud</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leftNav" runat="Server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div class="fontPlacement" id="breadcrumbs" runat="server">
            <a href="myMedia.aspx" style="text-decoration: none; text-decoration-color: none">My Media </a>> <a href="myVideos.aspx" style="text-decoration: none; text-decoration-color: none">My Videos</a> ><asp:Label ID="lblVideoName" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">

        <h2>
            <asp:Label ID="welcomeNote" runat="server" />
        </h2>
        <br />

        <div id="webSite">
            <a id="webLink" runat="server">
                <asp:Label ID="lblName" runat="server"></asp:Label></a>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="ownerPanel" runat="server" Visible="False">
                
<!-- START: Embed Code Edit -->

                <div id="embedCodes">
                    <strong>Links</strong>
                    <br />
                    <blockquote  class="embedCodeStyle">
                        <asp:Label ID="webLinkLabel" runat="server" />
                    </blockquote>
                    <h2>Embed code</h2>
                    <strong>Website width:</strong>
                    <asp:DropDownList ID="ddlWebSize"
                        AutoPostBack="True"
                        runat="server" OnSelectedIndexChanged="ddlWebSize_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="Default">Default</asp:ListItem>
                        <asp:ListItem Value="560*315">560 &times; 315</asp:ListItem>
                        <asp:ListItem Value="640*360"> 640 &times; 360 </asp:ListItem>
                        <asp:ListItem Value="853*480"> 853 &times; 480 </asp:ListItem>
                        <asp:ListItem Value="1024*720"> 1024 &times; 720 </asp:ListItem>
                        <asp:ListItem Value="1280*720"> 1280 &times; 720 </asp:ListItem>


                    </asp:DropDownList>
                    <br />
                    <br />
                    <strong>Embed Websites </strong>
                    <br />
                    <blockquote  class="embedCodeStyle">
                        <asp:Label ID="codeLabel" runat="server" />
                    </blockquote>
                </div>

            </asp:Panel>
            <br />
            <br />
            <a id="NwOnline" href="https://secure.ecollege.com/nwms/index.learn?action=welcome" target="_blank">Northwest Online</a>
        </div>

    </div>
</asp:Content>

