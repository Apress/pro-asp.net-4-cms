<%@ Page Title="Manage Sites | CMS" Language="C#" MasterPageFile="~/admin/masters/admin.master"
    AutoEventWireup="true" CodeFile="editSites.aspx.cs" Inherits="admin_editors_editSites"
    ClientIDMode="Static" %>

<%@ Register Src="~/admin/masters/controls/PrimaryNav.ascx" TagPrefix="cms" TagName="PrimaryNav" %>
<%@ Register Src="~/admin/masters/controls/SiteDropDown.ascx" TagPrefix="cms" TagName="SiteDropDown" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/admin/js/editSite.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Localize ID="manageSitesText" runat="server" Text="<%$ Resources:Localization, ManageSitesText %>"></asp:Localize></h2>
    <div id="subcontainer">
        <div id="sideNav">
            <cms:PrimaryNav ID="primaryNav" runat="server" />
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="createNewSiteText" runat="server" Text="<%$ Resources:Localization, CreateNewSiteText %>"></asp:Localize></h3>
            <div class="collapse">
                <p>
                    <asp:Localize ID="createSiteIntro" runat="server" Text="<%$ Resources:Localization, CreateSiteIntro %>"></asp:Localize>
                </p>
                <table class="tableForm">
                    <tr>
                        <td>
                            <asp:Localize ID="siteTitleField" runat="server" Text="<%$ Resources:Localization, SiteTitleField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="domainField" runat="server" Text="<%$ Resources:Localization, DomainField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomain" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Button ID="btnCreateSite" runat="server" 
                        Text="<%$ Resources:Localization, CreateSiteButtonText %>" 
                        onclick="btnCreateSite_Click" CssClass="createSite" />
                </p>
            </div>
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="editSiteText" runat="server" Text="<%$ Resources:Localization, EditSiteText %>"></asp:Localize></h3>
            <div class="collapse">
                <p>
                    <asp:Localize ID="editSiteIntro" runat="server" Text="<%$ Resources:Localization, EditSiteIntro %>"></asp:Localize>
                </p>
                <table class="tableForm">
                    <tr>
                        <td>
                            <asp:Localize ID="editSiteTitleField" runat="server" Text="<%$ Resources:Localization, SiteTitleField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSiteTitleUpdate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="editDomainField" runat="server" Text="<%$ Resources:Localization, DomainField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomainUpdate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Button ID="btnUpdateSite" runat="server" 
                        Text="<%$ Resources:Localization, UpdateSiteButtonText %>" 
                        onclick="btnUpdateSite_Click" CssClass="updateSite" />
                </p>
            </div>
        </div>
    </div>
</asp:Content>
