<%@ Page Title="System Administration | CMS" Language="C#" MasterPageFile="~/admin/masters/admin.master"
    AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin_admin" %>

<%@ Register Src="~/admin/masters/controls/PrimaryNav.ascx" TagPrefix="cms" TagName="PrimaryNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Localize ID="sysAdminText" runat="server" Text="<%$ Resources:Localization, SysAdminText %>"></asp:Localize></h2>
    <div id="subcontainer">
        <div id="sideNav">
            <cms:PrimaryNav ID="primaryNav" runat="server" />
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="administrationText" runat="server" Text="<%$ Resources:Localization, AdministrationText %>"></asp:Localize></h3>
            <div class="collapse">
                <h4>
                    <asp:Localize ID="sitesText" runat="server" Text="<%$ Resources:Localization, SitesText %>"></asp:Localize></h4>
                <p>
                    <a href="/admin/editors/editSites.aspx">
                        <asp:Localize ID="administerLinkText" runat="server" Text="<%$ Resources:Localization, AdministerLinkText %>"></asp:Localize></a>
                    <asp:Localize ID="administerLinkText2" runat="server" Text="<%$ Resources:Localization, AdministerLinkText2 %>"></asp:Localize>
                </p>
                <h4>
                    <asp:Localize ID="pluginsText" runat="server" Text="<%$ Resources:Localization, PluginsText %>"></asp:Localize></h4>
                <p>
                    <a href="plugins.aspx">
                        <asp:Localize ID="viewPluginsLinkText" runat="server" Text="<%$ Resources:Localization, ViewPluginsLinkText%>"></asp:Localize></a>
                    <asp:Localize ID="viewPluginsLinkText2" runat="server" Text="<%$ Resources:Localization, ViewPluginsLinkText2 %>"></asp:Localize>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
