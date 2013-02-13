<%@ Page language="c#" Codebehind="EventFiring.aspx.cs" AutoEventWireup="false" Inherits="DektopPanesTest.EventFiring" %>
<%@ Register TagPrefix="uc1" TagName="testcontrol" Src="testcontrol.ascx" %>
<%@ Register TagPrefix="CC2" Namespace="DUEMETRI.UI.WebControls" Assembly="DUEMETRI.UI.WebControls.Panes" %>
<form runat="server">
	<P>Full</P>
	<P><CC2:DESKTOPPANES id="ThreePanes" runat="server" BorderStyle="Solid" DESIGNTIMEDRAGDROP="2" ShowLastSeparator="False" ShowFirstSeparator="False" Cellpadding="4" cellspacing="0">
			<LeftPaneTemplate>
LeftPane <uc1:testcontrol id="Testcontrol2" runat="server"></uc1:testcontrol>
</LeftPaneTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneTemplate>
ContentPane <uc1:testcontrol id="Testcontrol3" runat="server"></uc1:testcontrol>
</ContentPaneTemplate>
			<RightPaneTemplate>
RightPane <uc1:testcontrol id="Testcontrol4" runat="server"></uc1:testcontrol>
</RightPaneTemplate>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES></P>
	<P>
		<uc1:testcontrol id="Testcontrol1" runat="server"></uc1:testcontrol></P>
</form>
