<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataGrid_Tests.aspx.cs" Inherits="SCv20.Tools.Web.Views.Shared.DataGrid_Tests" %>
<%@ Register src="~/Views/Shared/DynamicGrid.ascx" tagname="DynamicGrid" tagprefix="uc1" %>

<%--
<%@ Register src="~/Views/Shared/DataGrid.ascx" tagname="DataGrid" tagprefix="uc1" %>
--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataGrid Test Page</title>
   <%-- <link href="/Content/Styles/LayoutGrid.css" type="text/css" rel="stylesheet" />--%>
</head>
<body style="padding: 0; margin: 0">
    <form id="form1" runat="server">
    <div style="padding: 0; margin: auto; width: 1200px; border: 1px solid red">
        <h1>My DataGrid Test Page</h1>
        
        <%--<div style="border: 3px solid silver; height: 200px; margin: 20px 20px 20px 20px; padding:20px 20px 20px 20px">
        
            <uc1:DataGrid ID="DataGrid1" runat="server" Visible="false">
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
        
        </div>--%>
        <br />
        <hr />
        <br />

        <div style="width:800px; margin:auto">
            <uc1:DynamicGrid runat="server" ID="myGrid" Title="Grid de Teste">
            
            </uc1:DynamicGrid>
        </div>

        <%--<br />
        <hr />
        <br />

        <style type="text/css">
            .row {
                overflow: hidden;
                width: 100%;
                border:3px solid #333;
            }
        </style>

        <table style="border-collapse:collapse"  border="1px" >
            <tr>
                <th style="padding:0;margin:0;">
                    <span style="display:block; word-wrap: break-word; border:1px solid red; overflow:hidden; width:120px">VeryLongTextMustFitInTheSpecifiedSize</span>
                </th>
                <th style="padding:0;margin:0;">
                    <span style="display:block; word-wrap: break-word; border:1px solid red; overflow:hidden; width:120px">Coluna 2</span>
                </th>
            </tr>
            <tr>
                <td style="padding:0;margin:0;">
                    <span style="display:block; word-wrap: break-word; border:1px solid red; overflow:hidden; width:120px">VeryLongTextMustFitInTheSpecifiedSize</span>
                </td>

                <td style="padding:0;margin:0;">
                     <span style="display:block; word-wrap: break-word; border:1px solid red; overflow:hidden; width:120px">Coluna 2</span>
                </td>
            </tr>
        </table>
        
        <br />--%>
        
        

    </div>
    </form>
</body>
</html>
