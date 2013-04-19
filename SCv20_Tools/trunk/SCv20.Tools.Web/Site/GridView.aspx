<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GridView.aspx.cs" Inherits="SCv20.Tools.Web.Site.GridView" %>


<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Teste de GridView
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" /> | <asp:Button ID="Button2" runat="server" Text="Button" />

    <br/>
    <br/>

    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="8">
        <Fields>
        <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
        <asp:NumericPagerField />
        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>

    <br/>
    <br/>

    <asp:ListView ID="ListView1" runat="server" QueryStringField="ID">
        <ItemTemplate>
            <div class="div-grid" style="border-bottom:1px solid red; display:inline-block; margin:0; padding:0;">
                <div class="" style="float:left;border:0px solid silver; width:150px;"><%#Eval("Name")%></div>
                <div class="" style="float:left;border:0px solid silver; width:200px;"><%#Eval("IsSeasonsOnly")%></div>
                <div class="" style="float:left;border:0px solid silver; width:200px;"><%#Eval("BonusXP")%></div>
                <div class="" style="float:left;border:0px solid silver; width:100px;"><%#Eval("BonusAD")%></div>
            </div>
        </ItemTemplate>
    </asp:ListView>


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

</asp:Content>
