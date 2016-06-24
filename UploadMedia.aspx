<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadMedia.aspx.cs" Inherits="pages_UploadMedia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.80 maximum-scale=.80" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
   <script type="text/javascript" src="js/master.js"></script>

    <title>Northwest Cloud</title>
    <script type="text/javascript">
        $(document).ready(function () {
            // we call the function
            transcriptOptionChanged();
        });
    </script>
<script type="text/javascript">


    function transcriptOptionChanged() {
        var radio1 = $('#<%= transcriptBtn_Yes.ClientID%>');
       
        if (radio1.is(':checked')) {
        $("#showTranscript").css("visibility", "visible");
        console.log("show");
    }
    else {
        $("#showTranscript").css("visibility", "hidden");
        console.log("hide");
    }

    
}
</script>
    <script type="text/javascript">
        function show(msg, copy) {
            var item = document.getElementById(copy);
            if (item.style.display == "none") {
                item.style.display = "block";
                item.innerHTML = msg;

            }
        }
        function hide(copy) {
            var item = document.getElementById(copy);

            if (item.style.display == "block") {
                item.style.display = "none";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload">
            <img src="images/uploadIconBlue.png" alt="upload" style="float: left" />
        </div>
    </a>
    <a href="Help/help.aspx" target="_new">
        <div id="help"></div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin">
            </div>
        </a>
    </asp:Panel>
    <a href="Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div id="navText" class="fontPlacement" style="padding-left: 20px; margin-top: 7px; font-weight: bolder; color: #243E77;">
            Upload
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div ID="errorLabelContainer" runat="server" style="background-color: #ff9c23; margin: 0 0 10px -10px; padding: 10px;" visible="false">
            <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" viewBox="0 0 180 180" style="vertical-align: middle">
                    <path fill="none" stroke="#fff" stroke-width="20" stroke-linecap="round" d="M89,9a81,81 0 1,0 2,0zm1,38v58m0,25v1"></path>
            </svg>
            <asp:Label ID="errorLabel" runat="server" style="vertical-align: middle; color: white; font-size: 1.3em; font-weight: bold;"/>
        </div>


        <ul class="formList" id="uploadFormContainer" runat="server">
            <li>
                        <div class="uploadTextHiddenDesktop">
            <div>

                <p>
                    Enter a title, description, group,
        and then click &quot;Browse...&quot; to select the media. When the media is ready
        it will be on your "My Media" page.
                </p>

            </div>
        </div>
                <asp:Panel ID="studentPanel" runat="server">
                    <p style="font-style: italic;">
                        Please select the proper group for this
                    media to be posted to. If you are unsure of which group to use, please ask your
                    instructor.
                    </p>
                    <ul class="formList">
                    </ul>
                </asp:Panel>
            </li>
            <li><span class="formLabel">Title<span style="color: red;">&nbsp*</span></span> &emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="uploadTextShowMobile">
                    <br />
                </div>
                <asp:TextBox ID="titleTextBox" runat="server" MaxLength="50" Width="300 px" CssClass="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="titleTextBox"
                    ErrorMessage="Required" />
                <br />
                <br />
            </li>
            <li><span class="formLabel">Description<span style="color: red;">&nbsp*</span></span> &emsp;&emsp;&emsp;<div class="uploadTextShowMobile">
                <br />
            </div>
                <asp:TextBox ID="descriptionTextBox" Text="No description" runat="server" TextMode="MultiLine" Height="20px" Width="299 px" CssClass="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ControlToValidate="descriptionTextBox"
                    ErrorMessage="Required" CssClass="text" />
                <br />
                <br />
            </li>

            <li><span class="formLabel">&nbsp;</span>
                <div id="charCount" style="font-size: 10px; display: inline;">
                    &nbsp;
                </div>
            </li>

            <div class="copyright">
                <div class="copyright1">
                    <li><span class="formLabel">Group<span style="color: red;">&nbsp*</span></span>
                </div>
                &emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<div class="uploadTextShowMobile">
                    <br />
                </div>
                <asp:ListBox ID="studentGroupListBox" runat="server" TextMode="MultiLine" SelectionMode="Multiple" CssClass="text" Width="305px"></asp:ListBox>
                <asp:Label ID="lblNogroup" runat="server" Visible="false" Style="position: absolute">Uploading the media to your profile</asp:Label>
                </li>
                    <li>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ControlToValidate="studentGroupListBox"
                            ErrorMessage="Please select which group to post your video to.">  
                        </asp:RequiredFieldValidator></li>
            </div>
            <div id="type" runat="server">
                <li><span class="formLabel">Media Type<span style="color: red;">&nbsp*</span></span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="uploadTextShowMobile">
                    <br />
                </div>
                    <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="static" onchange="mediaTypeChanged()">


                        <asp:ListItem Value="select">Select Type</asp:ListItem>
                        <asp:ListItem Value="Video">Video</asp:ListItem>
                        <asp:ListItem Value="Audio">Audio</asp:ListItem>
                        <asp:ListItem Value="Website">Website</asp:ListItem>
                        <asp:ListItem Value="Documents">Documents</asp:ListItem>
                        <asp:ListItem Value="Images">Images</asp:ListItem>


                    </asp:DropDownList>
                </li>
            </div>
            <br />
            <br />


            <div id="uploadTranscript" runat="server">
                <li><span class="formLabel">File<span style="color: red;">&nbsp*</span></span>&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class="uploadTextShowMobile">
                    <br />
                </div>
                    &nbsp;&nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static" ControlToValidate="FileUpload1"
                        ErrorMessage="Required" />
                    <div class="uploadTextShowMobile">
                        <br />
                        <br />
                    </div>


                    <!-- Description text for required format when uploading a file. -->
                    <p id="uploadTypeInfo" style="visibility: hidden; padding-left: 125px;">
                        <br />
                    </p>
                    <!-- END -->
                    <br />
                    <br />
                </li>
                <li><span class="formLabel">Author (optional)</span>&nbsp;&nbsp;&nbsp;&nbsp;<div class="uploadTextShowMobile">
                    <br />
                </div>
                    <asp:TextBox ID="authorTextBox" runat="server" Width="300 px" />
                    <br />
                    <br />
                </li>

                <!-- START: AUTO REMOVAL DATE -->

                <li><span class="formLabel">Auto-Remove Date</span><div class="uploadTextShowMobile">
                    <br />
                </div>
                    <asp:TextBox ID="autoDeleteDateTextBox" runat="server" Width="300 px" />
                    <cc1:CalendarExtender ID="autoDeleteDateTextBox_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="autoDeleteDateTextBox">
                    </cc1:CalendarExtender>
                    <br />
                    <br />
                </li>

                <!-- END: AUTO REMOVAL DATE -->

                <div id="transcriptVisibility" runat="server" clientidmode="Static">

                    <li><span class="formLabel">Add Transcript</span>&emsp;&nbsp;&nbsp;&nbsp;<div class="uploadTextShowMobile">
                        <br />
                    </div>

                        <asp:RadioButton ID="transcriptBtn_Yes" runat="server" Text="YES" onchange="transcriptOptionChanged()" 
                            GroupName="transccript" />
                        <asp:RadioButton ID="transcriptBtn_No" runat="server" Text="NO" onchange="transcriptOptionChanged()" 
                            GroupName="transccript" Checked="true" />
                                </li>
               </div>



                <li>
                    <asp:Panel ID="showTranscript" runat="server"  clientidmode="Static" >
                        <%--<div id="transcript" style="display: none">--%>
                        <span class="formLabel" style="margin-top:10px;">Transcript File</span>
                        <asp:FileUpload ID="FileUpload_transcript" runat="server" />
                        <%-- </div>--%>
                    </asp:Panel>

                </li>
                <li>
                    <div id="emailCheckBoxContainer">
                    <asp:CheckBox runat="server" ID="emailCheckBox"
                           Text="Email me when the video becomes available" checked="false" ClientIDMode="Static"/>
                     </div>
                </li>
                <li style="margin-bottom: 50px">

                    <br />
                    <span><span style="color: red;">*&nbsp</span>Required Field</span>
                    <br />
                    <br />
                    <br />

                    <asp:Button ID="cancelButton" runat="server" ClientIDMode="Static" Text="Cancel" CausesValidation="False" Width="100 px"
                        OnClick="cancelButton_Click" />
                    <div class="uploadTextShowMobile">&emsp;&emsp;&nbsp;&nbsp; </div>
                    <div class="uploadTextHiddenDesktop">&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;</div>

                    <asp:Button ID="nextButton" runat="server" ClientIDMode="Static" Text="Next" Width="100 px" OnClick="nextButton_Click" />


                    <br />
                    <br />

                    <div id="uploadingSpinnerContainer">
                        <div id="uploadingText" style="margin: 0 0 10px -175px; background-color: #4A7CC6; color: white; font-weight: bold; padding: 10px 150px 10px 90px; font-size: 1.3em;">
                            <span>Please wait. Uploading file...</span>
                        </div>

                        <div id="uploadingSpinner" class="loading"></div>
                    </div>

                    <span class="formLabel">&nbsp;</span>

                </li>

            </div>
        </ul>

    </div>


</asp:Content>
