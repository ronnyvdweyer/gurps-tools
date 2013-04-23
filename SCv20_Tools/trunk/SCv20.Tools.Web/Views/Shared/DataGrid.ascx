<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataGrid.ascx.cs" Inherits="SCv20.Tools.Web.Views.Shared.DataGrid" %>
<%@ Register src="~/Views/Shared/Pager.ascx" tagname="Pager" tagprefix="view" %>

<div runat="server" id="qualityGrid" class="quality-grid">
    
    <div class="tb-containter" style="height:auto; width:900px;">
        <div class="tb-title">
            <asp:PlaceHolder runat="server" ID="gridTitleTemplate" />
        </div>

        <div class="tb-pager">
            <view:Pager ID="GridPager" runat="server" PageSize="10" PagerTitle="Page" PagerToken="of" />
        </div>

        <div class="tb-header">
                    
            <asp:PlaceHolder runat="server" ID="gridHeaderTemplate" />

            <%-- <div class="tb-col al-c" style="width:040px">
                <input type="checkbox" disabled="disabled" id="x-chk-all" class="x-chk-all" />
            </div>
            <div class="tb-col al-c" style="width:050px">ID</div>
            <div class="tb-col al-l" style="width:380px">Name</div>
            <div class="tb-col al-l" style="width:100px">Seasons</div>
            <div class="tb-col al-r" style="width:100px">XP Bonus</div>
            <div class="tb-col al-r" style="width:100px">AD Bonus</div>--%>
        </div>
                
        <div class="tb-body">
            <div class="tb-row">
                <asp:PlaceHolder runat="server" ID="gridItemTemplate" />
            </div>

            <%--
            <asp:ListView ID="ListQuality" runat="server" QueryStringField="ID">
                <ItemTemplate>
                    <div class="tb-row">
                        <div class="tb-col al-c" style="width:040px">
                            <input type="checkbox" id="x-chk-item" class="x-chk-item" data-id="<%#Eval("Id")%>" />
                        </div>
                        <div class="tb-col al-c" style="width:050px"><%#Eval("Id")           %></div>
                        <div class="tb-col al-l" style="width:380px" title="<%#Eval("Description")%>">
                            <%#Eval("Name")%>
                        </div>
                        <div class="tb-col al-l" style="width:100px"><%#Eval("IsSeasonsOnly")%></div>
                        <div class="tb-col al-r" style="width:100px"><%#Eval("BonusXP")      %></div>
                        <div class="tb-col al-r" style="width:100px;"><%#Eval("BonusAD")      %></div>
                        <div style="clear:both"></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            --%>
        </div>

        <div class="tb-toolbar" style="">
            <asp:PlaceHolder runat="server" ID="gridFooterTemplate" />
            <%--
            <asp:TextBox runat="server" ID="txt_seletion" />
            <asp:Button ID="SelectedItems" runat="server" CssClass="btn-ico-copy" ToolTip="Copy Selected Items" />
            --%>
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
</div>
