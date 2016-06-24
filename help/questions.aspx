<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="questions.aspx.cs" Inherits="help_questions" %>

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
        <a href="../admin.aspx">
            <div id="admin">
            </div>
        </a>
    </asp:Panel>
    <a href="../Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <asp:Image ID="Image1" ImageUrl="~/images/folderIcon.png" runat="server" />
        </div>
        <div class="fontPlacement">
            <a href="help.aspx" style="text-decoration: none;">Help</a>
            > FAQ
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2" style="width:600px;">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>



        <div id="body1" style="margin-left: 25px; margin-top: 5px;">

            <p>
                If you ever have any questions about the Northwest Cloud and how it works, please check here first. If the answer to your question is not here, come down to the CITE Office or send us an email with your question and we will do our best to answer it for you. 
            </p>
            <h3>Contents</h3>
            <ul class="tableOfContents">
                <li><a href="#sectionone">Why is Northwest Cloud better than YouTube?</a></li>
                <li><a href="#sectiontwo">How long does it take to upload a media file?</a></li>
                <li><a href="#sectionthree">Is there a size limit on my media upload?</a></li>
                <li><a href="#sectionfour">What file formats are accepted</a></li>
                <li><a href="#sectionfive">Are students allowed to login and use Northwest Cloud?</a></li>
                <li><a href="#sectionsix">I’m having difficulty logging in. What can I do?</a></li>
                <li><a href="#sectionseven">What do I do if I experience an error?</a></li>
                <li><a href="#sectioneight">My Question isn’t here! How do I get it answered?</a></li>
            </ul>

            <br />

            <h3><a name="sectionone">Why is Northwest Cloud better than YouTube?</a></h3>
            <p style="margin-left: 2%;">
                Northwest Cloud is recommended over YouTube for classroom use for several reasons, including privacy, the lack of advertisements and other distractions that will get your students off track, and the account synchronization with Northwest’s network. For more information, refer back to the 
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/howtouse.aspx">
            Northwest Cloud Introduction
        </asp:HyperLink>page.
            </p>

            <br />

            <h3><a name="sectiontwo">How long does it take to upload a media file?</a></h3>
            <p style="margin-left: 2%;">
                For small files that have less than 100 MB, the upload time should be no more than 5 – 10 minutes. However, depending on your internet connection, the speed of the upload may be slower due to file size, server load, the traffic on Northwest’s network, and many other factors, which could add up to being several hours. We recommend that you upload your media from an on-campus location if possible since this will take less time when connected to an Ethernet.
            </p>

            <br />

            <h3><a name="sectionthree">Is there a size limit on my media upload?</a></h3>
            <p style="margin-left: 2%;">
                Media files larger than one gigabyte will not be accepted during the upload process. When uploading, we recommend that even files larger than half a gigabyte are not suggested since they may take a very long time to upload. If it is at all possible, break the file into smaller pieces and upload them individually when uploading a file of that size. If you are recording for a video, you might consider reducing the capture resolution and bitrate of your camera, it will reduce the file size and make it a faster upload.
            </p>

            <br />

            <h3><a name="sectionfour">What video formats are accepted?</a></h3>
            <p style="margin-left: 2%;">
                Northwest Cloud accepts most file types of formats for video and audio.  Uploading a web site, the file type needs to be of type ‘.zip’ and the main page named index.html.  Images need to be jpg or png.  Documents can be of any type, but embedding will only be available for pdf file type.  If you are having trouble uploading a file, contact the CITE Office for assistance.
            </p>

            <br />

            <h3><a name="sectionfive">Are students allowed to login and use Northwest Cloud?</a></h3>
            <p style="margin-left: 2%;">
                Students can use Northwest Cloud to turn in multimedia assignments only if a faculty or staff member has created a group inside Northwest Cloud and enrolled the students as members of the group.
            </p>

            <br />

            <h3><a name="sectionsix">I'm having difficulty logging in. What can I do?</a></h3>
            <p style="margin-left: 2%;">
                Use your Northwest Network username and password to log into Northwest Cloud. It will be the same username and password used to login to the computer network. If you’re having difficulty logging in, try clearing your browser’s temporary internet files, refresh the page and try again. If it still fails, contact the CITE Office for assistance.
            </p>

            <br />
            <h3><a name="sectionseven">What do I do if I experience an error?</a></h3>
            <p style="margin-left: 2%;">
                If you experience an error, use the Report Error link in the bottom left hand menu to submit the issue.  The CITE Office will research and try to trouble shoot the issue.  Be as specific as you can when writing the about the error.  State exactly what the error message said and what you were trying to do.  It always good to mention which browser using and which media type you were working in.  If uploading media, include the file name and type of file you were trying to upload.  The report will automatically give us your username.  Include your phone number in the report so we can contact you faster. 
            </p>
            <br />
            <h3><a name="sectioneight">My question isn't here! How do I get it answered?</a></h3>
            <p style="margin-left: 2%;">
                Northwest Cloud was developed by student programmers under the supervision of the CITE Office.  It will have some problems as it begins to be used.  Please feel free to contact the CITE Office if you have questions or issues using Northwest Cloud.  cite@nwmissouri.edu 
            </p>

            <br />
            <br />
            <br />
            <br />

        </div>







    </div>

</asp:Content>



