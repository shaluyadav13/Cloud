<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditStudentGroup.aspx.cs" Inherits="EditStudentGroup" MasterPageFile="~/MasterPage.master" %>

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
        Edit Group
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:Panel ID="pnlcreateOrEdit" runat="server" DefaultButton="saveInfoButton">
        <h2>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </h2>
        <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />

        <ul class="formList">

            <!--Group Name-->
            <li width="600px">
                <table>
                    <tr>
                        <td width="100px">
                            <span class="formLabel" style="font-weight: bold;">Group Name</span>
                        </td>
                        <td>
                            <asp:TextBox ID="groupNameTextBox" runat="server" MaxLength="30" CssClass="text" />
                        </td>
                    </tr>
                </table>
            </li>

            <br />

            <!--Owner ID-->
            <li width="600px">
                <table>
                    <tr>
                        <td width="100px">
                            <span class="formLabel" style="font-weight: bold;">Owner ID</span>
                        </td>
                        <td>
                            <asp:TextBox ID="facultyTextBox" runat="server" CssClass="text" MaxLength="50" />
                        </td>
                    </tr>
                </table>
            </li>

            <br />

            <!--Description-->
            <li width="600px">
                <table>
                    <tr>
                        <td width="100px" style="float: left;">
                            <span class="formLabel" style="float: left; padding-top: 0; font-weight: bold;">Description</span>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="descriptionTextBox" runat="server" Height="150px" Width="167px" TextMode="MultiLine" OnKeyPress="javascript:return checkLength(this, 500, event)" OnKeyDown="javascript:return checkLength(this, 500, event)" OnKeyUp="javascript:countCharacters(this, 500, 'charCount')" CssClass="text" />
                        </td>
                    </tr>
                </table>
            </li>

            <br />

            <!-- Drop Down For Add/Edit Group Members -->
            <div width="95%">
                <!--Edit Group Members Drop Down-->
                <span id="toggleAddEditGroupMembers" style="font-weight: bold; color: #204279; margin-left: 0px; padding: 5px;"><div class="caretClosedIcon" style="margin-top:-4px;"></div> Add/Edit Group Members</span>

                <!--Background for Dropdown-->

                    <br />

                    <asp:Panel ID="pnlStudents" runat="server" ClientIDMode="Static" style="background-color: lightgrey; display: none; width: 95%;" DefaultButton="submitStudentButton">
                        <ul class="formList">

                            <br />

                            <!--Northwest ID Number Enter-->
                            <li>
                                <span class="formLabel" style="font-weight: bold;">Enter a Northwest ID (S number for students) and click &quot;submit&quot;.</span>
                            </li>

                            <!--NW ID Enter Box and Submit-->
                            <li>&nbsp; <span class="formLabel"></span>
                                <asp:TextBox ID="newStudentTextBox" runat="server" MaxLength="50" CssClass="text" />
                                <asp:Button ID="submitStudentButton" runat="server" Text="Submit" style="margin-left: 42px;" OnClick="submitStudentButton_Click" />
                            </li>

                            <br />

                            <!--Batch Upload Members-->
                            <li>
                                <span class="formLabel" style="font-weight: bold;">Batch Upload a List of Members
                                </span>

                                <br />

                                <!--Batch Upload File Chooser and Submit -->
                                &nbsp;
                                <asp:FileUpload ID="studentFileUpload" runat="server" Width="190px" />
                                <asp:Button ID="uploadButton" runat="server" Text="Upload File" OnClick="uploadButton_Click" Style="margin-left: 0px;" />
                            </li>

                            <br />

                            <!--Current Members List-->
                            <li>
                                <span class="formLabel" style="font-weight: bold;">Current Members</span>

                                <br />

                                <!--Current Students List Box-->
                                &nbsp;
                                <asp:ListBox ID="studentsListBox" runat="server" Height="200px" Width="275px" />
                                <asp:Label ID="studentLabel" runat="server" Text="No Members" />
                            </li>

                            <!--Delete Selected Student Button-->
                            <li>
                                <span class="formLabel"></span>

                                &nbsp; 
                                <asp:Button ID="deleteStudentButton" runat="server" Text="Remove Selected" OnClick="deleteStudentButton_Click" />
                            </li>

                            <!-- -->
                            <li>
                                <asp:Label ID="studentErrorLabel" runat="server" ForeColor="Red" />
                            </li>
                            <br />
                        </ul>
                    </asp:Panel>
            </div>  
            <!-- End -->

            <br />

            <!--Start Date-->
            <li width="600px">
                <table>
                    <tr>
                        <td width="100px">
                            <span class="formLabel" style="font-weight: bold;">Start Date</span>
                        </td>
                        <td>
                            <asp:TextBox ID="startDateTextBox" runat="server" CssClass="text" />
                            <cc1:CalendarExtender ID="startDateTextBox_CalendarExtender" runat="server" Enabled="True" TargetControlID="startDateTextBox">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </li>

            <br />

            <!--End Date-->
            <li width="600px">
                <table>
                    <tr>
                        <td width="100px">
                            <span class="formLabel" style="font-weight: bold;">End Date</span>
                        </td>
                        <td style="width: 525px; float: left;">
                            <asp:TextBox ID="endDateTextBox" runat="server" CssClass="text" />
                            <cc1:CalendarExtender ID="endDateTextBox_CalendarExtender" runat="server" Enabled="True"
                                TargetControlID="endDateTextBox">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </li>

            <br />

            <!--Share with Owner/Group buttons-->
            <li width="600px">
                <table>
                    <tr>
                        <td width="100px" style="float:left">
                            <%-- <asp:Button ID="Btn_GroupVisibility" runat="server" OnClick="Button1_Click" Text="Share" Width="134px" />--%>

                            <span class="formLabel" style="font-weight: bold;">Share With </span>

                        <td>
                            <asp:RadioButtonList ID="chk_GroupVisibility" runat="server" OnSelectedIndexChanged="chk_GroupVisibility_CheckedChanged" ClientIDMode="Static" style="margin-top: -4px; margin-left: -8px;">
                            <asp:ListItem Value="ownerOnly">Group owner only</asp:ListItem>
                            <asp:ListItem Value="allMembers">All group members</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </li>

            <br />
            <br />


            <li>
                <span class="formLabel">&nbsp;</span> <span class="formLabel">
                <asp:Button ID="saveInfoButton" runat="server" Text="Save" OnClick="saveInfoButton_Click" />
                </span>
                <asp:Button ID="cancelInfoButton" runat="server" Text="Cancel" OnClick="cancelInfoButton_Click" />
            </li>
        </ul>
    </asp:Panel>
    <div style="margin-left:48px">
    <asp:Label ID="savedSuccesfully" ForeColor="green"  Font-Bold="true" runat="server" Visible="false">Saved Successfully</asp:Label>
        </div>
    <hr />
    <div id="body2">
    </div>
</asp:Content>



