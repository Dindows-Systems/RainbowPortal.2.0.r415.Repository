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
	ForceLeft="true" 
	ForceRight="false"
	runat="server">
	<HeaderTemplate>
		<zen:ZenHeaderTitle 
			ID="PortalTitle" 
			ShowImage="true" 
			runat="server">
		</zen:ZenHeaderTitle>
		<zen:ZenHeaderMenu 
			ID="PortalHeaderMenu" 
			ButtonsCssClass="headermenu" 
			LabelsCssClass="headerlabels" 
			ShowLogon="true" 
			ShowSaveDesktop="false" 
			runat="server">
		</zen:ZenHeaderMenu>
		<zen:ZenNavigation 
			ID="PortalTopNav" 
			ContainerCssClass="mainmenu"
			XsltFile="BindTopOnly" 
			runat="server">
		</zen:ZenNavigation>
	</HeaderTemplate>
	<LeftColTemplate>
<script type="text/javascript">
startList = function() {

	// code for IE
	if(!document.body.currentStyle) return;
	var subs = document.getElementsByName('daddy');
	for(var i=0; i<subs.length; i++) {
		var li = subs[i].parentNode;
		if(li && li.lastChild.style) {
			li.onmouseover = function() {
				this.lastChild.style.visibility = 'visible';
			}
			li.onmouseout = function() {
				this.lastChild.style.visibility = 'hidden';
			}
		}
	}
}
window.onload=startList;
</script>
<zen:ZenNavigation 
			ID="PortalChildNav" 
			ContainerCssClass="leftmenu"
			XsltFile="BindRootChildren" 
			runat="server">
		</zen:ZenNavigation>
<zen:ZenContent ID="LeftPane" Content="LeftPane" runat="server"/></LeftColTemplate>		
	<CenterColTemplate><zen:ZenContent ID="CenterPane" Content="ContentPane" runat="server"/></CenterColTemplate>
	<RightColTemplate><zen:ZenContent ID="RightPane" Content="RightPane" runat="server"/></RightColTemplate>
	<FooterTemplate>
		<div class="zen-footer zen-clear">
			<div>Powered by Rainbow with Rainbow.Zen</div>
		</div>
	</FooterTemplate>
</zen:ZenLayout>

