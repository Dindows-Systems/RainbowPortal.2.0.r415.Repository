<%@ Control %>
<%@ Register TagPrefix="head" TagName="Banner" Src="DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="pane" TagName="DesktopThreePanes" Src="DesktopThreePanes.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="DesktopFooter.ascx" %>
<div class="rb_DefaultLayoutDiv">
                <div class="rb_DefaultPortalHeader" valign="top">
                    <head:Banner id="Banner" runat="server" SelectedTabIndex="0"></head:Banner>
                </div>
				<div>
					<pane:DesktopThreePanes id="DesktopThreePanes1" runat="server"></pane:DesktopThreePanes>
				</div>
                <div class="rb_DefaultPortalFooter">
                    <foot:Footer id="Footer" runat="server"></foot:Footer>
                </div>
</div>
