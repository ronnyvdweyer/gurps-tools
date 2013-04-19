<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QualityEditor.ascx.cs" Inherits="SCv20.Tools.Web.Views.QualityEditor" %>


<script type="text/javascript">
    $(function () {

    })
</script>


<div class="update-panel">
    <asp:UpdatePanel ID="ajax" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="form" style="">
                <div class="input">
                    <span class="label">Name:</span>
                    <asp:TextBox runat="server" id="txt_name" CssClass="text" Style="width:650px;" autofocus="autofocus" /> 
                </div>

                 <div class="input">
                    <asp:Label ID="Label1"  runat="server" AssociatedControlID="chk_season_only" CssClass="label" Text="Seasons Only" />
                    <asp:CheckBox runat="server" ID="chk_season_only" CssClass="check-box" />
            
                    <span class="label al-r">Bonus XP:</span>
                    <asp:TextBox runat="server" id="txt_bonus_xp" CssClass="text-number" style="width:50px;text-align:right" /> 
            
                    <span class="label al-r">Bonus AD:</span>
                    <asp:TextBox runat="server" id="txt_bonus_ad" CssClass="text-number" style="width:50px"/> 
                </div>
            
                <div class="input">
                    <span class="label">Description:</span>
                    <asp:TextBox runat="server" id="txt_desc" TextMode="MultiLine" CssClass="text-area" style="width:650px;"/>
                </div>

                <div runat="server" id="formControls" class="form-controls">
                    <asp:Button runat="server" id="cmd_save_new" UseSubmitBehavior="false" CssClass="btn medium default" Text="Save & New" CommandName="save-new" />
                    <asp:Button runat="server" id="cmd_save"     UseSubmitBehavior="false" CssClass="btn medium" Text="Save" CommandName="save" />
                </div>

                <ajaxToolkit:MaskedEditExtender ID="txt_bonus_xp_mask" runat="server" TargetControlID="txt_bonus_xp" Mask="999" PromptCharacter=" "
                    MaskType="Number" InputDirection="RightToLeft" AcceptNegative="Left" />
                <ajaxToolkit:MaskedEditExtender ID="txt_bonus_ad_mask" runat="server" TargetControlID="txt_bonus_ad" Mask="9" PromptCharacter=" " MaskType="Number"
                    InputDirection="RightToLeft" AcceptNegative="Left" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>