<%@ control autoeventwireup="false" codefile="SiteSettings.ascx.cs" inherits="Rainbow.Content.Web.Modules.SiteSettingsmod"
    language="c#" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>
<table border="0">
    <tr>
        <td class="Subhead" valign="top" width="140">
            <rbfwebui:localize id="site_title" runat="server" text="Site Title" textkey="SITESETTINGS_SITE_TITLE">
            </rbfwebui:localize>
        </td>
        <td class="NormalTextBox" colspan="2">
            <asp:textbox id="siteName" runat="server" cssclass="NormalTextBox" width="240"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td class="Subhead" valign="top" width="140">
            <rbfwebui:localize id="site_path" runat="server" text="Site Path" textkey="SITESETTINGS_SITE_PATH">
            </rbfwebui:localize>
        </td>
        <td class="Normal" colspan="2">
            <rbfwebui:label id="sitePath" runat="server" width="240"></rbfwebui:label>
        </td>
    </tr>
</table>
<rbfwebui:settingstable id="EditTable" runat="server" />
<rbfwebui:linkbutton id="updateButton" runat="server" cssclass="CommandButton" text="Apply Changes"
    textkey="APPLY">
</rbfwebui:linkbutton>

<script language="javascript" type="text/javascript">
	var tpg1 = new xTabPanelGroup('tpg1', 700, 350, 50, 'tabPanel', 'tabGroup', 'tabDefault', 'tabSelected');
	var tabgroupd = xGetElementById('tpg1');
	var pareTbl = xGetElementById(tabgroupd.id);
	//pareTbl.style.posHeight= (xHeight(tabgroupd)+50);
	pareTbl.style["height"] = 380;
	pareTbl.style["overflow"] = 'hidden';
	//xResizeTo(pareTbl, xWidth(pareTbl), 380);
//	alert(xHeight(tabgroupd));
</script>

