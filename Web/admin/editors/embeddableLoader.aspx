<%@ Page Language="C#" AutoEventWireup="true" CodeFile="embeddableLoader.aspx.cs"
    Inherits="admin_editors_embeddableLoader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="popupControls">
        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Localization, PopupSaveText %>"
            CssClass="popupSave" OnClick="btnSave_Click" />
    </div>
    <asp:PlaceHolder ID="plcEmbeddableControls" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
