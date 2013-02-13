<%@ Control %>
<%@ Register TagPrefix="head" TagName="Banner" Src="DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="pane" TagName="DesktopThreePanes" Src="DesktopThreePanes.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="DesktopFooter.ascx" %>
<div class="rb_DefaultLayoutDiv">
    <table class="rb_DefaultLayoutTable" id="Table1" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr valign="top">
                <td class="rb_DefaultPortalHeader" valign="top">
                    <head:Banner id="Banner" runat="server" SelectedTabIndex="0"></head:Banner>
                </td>
            </tr>
            <tr>
                <td>
                    <pane:DesktopThreePanes id="DesktopThreePanes1" runat="server"></pane:DesktopThreePanes>
                </td>
            </tr>
            <tr>
                <td class="rb_DefaultPortalFooter">
                    <foot:Footer id="Footer" runat="server"></foot:Footer>
                </td>
            </tr>
        </tbody>
    </table>
</div>
