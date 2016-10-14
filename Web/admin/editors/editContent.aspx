<%@ Page Title="Edit Content | CMS" Language="C#" MasterPageFile="~/admin/masters/admin.master"
    AutoEventWireup="true" CodeFile="editContent.aspx.cs" Inherits="admin_editors_editContent"
    ClientIDMode="Static" %>

<%@ Register Src="~/admin/masters/controls/PrimaryNav.ascx" TagPrefix="cms" TagName="PrimaryNav" %>
<%@ Register Src="~/admin/masters/controls/SiteDropDown.ascx" TagPrefix="cms" TagName="SiteDropDown" %>
<%@ Register Src="~/admin/masters/controls/AddEmbeddableRow.ascx" TagPrefix="cms"
    TagName="AddEmbeddableRow" %>
<%@ Register Src="~/admin/masters/controls/ContentVersionRow.ascx" TagPrefix="cms"
    TagName="ContentVersionRow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/admin/js/editContent.js"></script>
    <h2>
        <asp:Localize ID="editContentText" runat="server" Text="<%$ Resources:Localization, EditContentText %>"></asp:Localize></h2>
    <div id="subcontainer">
        <div id="sideNav">
            <cms:PrimaryNav ID="primaryNav" runat="server" />
        </div>
        <div id="dialog">
            <p>
                <a href="#" class="close">
                    <asp:Localize ID="popupCancelText" runat="server" Text="<%$ Resources:Localization, PopupCancelText %>"></asp:Localize></a>
            </p>
            <div id="dialogContents">
                <iframe id="dialogFrame" frameborder="0"></iframe>
            </div>
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="pageEditorText" runat="server" Text="<%$ Resources:Localization, PageEditorText %>"></asp:Localize></h3>
            <div class="collapse">
                <p>
                    <asp:Localize ID="pageEditorIntro" runat="server" Text="<%$ Resources:Localization, PageEditorIntro %>"></asp:Localize>
                </p>
                <table class="tableForm">
                    <tr>
                        <td>
                            <asp:Localize ID="pageTitleField" runat="server" Text="<%$ Resources:Localization, PageTitleField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="pageAuthorField" runat="server" Text="<%$ Resources:Localization, PageAuthorField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="pageUrlField" runat="server" Text="<%$ Resources:Localization, PageURLField%>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtURL" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <hr />
                <p>
                    <asp:Localize ID="pageEditorEmbeddableIntro" runat="server" Text="<%$ Resources:Localization, PageEditorEmbeddableIntro %>"></asp:Localize>
                </p>
                <ul class="ulForm">
                    <li>
                        <asp:CheckBox ID="chkHeader" runat="server" Text="<%$ Resources:Localization, HeaderBucketText %>" />
                        <asp:Panel ID="pnlHeaderEmbeddables" runat="server" CssClass="pnlEmbeddable">
                            <cms:AddEmbeddableRow ID="AddHeaderEmbeddableRow" runat="server" />
                        </asp:Panel>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkPrimaryNav" runat="server" Text="<%$ Resources:Localization, PrimaryNavBucketText %>" />
                        <asp:Panel ID="pnlPrimaryNavEmbeddables" runat="server" CssClass="pnlEmbeddable">
                            <cms:AddEmbeddableRow ID="AddPrimaryNavEmbeddableRow" runat="server" />
                        </asp:Panel>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkContent" runat="server" Text="<%$ Resources:Localization, ContentBucketText %>" />
                        <asp:Panel ID="pnlContentEmbeddables" runat="server" CssClass="pnlEmbeddable">
                            <cms:AddEmbeddableRow ID="AddContentEmbeddableRow" runat="server" />
                        </asp:Panel>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkSubNav" runat="server" Text="<%$ Resources:Localization, SubNavBucketText %>" />
                        <asp:Panel ID="pnlSubNavEmbeddables" runat="server" CssClass="pnlEmbeddable">
                            <cms:AddEmbeddableRow ID="AddSubNavEmbeddableRow" runat="server" />
                        </asp:Panel>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkFooter" runat="server" Text="<%$ Resources:Localization, FooterBucketText %>" />
                        <asp:Panel ID="pnlFooterEmbeddables" runat="server" CssClass="pnlEmbeddable">
                            <cms:AddEmbeddableRow ID="AddFooterEmbeddableRow" runat="server" />
                        </asp:Panel>
                    </li>
                </ul>
                <hr />
                <asp:Panel ID="noContent" runat="server">
                    <p>
                        <asp:Localize ID="noContentText" runat="server" Text="<%$ Resources:Localization, NoContentText %>"></asp:Localize>
                    </p>
                </asp:Panel>
                <asp:Panel ID="content" runat="server">
                    <div class="siteTree">
                        <p>
                            <asp:Localize ID="treeContentText" runat="server" Text="<%$ Resources:Localization, TreeContentText %>"></asp:Localize></p>
                        <div class="ulForm">
                            <asp:TreeView ID="siteTree" runat="server" ImageSet="Simple" OnSelectedNodeChanged="siteTree_SelectedNodeChanged">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                                    NodeSpacing="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                            </asp:TreeView>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="seoText" runat="server" Text="<%$ Resources:Localization, SEOText %>"></asp:Localize></h3>
            <div class="collapse">
                <p>
                    <asp:Localize ID="pageEditorSEOIntro" runat="server" Text="<%$ Resources:Localization, PageEditorSEOIntro %>"></asp:Localize>
                </p>
                <table class="tableForm">
                    <tr>
                        <td>
                            <asp:Localize ID="keywordsField" runat="server" Text="<%$ Resources:Localization, KeywordsField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeywords" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="descriptionField" runat="server" Text="<%$ Resources:Localization, DescriptionField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="visibleField" runat="server" Text="<%$ Resources:Localization, VisibleField %>"></asp:Localize>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkExcludeSearch" CssClass="chkNoIndex" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Localize ID="followLinksField" runat="server" Text="<%$ Resources:Localization, FollowLinksField%>"></asp:Localize>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkFollowLinks" CssClass="chkNoFollow" runat="server" Checked="true" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="versionControlText" runat="server" Text="<%$ Resources:Localization, VersionControlText %>"></asp:Localize></h3>
            <div class="collapse">
                <p>
                    <asp:Localize ID="versionControlIntro" runat="server" Text="<%$ Resources:Localization, VersionControlIntro %>"></asp:Localize>
                </p>
                <ul class="ulForm">
                    <li>
                        <cms:ContentVersionRow ID="c1" runat="server" />
                    </li>
                </ul>
                <hr />
                <asp:Button ID="btnSave" runat="server" 
                    Text="<%$ Resources:Localization, SaveRevisionText %>" 
                    onclick="btnSave_Click" />
            </div>
        </div>
    </div>
    <div id="mask">
    </div>
    <!-- used as transparency overlay -->
</asp:Content>
