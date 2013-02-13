<%@ page autoeventwireup="false" inherits="Rainbow.Content.Web.Modules.PageCustomPropertyPage"
    language="c#" codefile="CustomPropertyPage.aspx.cs" %>
<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<html>
<head id="Head1" runat="server">
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div class="rb_AlternateLayoutDiv">
            <table class="rb_AlternateLayoutTable">
                <tr valign="top">
                    <td class="rb_AlternatePortalHeader" valign="top">
                        <portal:banner id="SiteHeader" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br>
                        <table align="center" border="0" cellpadding="4" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="600">
                                        <tr>
                                            <td align="left" class="Head">
                                                <rbfwebui:localize id="Literal1" runat="server" text="Module Custom Settings" textkey="MODULE_CUSTOM_SETTINGS">
                                                </rbfwebui:localize>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><hr noshade="noshade" size="1" />
                                                <rbfwebui:settingstable id="EditTable" runat="server">
</rbfwebui:settingstable></td>
                                        </tr>
                                    </table>
                                    <p>
                                        <asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>
                                        &nbsp;
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="rb_AlternatePortalFooter">
                        <div class="rb_AlternatePortalFooter">
                            <foot:footer id="Footer" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
