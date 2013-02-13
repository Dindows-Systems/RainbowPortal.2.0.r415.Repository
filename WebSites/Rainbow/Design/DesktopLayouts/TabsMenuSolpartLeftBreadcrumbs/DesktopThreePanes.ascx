<%@ Control Language="c#" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }
</script>
<rbfwebui:DESKTOPPANES id="ThreePanes" ShowLogon="False" ShowLastSeparator="False" ShowFirstSeparator="False"
	Cellpadding="0" cellspacing="0" runat="server">
	<LEFTPANETEMPLATE>
			<rbfwebui:SOLPARTNAVIGATION id="PortalMenu" Bind="BindOptionCurrentChilds" AutoShopDetect="True" runat="server"
				MenuBarRightHTML="" MenuBarLeftHTML="" AutoBind="True" ForceDownlevel="False" Visible="True" ToolTip=""
				Moveable="False" Display="Vertical" MenuAlignment="Left" RootArrow="False" IconWidth="15" MenuItemHeight="22"
				MenuBorderWidth="0" MenuBarHeight="22" ShadowColor="transparent" SelectedForeColor="transparent" SelectedColor="transparent"
				SelectedBorderColor="transparent" MenuEffects-MouseOutHideDelay="500" MenuEffects-MouseOverExpand="True"
				MenuEffects-MenuTransitionStyle="None" MenuEffects-MouseOverDisplay="None" MenuEffects-MenuTransition="None"
				MenuCSS-MenuDefaultItemHighLight="spm_DefaultItemHighlight" MenuCSS-MenuDefaultItem="spm_DefaultItem"
				MenuCSS-MenuArrow="spm_MenuArrow" MenuCSS-MenuIcon="spm_MenuIcon" MenuCSS-RootMenuArrow="spm_RootMenuArrow"
				MenuCSS-MenuBreak="spm_MenuBreak" MenuCSS-SubMenu="spm_SubMenu" MenuCSS-MenuContainer="spm_MenuContainer"
				MenuCSS-MenuItem="spm_MenuItem" MenuCSS-MenuItemSel="spm_MenuItemSel" MenuCSS-MenuBar="spm_MenuBar"
				MenuCSSPlaceHolderControl="spMenuStyle"
				 />
	</LEFTPANETEMPLATE>
	<ContentPaneTemplate><rbfwebui:BreadCrumbs TextCSSClass="Normal" LinkCSSClass="Normal" id="BreadCrumbs1" runat="server"></rbfwebui:BreadCrumbs></ContentPaneTemplate>
	<LEFTPANESTYLE cssclass="LeftPane" width="190px" verticalalign="Top"></LEFTPANESTYLE>
	<CONTENTPANESTYLE cssclass="ContentPane" verticalalign="Top" horizontalalign="left"></CONTENTPANESTYLE>
	<RIGHTPANESTYLE cssclass="RightPane" verticalalign="Top"></RIGHTPANESTYLE>
</rbfwebui:DESKTOPPANES>
