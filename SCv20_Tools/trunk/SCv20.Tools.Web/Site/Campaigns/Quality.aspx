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

        <div class="clear"></div>

        <div style="float:left;">
            <div class="input-container">
                <span class="label">Quality:</span>
                <asp:DropDownList runat="server" ID="SelectQuality" DataValueField="Id" DataTextField="Name" CssClass="input-select w-3" 
                    OnSelectedIndexChanged="SelectQuality_SelectedIndexChanged" AutoPostBack="true" />
            </div>
            <div class="input-container">
                <span class="label">Action Dice:</span>
                <asp:TextBox runat="server" ID="txtBonusAD" CssClass="input-text w-1" Enabled="false" />
            </div>
            <div class="input-container">
                <span class="label">XP Bonus:</span>
                <asp:TextBox runat="server" ID="txtBonusXP" CssClass="input-text w-1"  Enabled="false"/>
            </div>
        </div>

        <div class="" style="float:left">
            <span class="label al-c">Description:</span>
            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" CssClass="input-area w-5" Style="height:78px;"  Enabled="false" />
        </div>
        <div class="clear" style="border:1px solid black"></div>
    </div>
<br />
<br />
-------- Contents Here Please --------
<br />
<br />

</asp:Content>
