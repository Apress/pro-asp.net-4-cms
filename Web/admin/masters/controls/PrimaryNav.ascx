<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrimaryNav.ascx.cs" Inherits="admin_masters_controls_PrimaryNav" %>
<div class="collapse" style="border: none;">
    <h3><asp:Localize ID="navLinkText" runat="server" Text="<%$ Resources:Localization, NavigationHeaderText %>"></asp:Localize></h3>
    <div style="border: 1px solid #000;">
    <ul>
        <li><a href="/admin/home.aspx"><asp:Localize ID="homeLink" runat="server" Text="<%$ Resources:Localization, HomeLinkText %>"></asp:Localize></a></li>
        <li><a href="/admin/content.aspx"><asp:Localize ID="contentLink" runat="server" Text="<%$ Resources:Localization, ContentLinkText %>"></asp:Localize></a></li>
        <li><a href="/admin/admin.aspx"><asp:Localize ID="adminLink" runat="server" Text="<%$ Resources:Localization, AdminLinkText %>"></asp:Localize></a></li>
        <li><a href="/" target="_blank"><asp:Localize ID="viewSiteLink" runat="server" Text="<%$ Resources:Localization, ViewSiteLinkText %>"></asp:Localize></a></li>
        <li><a href="/logout.ashx"><asp:Localize ID="logoutLink" runat="server" Text="<%$ Resources:Localization, LogoutLinkText %>"></asp:Localize></a></li>
    </ul>
    </div>
</div>
