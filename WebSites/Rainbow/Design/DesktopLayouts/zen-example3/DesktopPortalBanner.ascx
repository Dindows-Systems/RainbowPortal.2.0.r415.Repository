<%@ Control %>
<%@ Register TagPrefix="zen" Namespace="Rainbow.Framework.Web.UI.WebControls" assembly="Rainbow.Framework" %>
<zen:ZenLayout 
	ID="ZenHeader" 
	CssID="ZenHeader"
	HeaderContent="Header"
	runat="server">
	<HeaderTemplate>
		<zen:ZenHeaderTitle 
			ID="PortalTitle" 
			ShowImage="true" 
			runat="server">
		</zen:ZenHeaderTitle>
		<div class="header-image"></div>
		<zen:ZenHeaderMenu 
			ID="PortalHeaderMenu" 
			ButtonsCssClass="headermenu" 
			LabelsCssClass="headerlabels" 
			ShowLogon="true" 
			ShowSaveDesktop="false" 
			runat="server">
		</zen:ZenHeaderMenu>
		<zen:ZenNavigation 
			ID="PortalMainNav" 
			ContainerCssClass="mainmenu" 
			runat="server">
		</zen:ZenNavigation>
	</HeaderTemplate>
</zen:ZenLayout>

