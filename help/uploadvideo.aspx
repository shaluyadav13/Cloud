<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="uploadvideo.aspx.cs" Inherits="help_uploadvideo" %>

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
            <img src="images/helpIconBlue.png" alt="upload" style="float: left" />
        </div>

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
            > Uploading a Media File 
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>


        <div id="body1" style="margin-left: 25px; margin-top: 5px; overflow-y: auto; width: 800px;">

            <h3>Posting media files to Northwest Cloud is easy.  Follow these steps below.</h3>
            <div style="margin-left: 2%;">
                <table>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/uploadMediaPicOne.png" style="margin-bottom: 10px;" width="90%" />
                        </td>
                        <td>

                            <ol>
                                <li>Click the “Upload Media” link in the navigation pane on the left.
                                        &nbsp; - &nbsp;If you have not read and agreed to the "User Agreement", then you will be redirected to that page to complete it first before uploading will be allowed.  
                                </li>
                                <li>Enter a title and description for your video. Both are required in order to submit a file.
                                </li>
                                <li>The description should be a short summary of the media being uploaded.  When sharing the media with a group, and if you are willing to allowing others to reuse your media creation, please state in the description that the media can be reused under the Creative Commons License as stated in the upload user agreement.  
                                </li>
                                <li>For Group, select 'My profile' if not needing to share the media with a group, or select the group in which to share the media for the upload.
                                </li>
                                <li>If you have one or more groups listed, then you may select which group you would like the media file to be uploaded. If you are a student and unsure of which group to select, please contact your instructor before continuing.
                                </li>
                                <li>Select a 'Media Type', either video, audio, websites, documents, or images, from the drop down menu that matches the type of file or files uploading.
                                </li>
                                <li>Click the "Browse" button to locate the desired file you would like to upload. The file you select must be the same type that you selected from the drop down menu.
                                </li>
                                <li>Once you have the file type chosen, you can choose to place the name of the creator in the Author (optional) section.
                                </li>
                                <li>You may also create an Auto Removal Date, which will delete the file automatically when the date comes to pass.
                                </li>

                                <li>The last option you will have is to add a transcript.  If you choose yes, then a ‘Choose File’ button will appear to allow browsing to the transcript text file.  Please Note:  For audio and video to be used in online instruction, it must also have a transcript to be posted to make it ADA compliant.  The transcript file type for Northwest Cloud is a simple text file (.txt), giving a summary of what is being said and done in the video or audio file. 
                                </li>
                                <li>Lastly click the ‘Next’ to submit the information and begin the upload of the file(s).
                                </li>
                            </ol>
                        </td>
                    </tr>
                </table>



                <p>
                    <b>IMPORTANT NOTE: </b>Some files will take a while to upload, if you are uploading a large file, wait till the blue spinner icon is gone to navigate away from the current page, otherwise your file will not be uploaded.
                </p>
                <table>
                  
                    <tr>
                        <td width="50%" align="center">
                            <img src="helpImages/upload2.png" style="margin-bottom: 10px;" width="90%" />
                        </td>
                        <td>If you uploaded a video, it will be a selection screen with thumbnails. Select a thumbnail that you would like to represent your video and select submit at the bottom.  </td>
                    </tr>
                    
                      <tr>
                        <td width="50%" align="center">
                            <img src="helpImages/upload3.png" style="margin-bottom: 10px;" width="90%" />
                        </td>
                        <td>After the upload is complete, there will be a confirmation page loaded to confirm your upload.</td>
                    </tr>
                     <tr>
                        <td width="50%" align="center">
                            <img src="helpImages/upload4.png" style="margin-bottom: 10px;" width="90%" />
                        </td>
                        <td>     For video and audio media types, the uploaded file goes through a conversion process and it can take some time after you click the Finish” button before the media file appears in the desired media tab. Generally larger file types will take longer to process.</td>
                    </tr>
                </table>

            </div>
     


            <br />
            <br />
            <br />
            <br />









        </div>
</asp:Content>



