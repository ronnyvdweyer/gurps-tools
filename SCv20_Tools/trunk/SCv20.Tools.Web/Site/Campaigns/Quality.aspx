<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quality.aspx.cs" Inherits="SCv20.Tools.Web.Site.Campaigns.Quality" %>
<%@ Register src="~/Views/CampaignDisplay.ascx" tagname="CampaignDisplay" tagprefix="view" %>


<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="title" ContentPlaceHolderID="MainTitleContent" runat="server">
    Add Qualities to Campaign
</asp:Content>

<asp:Content ID="main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
        <div class="input-container" style="border:0px solid; height:80px">
            <view:CampaignDisplay ID="CampaignDisplay" runat="server" />
        </div>

        <asp:UpdatePanel runat="server" ID="ajax" UpdateMode="Always">
            <ContentTemplate>
                <div class="input-container">
                    <span class="label">Quality:</span>
                    <asp:DropDownList runat="server" ID="SelectQuality" DataValueField="Id" DataTextField="Name" CssClass="input-select w-5" Style=""
                                      OnSelectedIndexChanged="SelectQuality_SelectedIndexChanged" AutoPostBack="true" />
        
                    <span class="label al-r w-2" ">Action Dice:</span>
                    <asp:TextBox runat="server" ID="txtBonusAD" CssClass="input-text w-1 al-c" Enabled="false" />
        
                    <span class="label al-r">XP Bonus:</span>
                    <asp:TextBox runat="server" ID="txtBonusXP" CssClass="input-text w-1 al-c"  Enabled="false"/>
                </div>

                <div class="input-container">
                    <span class="label">Description:</span>
                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" CssClass="input-area w-8 h-6" Style=""  Enabled="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="clear" style="border:1px solid black"></div>
    </div>
<br />
<br />
-------- Contents Here Please --------
<br />
<br />

    </span>

</asp:Content>
