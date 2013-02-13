<%@ Control %>
<%@ Register TagPrefix="zen" Namespace="Rainbow.Framework.Web.UI.WebControls" assembly="Rainbow.Framework" %>
<zen:ZenLayout 
	ID="zenpanes" 
	CssID="zenpanes"
	LeftContent="LeftPane" 
	CenterContent="ContentPane" 
	RightContent="RightPane"
	HeaderContent="Header"
	FooterContent="Footer"
	ForceLeft="false" 
	ForceRight="false"
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
			XsltFile="BindAll" 
			runat="server">
		</zen:ZenNavigation>
	</HeaderTemplate>
	<LeftColTemplate><zen:ZenContent ID="LeftPane" Content="LeftPane" runat="server"/></LeftColTemplate>		
	<CenterColTemplate><zen:ZenContent ID="CenterPane" Content="ContentPane" runat="server"/></CenterColTemplate>
	<RightColTemplate><zen:ZenContent ID="RightPane" Content="RightPane" runat="server"/></RightColTemplate>
	<FooterTemplate>
		<div class="zen-footer zen-clear">
			<div>Powered by <a href="http://community.rainbowportal.net" title="Rainbow Community Site">Rainbow 2.0</a></div>
		</div>
	</FooterTemplate>
</zen:ZenLayout>

