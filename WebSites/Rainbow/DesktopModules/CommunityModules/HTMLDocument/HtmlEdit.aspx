<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<%@ page autoeventwireup="false" codefile="HtmlEdit.aspx.cs" inherits="Rainbow.Content.Web.Modules.HtmlEdit"
    language="c#" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <div id="zenpanes" class="zen-main">
            <div class="rb_DefaultPortalHeader">
                <portal:banner id="SiteHeader" runat="server" />
            </div>
            <div class="div_ev_Table">
                <table border="0" cellpadding="4" cellspacing="0" width="98%">
                    <tr>
                        <td align="left" class="Head">
                            <rbfwebui:localize id="Literal1" runat="server" text="HTML Editor" textkey="HTML_EDITOR">
                            </rbfwebui:localize>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr noshade="noshade" size="1" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="4" cellspacing="0" width="98%">
                    <tr>
                        <td class="SubHead">
                            <p>
                                <rbfwebui:localize id="Literal2" runat="server" text="Desktop HTML Content" textkey="HTML_DESKTOP_CONTENT">
                                </rbfwebui:localize><font face="ËÎÌו">:</font>
                                <br/>
                                <span class="normal">
                                    <asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
                                </span>
                            </p>
                        </td>
                    </tr>
                    <tr id="MobileRow" runat="server">
                        <td class="SubHead">
                            &nbsp;
                            <p>
                                <br />
                                <rbfwebui:localize id="Literal3" runat="server" text="Mobile Summary" textkey="HTML_MOBILE_SUMMARY">
                                </rbfwebui:localize><font face="ËÎÌו">:</font>
                                <br />
                                <asp:textbox id="MobileSummary" runat="server" columns="75" cssclass="NormalTextBox"
                                    rows="3" textmode="multiline" width="650"></asp:textbox><br/>
                                <rbfwebui:localize id="Literal4" runat="server" text="Mobile Details" textkey="HTML_MOBILE_DETAILS">
                                </rbfwebui:localize>:
                                <br />
                                <asp:textbox id="MobileDetails" runat="server" columns="75" cssclass="NormalTextBox"
                                    rows="5" textmode="multiline" width="650"></asp:textbox></p>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>
                </p>
            </div>
            <div class="rb_AlternatePortalFooter">
                <foot:footer id="Footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
