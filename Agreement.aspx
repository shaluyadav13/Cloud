<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Agreement.aspx.cs" Inherits="Agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.80 maximum-scale=.80 " />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>

    <title>Northwest Cloud</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload">
            <img src="images/uploadIconBlue.png" alt="upload" style="float: left" />
        </div>
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
    <div id="navText">
        User Agreement
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="body2" style="margin-right: 5%;">
        <h3>Please read Terms and Conditions carefully and sign the User Agreement</h3>

        <div style="margin-left: 2%;">
        <p>
            Northwest Cloud, a multimedia repository, is created for the sole purpose to assist faculty in the management and delivery of multimedia content, including video, audio, images, websites, and documents for the web through the internet.  Technology developments have made it easy for individuals to author their own multimedia content.  Northwest Cloud is the resource to store and publish your creations to share with your students and the Northwest community.
        </p>
        <p>
            All users, including faculty, staff and students of Northwest Missouri State University, must agree to use this resource, Northwest Cloud, in alignment with copyright laws and fair use guidelines.  Use the library’s <a href="http://www.nwmissouri.edu/library/courses/copyright/resources.html" target="_blank">copyright resource page</a> for information on copyright and note <a href="http://www.nwmissouri.edu/compserv/northwest-and-copyright.htm" target="_blank"> Northwest Information on Copyright</a>.  Determining copyright is extremely important before distributing other’s creations. 
        </p>
        <p>
            You must agree to the following before you will be allowed to upload any multimedia content to this repository.  Please read and if you accept the responsibility to follow these rules in your best effort, then accept the user agreement below.
        </p>
        <ol style="list-style-type: decimal;">
            <li>
                The individual uploading multimedia content files to Northwest Cloud is responsible for the lawful copyright usage of the files uploaded.
            </li>
            <li>
                If the multimedia content uploaded is not created or authored by the individual uploading, then special copyright permission and /or documentation of fair use must also be posted in Northwest Cloud under the document sections of uploading individual’s media collection for the multimedia item uploaded.
            </li>
            <li>
                If an individual uploads multimedia content that is created partially or fully by the same individual and the individual is willing to allow others to reuse the content, then declare in the description of the media the <a href="http://creativecommons.org/" target="_blank">Creative Commons</a> license and your agreement to allow others to use within the Creative Commons guidelines.  
            </li>
        </ol>

        <p>
            I do accept the responsibility to follow the above user agreement when uploading content to Northwest Cloud, a multimedia repository for Northwest Missouri State University.
        </p>

        <asp:Panel ID="signedPNL" runat="server">
            <asp:CheckBox runat="server" ID="agreed" /><asp:Label runat="server" Text="I have read the Terms and Conditions and accepted the User Agreement" ID="agreedText"></asp:Label><br />
            <br />
            <asp:Button runat="server" ID="submit" Text="submit" OnClick="submit_Click" /><br />
            <br />
            <asp:Label runat="server" ID="error" ForeColor="Red"></asp:Label>
        </asp:Panel>
            </div>
    </div>
</asp:Content>

