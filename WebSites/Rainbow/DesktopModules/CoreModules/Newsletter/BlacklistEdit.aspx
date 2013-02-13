<%@ page autoeventwireup="false" codefile="BlacklistEdit.aspx.cs" inherits="Rainbow.Admin.BlacklistEdit"
    language="c#" %>

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
                        <portal:banner id="SiteHeader" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                        <table border="0" cellpadding="4" cellspacing="0" width="98%">
                            <tr valign="top">
                                <td width="50">
                                    &nbsp;
                                </td>
                                <td width="*">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" class="Head">
                                                <rbfwebui:localize id="Literal1" runat="server" text="Blacklist" textkey="BLACKLIST">
                                                </rbfwebui:localize></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr noshade="noshade" size="1" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:repeater id="repListItem" runat="server">
                                        <headertemplate>
                                            <table width="100%">
                                            </table>
                                            <tr>
                                                <td colspan="5">
                                                </td>
                                                <td>
                                                    <input onclick="AllCheckboxCheck(0,true);" type="button" value='<%=General.GetString("BLACKLIST_ALL") %>'>&#160;
                                                    <input onclick="AllCheckboxCheck(0,false);" type="button" value='<%=General.GetString("BLACKLIST_NONE") %>'>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th align="left">
                                                    <rbfwebui:localize id="Localize1" runat="server" text="Name" textkey="BLACKLIST_NAME">
                                                    </rbfwebui:localize></th>
                                                <th align="left">
                                                    <rbfwebui:localize id="Localize2" runat="server" textkey="BLACKLIST_EMAIL">
                                                    </rbfwebui:localize></th>
                                                <th align="left">
                                                    <rbfwebui:localize id="Localize3" runat="server" text="Send Newsletter" textkey="BLACKLIST_SENDNEWSLETTER">
                                                    </rbfwebui:localize></th>
                                                <th align="left">
                                                    <rbfwebui:localize id="Localize4" runat="server" textkey="BLACKLIST_DATE">
                                                    </rbfwebui:localize></th>
                                                <th align="left">
                                                    <rbfwebui:localize id="Localize5" runat="server" textkey="BLACKLIST_REASON">
                                                    </rbfwebui:localize></th>
                                                <th align="left">
                                                    <rbfwebui:localize id="Localize6" runat="server" textkey="BLACKLISTED">
                                                    </rbfwebui:localize></th>
                                            </tr>
                                        </headertemplate>
                                        <itemtemplate>
                                            <tr>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                                    <rbfwebui:label id="lblEMail" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "EMail") %>'
                                                        visible="False">
                                                    </rbfwebui:label>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "Email") %>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "SendNewsletter") %>
                                                </td>
                                                <td>
                                                    <%# GetDate(Container.DataItem) %>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "Reason") %>
                                                </td>
                                                <td>
                                                    <asp:checkbox id="chkSelect" runat="server" checked='<%# GetBlacklisted(Container.DataItem) %>'
                                                        enableviewstate="False" /></td>
                                            </tr>
                                        </itemtemplate>
                                        <footertemplate>
                                            </table>
                                        </footertemplate>
                                    </asp:repeater>
                                    <p>
                                        <rbfwebui:linkbutton id="updateButton" runat="server" cssclass="CommandButton" text="Update">
                                        </rbfwebui:linkbutton>
                                        &nbsp;
                                        <rbfwebui:linkbutton id="cancelButton" runat="server" causesvalidation="False" cssclass="CommandButton"
                                            text="Cancel">
                                        </rbfwebui:linkbutton>
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
