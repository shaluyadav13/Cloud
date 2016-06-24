<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="help_Default" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
    <link href="css/status.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <style>
        .container .content {
            display: none;
            padding: 5px;
        }
    </style>

    <script>
        $(document).ready(function () {
            $("#header").click(function () {
                $("#contentdiv").toggle();
            });
        });
    </script>
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

    <a href="#">
        <div id="header">
            <div id="help"></div>


            <div id="contentdiv" style="display:none" >
                
                  <a href="howtouse.aspx">
        <div id="howtouse" style="color:white;font-weight:300;">How to use
        </div> </a>
                  <a href="loginhelp.aspx">
                      <div id="logging" style="color:white;font-weight:300">Logging In 
        </div></a>
                 <a href="uploadvideo.aspx">
                       <div id="UploadingMedia" style="color:white;font-weight:300">Upload Media
        </div></a>
                  <a href="publishmedia.aspx">
                      <div id="PublishingMedia" style="color:white;font-weight:300">Publish Media
        </div></a>
                <a href="editmedia.aspx">
                      <div id="Editing" style="color:white;font-weight:300">Edit Media
        </div></a>
                 <a href="groups.aspx">
                      <div id="Groups" style="color:white;font-weight:300"> Groups     
        </div></a>
   
            </div>
        </div>
    </a>


    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin">
                <img src="helpImages/adminIconBlue.png" alt="myVideosIcon" style="float: left" />
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
        Application Status
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>


        <div id="wrapper">
            <h1>Application Status</h1>





        </div>








        <%--  <h3>Web Counter</h3>
        <p>
            Sitewide hit statistics are tracked through the CITE web counter. This single counter is for the entire site
        (except the help pages). To view detailed statistics, click on the counter image below.
        </p>
        <a target="_new" href="http://cite.nwmissouri.edu/WebCounter/PageDetails.aspx?id=72">
            <img src="http://cite.nwmissouri.edu/webcounter/72n/Black/White/Rounded.png"
                alt="webCounter" border="none" />
        </a>
        <p>&nbsp;</p>--%>
    </div>

</asp:Content>

