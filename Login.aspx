<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon"/>
    <meta name="viewport" content="width=device-width, initial-scale=.70"/>
     <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <title>NW Cloud Login</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <div id="loginPage">
            <div id="loginImage"><img src="images/nwLogoLoginScreen.png" alt="NwLogo"  /></div>
            <div id ="loginPageContent">
                <div id ="loginInformation">
        
        <asp:TextBox ID="usernameTextBox" runat="server" Width="162px" placeholder="Northwest Username" BorderStyle="Solid" BorderWidth="1px" BorderColor="#243E77" style="padding: 4px;" autofocus></asp:TextBox>
        <br />
        <asp:TextBox ID="passwordTextBox" runat="server" Width="162px" placeholder="Northwest Password" TextMode="Password" BorderStyle="Solid" BorderWidth="1px" BorderColor="#243E77"  style="padding: 4px;" ></asp:TextBox>
        <p>
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" ForeColor="white"  Text="Sign in" BackColor="#243E77" Width="172px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#243E77" style="padding: 4px;"/>
        </p>
        <asp:Label ID="errorLabel" runat="server"></asp:Label>
                </div>

            </div>
              <div style="margin-left:15px;margin-top:5px;">
            Northwest Cloud is a multi-media repository for Northwest faculty staff and students.  It was created in the CITE Office by student programmers. It uses HTML5 technology which means it will work best with the latest versions of the browsers that are available.
        </div>
        </div>
      
    </form>
</body>
</html>
