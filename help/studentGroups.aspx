<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentGroups.aspx.cs" Inherits="help_Default" %>

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
                            Student Groups
                        </div>

                    </div>

                </div>

            </div>

            <div id="body1" style="margin-right: 5%;">


                <h3>What is a student group?</h3>
                <p style="margin-left: 2%;">
                    A student group is a group created for a class when requested by a faculty or staff member, allowing students to log into Northwest Cloud and post content for easy viewing. Student groups are time-limited and will expire after a certain amount of time, usually after the current semester ends. This means that students will only be able to log into and post content during that certain date range, not allowing the students to log in after that date has passed.
                </p>

                <br />

                <h3>Where can I find my Students’ Media?</h3>
                <p style="margin-left: 2%;">
                    If you have requested a student group for a class that you are teaching, you will become the “owner” of the student group. If you have any groups, you will see a link in the navigation bar on the left of the web page labeled “Groups” with all of the groups you are currently the owner of. You can click on the group you wish to view and see all the students in the group. Then you can click on each student and see the media that they uploaded. You can directly click on the media’s title to view it, or you can expand the media and see more information, including and edit link if you need to edit the information that the student placed on their media.
                </p>

                <br />

                <h3>What happens to my Students’ Media after the group expires?</h3>
                <p style="margin-left: 2%;">
                    After the student group expires, the media content will remain and you will still be able to find it through the “Groups” tab on the left navigation bar. However, the student will still no longer be able to log in and upload anything new or modify any of their old content.
                </p>

                <br />

                <h3>I’m an instructor, how do I request a student group for one of my classes?</h3>
                <p style="margin-left: 2%;">
                    Please contact the CITE Office at (660) 562-1532 and we can set this up for you. Be prepared to tell us what class the group is for, during what date range the group should be valid, and also please have a list of your students' logins (S-numbers) handy so that we can enroll your students.
        <br />
                    - A new and much easier way will be implemented at a later date, for now just contact us for help.

                </p>

                <br />

                <h3>I'm a student and my instructor requires me to upload media. What do I do?</h3>
                <p style="margin-left: 2%;">
                    First off, read through the help pages to try and find the information you are looking for. To log in, go to the 
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">
            Northwest Cloud
        </asp:HyperLink>
                    login page to get started or go to the 
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="loggingIn.aspx">
            Logging In
        </asp:HyperLink>
                    help section for more help. If you are unable to login, please contact your instructor or the CITE Office for help. For help on submitting media, check out the 
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="uploadingVideo.aspx">
            Uploading Videos
        </asp:HyperLink>page for more the step by step instructions.
                </p>

                <br />
                <br />
                <br />
                <br />

            </div>
    </form>
</body>
</html>
