<%@ page autoeventwireup="false" codefile="EditPortal.aspx.cs" inherits="Rainbow.AdminAll.EditPortal"
    language="c#" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>

<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div class="rb_AlternateLayoutDiv">
            <table class="rb_AlternateLayoutTable">
                <tr valign="top">
                    <td class="rb_AlternatePortalHeader" valign="top">
                        <portal:banner id="Banner1" runat="server" showtabs="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                        <table cellpadding="4" cellspacing="0" width="98%">
                            <tr valign="top">
                                <td width="150">
                                    &nbsp;
                                </td>
                                <td width="*">
                                    <table border="0" cellpadding="2" cellspacing="1">
                                        <tr>
                                            <td align="left" class="Head" colspan="3">
                                                <rbfwebui:localize id="Literal1" runat="server" text="Edit Portal"  TextKey="EDIT_PORTAL">
                                                </rbfwebui:localize></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr  noshade="noshade" size="1" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" width="140">
                                                <rbfwebui:localize id="Literal2" runat="server" text="Portal ID" textkey="AM_PORTALID">
                                                </rbfwebui:localize></td>
                                            <td class="Normal">
                                                <rbfwebui:label id="PortalIDField" runat="server"></rbfwebui:label></td>
                                            <td class="Normal">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" width="140">
                                                <rbfwebui:localize id="Literal3" runat="server" text="Portal Title" textkey="AM_SITETITLE">
                                                </rbfwebui:localize></td>
                                            <td class="Normal">
                                                <asp:textbox id="TitleField" runat="server" cssclass="NormalTextBox" width="350"></asp:textbox></td>
                                            <td class="Normal">
                                                <asp:requiredfieldvalidator id="RequiredTitle" runat="server" controltovalidate="TitleField"
                                                    errormessage="Required Field"></asp:requiredfieldvalidator></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" width="140">
                                                <rbfwebui:localize id="Literal4" runat="server" text="Portal Alias" textkey="AM_PORTALALIAS">
                                                </rbfwebui:localize></td>
                                            <td class="Normal">
                                                <rbfwebui:label id="AliasField" runat="server"></rbfwebui:label></td>
                                            <td class="Normal">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" width="140">
                                                <rbfwebui:localize id="Literal5" runat="server" text="Site Path" textkey="AM_SITEPATH">
                                                </rbfwebui:localize></td>
                                            <td class="Normal">
                                                <rbfwebui:label id="PathField" runat="server"></rbfwebui:label></td>
                                            <td class="Normal">
                                            </td>
                                        </tr>
                                    </table>
                                    <rbfwebui:settingstable id="EditTable" runat="server" />
                                    <p>
                                        <rbfwebui:linkbutton id="updateButton" runat="server" class="CommandButton"></rbfwebui:linkbutton></p>
                                    <p class="Normal">
                                        <rbfwebui:label id="ErrorMessage" runat="server" cssclass="Error" visible="false"></rbfwebui:label></p>
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
