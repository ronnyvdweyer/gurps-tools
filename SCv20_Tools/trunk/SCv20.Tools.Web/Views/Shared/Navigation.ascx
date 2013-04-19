<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="SCv20.Tools.Web.Views.Shared.Navigation" %>


<style type="text/css">
    .jqx-tree-item-li  {
        line-height:12px;
    }
    .jqx-tree-item{
        /*width:140px;*/
    }
    .jqx-tree-item-li a {
        text-decoration:none;    
        color:Black;
    }
</style>


<div class="menu-view" id="left-menu">
    <ul>
        <li item-expanded="true"><b>Planning</b>
            <ul>
                <li item-expanded="true"><a runat="server" href="~/Site/Campaigns/Default.aspx">Campaings</a>
                    <ul>
                        <li><a runat="server" href="~/Site/Campaigns/Create.aspx">- Create New</a></li>
                        <li><a runat="server" href="~/Site/Campaigns/Create.aspx">- Edit Existing</a></li>
                    </ul>
                </li>
                <li><a runat="server" href="#">Missions</a></li>
                <li><a runat="server" href="#">Organizations</a>
                    <%--
                    <ul style="display:none">
                        <li><a href="#">New</a>
                            <ul>
                                <li><a href="#">Corporate Use</a></li>
                                <li><a href="#">Private Use</a></li>
                            </ul>
                        </li>
                        <li><a href="#">Used</a>
                            <ul>
                                <li><a href="#">Corporate Use</a></li>
                                <li><a href="#">Private Use</a></li>
                            </ul>
                        </li>
                        <li><a href="#">Featured</a></li>
                    </ul>
                    --%>
                </li>
                <li><a href="#">Contact Us</a></li>
            </ul>
        </li>
        <li item-expanded="true"><a runat="server" href="~/Site/Admin/Default.aspx"><b>Data Files</b></a>
            <ul>
                <li item-expanded="true">Campaign Qualities
                    <ul>
                        <li><a id="A1" runat="server" href="~/Site/Admin/CampaignQualities/Create.aspx">- Create New</a></li>
                        <li><a id="A2" runat="server" href="~/Site/Campaigns/Create.aspx">- Edit Existing</a></li>
                    </ul>
                </li>
                <li>Mission Objectives</li>
                <li>NPC Qualities</li>
            </ul>
        </li>
    </ul>
</div>


<script type="text/javascript">
$(document).ready(function () {
    $("#left-menu").jqxTree(
        { width: '100%'/*, height: '300px'*/, allowDrag: false, allowDrop: false}
    );
})
</script>


<%--
<asp:SiteMapDataSource id="siteMap" ShowStartingNode="true" Runat="server" />
<asp:Menu ID="menu" runat="server" DataSourceID="siteMap" StaticDisplayLevels="3" Orientation="Vertical" DataSourceID="siteMap"
    StaticMenuStyle-CssClass="x-menu"
    StaticMenuItemStyle-CssClass="x-menu-item" 
    StaticSelectedStyle-CssClass="x-menu-selected" 
    StaticHoverStyle-CssClass="x-menu-hover" 

    DynamicMenuStyle-CssClass="x-menu"
    DynamicMenuItemStyle-CssClass="x-menu-item" 
    DynamicHoverStyle-CssClass="x-menu-hover" 
    DynamicSelectedStyle-CssClass="x-menu-selected" 
/>
--%>