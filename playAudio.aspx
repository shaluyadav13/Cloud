<%@ Page Language="C#" AutoEventWireup="true" CodeFile="playAudio.aspx.cs" Inherits="playAudio" MasterPageFile="~/MasterPage.master" %>

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
        <div class="fontPlacement" ID="breadcrumbs" runat="server">
            <a href="myMedia.aspx" style="text-decoration: none; text-decoration-color: none">MyMedia </a>> <a href="myAudio.aspx" style="text-decoration: none; text-decoration-color: none">My Audios</a> ><asp:Label ID="lblAudioName" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">

        <h2>
            <asp:Label ID="welcomeNote" runat="server" />
        </h2>
        <br />
        <!-- Error message for if the video hasn't been converted yet -->
        <div id="audioErrorContainer" runat="server" style="background-color: #ff9c23; margin: 0 0 10px -10px; padding: 10px">
            <div id="audioError" runat="server">
                <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" viewBox="0 0 180 180" style="vertical-align: middle">
                    <path fill="none" stroke="#fff" stroke-width="20" stroke-linecap="round" d="M89,9a81,81 0 1,0 2,0zm1,38v58m0,25v1" />
                </svg>&nbsp;&nbsp;<span style="vertical-align: middle; color: white; font-size: 1.3em; font-weight: bold">This audio is currently being processed</span>
            </div>

        </div>
        <div id="audioPlayer">
            <audio id="audioTag" controls="controls"
                preload="auto" runat="server" src="null">
                This audio has not been converted yet.
            </audio>
        </div>
        <br />
        <a id="transcriptLink" runat="server">
            <asp:Label ID="lblName" runat="server" Text="Transcript"></asp:Label></a>
        <br />
        <div>
            <asp:Panel ID="ownerPanel" runat="server" Visible="False">

                <!-- START: Embed Code Edit -->

                <div id="embedCodes">
                    <strong>Links</strong>
                    <br />
                    <blockquote class="embedCodeStyle">

                        <asp:Label ID="audioLinkLabel" runat="server" />
                    </blockquote>
                    <br />
                    <h2>Embed code</h2>
                    <strong>Embed Audio</strong>
                    <br />
                    <blockquote class="embedCodeStyle">
                        <asp:Label ID="codeLabel" runat="server" />
                    </blockquote>
                    <br />
                    <div id="transcript" runat="server">
                        <strong>Transcript Link</strong>
                        <br />
                        <blockquote class="embedCodeStyle">
                            <asp:Label ID="lblTranscript" runat="server" />
                        </blockquote>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <br />
            <a id="NwOnline" href="https://secure.ecollege.com/nwms/index.learn?action=welcome" target="_blank">Northwest Online</a>
        </div>

    </div>
</asp:Content>
