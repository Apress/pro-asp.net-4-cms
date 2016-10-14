<%@ Page Language="C#" AutoEventWireup="true" CodeFile="content.aspx.cs" Inherits="content" ClientIDMode="Static" %>

<!doctype html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CMS Content Page</title> 
    <link href="/css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.pack.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="cmsControls" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
