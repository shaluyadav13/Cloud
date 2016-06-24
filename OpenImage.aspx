<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OpenImage.aspx.cs" Inherits="OpenImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>

    <script type="text/javascript" src="js/ZeroClipboard.min.js"></script>

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


        <div id="image" style="width:15%" >
            <img id="imageLink" runat="server" style="width:100%"  onclick="javascript:toggleImageSize()" /><br />
            <br />
            <asp:Label ID="lblName" runat="server" style="font-weight:bold"></asp:Label>
            <a id="fullSizeImageLink" runat="server" href="" target="_blank">Full size image</a>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="ownerPanel" runat="server" Visible="False">
               
<!-- START: Embed Code Edit -->

                <div id="embedCodes">
                    <strong runat="server" id="heading">Links</strong>
                    <br />


                    <blockquote class="embedCodeStyle">
                        <asp:Label ID="fileLinkLabel" runat="server" />
                    </blockquote>
                    <br />
                     <h2>Embed code</h2>
                    <div runat="server" id="fileWidth">
                        <strong>Image width:</strong>
                        <asp:DropDownList ID="ddlFileSize"
                            AutoPostBack="True"
                            runat="server" OnSelectedIndexChanged="ddlImageSize_SelectedIndexChanged">

                            <asp:ListItem Selected="True" Value="Original">Original</asp:ListItem>
                            <asp:ListItem Value="560*315">560 &times; 315</asp:ListItem>
                            <asp:ListItem Value="640*360"> 640 &times; 360 </asp:ListItem>
                            <asp:ListItem Value="853*480"> 853 &times; 480 </asp:ListItem>
                            <asp:ListItem Value="1024*720"> 1024 &times; 720 </asp:ListItem>
                            <asp:ListItem Value="1280*720"> 1280 &times; 720 </asp:ListItem>


                        </asp:DropDownList>
                    </div>
                    <br />
                    <br />
                    <strong>Embed Files</strong>
                    <br />

<!-- This is where the Embed Link code is -->
                   
                    <blockquote class="embedCodeStyle">
                        <asp:Label ID="codeLabel" ClientIDMode="Static" runat="server"/>
                    </blockquote>
                    <!-- <button id="my-button_text" data-clipboard-target="clipboard_text">Copy Embed</button> -->

                </div>
            </asp:Panel>
            <br />
            <br />
            <a id="NwOnline" href="https://secure.ecollege.com/nwms/index.learn?action=welcome" target="_blank">Northwest Online</a>
        </div>

    </div>
</asp:Content>

