<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomContentEditorUserControl.ascx.cs"
    Inherits="SP10Concoct.WebParts.CustomContentEditor.CustomContentEditorUserControl" %>
<div class="DCContentBlock">
    <div class="DCContent">
        <table runat="server" id="tblHeader" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left" valign="top">
                    <h1>
                        <span runat="server" id="spnWPheader"></span></h1>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <div class="blue_content" style="padding:5px">
                        <asp:Panel ID="plhContentEdit" runat="server" />
                        <asp:Panel ID="plhContentDisplay" runat="server">
                        </asp:Panel>
                        <asp:Panel ID="plhNoContent" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
