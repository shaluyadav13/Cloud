<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frequent.aspx.cs" Inherits="help_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="help.css" rel="stylesheet" type="text/css" media="all" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="navigation.js"></script>
    <title>Help</title>


</head>
<body>
    <form id="form1" runat="server">


        <div id="container">
            <div id="leftNavigationContainer">
                <div id="navigation">
                    <div class="headingBox">
                        <h4 style="text-decoration: none;"><a href="../../Login.aspx">NWCloud</a></h4>
                    </div>
                    <div class="textHolder">
                        <div class="headingBox">
                            <h5>Important</h5>
                        </div>
                        <div class="textHolder">
                            <a href="index.aspx" style="text-decoration: none">
                                <h5>Introduction</h5>
                            </a>
                            <br />
                            <a href="frequent.aspx" style="text-decoration: none">
                                <h5>Frequent Questions (FAQ)</h5>
                            </a>
                            <br />
                            <br />
                        </div>
                        <div class="headingBox">
                            <h5>How to Use</h5>
                        </div>
                        <div class="textHolder">
                            <a href="loggingIn.aspx" style="text-decoration: none">
                                <h5>Logging In</h5>
                            </a>
                            <br />
                            <a href="uploadingVideo.aspx" style="text-decoration: none">
                                <h5>Uploading Media</h5>
                            </a>
                            <br />
                            <a href="publishingVideo.aspx" style="text-decoration: none">
                                <h5>Publishing Media</h5>
                            </a>
                            <br />
                            <a href="editingVideo.aspx" style="text-decoration: none">
                                <h5>Editing Media Information</h5>
                            </a>
                            <br />
                            <a href="studentGroups.aspx" style="text-decoration: none">
                                <h5>Student Groups </h5>
                            </a>
                        </div>
                        <br />
                        <br />
                        <br />

                        <div id="carrot">
                            <img src="leftcarrot.png" alt="leftcarrot" />
                        </div>
                    </div>

                    <div id="smallNavigation">
                        <div id="fillerBox"></div>
                        <div id="carrot2">
                            <img src="rightcarrot.png" alt="leftcarrot" />
                        </div>
                    </div>
                </div>

                <div id="topNavigationContainer">
                    <div id="topNavigation">
                        <div id="northwestLogo">
                        </div>
                    </div>


                    <div id="fileNavigation">

                        <div id="navText">
                            Frequently Asked Questions (FAQ)
                        </div>

                    </div>

                </div>
            </div>

            <div id="body1" style="margin-right: 5%;">

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
                    <li><a href="#sectionseven">My Question isn’t here! How do I get it answered?</a></li>
                </ul>

                <br />

                <h3><a name="sectionone">Why is Northwest Cloud better than YouTube?</a></h3>
                <p style="margin-left: 2%;">
                    Northwest Cloud is recommended over YouTube for classroom use for several reasons, including privacy, the lack of advertisements and other distractions that will get your students off track, and the account synchronization with Northwest’s network. For more information, refer back to the 
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/index.aspx">
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
                    Northwest Cloud accepts many different types of formats, when uploading a file, if there are any restriction then there will be a message below the “Select File” button saying what it has to be. For websites, all content of the website must be in a .zip file format with the main page title index.html so that the site can register that it is a website. If you are having trouble uploading a file, contact the CITE Office and we will see what we can do for you.
                </p>

                <br />

                <h3><a name="sectionfive">Are students allowed to login and use Northwest Cloud?</a></h3>
                <p style="margin-left: 2%;">
                    Yes they are, but only if the faculty or staff member of a student group asks to have a group made and sends a file with all of their sNumbers attached so we can add them to that group. For more information, look at the 
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Help/Using/Students.aspx">
            Student Groups.
        </asp:HyperLink>
                    tab.
                </p>

                <br />

                <h3><a name="sectionsix">I'm having difficulty logging in. What can I do?</a></h3>
                <p style="margin-left: 2%;">
                    Use your Northwest Network username and password to log into Northwest Cloud, it will be the same username and password you use for your email. It is not case-sensitive, and you do not put the “@nwmissouri.edu” on the end of your username. If you’re still having difficulty logging in, try clearing your browser’s temporary internet files, refresh the page and try again. If all else fails, contact the CITE Office and we will do your best to help you.
                </p>

                <br />

                <h3><a name="sectionseven">My question isn't here! How do I get it answered?</a></h3>
                <p style="margin-left: 2%;">
                    This page is still under development, so if you have any questions that do not show up, send them to the CITE Office and we will answer it for you. We appreciate any comments, questions, or feedback that you have for us.
                </p>

                <br />
                <br />
                <br />
                <br />

            </div>
    </form>
</body>
</html>
