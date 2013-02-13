<%@ control language="c#" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }
</script>

<rbfwebui:desktoppanes id="ThreePanes" runat="server" cellpadding="0" cellspacing="0"
    showfirstseparator="False" showlastseparator="False" showlogon="False">
    <leftpanetemplate>
        <div>
            <rbfwebui:solpartnavigation id="PortalMenu" runat="server" autobind="True" autoshopdetect="True"
                bind="BindOptionCurrentChilds" display="Vertical" forcedownlevel="False" iconwidth="15"
                menualignment="Left" menubarheight="22" menubarlefthtml="" menubarrighthtml=""
                menuborderwidth="0" menucss-menuarrow="spm_MenuArrow" menucss-menubar="spm_MenuBar"
                menucss-menubreak="spm_MenuBreak" menucss-menucontainer="spm_MenuContainer" menucss-menudefaultitem="spm_DefaultItem"
                menucss-menudefaultitemhighlight="spm_DefaultItemHighlight" menucss-menuicon="spm_MenuIcon"
                menucss-menuitem="spm_MenuItem" menucss-menuitemsel="spm_MenuItemSel" menucss-rootmenuarrow="spm_RootMenuArrow"
                menucss-submenu="spm_SubMenu" menucssplaceholdercontrol="spMenuStyle" menueffects-menutransition="None"
                menueffects-menutransitionstyle="None" menueffects-mouseouthidedelay="500" menueffects-mouseoverdisplay="None"
                menueffects-mouseoverexpand="True" menuitemheight="22" moveable="False" rootarrow="False"
                selectedbordercolor="transparent" selectedcolor="transparent" selectedforecolor="transparent"
                shadowcolor="transparent" tooltip="" visible="True">
            </rbfwebui:solpartnavigation>
        </div>
    </leftpanetemplate>
    <leftpanestyle cssclass="LeftPane" verticalalign="Top" width="190px" />
    <contentpanestyle cssclass="ContentPane" horizontalalign="left" verticalalign="Top" />
    <rightpanestyle cssclass="RightPane" verticalalign="Top" />
</rbfwebui:desktoppanes>
