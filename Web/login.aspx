<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login | CMS</title>
    <link href="admin/css/admin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.pack.js"></script>
    <script src="/js/iframeTest.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="nav">
        <h1>
            CMS<span>2010</span></h1>
    </div>
    <div id="main">
        <h2>
            <asp:Localize ID="loginText" runat="server" Text="<%$ Resources:Localization, LoginText %>"></asp:Localize></h2>
        <div id="subcontainer">
            <div id="login">
                <h3>
                    <asp:Localize ID="loginPanelHeader" runat="server" Text="<%$ Resources:Localization, LoginPanelHeader %>"></asp:Localize>
                </h3>
                <div class="collapse">
                    <table>
                        <tr>
                            <td>
                                <span><asp:Localize ID="loginUsername" runat="server" Text="<%$ Resources:Localization, LoginUsername %>"></asp:Localize></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><asp:Localize ID="loginPassword" runat="server" Text="<%$ Resources:Localization, LoginPassword %>"></asp:Localize></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: right;">
                                <asp:Button ID="btnLogin" runat="server" Text="<%$ Resources:Localization, LoginButtonText %>" OnClick="btnLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="footer">
            <p><asp:Localize ID="footerText" runat="server" Text="<%$ Resources:Localization, FooterText %>"></asp:Localize></p>
        </div>
    </div>
    </form>
</body>
</html>
