<%@ Page language="c#" Codebehind="ContentTest.aspx.cs" AutoEventWireup="false" Inherits="DektopPanesTest.ContentTest" %>
<%@ Register TagPrefix="CC2" Namespace="DUEMETRI.UI.WebControls" Assembly="DUEMETRI.UI.WebControls.Panes" %>
<form runat="server">
	<P>Full</P>
	<P>
		<CC2:DESKTOPPANES id="ThreePanes" runat="server" cellspacing="0" Cellpadding="4" ShowFirstSeparator="False" ShowLastSeparator="False" DESIGNTIMEDRAGDROP="2" BorderStyle="Solid">
			<RightPaneTemplate>
RightPane 
</RightPaneTemplate>
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneTemplate>
ContentPane 
</ContentPaneTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<LeftPaneTemplate>
LeftPane
</LeftPaneTemplate>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES></P>
	<P>No left</P>
	<P>
		<CC2:DESKTOPPANES id="DESKTOPPANES1" runat="server" cellspacing="0" Cellpadding="4" ShowFirstSeparator="False" ShowLastSeparator="False" DESIGNTIMEDRAGDROP="9" BorderStyle="Solid">
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneTemplate>
ContentPane 
</ContentPaneTemplate>
			<RightPaneTemplate>
RightPane 
</RightPaneTemplate>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES></P>
	<P>No right</P>
	<P>
		<CC2:DESKTOPPANES id="DESKTOPPANES2" runat="server" ShowLastSeparator="False" ShowFirstSeparator="False" Cellpadding="4" cellspacing="0" BorderStyle="Solid">
			<LeftPaneTemplate>
LeftPane
</LeftPaneTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneTemplate>
ContentPane 
</ContentPaneTemplate>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES></P>
</form>
