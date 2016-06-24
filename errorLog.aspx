<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errorLog.aspx.cs" Inherits="errorLog" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
    <style type="text/css">
        .errorList {
            list-style: none;
        }

            .errorList span {
                display: inline-block;
            }

            .errorList li {
                border: 1px solid black;
                width: 540px;
                padding-top: 2px;
                padding-bottom: 2px;
                margin-top: -1px;
            }

            .errorList .headerErrorID {
                width: 60px;
                font-weight: bold;
            }

            .errorList .headerType {
                width: 280px;
                font-weight: bold;
            }

            .errorList .headerDate {
                width: 160px;
                font-weight: bold;
            }

            .errorList .errorID {
                width: 60px;
                font-size: smaller;
            }

            .errorList .type {
                width: 280px;
                font-size: smaller;
            }

            .errorList .date {
                width: 160px;
                font-size: smaller;
            }
    </style>
    <title>Northwest Cloud</title>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups" >
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="navText">
        Northwest Video Error Log
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>Any unhandled exceptions will be logged here. Click on an error for more details.</p>

        <div>
            <asp:Label ID="outputLabel" runat="server" />
        </div>
        <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        <p>&nbsp;</p>
    </div>

</asp:Content>
