<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicGrid.ascx.cs" Inherits="SCv20.Tools.Web.Views.Shared.DynamicGrid" %>
<%@ Register src="~/Views/Shared/Pager.ascx" tagname="Pager" tagprefix="view" %>

<asp:panel runat="server" id="QualityGridContainer" style="display:block;width:100%">
    
    <div class="tb-container" style="height:auto">
        <div class="tb-title">
            <span runat="server" id="gridTitle" />
        </div>

        <div runat="server" class="tb-pager" id="PagerContainer">
            <view:Pager ID="GridPager" runat="server" PageSize="10" PagerTitle="Page" PagerToken="of" />
        </div>

        <div class="tb-header">
            <asp:PlaceHolder runat="server" ID="gridHeaderContainer" />
        </div>
                
        <div class="tb-body">
            <asp:PlaceHolder runat="server" ID="gridItemContainer" />
        </div>

        <div class="tb-toolbar">
            <asp:PlaceHolder runat="server" ID="gridFooterTemplate" />
        </div>
    </div>

    <%--
    <script type="text/javascript">
        $('.quality-grid').on('click', '.x-chk-item', function () {
            var value = [];

            $(".quality-grid .x-chk-item").each(function (idx) {
                var checked = $(this).is(':checked');
                if (checked) {
                    value.push($(this).data('id'));
                }
            });

            var text = $('#<%=txt_seletion.ClientID%>');

            text.val(value);
        });
    </script>
    --%>
</asp:panel>
