<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CKEditor.ascx.cs" Inherits="SCv20.Tools.Web.Views.Shared.CKEditor" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="ck" %>

<asp:Panel ID="ckEditorContainer" runat="server">
    <ck:CKEditorControl ID="ck" BasePath="~/Content/Widgets/ckeditor/" runat="server" EnterMode="P" ResizeEnabled="false" Toolbar="NewPage|Preview
                 Cut|Copy|Paste|PasteText|PasteFromWord|-|Print
                 Undo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat
                 /   
                 Bold|Italic|Underline|Strike|-|Subscript|Superscript
                 JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|TextColor|BGColor|Format|Font|FontSize
                 NumberedList|BulletedList|Outdent|Indent|-|Blockquote
                 Link|Unlink|Anchor
                 Maximize|ShowBlocks">
    </ck:CKEditorControl>
</asp:Panel>
