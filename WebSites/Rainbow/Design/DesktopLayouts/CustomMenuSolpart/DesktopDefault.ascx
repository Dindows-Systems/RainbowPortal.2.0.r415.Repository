<%@ Register TagPrefix="head" TagName="Banner" Src="DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="pane" TagName="DesktopThreePanes" Src="DesktopThreePanes.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="DesktopFooter.ascx" %>
<%@ Control %>
<div class="rb_DefaultLayoutDiv">
<table id="Table1" class="rb_DefaultLayoutTable">
   <TR vAlign="top">
        <td class="rb_DefaultPortalHeader" valign="top">
            <head:Banner id="Banner" runat="server" SelectedTabIndex="0"></head:Banner>
        </td>
    </tr>
    <TR>
        <td>
            <pane:DesktopThreePanes id="ThreePanes" runat="server"></pane:DesktopThreePanes>
        </td>
    </tr>
	<tr>
	<td class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer>
	</td>
	</tr>
</TABLE>
</div>