<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditEmbeddableRow.ascx.cs"
    Inherits="admin_masters_controls_EditEmbeddableRow" %>
<tr class="embeddableRow">
    <td>
        <asp:Literal ID="litEmbeddableName" runat="server"></asp:Literal>
    </td>
    <td>
        <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Localization, EditText %>" CssClass="editEmbeddable"></asp:Button>
        <span><asp:Localize ID="orText" runat="server" Text="<%$ Resources:Localization, OrText %>"></asp:Localize></span>
        <asp:LinkButton ID="lnkDelete" runat="server" 
    Text="<%$ Resources:Localization, RemoveText %>" onclick="lnkDelete_Click"></asp:LinkButton>
    </td>
</tr>
