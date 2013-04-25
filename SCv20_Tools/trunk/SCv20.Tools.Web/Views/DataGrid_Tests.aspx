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
    <div style="padding: 0; margin: auto; width: 1000px; border: 1px solid red">
        <h1>My DataGrid Test Page</h1>
        
        <div style="width:900px; margin:auto">
            <uc1:DynamicGrid runat="server" ID="myGrid" 
                CssClass            =""
                PageSize            ="2" 
                PaginateResults     ="true" 
                Title               ="Qualities" 
                TitleCssClass       ="" 
                TitleStyle          ="">
                
                <%--<DynamicColumns>
                    <DynamicColumn ItemValue="Name" 
                        DataItemCssClass="" 
                        DataItemStyle   ="" 
                        DataItemValue   ="Id" 
                        HeaderCssClass  =""
                        HeaderStyle     ="" 
                        HeaderWidth     ="30px" 
                        HeaderType      ="Checkbox" 
                        HeaderText      ="Quality" 
                        DataItemValueID ="Id"/>
                </DynamicColumns>--%>
                
            </uc1:DynamicGrid>
            
        </div>

        <hr />

        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />

        <hr />

        <table style="table-layout: fixed; width:100%" >
            <tr>
                <td style="width:100px; border:1px solid red; word-wrap:break-word">
                    <span style="display:block;">
                    ThisIsVeryLongTextThaShouldBeBrokedOnCharacterLevlNotInWrapLevel.
                    </span>
                </td>
                <td style="border: 1px solid red">&nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
