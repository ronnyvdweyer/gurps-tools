<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataGrid_Tests.aspx.cs" Inherits="SCv20.Tools.Web.Views.Shared.DataGrid_Tests" %>

<%@ Register src="DataGrid.ascx" tagname="DataGrid" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataGrid Test Page</title>
    <link href="/Content/Styles/LayoutGrid.css" rel="stylesheet" type="text/css" />
</head>
<body style="padding: 0; margin: 0">
    <form id="form1" runat="server">
    <div style="padding: 0; margin: auto; width: 1000px; border: 1px solid red">
        <h1>My DataGrid Test Page</h1>
        
        <div style="border: 3px solid silver; height: 200px; margin: 20px 20px 20px 20px; padding:20px 20px 20px 20px">
        
            <uc1:DataGrid ID="DataGrid1" runat="server">
                <HeaderTemplate>
                    <b>Apenas um teste Final</b>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="myRow" class="tb-col al-l" style="width:380px">
                        <%#Eval("Name")%>
                    </div>
                    <div style="clear:both"></div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox runat="server" ID="txt_seletion" />
                    <asp:Button ID="SelectedItems" runat="server" CssClass="btn-ico-copy" ToolTip="Copy Selected Items" />
                </FooterTemplate>
            </uc1:DataGrid>
        
        </div>
    </div>
    </form>
</body>
</html>
