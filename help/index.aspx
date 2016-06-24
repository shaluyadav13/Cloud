<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="help_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                            Northwest Cloud Introduction
                        </div>
                    </div>
                </div>
            </div>
            <div id="body1">

                <div>

                    <br />
                    <h3>What is Northwest Cloud?</h3>
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
                            <li>
                                Northwest Cloud is under complete control by Northwest Missouri State University.
                            </li>
                            <li>
                                The cloud resource will allow faculty to post their media content here in the cloud and then embed the content in one or more course sites.  If the content needs updating, then by update the content in Northwest Cloud, it will automatically update in all places the content is already embedded.
                            </li>
                            <li>
                                Northwest Cloud’s login system is incorporated within the Northwest’s domain, so your username and password are already synchronized with your Northwest network account. The Northwest faculty and staff already have accounts setup and can used it immediately.
                            </li>
                            <li>
                                Students have access only when a faculty or staff member creates a group and as owner of group enrolls students into the group. This allow students to also upload media either as an assignment or to share with the group for class. 
                            </li>
                            <li>
                                Faculty will also be able to create departmental groups to share academic content between colleagues and courses.  
                            </li>
                            <li>
                                The general public does not have access to Northwest Cloud, making it impossible to browse through and view the content. The content stored here is for Northwest personnel viewing only.
                            </li>
                            <li>
                                Northwest Cloud is created for the learning environment of our university.
                            </li>
                        </ol>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <br />
            <br />

        </div>
    </form>
</body>
</html>
