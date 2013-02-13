<%@ control language="c#" %>
<%@ register assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" tagprefix="rbfwebui" %>
<%@ register assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ import namespace="Rainbow.Framework.Design" %>

<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        PortalHeaderMenu.DataBind();
        PortalImage.DataBind();
        PortalTitle.DataBind();
        PortalMenu.DataBind();
    }
</script>

<!-- Portal BANNER -->
<table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="DefaultBanner1" colspan="2" height="1">
                <asp:image id="IMAGE1" runat="server" border="0" imageurl="~/design/Themes/default/img/shim.gif" /></td>
        </tr>
        <tr>
            <td align="left" rowspan="2" valign="center">
                <table cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td>
                                <rbfwebui:headerimage id="PortalImage" runat="server" enableviewstate="false" style="margin-top: 0px;
                                    margin-right: 0px" />
                            </td>
                            <!--headerimage=images/logo-default.gif-->
                            <td height="50">
                                <rbfwebui:headertitle id="PortalTitle" runat="server" cssclass="SiteTitle" enableviewstate="false"></rbfwebui:headertitle>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td align="right" valign="top">
                <table cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td valign="top">
                            </td>
                            <td valign="top">
                                <rbfwebui:headermenu id="PortalHeaderMenu" runat="server" cellpadding="4" cssclass="SiteLink"
                                    repeatdirection="Horizontal" showhelp="True" showhome="False">
                                    <separatorstyle />
                                    <itemtemplate>
                                        <span class="SiteLink">
                                            <%# Container.DataItem %>
                                        </span>
                                    </itemtemplate>
                                    <separatortemplate>
                                        |
                                    </separatortemplate>
                                </rbfwebui:headermenu>
                            </td>
                            <rbfwebui:linkbutton id="saveConfig" runat="server" visible="False">
                            </rbfwebui:linkbutton>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td align="right">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="DefaultBanner1" colspan="2" height="1">
                <asp:image id="IMAGE2" runat="server" border="0" imageurl="~/design/Themes/default/img/shim.gif" /></td>
        </tr>
        <tr>
            <td class="DefaultBanner2" colspan="2" height="1">
                <asp:image id="IMAGE3" runat="server" border="0" imageurl="~/design/Themes/default/img/shim.gif" /></td>
        </tr>
        <tr>
            <td class="DefaultTD" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr height="22">
                            <td class="navigationBar spm_MenuBar">
                                <rbfwebui:solpartnavigation id="PortalMenu" runat="server" autobind="False" autoshopdetect="True"
                                    bind="BindOptionTop" display="Horizontal" forcedownlevel="False" iconwidth="15"
                                    menualignment="Left" menubarheight="25" menubarlefthtml="" menubarrighthtml=""
                                    menuborderwidth="1" menucss-menuarrow="spm_MenuArrow" menucss-menubar="spm_MenuBar"
                                    menucss-menubreak="spm_MenuBreak" menucss-menucontainer="spm_MenuContainer" menucss-menuicon="spm_MenuIcon"
                                    menucss-menuitem="spm_MenuItem" menucss-menuitemsel="spm_MenuItemSel" menucss-rootmenuarrow="spm_RootMenuArrow"
                                    menucss-submenu="spm_SubMenu" menucssplaceholdercontrol="spMenuStyle" menueffects-menutransition="None"
                                    menueffects-menutransitionstyle="None" menueffects-mouseoverdisplay="None" menueffects-mouseoverexpand="True"
                                    menuitemheight="24" moveable="False" parentpageid="0" rootarrow="False" selectedbordercolor="Silver"
                                    selectedcolor="Black" selectedforecolor="White" shadowcolor="Gray" showiconmenu="True"
                                    tooltip="" visible="True">
                                </rbfwebui:solpartnavigation>
                            </td>
                            <td align="right" class="navigationBar navigationNonSelected" nowrap="nowrap">
                                <img alt="Design" border="0" height="1" src="<%=LayoutManager.WebPath%>/MenuTopSolpart/images/spacer.gif"
                                    width="60" /><span class="dateCurrent"><%=System.DateTime.Now.ToLongDateString()%></span><span
                                        class="dateLastVisit"></span></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="bc_Row" colspan="2">
                <rbfwebui:breadcrumbs id="Breadcrumbs1" runat="server" linkcssclass="bc_Link" textcssclass="bc_Text">
                </rbfwebui:breadcrumbs>
            </td>
        </tr>
    </tbody>
</table>
<!-- END Portal BANNER -->
