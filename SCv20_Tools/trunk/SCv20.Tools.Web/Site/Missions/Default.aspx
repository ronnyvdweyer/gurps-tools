<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SCv20.Tools.Web.Site.Missions.Default" %>
<%@ Register src="~/Views/QualityGrid.ascx" tagname="QualityGrid" tagprefix="view" %>

<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Mission Design
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <view:QualityGrid ID="Grid" runat="server" />
</asp:Content>
