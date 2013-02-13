<%@ page autoeventwireup="false" codefile="FlashEdit.aspx.cs" inherits="Rainbow.Content.Web.Modules.FlashEdit"
    language="c#" %>

<%@ register assembly="Rainbow.Framework" namespace="Rainbow.Framework.Web.UI.WebControls"
    tagprefix="rbfwebui" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div id="zenpanes" class="zen-main">
            <div class="rb_DefaultPortalHeader">
                <portal:banner id="SiteHeader" runat="server" />
            </div>
            <div class="div_ev_Table">
                <table cellpadding="0" cellspacing="0" width="80%">
                    <tr>
                        <td align="left" class="Head">
                            <rbfwebui:localize id="Literal2" runat="server" text="Flash Settings" textkey="FLASH_SETTINGS">
                            </rbfwebui:localize>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr noshade="noshade" size="1" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="80%">
                    <tr valign="top">
                        <td class="SubHead" width="100">
                            <rbfwebui:localize id="Literal1" runat="server" text="Swf-File Path" textkey="FLASH_PATH">
                            </rbfwebui:localize>
                        </td>
                        <td rowspan="3" width="251">
                            &nbsp;
                        </td>
                        <td class="Normal">
                            <asp:textbox id="Src" runat="server" columns="30" cssclass="NormalTextBox" width="390"></asp:textbox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="SubHead">
                            <rbfwebui:localize id="Literal3" runat="server" text="Width" textkey="WIDTH">
                            </rbfwebui:localize>
                        </td>
                        <td>
                            <asp:textbox id="Width" runat="server" columns="30" cssclass="NormalTextBox" width="390">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="SubHead">
                            <rbfwebui:localize id="Literal4" runat="server" text="Height" textkey="HEIGHT">
                            </rbfwebui:localize>
                        </td>
                        <td>
                            <asp:textbox id="Height" runat="server" columns="30" cssclass="NormalTextBox" width="390">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead" width="100">
                            <rbfwebui:localize id="Literal5" runat="server" text="Background Color" textkey="FLASH_BACKGROUNDCOLOR">
                            </rbfwebui:localize>
                            (rrggbb)
                        </td>
                        <td rowspan="3" width="251">
                            &nbsp;
                        </td>
                        <td class="Normal">
                            <asp:textbox id="BackgroundCol" runat="server" columns="30" cssclass="NormalTextBox"
                                width="390"></asp:textbox>
                        </td>
                    </tr>
                </table>
                <p>
                    <rbfwebui:linkbutton id="updateButton" runat="server" class="CommandButton" text="Update"
                        textkey="UPDATE">Update</rbfwebui:linkbutton>
                    &nbsp;
                    <rbfwebui:linkbutton id="cancelButton" runat="server" causesvalidation="False" class="CommandButton"
                        text="Cancel" textkey="CANCEL">Cancel</rbfwebui:linkbutton>&nbsp;
                    <rbfwebui:hyperlink id="showGalleryButton" runat="server" cssclass="CommandButton"
                        text="Show Gallery" textkey="SHOW_FLASH_GALLERY">Show Gallery</rbfwebui:hyperlink>
                </p>
            </div>
            <div class="rb_AlternatePortalFooter">
                <foot:footer id="Footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
