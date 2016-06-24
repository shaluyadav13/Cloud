<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editingVideo.aspx.cs" Inherits="help_Default" %>

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
                            Editing Media Information
                        </div>

                    </div>

                </div>
            </div>

            <div id="body1" style="margin-right: 5%;">


                <h3>Editing the Media File</h3>
                <p style="margin-left: 2%;">
                    You may edit the any of the information for any media file you want at any time, you may also delete the video, but exchanging the file for another is not possible. For that, you must upload it separately.
                </p>
                <ol>
                    <li>The edit a media file, navigate your way from the “My Media” Tab to the where the file is located.
                    </li>
                    <li>Click on the “Edit” button under the Title and description of the file.
                    </li>
                    <li>Use the form to change the information to what you like.
                    </li>
                    <li>You are also able to add or remove an auto-removal date if you wish. Click the textbox and a calendar popup will help you select a date. If you select a date, this media file will automatically be deleted from the Northwest Cloud on that date.
                    </li>
                    <li>Click “Submit” to save your changes or “Cancel” if you changed your mind. In either case, you will be sent back to the “My Media” page.
                    </li>
                </ol>

                <br />

                <h3>Deleting the Media File</h3>
                <ol>
                    <li>The delete the media file, navigate your way from the “My Media” tab to where the file is located.              </li>
                    <li>Then Click the “Delete” button under the title and description of the file, right next to the “Edit” Button.
                    </li>
                    <li>You will be prompted if you are certain that you would like to delete the file.
                    </li>
                    <li>Click “Yes” if you wish to delete the file and “No” if you changed your mind.
                    </li>
                </ol>


            </div>

            <br />
            <br />
            <br />
            <br />

        </div>
    </form>
</body>
</html>
