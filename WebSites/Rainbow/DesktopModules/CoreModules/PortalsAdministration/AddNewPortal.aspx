<%@ page autoeventwireup="false" inherits="Rainbow.AdminAll.AddNewPortal" language="c#"
    codefile="AddNewPortal.aspx.cs" %>
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
                <portal:banner id="Banner1" runat="server" showtabs="false" />
                
            </div>
            <div class="div_ev_Table">
                <table cellpadding="4" cellspacing="0" width="98%">
                    <tr valign="top">
                        <td width="150">
                            &nbsp;
                        </td>
                        <td width="*">
                            <table border="0" cellpadding="2" cellspacing="1">
                                <tr>
                                    <td align="left" class="Head" colspan="3">
                                        <rbfwebui:localize id="Literal1" runat="server" text="Add new portal" textkey="ADD_PORTAL">
                                        </rbfwebui:localize></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr noshade="noshade" size="1" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="140">
                                        <rbfwebui:localize id="Literal2" runat="server" text="Site title" textkey="AM_SITETITLE">
                                        </rbfwebui:localize></td>
                                    <td class="Normal">
                                        <asp:textbox id="TitleField" runat="server" cssclass="NormalTextBox" width="350"></asp:textbox></td>
                                    <td class="Normal">
                                        <asp:requiredfieldvalidator id="RequiredTitle" runat="server" controltovalidate="TitleField"
                                            errormessage="Required Field"></asp:requiredfieldvalidator></td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="140">
                                        <rbfwebui:localize id="Literal3" runat="server" text="Portal Alias" textkey="AM_PORTALALIAS">
                                        </rbfwebui:localize></td>
                                    <td class="Normal">
                                        <asp:textbox id="AliasField" runat="server" cssclass="NormalTextBox" width="350"></asp:textbox></td>
                                    <td class="Normal">
                                        <asp:requiredfieldvalidator id="RequiredAlias" runat="server" controltovalidate="AliasField"
                                            errormessage="Required Field"></asp:requiredfieldvalidator></td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="140">
                                        <rbfwebui:localize id="Literal4" runat="server" text="Site Path" textkey="AM_SITEPATH">
                                        </rbfwebui:localize></td>
                                    <td class="Normal">
                                        <asp:textbox id="PathField" runat="server" cssclass="NormalTextBox" width="350"></asp:textbox></td>
                                    <td class="Normal">
                                        <asp:requiredfieldvalidator id="RequiredSitepath" runat="server" controltovalidate="PathField"
                                            errormessage="Required Field"></asp:requiredfieldvalidator></td>
                                </tr>
                                <tr>
                                    <td class="SubHead" width="140">
                                        <asp:checkbox id="chkUseTemplate" runat="server" autopostback="True" text="Use Template?"
                                            textkey="AM_USETEMPLATE" /></td>
                                    <td class="Normal">
                                        <asp:dropdownlist id="SolutionsList" runat="server" cssclass="NormalTextBox" datatextfield="PortalAlias"
                                            datavaluefield="PortalID" width="350px">
                                        </asp:dropdownlist></td>
                                    <td class="Normal">
                                    </td>
                                </tr>
                            </table>
                            <rbfwebui:settingstable id="EditTable" runat="server" />
                            <p>
                                <rbfwebui:LinkButton id="updateButton" runat="server" class="CommandButton"></rbfwebui:LinkButton></p>
                            <p class="Normal">
                                <rbfwebui:label id="ErrorMessage" runat="server" cssclass="Error" visible="false"></rbfwebui:label></p>
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
