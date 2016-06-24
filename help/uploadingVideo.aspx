<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadingVideo.aspx.cs" Inherits="help_Default" %>

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
                            Uploading a Media File
                        </div>

                    </div>

                </div>
            </div>

            <div id="body1" style="margin-right: 5%;">

                <h3>Posting media files to Northwest Cloud is easy.  Follow these steps below.</h3>
                <div style="margin-left: 2%;">
                    <table>
                        <tr>
                            <td width="50%" align="center">
                                <img src="../help/helpImages/uploadMediaPicOne.png" style="margin-bottom: 10px;" width="91%"/>
                            </td>
                            <td>
                                <ol>
                                    <li>
                                        Click the “Upload Media” link in the navigation pane on the left.
                                        &nbsp; - &nbsp;If you have not signed the "User Agreement" form, then you will be redirected to that page to complete it.
                                    </li>
                                    <li>
                                        Enter a title and description for your video. Both are required in order to submit a file.
                                    </li>
                                    <li>
                                        For Group, select ‘My profile’ if not needing to share the media with a group, or select the group in which to share the media for the upload.
                                    </li>
                                    <li>
                                        If you have one or more groups listed, then you may select which group you would like the media file to be uploaded. If you are a student and unsure of which group to select, please contact your instructor before continuing.
                                    </li>
                                    <li>
                                        Click the “Browse” button to locate the desired file you would like to upload. The file you select must be the same type that you selected from the drop down menu.
                                    </li>
                                    <li>
                                        Once you have the file type chosen, you can choose to place the name of the creator in the Author (optional) section.
                                    </li>
                                    <li>
                                        You may also create an Auto Removal Date, which will delete the file automatically when the date comes to pass.
                                    </li>
                                    <li>
                                        
                                    </li>
                                    <li>
                                        The last option you will have is to add a transcript or not. Also not required.
                                    </li>
                                    <li>
                                        Last click Next to submit your upload.
                                    </li>
                                </ol>
                            </td>
                        </tr>
                    </table>



                    <p>
                        <b>NOTE: </b>Some files will take a while to upload, if you are uploading a large file, wait till the blue spinner icon is gone to navigate away from the current page, otherwise your file will not be uploaded.
                    </p>
                    
                    <p>
                        After the upload is complete, there will be a confirmation page loaded to confirm your upload. If you uploaded a video, it will be a selection screen with thumbnails. Select a thumbnail that you would like to represent your video and select submit at the bottom.  For video and audio media types, it may take some time after you click the “Finish” button before the media file appears in the desired media tab. It has to be converted to a specific format before the system can use it. Generally larger file types will take longer to process.
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
