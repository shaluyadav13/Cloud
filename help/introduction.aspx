<%@ Page Language="C#" AutoEventWireup="true" CodeFile="introduction.aspx.cs" Inherits="help_Default" %>

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
                        <h4>Help Contents</h4>
                    </div>
                    <br />
                    <br />
                    <div class="headingBox">
                        <h4>Introduction</h4>
                    </div>
                    <div class="textHolder">
                        <a href="introduction.aspx" style="text-decoration: none">
                            <h5>Introduction</h5>
                        </a>
                        <br />
                        <a href="frequent.aspx" style="text-decoration: none">
                            <h5>Frequently Asked<br />
                                Questions (FAQ)</h5>
                        </a>
                        <br />
                        <br />
                    </div>
                    <div class="headingBox">
                        <h4>Using</h4>
                    </div>
                    <div class="textHolder">
                        <a href="loggingIn.aspx" style="text-decoration: none">
                            <h5>Logging In</h5>
                        </a>
                        <br />
                        <a href="uploadingVideo.aspx" style="text-decoration: none">
                            <h5>Uploading a Video</h5>
                        </a>
                        <br />
                        <a href="publishingVideo.aspx" style="text-decoration: none">
                            <h5>Publishing a Video</h5>
                        </a>
                        <br />
                        <a href="incompleteUpload.aspx" style="text-decoration: none">
                            <h5>Incomplete Uploads </h5>
                        </a>
                        <br />
                        <a href="editingVideo.aspx" style="text-decoration: none">
                            <h5>Editing Video Information</h5>
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
                        Introduction to Northwest Video
                    </div>

                </div>


            </div>

            <div id="body1">

                <div>

                    <br />
                    <h2>What is Northwest Video?</h2>
                    <p>
                        Northwest Video is a video repository web site where Northwest faculty and 
        staff can securely and easily upload video content. Northwest faculty and 
        staff automatically have accounts with the system and can use it immediately. 
        The site provides a clean and simple interface to upload a video and also 
        provides a video player that can easily be embedded within an eCollege course 
        site to allow students to view content.
                    </p>
                    <p>
                        Northwest Video is a video storage site, NOT an open, social web site where 
        anyone can browse through and watch any video. It is left up to you to 
        make videos available to students by embedding within eCollege course sites.
        Content posted within Northwest Video is secure and not available to the 
        general public.
                    </p>
                    <br />
                    <h2>Why should I use Northwest Video instead of another video sharing site?</h2>
                    <p>
                        Northwest Video offers several advantages compared to other video sites, listed below.
                    </p>
                    <h3>Northwest Video is under complete control by Northwest Missouri State University.</h3>
                    <ul class="helpList">
                        <li>Northwest Video's login system is integrated with the Northwest domain. Your 
            username and password are synchronized with your network/email account. This 
            means Northwest Video won't be just another account and password to remember, 
            and when you change your Northwest password, it will automatically be changed 
            here.
                        </li>
                        <li>Faculty and Staff automatically have accounts with Northwest Video. No 
            registration is necessary. Faculty and Staff may immediately use the site 
            without performing any setup at all.
                        </li>
                        <li>Since the site is maintained by Northwest, features and limits can be 
            changed as necessary.
                        </li>
                    </ul>
                    <h3>Northwest Video is secure and not available to the general public.</h3>
                    <ul class="helpList">
                        <li>The general public on the Internet cannot access Northwest Video to browse through
            and watch videos. Content stored here is for Northwest students only.
                        </li>
                    </ul>
                    <h3>No distractions</h3>
                    <ul class="helpList">
                        <li>No advertisements or pop-ups.
                        </li>
                        <li>No comments.
                        </li>
                        <li>No "Related Videos."
                        </li>
                    </ul>




                </div>




            </div>


        </div>



    </form>
</body>
</html>
