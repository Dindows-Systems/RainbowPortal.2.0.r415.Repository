<%@ control language="c#" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<script runat="server">

    private void Page_Load(object sender, System.EventArgs e)
    {
        PortalHeaderMenu.DataBind();
        PortalTitle.DataBind();
        PortalImage.DataBind();
        PortalTabs.DataBind();
        PortalSubTabs.DataBind();
    }

</script>

<!-- Portal BANNER -->
<table id="Table1" border="0" cellpadding="0" cellspacing="0" class="HeadBg" width="100%">
    <tbody>
        <tr>
            <td class="DefaultBanner1" colspan="2" height="1">
                <asp:image id="IMAGE1" runat="server" imageurl="~/design/Themes/default/img/shim.gif" /></td>
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
                            <!--headerimage=/_rainbow/images/logo-default.gif-->
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
                <asp:image id="IMAGE2" runat="server" imageurl="~/design/Themes/default/img/shim.gif" /></td>
        </tr>
        <tr>
            <td class="DefaultBanner2" colspan="2" height="1">
                <asp:image id="IMAGE3" runat="server" imageurl="~/design/Themes/default/img/shim.gif" /></td>
        </tr>
        <tr>
            <td class="DefaultTD" colspan="2">
                <rbfwebui:desktopnavigation id="PortalTabs" runat="server" enableviewstate="false" horizontalalign="left"
                    repeatdirection="horizontal">
                    <itemstyle cssclass="Tabs" />
                    <itemtemplate>
                        <a href="<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl(((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>">
                            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
                        </a>
                    </itemtemplate>
                    <selecteditemstyle cssclass="SelectedTabs" />
                    <selecteditemtemplate>
                        <a href="<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl(((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>">
                            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
                        </a>
                    </selecteditemtemplate>
                </rbfwebui:desktopnavigation>
            </td>
        </tr>
        <tr>
            <td class="DefaultTDSub" colspan="2">
                <rbfwebui:desktopnavigation id="PortalSubTabs" runat="server" bind="BindOptionSubtabSibling"
                    enableviewstate="false" horizontalalign="left" repeatdirection="horizontal">
                    <itemstyle cssclass="SubTabs" />
                    <itemtemplate>
                        <a href="<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl(((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>">
                            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
                        </a>
                    </itemtemplate>
                    <selecteditemstyle cssclass="SelectedSubTabs" />
                    <selecteditemtemplate>
                        <a href="<%#Rainbow.Framework.HttpUrlBuilder.BuildUrl(((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageID)%>">
                            <%# ((Rainbow.Framework.Site.Configuration.PageStripDetails) Container.DataItem).PageName %>
                        </a>
                    </selecteditemtemplate>
                </rbfwebui:desktopnavigation>
            </td>
        </tr>
        <tr>
            <td class="DefaultBanner2" colspan="2" height="1">
                <asp:image id="IMAGE4" runat="server" imageurl="~/design/Themes/default/img/shim.gif" /></td>
        </tr>
    </tbody>
</table>
<!-- END Portal BANNER -->
