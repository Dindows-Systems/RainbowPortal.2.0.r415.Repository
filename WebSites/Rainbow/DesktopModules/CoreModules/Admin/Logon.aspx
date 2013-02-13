<%@ Page language="c#" codefile="Logon.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Admin.LogonPage" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server" ID="Form1">
			<div id="zenpanes">
				<div class="rb_DefaultPortalHeader">
					<portal:Banner ShowTabs="false" runat="server" id="Banner1" />
				</div>
				<div class="div_ev_Table">
					<div align="center">
						<div class="rb_DefaultLayoutDiv" style="width:250px;">
							<asp:PlaceHolder id="signIn" runat="server"></asp:PlaceHolder>
						</div>
					</div>
				</div>
				<foot:Footer id="Footer" runat="server"></foot:Footer>
			</div>
		</form>
	</body>
</html>