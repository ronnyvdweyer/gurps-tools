<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GridView.aspx.cs" Inherits="SCv20.Tools.Web.Site.GridView" %>
<%@ Register src="~/Views/Shared/Pager.ascx" tagname="Pager" tagprefix="view" %>


<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Teste de GridView
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" /> | <asp:Button ID="Button2" runat="server" Text="Button" />

    <br/>
    <br/>


    <div class="outer" style="position:relative;height:auto;width:200px;border:1px solid red">
        <div class="inner" style="height:100%;width:60px;border:1px solid black">
        a very long text
        </div>
    </div>

    <br/>

    <style type="text/css">
        .tb-area {
            background-color: #F3E8CF;
            padding: 0 10px 0 10px;
            border-top-left-radius: 8px;
            -moz-border-top-left-radius: 8px;
            border-top-right-radius: 8px;
            -moz-border-top-right-radius: 8px;
            border-radius: 8px;
            display: inline-block;
        }
        .tb-title {
            border: 0px solid black;
            color: #70899B;
            float: left;
            line-height: 27px;
            font-weight: bold;
        }
        .tb-pager {
            border: 0px solid red;
            float: right;
        }
        .tb-header {
            line-height: 22px;
            height:22px;
            border-top: 2px dotted #EBD894;
            border-bottom: 2px dotted #EBD894;
            clear: both;
            display: block;
        }
        .tb-body {
            height:auto;
            border-bottom: 2px dotted #EBD894;
            clear: both;
            display: block;
        }
        
        
        .tb-header .tb-col {
            float:left;
            color:#913D4A;
            font-weight:bold;
        }
        
        
        
        .tb-row {
            line-height: 22px;
            height:22px;
            border-bottom: 1px dotted silver;
        }
        
        .tb-row .tb-col {
            float:left;
            background-color:#F5F3F1;
        }
    </style>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <br/>

            <div class="tb-area" style="height:auto;">
                <div class="tb-title">
                    Available Qualities
                </div>
                <div class="tb-pager" style="">
                    <view:Pager ID="Pager1" runat="server" PageSize="10" PagerTitle="Page" PagerToken="of" />
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
                                <div class="tb-col al-c" style="width:050px"><%#Eval("Id")      %></div>
                                <div class="tb-col al-l" style="width:380px"><%#Eval("Name")         %></div>
                                <div class="tb-col al-c" style="width:100px"><%#Eval("IsSeasonsOnly")%></div>
                                <div class="tb-col al-c" style="width:100px"><%#Eval("BonusXP")      %></div>
                                <div class="tb-col al-c" style="width:100px"><%#Eval("BonusAD")      %></div>
                                <div style="clear:both"></div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>

                <div style="display:inline-block; border:1px solid #F3EBD8">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--
    <div class="grid" style="display:none">
        <asp:GridView ID="GridView1" runat="server" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" Font-Size="10" DataKeyNames="Id"
            AutoGenerateColumns="false">
            <HeaderStyle BackColor="#336699" ForeColor="White" HorizontalAlign="Left" Height="25" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" />
                <asp:BoundField DataField="Id" HeaderText="#ID" ReadOnly="true" />
                
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("Name")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtName" Text='<%# Eval("Name")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Seasons Only">
                    <ItemTemplate>
                        <%# Eval("IsSeasonsOnly")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox runat="server" ID="chkSeasonsOnly" Text='<%# Eval("IsSeasonsOnly")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Bonus XP">
                    <ItemTemplate>
                        <%# Eval("BonusXP")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtBonusXP" Text='<%# Eval("BonusXP")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Bonus AD">
                    <ItemTemplate>
                        <%# Eval("BonusAD")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtBonusAD" Text='<%# Eval("BonusAD")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="[&lt;-]" LastPageText="[-&gt;]" Mode="NumericFirstLast" NextPageText="&gt;" PageButtonCount="5" 
                Position="Top" PreviousPageText="&lt;" />
        </asp:GridView>
    </div>
    --%>

</asp:Content>
