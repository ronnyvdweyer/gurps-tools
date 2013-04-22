<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QualityGrid.ascx.cs" Inherits="SCv20.Tools.Web.Views.QualityGrid" %>
<%@ Register src="~/Views/Shared/Pager.ascx" tagname="Pager" tagprefix="view" %>

<div runat="server" id="qualityGrid" class="quality-grid">
    <asp:UpdatePanel ID="ajax" runat="server">
        <ContentTemplate>
            <div class="tb-containter" style="height:auto;">
                <div class="tb-title">
                    Available Qualities
                </div>

                <div class="tb-pager">
                    <view:Pager ID="GridPager" runat="server" PageSize="10" PagerTitle="Page" PagerToken="of" />
                </div>

                <div class="tb-header">
                    <div class="tb-col al-c" style="width:050px">ID</div>
                    <div class="tb-col al-l" style="width:380px">Name</div>
                    <div class="tb-col al-c" style="width:100px">Seasons</div>
                    <div class="tb-col al-c" style="width:100px">XP Bonus</div>
                    <div class="tb-col al-c" style="width:100px">AD Bonus</div>
                </div>
                
                <div class="tb-body">
                    <asp:ListView ID="ListView1" runat="server" QueryStringField="ID">
                        <ItemTemplate>
                            <div class="tb-row">
                                <div class="tb-col al-c" style="width:050px"><%#Eval("Id")           %></div>
                                <div class="tb-col al-l" style="width:380px"><%#Eval("Name")         %></div>
                                <div class="tb-col al-c" style="width:100px"><%#Eval("IsSeasonsOnly")%></div>
                                <div class="tb-col al-c" style="width:100px"><%#Eval("BonusXP")      %></div>
                                <div class="tb-col al-c" style="width:100px"><%#Eval("BonusAD")      %></div>
                                <div style="clear:both"></div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>

                <div style="display:inline-block; border:1px solid #F3EBD8"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
