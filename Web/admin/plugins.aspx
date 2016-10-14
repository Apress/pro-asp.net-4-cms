<%@ Page Title="Plugins and Modules | CMS" Language="C#" MasterPageFile="~/admin/masters/admin.master"
    AutoEventWireup="true" CodeFile="plugins.aspx.cs" Inherits="admin_plugins" %>

<%@ Register Src="~/admin/masters/controls/PrimaryNav.ascx" TagPrefix="cms" TagName="PrimaryNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Localize ID="pluginsAndModulesText" runat="server" Text="<%$ Resources:Localization, PluginsAndModulesText %>"></asp:Localize></h2>
    <div id="subcontainer">
        <div id="sideNav">
            <cms:PrimaryNav ID="primaryNav" runat="server" />
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="currentPluginsText" runat="server" Text="<%$ Resources:Localization, CurrentPluginsText %>"></asp:Localize></h3>
            <div class="collapse">
                <p>
                    <asp:Localize ID="pluginsIntroText" runat="server" Text="<%$ Resources:Localization, PluginsIntroText %>"></asp:Localize></p>
                <ul>
                    <asp:Literal ID="litPlugins" runat="server"></asp:Literal>
                </ul>
                <p>
                    <asp:Localize ID="repositoryIntroText" runat="server" Text="<%$ Resources:Localization, RepositoryIntroText %>"></asp:Localize></p>
                <ul>
                    <asp:Literal ID="litLocation" runat="server"></asp:Literal>
                </ul>
                <asp:GridView ID="grdPlugins" runat="server">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
