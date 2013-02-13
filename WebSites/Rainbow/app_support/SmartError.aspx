<%@ register src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" tagname="Banner"
    tagprefix="portal" %>
<%@ register src="~/Design/DesktopLayouts/DesktopFooter.ascx" tagname="Footer" tagprefix="foot" %>
<%@ page autoeventwireup="false" inherits="Rainbow.Error.SmartError" language="c#"
    src="SmartError.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
        <div id="zenpanes">
            <div class="rb_DefaultPortalHeader">
                <portal:banner id="Banner2" runat="server" showtabs="false" />
            </div>
            <div class="div_ev_Table">
                <div class="rb_DefaultLayoutDiv SmartError">
                    <asp:placeholder id="PageContent" runat="server"></asp:placeholder>
                </div>
            </div>
            <foot:footer id="Footer" runat="server" />
        </div>
    </form>
</body>
</html>
