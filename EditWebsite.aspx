<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditWebsite.aspx.cs" Inherits="EditWebsite" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
            <img src="images/myVideosIconBlue.png" alt="myVideosIcon" style="float: left" />
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload"></div>
    </a>
    <a href="Help/help.aspx">
        <div id="help"></div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin"></div>
        </a>
    </asp:Panel>
      <a href="Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="navText">
        Edit Website
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">

        <h2>
            <asp:Label ID="welcomeNote" runat="server" />
        </h2>
        <br />


        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
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

            <p>
                You can edit the basic information for your website below. When you are finished,
        click &quot;Submit.&quot; You may click &quot;Cancel&quot; at any time if you decide
        you do not want to keep any changes.
            </p>
            <ul class="formList">
                <li>
                    <asp:Panel ID="studentGroupPanel" runat="server">

                        <ul class="formList">
                            <%-- <li><span class="formLabel">Share</span>
                        <asp:RadioButton ID="rbtn1" runat="server" AutoPostBack="true" Text="YES" 
                            oncheckedchanged="rbtn1_CheckedChanged" GroupName="share" Checked="true"/>
                        <asp:RadioButton ID="rbtn2" runat="server" AutoPostBack="true" Text="NO" 
                            oncheckedchanged="rbtn2_CheckedChanged" GroupName="share" />
                    </li>--%>
                            <div style="margin-left: -39px">
                                <li><span class="formLabel">Title</span> &emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;&nbsp;
                                                <div class="uploadTextShowMobile">
                                                    <br />
                                                </div>
                                    <asp:TextBox ID="titleTextBox" runat="server" MaxLength="50" Width="180px" CssClass="text" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="titleTextBox"
                                        ErrorMessage="Required" />
                                    <br />
                                    <br />
                                </li>
                            </div>
                        </ul>
                    </asp:Panel>
                </li>

                <li><span class="formLabel">Description</span> &emsp;&emsp;&emsp;<div class="uploadTextShowMobile">
                    <br />
                </div>
                    <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine" Height="20px"
                        OnKeyPress="javascript:return checkLength(this, 1000, event)" OnKeyDown="javascript:return checkLength(this, 1000, event)"
                        OnKeyUp="javascript:countCharacters(this, 1000, 'charCount')" CssClass="text" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="descriptionTextBox"
                        ErrorMessage="Required" CssClass="text" />
                    <br />
                    <br />
                </li>

                <br />
                <div class="copyright">
                    <div class="copyright1">
                        <li><span class="formLabel">Group</span>
                    </div>
                    &emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;<div class="uploadTextShowMobile">
                        <br />
                    </div>
                    <asp:ListBox ID="studentGroupListBox" runat="server" TextMode="MultiLine" CssClass="text" Width="186px"></asp:ListBox>

                    </li>
                    <li>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="studentGroupListBox"
                            ErrorMessage="Please select which group to post your website to.">  
                        </asp:RequiredFieldValidator></li>
                </div>
                <li><span class="formLabel">&nbsp;</span>
                    <div id="charCount" style="font-size: 10px; display: inline;">
                        &nbsp;
                    </div>
                </li>
                <li>
                    <span class="replaceFileLabel">Replace File</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileToUpload" runat="server" title="Test" Width="183px" />
                    <br />
                    <br />
                    <br />
                </li>
                <li><span class="formLabel">Author (optional)</span>&nbsp;&nbsp;&nbsp;&nbsp;<div class="uploadTextShowMobile">
                    <br />
                </div>
                    <asp:TextBox ID="authorTextBox" runat="server" Width="183px" />
                    <br />
                    <br />
                </li>
                <li><span class="formLabel">Auto-Removal Date</span><div class="uploadTextShowMobile">
                    <br />
                </div>
                    <asp:TextBox ID="autoRemoveDateTextBox" runat="server" Width="183px" />
                    <cc1:CalendarExtender ID="autoRemoveDateTextBox_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="autoRemoveDateTextBox">
                    </cc1:CalendarExtender>
                </li>
                <br />

                <%--  <li><span class="formLabel">Update Video(New vesrion)</span>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FileUpload1"
                ErrorMessage="Required" />
        </li>--%>


                <%--<%-- <li><span class="formLabel">Add Transcript</span>
            <asp:RadioButtonList ID="transcriptBtnList" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
            </asp:RadioButtonList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="transcriptBtnList"
                            ErrorMessage="Choose one">  
                        </asp:RequiredFieldValidator>
        </li>
        <li><span class="formLabel">Transcript File</span>
            <asp:FileUpload ID="FileUpload2" runat="server" />
        </li>--%>
                <li><span class="formLabel">&nbsp;</span>
                    <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False"
                        OnClick="cancelButton_Click" />
                    <div class="uploadTextShowMobile">&emsp;&emsp;&nbsp;&nbsp; </div>
                    <div class="uploadTextHiddenDesktop">&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;</div>
                    <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
                </li>
                <br />
                <br />
                <li><span class="formLabel">&nbsp;</span>
                    <asp:Button ID="removeVideoButton" runat="server" Text="Remove Website" CausesValidation="False"
                        OnClientClick="javascript:return confirm('Are you sure you want to delete this website? Click OK to delete.');"
                        OnClick="removeWebButton_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnClaim" runat="server" Text="Claim Website" Visible="false"
                OnClick="btnClaim_Click" />
                    &nbsp;&nbsp;
            <asp:Label ID="lbl_ClaimMessage" runat="server" ForeColor="Red" Visible="false"
                Text="Claimed Successfully"></asp:Label>
                </li>
                <div id="uploadingSpinnerContainer">
                    <div id="uploadingText" style="margin: 0 0 10px -175px; background-color: #4A7CC6; color: white; font-weight: bold; padding: 10px 150px 10px 90px; font-size: 1.3em;">
                        <span>Please wait. Uploading file...</span>
                    </div>

                    <div id="uploadingSpinner" class="loading"></div>
                </div>
                <li><span class="formLabel">&nbsp;</span>
                    <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
                </li>
            </ul>
        </div>


    </div>
</asp:Content>




