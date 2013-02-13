<%@ Control AutoEventWireup="false" CodeFile="RainbowVersion.ascx.cs" Inherits="Rainbow.Content.Web.Modules.RainbowVersion" Language="c#" %>
<p>
    <rbfwebui:Label ID="RainbowVersionLabel" runat="server" CssClass="Normal" EnableViewState="false"
        Text="The running rainbow version is" TextKey="RAINBOW_RUNNING_VERSION">
    </rbfwebui:Label>
    <rbfwebui:Label ID="VersionLabel" runat="server" CssClass="Normal" EnableViewState="False"></rbfwebui:Label>
</p>
<p>
    <rbfwebui:Label ID="currentUILanguage" runat="server" CssClass="Normal" EnableViewState="False"></rbfwebui:Label>/
    <rbfwebui:Label ID="currentLanguage" runat="server" CssClass="Normal" EnableViewState="False"></rbfwebui:Label>
</p>
