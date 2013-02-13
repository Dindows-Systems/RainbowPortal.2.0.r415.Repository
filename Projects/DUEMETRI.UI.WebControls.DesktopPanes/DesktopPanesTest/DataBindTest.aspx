<%@ Page language="c#" Codebehind="DataBindTest.aspx.cs" AutoEventWireup="false" Inherits="DektopPanesTest.WebForm1" %>
<%@ Register TagPrefix="CC2" Namespace="DUEMETRI.UI.WebControls" Assembly="DUEMETRI.UI.WebControls.Panes" %>
<form runat="server">
	<P>DataBind (three)</P>
	<P>
		<CC2:DESKTOPPANES id="ThreePanes" runat="server" cellspacing="0" Cellpadding="4" ShowFirstSeparator="False" ShowLastSeparator="False" DESIGNTIMEDRAGDROP="2" BorderStyle="Solid">
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES>
	</P>
	<P>DataBind (left + content)</P>
	<P>
		<CC2:DESKTOPPANES id="DESKTOPPANES1" runat="server" BorderStyle="Solid" DESIGNTIMEDRAGDROP="2" ShowLastSeparator="False" ShowFirstSeparator="False" Cellpadding="4" cellspacing="0">
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES></P>
	<P>DataBind (content + right)</P>
	<P>
		<CC2:DESKTOPPANES id="DESKTOPPANES2" runat="server" BorderStyle="Solid" DESIGNTIMEDRAGDROP="2" ShowLastSeparator="False" ShowFirstSeparator="False" Cellpadding="4" cellspacing="0">
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES></P>
	<P>&nbsp;</P>
	<P>
	</P>
</form>
