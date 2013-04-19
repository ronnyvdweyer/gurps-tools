<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SCv20.Tools.Web._Default" %>


<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Welcome
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Width="134px"></asp:Label><br />
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Initialize Database" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
