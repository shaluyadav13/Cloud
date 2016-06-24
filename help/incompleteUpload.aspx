<%@ Page Language="C#" AutoEventWireup="true" CodeFile="incompleteUpload.aspx.cs" Inherits="help_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70"/>
    <link href="help.css" rel="stylesheet" type="text/css" media="all" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="navigation.js"></script>
    <title>Help</title>


</head>
<body>
    <form id="form1" runat="server">


    <div id = "container">
	    <div id = "leftNavigationContainer">
		    <div id = "navigation">
                <div class ="headingBox"><h4 style="text-decoration: none;"><a href="../../Login.aspx">NWCloud</a></h4></div>
                <div class ="textHolder">
                <div class ="headingBox"><h5>Important</h5></div>
                <div class ="textHolder">
                <a href="index.aspx" style="text-decoration: none"><h5>Introduction</h5></a>
                <br />
                <a href="frequent.aspx" style="text-decoration: none"><h5>Frequent Questions (FAQ)</h5></a>
                <br /><br /></div>
                <div class ="headingBox"><h5>How to Use</h5></div>
                <div class ="textHolder">
                <a href="loggingIn.aspx" style="text-decoration: none"> <h5>Logging In</h5></a><br />
        	    <a href="uploadingVideo.aspx" style="text-decoration: none"> <h5>Uploading a Video</h5></a><br />
                <a href="publishingVideo.aspx" style="text-decoration: none"> <h5>Publishing a Video</h5></a><br />
                <a href="incompleteUpload.aspx" style="text-decoration: none"> <h5>Incomplete Uploads </h5></a><br />
                <a href="editingVideo.aspx" style="text-decoration: none"> <h5>Editing Video Information</h5></a><br />
                <a href="studentGroups.aspx" style="text-decoration: none"> <h5>Student Groups </h5></a></div>
                <br /><br /><br />
            
                <div id="carrot">
                    <img src="leftcarrot.png" alt="leftcarrot"/>
                </div>
    	    </div>

            <div id ="smallNavigation">
                <div id="fillerBox"></div>
                <div id ="carrot2">
                     <img src="rightcarrot.png" alt="leftcarrot"/>
                </div>
            </div>
        </div>

	    <div id = "topNavigationContainer">
		    <div id = "topNavigation">
			    <div id ="northwestLogo">
            
                </div>
		    </div>
        <div id = "fileNavigation">
        
        	<div id = "navText">
            Incomplete Uploads
            </div>
            
        </div>
        
</div>
     </div>
        
        <div id = "body1" style="margin-right: 5%;">
        
    <p>
        Sometimes, you may be uploading a media file, but the conversion process does not begin. This can happen from time to time if your session times out during the upload process, or you navigate away from the page when uploading. Follow the steps below in order to complete these uploads.
    </p>
    <ol>
        <li>
            This page is still under development, a solution to this problem will be posted shortly. Please contact the CITE Office if you are experiencing this issue and we will help you.
        </li>
        <!-- 
        <li>
            <a href="../../Login.aspx">Login to Northwest Cloud.</a>
        </li>
        <li>
            A navigation link will appear on the left, labeled "Incomplete Uploads." If you do
            not see this link, then you do not have any incomplete uploads.
        </li>
        <li>
            Any incomplete uploads will show up in the list box. Select a video and click "Next."
        </li>
        <li>
            You will be directed to the "Select Thumbnail" page for that video and can continue
            the upload process as normal. See <a href="uploadingVideo.aspx" style="text-decoration: none"> Uploading Videos</a>
            for more information.
        </li>
            -->
    </ol>




        </div>
        
        
	


    
    </form>
</body>
</html>