<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SCv20.Tools.Web.Site.Campaigns.Create" %>
<%@ Register src="~/Views/Shared/CKEditor.ascx" tagname="CKEditor" tagprefix="uc2" %>
<%@ Register src="~/Views/QualityGrid.ascx" tagname="QualityGrid" tagprefix="uc1" %>

<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
<%--    <script src="/Content/Widgets/nicEdit/nicEdit.js" type="text/javascript"></script>--%>
<%--    <script type="text/javascript">
        var nicEditors;

        $(function () {
            nicEditors = new nicEditor({ iconsPath: '/Content/Widgets/nicEdit/nicEditorIcons.gif' }).panelInstance('<%=txt_summary.ClientID%>');
        })

        function update() {
            console.log(nicEditors);
            for (var i = 0; i < nicEditors.nicInstances.length; i++) {
                nicEditors.nicInstances[i].saveContent();
            }
            return false;
        }
    </script>
--%>
</asp:Content>


<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Campaing Design
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajax" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="form">
                <asp:HiddenField runat="server" ID="hid_campaign_id" Value="0" />

                <div class="input-container">
                    <span class="label">Name:</span>
                    <asp:TextBox runat="server" ID="txt_name" MaxLength="100" CssClass="input-text w-8" autofocus="autofocus" />
                </div>

                <div class="input-container" style="height:auto">
                    <span class="label">Concept:</span>
                    <asp:TextBox runat="server" ID="txt_concept" CssClass="input-area w-8 h-2" TextMode="MultiLine" MaxLength="250"/>
                </div>

                <div class="input-container">
                    <span class="label">Year:</span>
                    <asp:DropDownList runat="server" CssClass="w-2 input-select" style="" ID="sel_year" DataValueField="Id" DataTextField="Year"/>
                    <span class="label al-r">Details:</span>
                    <asp:TextBox runat="server" ID="txt_year" CssClass="w-6 input-text" />
                </div>

                <div class="input-container">
                    <span class="label">Level:</span>
                    <asp:DropDownList runat="server" CssClass="w-2 input-select" style="" ID="sel_level" DataValueField="key" DataTextField="value"/>
                    <span class="label al-r">Reputation:</span>
                    <asp:TextBox runat="server" ID="txt_reputation" MaxLength="100" CssClass="input-text w-2"/>
                    <span class="label al-r">Net worth:</span>
                    <asp:TextBox runat="server" ID="txt_networth" MaxLength="100" CssClass="input-text w-2"/>
                </div>

                <div class="input-container" style="">
                    <span class="label">Summary:</span>
                    <div class="input-editor w-8" style="float:left; height:262px">
                        <uc2:CKEditor runat="server" ID="txt_summary" CompactToolbar="true"/>
                    </div>
                </div>
                <div class="clear"></div>

                <div class="form-controls">
                    <asp:Button runat="server" id="Save" UseSubmitBehavior="false" CssClass="btn medium default" Text="Save" CommandName="save-new" 
                        onclick="Save_Click" />
                </div>
                <%--
                <div class="clear">
                    <asp:ValidationSummary runat="server" ID="errorSummary" DisplayMode="List" HeaderText="Some invalid data eas inputed:" ShowSummary="true" />
                </div>
                --%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <uc1:QualityGrid ID="QualityGrid1" runat="server" Visible="true"/>
</asp:Content>
