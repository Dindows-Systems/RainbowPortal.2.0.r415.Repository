<%@ Register Src="~/Design/DesktopLayouts/DesktopFooter.ascx" TagName="Footer" TagPrefix="foot" %>
<%@ Register Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" TagName="Banner"
    TagPrefix="portal" %>

<%@ Page AutoEventWireup="false" CodeFile="ModuleSettings.aspx.cs" Inherits="Rainbow.Admin.ModuleSettingsPage"
    Language="c#" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body id="Body1" runat="server"> 
    <form id="Form1" runat="server">
        <div id="zenpanes" class="zen-main">
            <div class="rb_DefaultPortalHeader">
                <portal:Banner ID="Banner1" runat="server" showtabs="false" />
            </div>
            <div class="div_ev_Table">
                <table border="0" cellpadding="4" cellspacing="0" width="98%">
                    <tr valign="top">
                        <td width="150">
                            &nbsp;</td>
                        <td width="*">
                            <table border="0" cellpadding="2" cellspacing="1">
                                <tr>
                                    <td colspan="4" height="20">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" class="Head" nowrap="true" width="1%">
                                                    <rbfwebui:localize id="Literal1" runat="server" text="Module base settings" textkey="MODULESETTINGS_BASE_SETTINGS">
                                                    </rbfwebui:localize></td>
                                                <td align="right" nowrap="true" width="99%">
                                                    <asp:placeholder id="PlaceholderButtons2" runat="server"></asp:placeholder>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                        <hr noshade="noshade" size="1" />
                                    </td>
                                </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" height="50" width="200">
                                        <rbfwebui:localize id="Literal2" runat="server" text="Module type" textkey="MODULESETTINGS_MODULE_TYPE">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" height="38">
                                        &nbsp;<rbfwebui:label id="moduleType" runat="server" cssclass="NormalBold" width="300"></rbfwebui:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="200">
                                        <rbfwebui:localize id="Literal18" runat="server" text="Module name" textkey="MODULESETTINGS_MODULE_NAME">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3">
                                        &nbsp;<asp:textbox id="moduleTitle" runat="server" cssclass="NormalTextBox" width="300"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="200">
                                        <rbfwebui:localize id="Literal3" runat="server" text="Cache Timeout" textkey="MODULESETTINGS_CACHE_TIMEOUT">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3">
                                        &nbsp;<asp:textbox id="cacheTime" runat="server" cssclass="NormalTextBox" width="100"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="200">
                                        <rbfwebui:localize id="Literal13" runat="server" text="Move to tab" textkey="MODULESETTINGS_MOVE_TO_TAB">
                                        </rbfwebui:localize>:</td>
                                    <td colspan="3">
                                        &nbsp;<asp:dropdownlist id="tabDropDownList" runat="server" cssclass="NormalTextBox"
                                            datasource="<%# portalTabs %>" datatextfield="Name" datavaluefield="ID" width="300px">
                                        </asp:dropdownlist></td>
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
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal5" runat="server" text="Roles that can view" textkey="MODULESETTINGS_ROLE_VIEW">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authViewRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal4" runat="server" text="Roles that can edit" textkey="MODULESETTINGS_ROLES_EDIT">
                                        </rbfwebui:localize>:</td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authEditRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal6" runat="server" text="Roles that can add" textkey="MODULESETTINGS_ROLES_ADD">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authAddRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal7" runat="server" text="Roles that can delete" textkey="MODULESETTINGS_ROLES_DELETE">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authDeleteRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal8" runat="server" text="Roles that can edit properties"
                                            textkey="MODULESETTINGS_ROLES_EDIT_COLLECTION">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authPropertiesRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal16" runat="server" text="Roles that can move modules"
                                            textkey="MODULESETTINGS_ROLES_MOVE_MODULES">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authMoveModuleRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal17" runat="server" text="Roles that can delete modules"
                                            textkey="MODULESETTINGS_ROLES_DELETE_MODULES">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authDeleteModuleRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
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
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal9" runat="server" text="Enable workflow" textkey="MODULESETTINGS_SUPPORT_WORKFLOW">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3">
                                        <asp:checkbox id="enableWorkflowSupport" runat="server" autopostback="True" /></td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal10" runat="server" text="Approve roles" textkey="MODULESETTINGS_ROLES_APPROVING">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authApproveRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" valign="top" width="200">
                                        <rbfwebui:localize id="Literal11" runat="server" text="Publishing roles" textkey="MODULESETTINGS_ROLES_PUBLISHING">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3" nowrap="nowrap">
                                        <asp:checkboxlist id="authPublishingRoles" runat="server" cellpadding="0" cellspacing="0"
                                            cssclass="Normal" repeatcolumns="2" width="300">
                                        </asp:checkboxlist>
                                    </td>
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
                                    <td class="SubHead" nowrap="nowrap" width="200">
                                        <rbfwebui:localize id="Literal12" runat="server" text="Show to mobile users" textkey="SHOWMOBILE">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3">
                                        <asp:checkbox id="ShowMobile" runat="server" cssclass="Normal" /></td>
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
                                    <td class="SubHead" nowrap="nowrap" width="200">
                                        <rbfwebui:localize id="Literal14" runat="server" text="Show on every page?" textkey="MODULESETTINGS_SHOW_EVERYWHERE">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3">
                                        <asp:checkbox id="showEveryWhere" runat="server" cssclass="Normal" /></td>
                                </tr>
                                <tr>
                                    <td class="SubHead" nowrap="nowrap" width="200">
                                        <rbfwebui:localize id="Literal15" runat="server" text="Can collapse window?" textkey="MODULESETTINGS_SHOW_COLLAPSABLE">
                                        </rbfwebui:localize>:
                                    </td>
                                    <td colspan="3">
                                        <asp:checkbox id="allowCollapsable" runat="server" cssclass="Normal" /></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr noshade="noshade" size="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" nowrap="true">
                                        <asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>
                                        &nbsp;
                                    </td> 
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="rb_AlternatePortalFooter">
                <foot:footer id="Footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
