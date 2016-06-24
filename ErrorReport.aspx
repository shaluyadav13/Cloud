<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorReport.aspx.cs" Inherits="ErrorReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.80 maximum-scale=.80 " />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>

    <title>Northwest Cloud</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload"></div>
    </a>
    <a href="Help/help.aspx" >
        <div id="help">

<!-- Changed help icon normal into help icon blue for clarity -->
            <img src="images/helpIconBlue.png" alt="upload" style="float: left" />
        </div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin">
            </div>
        </a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="navText">
        Report an Error
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />

<!-- START: Changes to Error Report Text -->
        <asp:Label runat="server" ID="errorReportText" Text="Please be as thorough as possible when describing the error(s) you encountered. Thank You!"></asp:Label><br />
        <br />

        <!-- I have to add in a form here so that the user can enter in their name and description of the error they            found. Then be able to send that form to CITE@NWMISSOURI.EDU 
            
            Will have to replace the mailto:someone@example.com to be the actual CITE email-->


    <form>      
        <div class="row">
		    <label for="message"></label><br />
		    <asp:TextBox id="userMessage" runat="server" class="input" name="message" rows="7" Columns="50" TextMode="multiline" ></asp:TextBox>
            <br />
	    </div>
        <br />


	    <asp:Button id="submit_button" runat="server" text="Send Report" OnClick="submit_button_Click" />
        <asp:Label ID="successMessage" runat="server" ></asp:Label>
    </form>	

        <!-- Figure out how to use this 


  <system.net>
    <mailSettings>
      <smtp from="citedev@nwmissouri.edu">
        <network host="email.nwmissouri.edu" port="25" userName="citedev@nwmissouri.edu" password="su14develop!" enableSsl="true" />
      </smtp>
    </mailSettings>
  </system.net>


<script type="text/javascript">

// Code for sending the Error Report Directly to the cite@nwmissouri.edu email from the website
function sendMail(name, studentEmail, content)
{
    section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

    var emailTo = "fatmandecker@gmail.com"; // Will change to cite@nwmissouri.edu when done testing
    var subject = "NWCloud Error Report";
    var body = "Error recoreded by " + name + ". \nContent: \n" + content;

    using (mail = new MailMessage())
    {
        mail.From = new MailAddress(section.From);
        mail.To.Add(toEmail);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = false;

        using (smtp = new SmtpClient())
        {
            smtp.Credentials = new System.Net.NetworkCredentia(section.Network.UserName,section.Network.Password);
            smtp.EnableSsl = section.Network.EnableSsl;
            smtp.Send(mail);
        }
    }
}
</script>


<!-- END -->

    </div>
</asp:Content>

