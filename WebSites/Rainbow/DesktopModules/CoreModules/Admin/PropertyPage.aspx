<%@ Register Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" TagName="Banner"
    TagPrefix="portal" %>
<%@ Register Src="~/Design/DesktopLayouts/DesktopFooter.ascx" TagName="Footer" TagPrefix="foot" %>

<%@ Page AutoEventWireup="false" Inherits="Rainbow.Content.Web.Modules.PagePropertyPage"
    Language="c#" CodeFile="PropertyPage.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div id="zenpanes" class="zen-main">
            <div class="rb_DefaultPortalHeader">
                <portal:Banner ID="SiteHeader" runat="server" />
            </div>
            <div class="div_ev_Table">
                <table align="center" border="0" cellpadding="4" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <table cellpadding="0" cellspacing="0" width="600">
                                <tr>
                                    <td align="left" class="Head" nowrap="nowrap">
                                        <rbfwebui:Localize ID="Literal1" runat="server" Text="Module settings" TextKey="MODULESETTINGS_SETTINGS" />
                                    </td>
                                    <td align="right">
                                        <asp:PlaceHolder ID="PlaceholderButtons2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr noshade="noshade" size="1" />
                                        <rbfwebui:SettingsTable ID="EditTable" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:PlaceHolder ID="PlaceHolderButtons" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <foot:Footer ID="Footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
