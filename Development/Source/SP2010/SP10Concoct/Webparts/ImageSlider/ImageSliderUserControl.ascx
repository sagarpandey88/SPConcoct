<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageSliderUserControl.ascx.cs"
    Inherits="SP10Concoct.WebParts.ImageSlider.ImageSliderUserControl" %>
<script type="text/javascript" src="_layouts/SP10Concoct.WebParts/ImageSlider/js/bjqs-1.3.min.js"></script>
<link rel="Stylesheet" type="text/css" href="_layouts/SP10Concoct.WebParts/ImageSlider/css/carousel.css" />

<div id="slider">
    <div id="banner-imageSlider">
        <ul class="bjqs">
            <asp:Literal ID="ltrImageSlider" runat="server"></asp:Literal>
        </ul>
    </div>
</div>

<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#banner-imageSlider').bjqs({
            height: 247,
            width: "100%",
            responsive: true
        });

    });
</script>
