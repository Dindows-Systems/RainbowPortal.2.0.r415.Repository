<%@ page autoeventwireup="false" codefile="ContentManagerEdit.aspx.cs" inherits="Rainbow.Content.Web.Modules.ContentManagerEdit"
    language="c#" %>

<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body >
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
                        <br/>
                        <table border="0" cellpadding="4" cellspacing="0" width="98%">
                            <tr valign="top">
                                <td width="150">
                                    &nbsp;
                                </td>
                                <td width="*">
                                    <table cellpadding="0" cellspacing="0" width="500">
                                        <tr>
                                            <td align="left" class="Head">
                                                <rbfwebui:label id="Label1" runat="server" text="Content Manager 3rd Party Module-Support Installer"
                                                    textkey="CONTENT_MGR_TITLE"></rbfwebui:label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr noshade="noshade" size="1" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tableInstaller" runat="server" border="0" cellpadding="3" cellspacing="0"
                                        width="750">
                                        <tr>
                                            <td class="SubHead" nowrap="nowrap" width="106">
                                                <rbfwebui:label id="Label2" runat="server" text="Friendly Name" textkey="INSTALLER_FILE">Installer file</rbfwebui:label>:</td>
                                            <td width="6">
                                            </td>
                                            <td>
                                                <asp:textbox id="InstallerFileName" runat="server" columns="30" cssclass="NormalTextBox"
                                                    maxlength="150" width="390"></asp:textbox></td>
                                            <td width="10">
                                            </td>
                                            <td class="Normal" width="250">
                                                <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" controltovalidate="InstallerFileName"
                                                    cssclass="Error" display="Dynamic" errormessage="Enter an Installer Name" textkey="ERROR_ENTER_A_FILE_NAME"></asp:requiredfieldvalidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <p>
                                        <rbfwebui:linkbutton id="updateButton" runat="server" class="CommandButton" text="Update"></rbfwebui:linkbutton>&nbsp;
                                        <rbfwebui:linkbutton id="cancelButton" runat="server" causesvalidation="False" class="CommandButton"
                                            text="Cancel"></rbfwebui:linkbutton>&nbsp;
                                        <rbfwebui:linkbutton id="deleteButton" runat="server" causesvalidation="False" class="CommandButton"
                                            text="Delete this module type"></rbfwebui:linkbutton></p>
                                    <p>
                                        <rbfwebui:label id="lblErrorDetail" runat="server" cssclass="error"></rbfwebui:label></p>
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
