<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignDisplay.ascx.cs" Inherits="SCv20.Tools.Web.Views.CampaignDisplay" %>

<asp:Panel ID="CampaignDisplayContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="ajax">
        <ContentTemplate>
            <div class="input-container">
                <span class="label">Campaign:</span>
                <asp:DropDownList runat="server" ID="SelectCampaignField" DataValueField="Id" DataTextField="Name" CssClass="input-select w-6"
                    AutoPostBack="true" onselectedindexchanged="SelectCampaignField_SelectedIndexChanged" />
                <span class="label al-r">Created On:</span>
                <asp:TextBox runat="server" ID="txtCreatedOn" CssClass="input-text w-2 al-c" MaxLength="20" Enabled="false" />
            </div>

            <div class="input-container" style="height:auto">
                <span class="label">Concept:</span>
                <asp:TextBox runat="server" ID="txtConcept" CssClass="input-area w-8 h-2" TextMode="MultiLine" MaxLength="250" Enabled="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
     