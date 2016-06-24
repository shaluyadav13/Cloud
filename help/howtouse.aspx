<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="howtouse.aspx.cs" Inherits="howtouse" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <style>
        .container .content {
            display: none;
            padding: 5px;
        }
    </style>


    <title>Northwest Cloud</title>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="../myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="../myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="../UploadMedia.aspx">
        <div id="upload"></div>
    </a>

    <a href="help.aspx">

        <div id="help">
            <img src="images/helpIconBlue.png" alt="upload" style="float: left" /></div>

    </a>

    <asp:Panel ID="ad" runat="server">
        <a href="../admin.aspx"></a>
    </asp:Panel>
    <a href="../Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <asp:Image ImageUrl="~/images/folderIcon.png" runat="server" />
        </div>
        <div class="fontPlacement">
            <a href="help.aspx" style="text-decoration: none;">Help</a>
            > Northwest Cloud Introduction 
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>

        <div id="body1" style="margin-left: 25px; margin-top: 5px; width: 600px;">

            <div>

                <br />
                <h3>An Introduction to Northwest Cloud</h3>
                <p style="margin-left: 2%; margin-right: 5%;">
                    Northwest Cloud is a multimedia repository that has been created for Northwest faculty, staff and students, so they can easily upload video, audio, photos, documents and website content for use in courses. This media site uses HTML5 technology for streaming video and audio and allows each browser to use its default player for the media type.  The media content stored in Northwest Cloud can easily be embedded in any Northwest Online course site to allow for student viewing.
                </p>
                <p style="margin-left: 2%; margin-right: 5%;">
                    Northwest Cloud is not an open social media website where anyone on the internet can access the content. The content is only shown to those who you allow to view it, by either sharing the URL, embedding content within a Northwest Online course site or by sharing through a group in Northwest Cloud. Otherwise, content that is posted within Northwest Cloud is not available to the general public.
                </p>
                <br />
                <h3>The advantages of why you should use Northwest Cloud!</h3>
                <div style="margin-left: 2%;">
                    <ol class="helpList" style="margin-right: 5%;">
                        <li>Northwest Cloud is under complete control by Northwest Missouri State University.
                        </li>
                        <li>The cloud resource will allow faculty to post their media content here in the cloud and then embed the content in one or more course sites. If the content needs updating, then by update the content in Northwest Cloud, it will automatically update in all places the content is already embedded.
                        </li>
                        <li>Northwest Cloud’s login system is incorporated within the Northwest’s domain, so your username and password are already synchronized with your Northwest network account. The Northwest faculty and staff already have accounts setup and can used it immediately.
                        </li>
                        <li>Students have access only when a faculty or staff member creates a group and as owner of group enrolls students into the group.This allow students to also upload media either as an assignment or to share with the group for class. 
                        </li>
                        <li>Faculty will also be able to create departmental groups to share academic content between colleagues and courses.  
                        </li>
                        <li>The general public does not have access to Northwest Cloud, making it impossible to browse through and view the content. The content stored here is for Northwest personnel viewing only.
                        </li>
                        <li>Northwest Cloud is created for the learning environment of our university.
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

