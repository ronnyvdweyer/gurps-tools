<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="SCv20.Tools.Web.Views.Shared.Pager" %>

<style type="text/css">
    .pager-container {
        border: 0px solid black;
        display:table-cell;
        background-color:#F3E8CF;
        height:25px;
        vertical-align:middle;
        -moz-border-radius: 8px;
        border-radius: 8px;
        padding:0 10px 0 10px;
    }
    .pager-title-text {
        line-height: 25px;
        font-size: .9em;
        display:inline-block;
        font-weight:normal;
    }
    .pager-prev-button, .pager-next-button {
        border: 0px solid silver;
        height: 20px;
        width: 20px;
        background-color: #F3E8CF;
        font-family: Verdana;
        -moz-border-radius: 4px;
        border-radius: 4px;
        padding:1px 0 1px 0;
        cursor:pointer;
    }
    .pager-prev-button {
        background-image: url(/content/images/nav-left.png);
        background-position: center center;
        background-repeat:no-repeat;
    }
    .pager-next-button {
        background-image: url(/content/images/nav-right.png);
        background-position: center center;
        background-repeat:no-repeat;
    }
    .pager-prev-button:active, .pager-next-button:active  {
        background-color:#F6F5EA;        
    }
    .pager-prev-button:focus, .pager-next-button:focus  {
        background-color:#F6F5EA;        
    }
    .pager-prev-button[disabled], .pager-next-button[disabled] {
        opacity:0.3;
        filter:alpha(opacity=30); 
        cursor:default;
    }
    .pager-current-text {
        border: 1px solid silver;
        height: 16px;
        line-height: 16px;
        width: 30px;
        text-align: center;
        font-size: 0.8em;
        background-color: #F6F5EA;
        float:left;
        border-radius: 4px;
        -moz-border-radius: 4px;
    }
    .pager-token-text {
        height: 16px;
        line-height:16px;
        text-align:center;
        width: 20px;
        float:left;
        font-size: 0.8em;
        padding:1px 0 1px 0;        
        display:inline-block;
    }
    
    .pager-total-pages {
        height: 16px;
        line-height: 16px;
        width: 30px;
        display:inline-block;
        border: 1px solid silver;
        background-color: #F3E8CF;
        padding:1px 0 1px 0;
        text-align:center;
        font-size: 0.8em;
        -moz-border-radius: 4px;
        border-radius: 4px;
        float:left;
    }
</style>

<div id="pagerContainer" class="pager-container">
    <table style="border-collapse:collapse">
        <tr>
            <td>
                <span class="pager-title-text"><%=PagerTitle%></span>
            </td>
            <td>
                <asp:Button ID="prevPage" runat="server"  Text="&nbsp;" CssClass="pager-prev-button" OnClick="Prev_Click" />
            </td>
        
            <td>
                <asp:TextBox ID="currentPage" runat="server"  AutoPostBack="true"  CssClass="pager-current-text" OnTextChanged="CurrentPage_Change" />
                <span class="pager-token-text"> <%=PagerToken%></span>
                <span class="pager-total-pages"><%=TotalPages%></span>
            </td>
    
            <td>
                <asp:Button ID="nextPage" runat="server"  Text="&nbsp;" CssClass="pager-next-button" OnClick="Next_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="currentPageIndex"  runat="server" Value="0" />
    <asp:HiddenField ID="totalPages"        runat="server" Value="0" />
    <asp:HiddenField ID="totalRecords"      runat="server" Value="0" />
</div>