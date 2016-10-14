<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEmbeddableRow.ascx.cs"
    Inherits="admin_masters_controls_AddEmbeddableRow" %>
<%@ Reference Control="EditEmbeddableRow.ascx" %>
<table>
<asp:PlaceHolder ID="plcEmbeddables" runat="server"></asp:PlaceHolder>
</table><br />
<asp:DropDownList ID="drpEmbeddables" runat="server" ClientIDMode="Static">
</asp:DropDownList>
&nbsp;|&nbsp;
<asp:LinkButton ID="lnkAddEmbeddable" runat="server" ClientIDMode="Static" 
    Text="<%$ Resources:Localization, AddEmbeddableText %>" onclick="lnkAddEmbeddable_Click"></asp:LinkButton>