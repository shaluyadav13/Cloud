<%@ Page Language="C#" AutoEventWireup="true" CodeFile="publishingVideo.aspx.cs" Inherits="help_Default" %>

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
                            Publishing Media
                        </div>

                    </div>


                </div>
            </div>

            <div id="body1" style="margin-right: 5%; overflow-y: auto;">
                <p style="margin-left: 2%;">
                    Northwest Cloud has a built in Search bar and easy to comprehend navigation bar, allowing you to view the content with ease. But if your students are not registered in a group for Northwest Cloud, then they will not be able to see the content that you upload, which means you will have to publish the media on a MyNorthwest course site. There are two embedded links and a hyperlink provided for your use, located directly below the media. You may also email the URL of the page from your browser’s address bar to your students, but we recommend using either of the previous options.
                </p>

                <p>&nbsp;</p>
                <h3><a name="wholePage">Embedding the Media</a></h3>
                <div style="margin-left: 2%;">
                    <p>
                        The easiest way of publishing media, is to embed the video player page into a course site. Do so by following these steps below.
                    </p>
                    <ol>
                        <li>
                            <a href="../../Login.aspx">Login to Northwest Cloud.</a>
                        </li>
                        <li>Navigate to the video you want to publish.
                        </li>
                        <li>Scroll (if needed) to the Embed Code section
                        </li>
                        <li>Select the video size you would like to have, we recommend the default as to not mess with the aspect ratio
                        </li>
                        <li>Then wait for the page to refresh itself, highlight and copy the code from the < to the farthest right > 
                        </li>
                        <li>Login to MyNorthwest, navigate to your course and enter author mode.
                        </li>
                        <li>Select your content page to edit it. Click the button labeled “<html>” to enter HTML view
                        </li>
                        <li>Right–click in the text box and choose “Paste”, or you can press Ctrl-V to paste into the text box
                        </li>
                        <li>Click “Save Changes.”
                        </li>
                    </ol>
                    <p>
                        The media is now embedded into the course site and can now be seen by students through their MyNorthwest or eCourse site.
                    </p>
                </div>

                <p>&nbsp;</p>
                <h3><a name="embedPlayer">Embedding the video player (advanced)</a></h3>
                <div style="margin-left: 2%;">
                    <p>
                        Content Missing... Please come back later.
                    </p>
                </div>
                <p>&nbsp;</p>
                <h3><a name="link">Linking to the Media</a></h3>
                <p style="margin-left: 2%;">
                    Follow the same steps as before, except instead of copying the embed code, copy the link code. Use “Direct Link” if you wish to just have the hyperlink or use “HTML Link” if you want to have a specific text be the link to the media.
                </p>
            </div>

            <br />
            <br />
            <br />
            <br />

        </div>
    </form>
</body>
</html>
