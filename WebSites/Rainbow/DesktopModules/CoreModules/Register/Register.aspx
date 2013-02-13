<%@ page autoeventwireup="false" codefile="Register.aspx.cs" inherits="Rainbow.Admin.Register"
    language="c#" %>

<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div id="zenpanes" class="zen-main">
            <div class="rb_DefaultPortalHeader">
                <portal:banner id="Banner1" runat="server" />
            </div>
            <div class="div_ev_Table">
                <table border="0" cellpadding="0" cellspacing="0" width="90%">
                    <tr>
                        <td>
                            <!-- Start Register control -->
                            <asp:placeholder id="register" runat="server" />
                            <!-- End Register control -->
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
