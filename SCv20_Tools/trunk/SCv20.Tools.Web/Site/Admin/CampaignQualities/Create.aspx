<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SCv20.Tools.Web.Site.Admin.CampaignQualities.Create" %>
<%@ Register src="~/Views/QualityEditor.ascx" tagname="QualityEditor" tagprefix="view" %>


<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Create New Quality
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <view:QualityEditor runat="server" ID="qualityEditor" Enabled="true"/>
</asp:Content>
