<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loggingIn.aspx.cs" Inherits="help_Default" %>

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
                            Logging In
                        </div>

                    </div>
                </div>

            </div>

            <div id="body1" style="margin-right: 5%;">

                <h3>How to log into Northwest Cloud</h3>

                <p>To log into Northwest Cloud is very simple, just follow the steps below.</p>

                
                <table>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/loggingInPicOne.png" style="margin-bottom: 10px;" width="91%"/>
                        </td>
                        <td>
                            Open up any web browser or create another tab on the current one, and navigate to <a href="../../Login.aspx">https://cite.nwmissouri.edu/nwcloud</a>.
                            <br />
                            <br />
                            Enter your Northwest network username and password in the login form just like you would for your campus or email login. Then just click “Sign in”.
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/loggingInPicTwo.png" style="margin-bottom: 10px;" width="90%"/>
                        </td>
                        <td>
                            After you login, the “My Media” page will load up and you are all set to go!
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/loggingInPicThree.png"margin-bottom: 10px;" width="90%"/>
                        </td>
                        <td>
                            To logout, just click on your username at the top left of the page and click the link “Logout” when it drops down.
                        </td>
                    </tr>
                </table>

                <p>
                </p>
            </div>

            <br />
            <br />
            <br />
            <br />

    </form>
</body>
</html>
