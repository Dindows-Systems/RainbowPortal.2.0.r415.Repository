<%@ page autoeventwireup="false" inherits="Rainbow.Admin.PageLayout" language="c#" codefile="PageLayout.aspx.cs" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner" tagprefix="portal" %>
<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register assembly="Rainbow.Framework" Namespace="Rainbow.Framework.Web.UI.WebControls" tagprefix="rbfwebui" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div class="rb_AlternateLayoutDiv">
            <table class="rb_AlternateLayoutTable">
                <tr valign="top">
                    <td class="rb_AlternatePortalHeader" valign="top">
                        <portal:banner id="Banner1" runat="server" showpages="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="2" cellspacing="1">
                            <tr>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" class="Head">
                                                <rbfwebui:localize id="tab_name" runat="server" text="Page Layouts" textkey="AM_TABNAME" />
                                             </td>
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
                                    <asp:textbox id="tabName" runat="server" cssclass="NormalTextBox" maxlength="50"
                                        width="300" OnTextChanged="PageSettings_Change" /></td>
                            </tr>
                            <tr>
                                <td class="Normal" nowrap="nowrap">
                                    <rbfwebui:localize id="roles_auth" runat="server" text="Authorized Roles" textkey="AM_ROLESAUTH">
                                    </rbfwebui:localize>
                                </td>
                                <td colspan="3">
                                    <asp:checkboxlist id="authRoles" runat="server" cssclass="Normal" repeatcolumns="2"
                                        width="300" OnSelectedIndexChanged="PageSettings_Change" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Normal" nowrap="nowrap">
                                    <rbfwebui:localize id="tab_parent" runat="server" text="Parent Page" textkey="TAB_PARENT">
                                    </rbfwebui:localize></td>
                                <td colspan="3">
                                    <asp:dropdownlist id="parentPage" runat="server" cssclass="NormalTextBox" width="300px" 
                                        DataTextField="Name" DataValueField="ID">
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
                                    <asp:checkbox id="showMobile" runat="server" cssclass="Normal" OnCheckedChanged="PageSettings_Change" /></td>
                            </tr>
                            <tr>
                                <td class="Normal" nowrap="nowrap">
                                    <rbfwebui:localize id="mobiletab" runat="server" text="Mobile Page Name" textkey="AM_MOBILETAB">
                                    </rbfwebui:localize></td>
                                <td colspan="3">
                                    <asp:textbox id="mobilePageName" runat="server" cssclass="NormalTextBox" width="300" OnTextChanged="PageSettings_Change" /></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Normal">
                                    <rbfwebui:localize id="addmodule" runat="server" text="Add module" textkey="AM_ADDMODULE">
                                    </rbfwebui:localize></td>
                                <td class="Normal">
                                    <rbfwebui:localize id="module_type" runat="server" text="Module type" textkey="AM_MODULETYPE">
                                    </rbfwebui:localize></td>
                                <td colspan="2">
                                    <asp:dropdownlist id="moduleType" runat="server" cssclass="NormalTextBox" datatextfield="FriendlyName"
                                        datavaluefield="ModuleDefID">
                                    </asp:dropdownlist></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="Normal">
                                    <rbfwebui:localize id="moduleLocationLabel" runat="server" text="Module Location:"
                                        textkey="AM_MODULELOCATION">
                                    </rbfwebui:localize></td>
                                <td colspan="2" valign="top">
                                    <asp:dropdownlist id="paneLocation" runat="server">
                                        <asp:listitem value="LeftPane">Left Column</asp:listitem>
                                        <asp:listitem selected="True" value="ContentPane">Center Column</asp:listitem>
                                        <asp:listitem value="RightPane">Right Column</asp:listitem>
                                    </asp:dropdownlist></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="Normal" valign="top">
                                    <rbfwebui:localize id="moduleVisibleLabel" runat="server" text="Module Visible To:"
                                        textkey="AM_MODULEVISIBLETO">
                                    </rbfwebui:localize></td>
                                <td colspan="2" valign="top">
                                    <asp:dropdownlist id="viewPermissions" runat="server">
                                        <asp:listitem selected="True" value="All Users;">All Users</asp:listitem>
                                        <asp:listitem value="Authenticated Users;">Authenticated Users</asp:listitem>
                                        <asp:listitem value="Unauthenticated Users;">Unauthenticated Users</asp:listitem>
                                        <asp:listitem value="Admins;">Admins Role</asp:listitem>
                                    </asp:dropdownlist></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="Normal">
                                    <rbfwebui:localize id="module_name" runat="server" text="Module Name" textkey="AM_MODULENAME">
                                    </rbfwebui:localize></td>
                                <td colspan="2">
                                    <asp:textbox id="moduleTitle" runat="server" cssclass="NormalTextBox" enableviewstate="false"
                                        text="New Module Name" width="250"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <rbfwebui:linkbutton id="AddModuleBtn" runat="server" cssclass="CommandButton" text="Add to 'Organize Modules' Below"
                                        textkey="AM_ADDMODULEBELOW" OnClick="AddModuleToPane_Click" /></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="Normal">
                                    <rbfwebui:localize id="organizemodule" runat="server" text="Organize Module" textkey="AM_ORGANIZEMODULE">
                                    </rbfwebui:localize></td>
                                <td width="120">
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="NormalBold">
                                                <rbfwebui:localize id="LeftPanel" runat="server" text="Left Pane" textkey="AM_LEFTPANEL">
                                                </rbfwebui:localize></td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2">
                                                    <tr valign="top">
                                                        <td rowspan="2">
                                                            <asp:listbox id="leftPane" runat="server" cssclass="NormalTextBox" datasource="<%# leftList %>"
                                                                datatextfield="Title" datavaluefield="ID" rows="8" width="110"></asp:listbox></td>
                                                        <td nowrap="nowrap" valign="top">
                                                            <rbfwebui:imagebutton id="LeftUpBtn" runat="server" commandargument="leftPane" commandname="up"
                                                                text="Move Up" textkey="MOVEUP" OnClick="UpDown_Click" /><br/>
                                                            <rbfwebui:imagebutton id="LeftRightBtn" runat="server" commandname="right" sourcepane="leftPane"
                                                                targetpane="contentPane" text="Move Right" textkey="MOVERIGHT" OnClick="RightLeft_Click" /><br/>
                                                            <rbfwebui:imagebutton id="LeftDownBtn" runat="server" commandargument="leftPane"
                                                                commandname="down" text="Move Down" textkey="MOVEDOWN" OnClick="UpDown_Click" />&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap" valign="bottom">
                                                            <rbfwebui:imagebutton id="LeftEditBtn" runat="server" commandargument="leftPane"
                                                                commandname="edit" text="Edit" textkey="EDIT" OnClick="EditBtn_Click" /><br/>
                                                            <br/>
                                                            <rbfwebui:imagebutton id="LeftDeleteBtn" runat="server" commandargument="leftPane"
                                                                commandname="delete" text="Delete" textkey="DELETE" OnClick="DeleteBtn_Click"/></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="*">
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="NormalBold">
                                                &nbsp;
                                                <rbfwebui:localize id="contentpanel" runat="server" text="Content Pane" textkey="AM_CENTERPANEL">
                                                </rbfwebui:localize></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table border="0" cellpadding="0" cellspacing="2">
                                                    <tr valign="top">
                                                        <td rowspan="2">
                                                            <asp:listbox id="contentPane" runat="server" cssclass="NormalTextBox" datasource="<%# contentList %>"
                                                                datatextfield="Title" datavaluefield="ID" rows="8" width="170"></asp:listbox></td>
                                                        <td nowrap="nowrap" valign="top">
                                                            <rbfwebui:imagebutton id="ContentUpBtn" runat="server" commandargument="contentPane"
                                                                commandname="up" text="Move Up" textkey="MOVEUP" OnClick="UpDown_Click" /><br/>
                                                            <rbfwebui:imagebutton id="ContentLeftBtn" runat="server" sourcepane="contentPane"
                                                                targetpane="leftPane" text="Move Left" textkey="MOVELEFT" OnClick="RightLeft_Click" /><br/>
                                                            <rbfwebui:imagebutton id="ContentRightBtn" runat="server" sourcepane="contentPane"
                                                                targetpane="rightPane" text="Move Right" textkey="MOVERIGHT"  OnClick="RightLeft_Click" /><br/>
                                                            <rbfwebui:imagebutton id="ContentDownBtn" runat="server" commandargument="contentPane"
                                                                commandname="down" text="Move Down" textkey="MOVEDOWN" OnClick="UpDown_Click"/>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap" valign="bottom">
                                                            <rbfwebui:imagebutton id="ContentEditBtn" runat="server" commandargument="contentPane"
                                                                commandname="edit" text="Edit" textkey="EDIT" OnClick="EditBtn_Click" /><br/>
                                                            <br/>
                                                            <rbfwebui:imagebutton id="ContentDeleteBtn" runat="server" commandargument="contentPane"
                                                                commandname="delete" text="Delete" textkey="DELETE" OnClick="DeleteBtn_Click" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="120">
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="NormalBold">
                                                &nbsp;
                                                <rbfwebui:localize id="rightpanel" runat="server" text="Right Pane" textkey="AM_RIGHTPANEL">
                                                </rbfwebui:localize></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2">
                                                    <tr valign="top">
                                                        <td rowspan="2">
                                                            <asp:listbox id="rightPane" runat="server" cssclass="NormalTextBox" datasource="<%# rightList %>"
                                                                datatextfield="Title" datavaluefield="ID" rows="8" width="110"></asp:listbox></td>
                                                        <td nowrap="nowrap" valign="top">
                                                            <rbfwebui:imagebutton id="RightUpBtn" runat="server" commandargument="rightPane"
                                                                commandname="up" text="Move Up" textkey="MOVEUP" OnClick="UpDown_Click" /><br/>
                                                            <rbfwebui:imagebutton id="RightLeftBtn" runat="server" sourcepane="rightPane" targetpane="contentPane"
                                                                text="Move Left" textkey="MOVELEFT" OnClick="RightLeft_Click" /><br/>
                                                            <rbfwebui:imagebutton id="RightDownBtn" runat="server" commandargument="rightPane"
                                                                commandname="down" text="Move Down" textkey="MOVEDOWN" OnClick="UpDown_Click" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap="nowrap" valign="bottom">
                                                            <rbfwebui:imagebutton id="RightEditBtn" runat="server" commandargument="rightPane"
                                                                commandname="edit" text="Edit" textkey="EDIT" OnClick="EditBtn_Click" /><br/>
                                                            <br/>
                                                            <rbfwebui:imagebutton id="RightDeleteBtn" runat="server" commandargument="rightPane"
                                                                commandname="delete" text="Delete" textkey="DELETE" OnClick="DeleteBtn_Click"/></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="Error" colspan="4">
                                    <rbfwebui:localize id="msgError" runat="server" text="You do not have the appropriate permissions to delete or move this module"
                                        textkey="AM_NO_RIGHTS">
                                    </rbfwebui:localize>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr noshade="noshade" size="1" />
                                    <rbfwebui:settingstable id="EditTable" runat="server" OnUpdateControl="EditTable_UpdateControl" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <rbfwebui:linkbutton id="updateButton" runat="server" class="CommandButton" text="Apply Changes"
                                        textkey="APPLY_CHANGES"></rbfwebui:linkbutton>&nbsp;
                                    <rbfwebui:linkbutton id="cancelButton" runat="server" class="CommandButton" text="Cancel"
                                        textkey="CANCEL"></rbfwebui:linkbutton>
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
