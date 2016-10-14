<%@ Page Title="Content | CMS" Language="C#" MasterPageFile="~/admin/masters/admin.master"
    AutoEventWireup="true" CodeFile="content.aspx.cs" Inherits="admin_content" %>

<%@ Register Src="~/admin/masters/controls/PrimaryNav.ascx" TagPrefix="cms" TagName="PrimaryNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Localize ID="contentText" runat="server" Text="<%$ Resources:Localization, ContentText %>"></asp:Localize></h2>
    <div id="subcontainer">
        <div id="sideNav">
            <cms:PrimaryNav ID="primaryNav" runat="server" />
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="contentManagementText" runat="server" Text="<%$ Resources:Localization, ContentManagementText %>"></asp:Localize></h3>
            <div class="collapse">
                    <div class="siteTree">
                        <p>
                            <asp:Localize ID="treeContentsText" runat="server" Text="<%$ Resources:Localization, TreeContentsText %>"></asp:Localize>
                        </p>
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
                        <p>
                            <a href="/admin/editors/editContent.aspx"><asp:Localize ID="createContentText" runat="server" Text="<%$ Resources:Localization, CreateContentText %>"></asp:Localize></a> |
                            <asp:LinkButton ID="lnkShowAll" runat="server" Text="<%$ Resources:Localization, ExpandTreeText %>" OnClick="lnkShowAll_Click"></asp:LinkButton>
                        </p>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
