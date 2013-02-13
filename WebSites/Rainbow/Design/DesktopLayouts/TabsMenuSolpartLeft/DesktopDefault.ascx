<%@ Control %>
<%@ Register TagPrefix="pane" TagName="DesktopThreePanes" Src="DesktopThreePanes.ascx" %>
<%@ Register TagPrefix="head" TagName="Banner" Src="DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="DesktopFooter.ascx" %>
<div class="rb_DefaultLayoutDiv">
    <table class="rb_DefaultLayoutTable" cellspacing="0" cellpadding="0" id="Table1" width="100%">
        <tbody>
            <tr valign="top">
                <td class="rb_DefaultPortalHeader" valign="top">
                    <head:Banner id="Banner" SelectedTabIndex="0" runat="server"></head:Banner>
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
