<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="SCv20.Tools.Web.Views.Shared.Pager" %>
<link href="/Views/Shared/Pager.ascx.css" rel="stylesheet" type="text/css" />

<div id="pagerContainer" class="pager-container">
    <table style="border-collapse:collapse">
        <tr>
            <td><span class="pager-title-text"><%=PagerTitle%></span></td>
            <td><asp:Button ID="prevPage" runat="server" Text="&nbsp;" CssClass="pager-prev-button" OnClick="Prev_Click" /></td>
            <td>
                <asp:TextBox ID="currentPage" runat="server"  AutoPostBack="true"  CssClass="pager-current-text" OnTextChanged="CurrentPage_Change" />
                <span class="pager-token-text"> <%=PagerToken%></span>
                <span class="pager-total-pages"><%=TotalPages%></span>
            </td>
            <td><asp:Button ID="nextPage" runat="server"  Text="&nbsp;" CssClass="pager-next-button" OnClick="Next_Click" /></td>
        </tr>
    </table>

    <asp:HiddenField ID="currentPageIndex"  runat="server" Value="0" />
    <asp:HiddenField ID="totalPages"        runat="server" Value="0" />
    <asp:HiddenField ID="totalRecords"      runat="server" Value="0" />
</div>