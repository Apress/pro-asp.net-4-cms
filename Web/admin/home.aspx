<%@ Page Title="Home | CMS" Language="C#" MasterPageFile="~/admin/masters/admin.master"
    AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="admin_home" %>

<%@ Register Src="~/admin/masters/controls/PrimaryNav.ascx" TagPrefix="cms" TagName="PrimaryNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Localize ID="homeText" runat="server" Text="<%$ Resources:Localization, HomeText %>"></asp:Localize></h2>
    <div id="subcontainer">
        <div id="sideNav">
            <cms:PrimaryNav ID="primaryNav" runat="server" />
        </div>
        <div class="collapseWrapper">
            <h3>
                <asp:Localize ID="newsAndAlertsText" runat="server" Text="<%$ Resources:Localization, NewsAndAlertsText %>"></asp:Localize></h3>
            <div class="collapse">
                <asp:PlaceHolder ID="plcEntries" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <asp:Panel ID="pnlSysAdmin" runat="server" Visible="false">
            <div class="collapseWrapper">
                <h3>
                    <asp:Localize ID="postNewsText" runat="server" Text="<%$ Resources:Localization, PostNewsText %>"></asp:Localize></h3>
                <div class="collapse">
                    <p>
                        <asp:Localize ID="postNewsIntroText" runat="server" Text="<%$ Resources:Localization, PostNewsIntroText %>"></asp:Localize>
                    </p>
                    <div class="ulForm">
                        <asp:TextBox ID="txtNewsEntry" TextMode="MultiLine" Rows="1" runat="server" CssClass="adminNewsEntryBox"></asp:TextBox>
                        <asp:Button ID="btnPost" runat="server" Text="<%$ Resources:Localization, PostButtonText %>"
                            CssClass="submitButton" OnClick="btnPost_Click" OnClientClick="javascript:return confirm('Are you sure you want to publish this news entry to the system?');" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
