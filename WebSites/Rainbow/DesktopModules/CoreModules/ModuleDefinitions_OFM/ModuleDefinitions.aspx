<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>

<%@ page autoeventwireup="false" codefile="ModuleDefinitions.aspx.cs" inherits="Rainbow.AdminAll.ModuleDefinitions_OFM"
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
                                                <rbfwebui:label id="Label1" runat="server" text="OneFileModule type definition" textkey="MODULE_TYPE_DEFINITION_OFM"></rbfwebui:label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr noshade="noshade" size="1" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="Table1" runat="server" border="0" cellpadding="3" cellspacing="0" width="750">
                                        <tr>
                                            <td class="SubHead" width="105">
                                                <rbfwebui:label id="Label2" runat="server" text="Friendly Name" textkey="FRIENDLY_NAME"></rbfwebui:label>:
                                            </td>
                                            <td rowspan="6" width="3">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:textbox id="FriendlyName" runat="server" columns="30" cssclass="NormalTextBox"
                                                    maxlength="150" width="390"></asp:textbox></td>
                                            <td rowspan="6" width="10">
                                                &nbsp;
                                            </td>
                                            <td class="Normal" width="250">
                                                <asp:requiredfieldvalidator id="Req1" runat="server" controltovalidate="FriendlyName"
                                                    cssclass="Error" designtimedragdrop="235" display="Dynamic" errormessage="Enter a Module Name"
                                                    textkey="ERROR_ENTER_A_MODULE_NAME"></asp:requiredfieldvalidator></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" nowrap="nowrap" width="105">
                                                <rbfwebui:label id="Label3" runat="server" text="Desktop Source" textkey="DESKTOP_SOURCE"></rbfwebui:label>:
                                            </td>
                                            <td>
                                                <asp:textbox id="DesktopSrc" runat="server" columns="30" cssclass="NormalTextBox"
                                                    maxlength="150" width="390"></asp:textbox></td>
                                            <td class="Normal">
                                                <asp:requiredfieldvalidator id="Req2" runat="server" controltovalidate="DesktopSrc"
                                                    cssclass="Error" display="Dynamic" errormessage="You Must Enter Source Path for the Desktop Module"
                                                    textkey="ERROR_ENTER_A_SOURCE_PATH"></asp:requiredfieldvalidator><rbfwebui:label
                                                        id="lblInvalidModule" runat="server" cssclass="Error" enableviewstate="False"
                                                        text="Invalid module!" textkey="ERROR_INVALID_MODULE" visible="False">
											Invalid module!</rbfwebui:label></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" width="105">
                                                <rbfwebui:label id="Label4" runat="server" text="Mobile Source" textkey="MOBILE_SOURCE"></rbfwebui:label>:
                                            </td>
                                            <td>
                                                <asp:textbox id="MobileSrc" runat="server" columns="30" cssclass="NormalTextBox"
                                                    maxlength="150" width="390"></asp:textbox></td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" width="105">
                                                <rbfwebui:label id="Label5" runat="server" text="Guid" textkey="GUID"></rbfwebui:label>:</td>
                                            <td>
                                                <asp:textbox id="ModuleGuid" runat="server" columns="1" cssclass="NormalTextBox"
                                                    maxlength="36" width="390"></asp:textbox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" valign="top" width="116">
                                            </td>
                                            <td>
                                                <rbfwebui:linkbutton id="selectAllButton" runat="server" cssclass="CommandButton"
                                                    text="Select all" textkey="SELECT_ALL"></rbfwebui:linkbutton>&nbsp;&nbsp;
                                                <rbfwebui:linkbutton id="selectNoneButton" runat="server" cssclass="CommandButton"
                                                    text="Select none" textkey="SELECT_NONE"></rbfwebui:linkbutton></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" valign="top" width="116">
                                                <rbfwebui:label id="Label6" runat="server" text="Portals" textkey="PORTALS"></rbfwebui:label>:
                                            </td>
                                            <td>
                                                <div style="overflow: auto; height: 200px">
                                                    <asp:checkboxlist id="PortalsName" runat="server" cellpadding="0" cellspacing="0"
                                                        cssclass="Normal" repeatcolumns="1" repeatdirection="Horizontal">
                                                    </asp:checkboxlist></div>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <p>
                                        <rbfwebui:linkbutton id="updateButton" runat="server" class="CommandButton" text="Update"
                                            textkey="UPDATE">Update</rbfwebui:linkbutton>&nbsp;
                                        <rbfwebui:linkbutton id="cancelButton" runat="server" causesvalidation="False" class="CommandButton"
                                            text="Cancel" textkey="CANCEL">Cancel</rbfwebui:linkbutton>&nbsp;
                                        <rbfwebui:linkbutton id="deleteButton" runat="server" causesvalidation="False" class="CommandButton"
                                            text="Delete this module type" textkey="DELETE_THIS_MODULE_TYPE">Delete this module type</rbfwebui:linkbutton></p>
                                    <p>
                                        <rbfwebui:label id="lblErrorDetail" runat="server" cssclass="Error"></rbfwebui:label></p>
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
