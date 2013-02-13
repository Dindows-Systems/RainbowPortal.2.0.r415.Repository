<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<%@ page autoeventwireup="false" codefile="AddPage.aspx.cs" inherits="Rainbow.Admin.AddPage"
    language="c#" %>

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
                                            <td colspan="4">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="left" class="Head">
                                                            <rbfwebui:localize id="tab_name" runat="server" text="Add New Page" textkey="AM_TABNAME">
                                                            </rbfwebui:localize></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <hr noshade="noshade" size="1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" width="100">
                                                <rbfwebui:localize id="tab_name1" runat="server" text="Page Name" textkey="AM_TABNAME1">
                                                </rbfwebui:localize></td>
                                            <td colspan="3">
                                                <asp:textbox id="tabName" runat="server" cssclass="NormalTextBox" maxlength="47"
                                                    width="300"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" nowrap="nowrap">
                                                <rbfwebui:localize id="roles_auth" runat="server" text="Authorized Roles" textkey="AM_ROLESAUTH">
                                                </rbfwebui:localize></td>
                                            <td colspan="3">
                                                <asp:checkboxlist id="authRoles" runat="server" cssclass="Normal" repeatcolumns="2"
                                                    width="300">
                                                </asp:checkboxlist>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" nowrap="nowrap">
                                                <rbfwebui:localize id="tab_parent" runat="server" text="Parent Page" textkey="TAB_PARENT">
                                                </rbfwebui:localize></td>
                                            <td colspan="3">
                                                <asp:dropdownlist id="parentPage" runat="server" cssclass="NormalTextBox" datatextfield="Name"
                                                    datavaluefield="ID" width="300px">
                                                </asp:dropdownlist><rbfwebui:label id="lblErrorNotAllowed" runat="server" cssclass="Error"
                                                    enableviewstate="False" textkey="ERROR_NOT_ALLOWED_PARENT" visible="False">Not allowed to choose that parent</rbfwebui:label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="3">
                                                <hr noshade="noshade" size="1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" nowrap="nowrap">
                                                <rbfwebui:localize id="show_mobile" runat="server" text="Show to mobile users" textkey="AM_SHOWMOBILE">
                                                </rbfwebui:localize></td>
                                            <td colspan="3">
                                                <asp:checkbox id="showMobile" runat="server" cssclass="Normal" /></td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" nowrap="nowrap">
                                                <rbfwebui:localize id="mobiletab" runat="server" text="Mobile Page Name" textkey="AM_MOBILETAB">
                                                </rbfwebui:localize></td>
                                            <td colspan="3">
                                                <asp:textbox id="mobilePageName" runat="server" cssclass="NormalTextBox" maxlength="50"
                                                    width="300"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr noshade="noshade" size="1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" nowrap="nowrap">
                                                <rbfwebui:localize id="lbl_jump_to_tab" runat="server" text="Jump to this tab?" textkey="AM_JUMPTOTAB">
                                                </rbfwebui:localize></td>
                                            <td colspan="3">
                                                <asp:checkbox id="cb_JumpToPage" runat="server" cssclass="Normal" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr noshade="noshade" size="1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="Error" colspan="4">
                                                <rbfwebui:localize id="msgError" runat="server" text="You do not have the appropriate permissions to delete or move this module"
                                                    textkey="AM_NO_RIGHTS">
                                                </rbfwebui:localize></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr noshade="noshade" size="1" />
                                                <rbfwebui:settingstable id="EditTable" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <rbfwebui:linkbutton id="saveButton" runat="server" class="CommandButton" text="Save Changes"
                                                    textkey="SAVE_CHANGES">Save Page</rbfwebui:linkbutton>&nbsp;
                                                <rbfwebui:linkbutton id="cancelButton" runat="server" class="CommandButton" text="Cancel"
                                                    textkey="CANCEL"></rbfwebui:linkbutton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="rb_AlternatePortalFooter">
                        <foot:footer id="Footer" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
