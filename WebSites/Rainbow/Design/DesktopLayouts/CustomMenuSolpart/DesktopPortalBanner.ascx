<%@ Control Language="c#" %>
<%@ Import Namespace="Rainbow.Framework.Design" %>
<script runat="server" language="C#">
    public Hashtable userProfile;
    private void Page_Load( object sender, System.EventArgs e ) {
        PortalSettings portalSettings = ( PortalSettings )HttpContext.Current.Items["PortalSettings"];

        // Obtain the Profile details for the current user

        // TODO Ver esto
        /*         
        userProfile = PortalSettings.GetCurrentUserProfile(portalSettings.PortalID);

          PortalHeaderMenu.DataBind();
          PortalImage.DataBind();
          PortalTitle.DataBind();
          PortalMenu.DataBind();*/
    }
</script>

<!-- Portal BANNER -->
<table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" class="HeadBg" width="100%">
                <tr height="22">
                    <td background="<%=LayoutManager.WebPath%>/CustomMenuSolPart/images/header_top_bkd.gif"
                        colspan="2" nowrap="nowrap">
                        <rbfwebui:HeaderMenu ID="PortalHeaderMenu" runat="server" BackColor="" bind="BindOptionTop"
                            CellPadding="2" CssClass="SiteLink" RepeatDirection="Horizontal" ShowHome="false"
                            ShowWelcome="false">
                            <SeparatorStyle ForeColor="White" />
                            <ItemTemplate>
                                <%# Container.DataItem %>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                |</SeparatorTemplate>
                        </rbfwebui:HeaderMenu>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" width="5%">
                        <rbfwebui:HeaderImage ID="PortalImage" runat="server" EnableViewState="false" /></td>
                    <td colspan="2" height="100%" nowrap="nowrap" width="95%">
                        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
                            <tr>
                                <td height="10%" width="100%">
                                    <img alt="" border="0" height="1" src="<%=LayoutManager.WebPath%>/CustomMenuSolPart/images/spacer.gif"
                                        width="1" /></td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <rbfwebui:HeaderTitle ID="PortalTitle" runat="server" CssClass="SiteTitle" EnableViewState="false"></rbfwebui:HeaderTitle></td>
                            </tr>
                            <tr>
                                <td align="right" class="headerUserInfo" nowrap="nowrap">
                                    <%=PortalSettings.CurrentUser.Identity.UserName%>
                                    <br />
                                    <%=userProfile["Title"]%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr height="4">
                    <td background="<%=LayoutManager.WebPath%>/CustomMenuSolPart/images/nav_top_bkd.gif"
                        border="0" colspan="2">
                    </td>
                </tr>
                <tr height="24">
                    <td class="navigationBar spm_MenuBar">
                        <rbfwebui:SolpartNavigation ID="PortalMenu" runat="server" AutoBind="False" Bind="BindOptionTop"
                            Display="Horizontal" ForceDownlevel="False" IconWidth="15" MenuAlignment="Left"
                            MenuBarHeight="23" MenuBarLeftHTML="" MenuBarRightHTML="" MenuBorderWidth="0"
                            MenuCSS-MenuArrow="spm_MenuArrow" MenuCSS-MenuBar="spm_MenuBar" MenuCSS-MenuBreak="spm_MenuBreak"
                            MenuCSS-MenuContainer="spm_MenuContainer" menucss-menudefaultitem="spm_DefaultItem"
                            menucss-menudefaultitemhighlight="spm_DefaultItemHighlight" MenuCSS-MenuIcon="spm_MenuIcon"
                            MenuCSS-MenuItem="spm_MenuItem" MenuCSS-MenuItemSel="spm_MenuItemSel" MenuCSS-RootMenuArrow="spm_RootMenuArrow"
                            MenuCSS-SubMenu="spm_SubMenu" MenuCSSPlaceHolderControl="spMenuStyle" MenuEffects-MenuTransition="None"
                            MenuEffects-MenuTransitionStyle="None" MenuEffects-MouseOutHideDelay="500" MenuEffects-MouseOverDisplay="None"
                            MenuEffects-MouseOverExpand="True" MenuItemHeight="23" Moveable="False" parenttabid="0"
                            RootArrow="False" SelectedBorderColor="#cccccc" SelectedColor="transparent" SelectedForeColor="transparent"
                            ShadowColor="transparent" ToolTip="" Visible="True">
                        </rbfwebui:SolpartNavigation>
                    </td>
                    <td align="right" class="navigationBar navigationNonSelected" nowrap="nowrap" width="99%">
                        <img alt="" border="0" height="1" src="<%=LayoutManager.WebPath%>/CustomMenuSolPart/images/spacer.gif"
                            width="20" /><span class="dateCurrent"><%=System.DateTime.Now.ToLongDateString()%></span><span
                                class="dateLastVisit"><%=(userProfile["user_last_visit"] != null)? "<br>Last visit was "+DateTime.Parse(userProfile["user_last_visit"].ToString()).ToShortDateString():""%></span></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="bc_Row">
            <rbfwebui:BreadCrumbs ID="Breadcrumbs1" runat="server" LinkCSSClass="bc_Link" TextCSSClass="bc_Text" />
        </td>
    </tr>
</table>
<!-- END Portal BANNER -->
