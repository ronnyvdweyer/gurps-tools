<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SCv20.Tools.Web.Site.WebForm1" %>
<%@ Register src="../Views/Shared/Pager.ascx" tagname="Pager" tagprefix="view" %>


<%@ Register src="../Views/QualityGrid.ascx" tagname="QualityGrid" tagprefix="uc1" %>


<%@ Register src="../Views/Shared/CKEditor.ascx" tagname="CKEditor" tagprefix="uc2" %>


<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Teste de GridView
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:QualityGrid ID="QualityGrid1" runat="server" Visible="false"/>
</asp:Content>
