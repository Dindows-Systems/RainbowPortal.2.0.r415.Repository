<%@ Register TagPrefix="CC2" Namespace="DUEMETRI.UI.WebControls" Assembly="DUEMETRI.UI.WebControls.Panes" %>
<%@ Register TagPrefix="cc1" Namespace="DUEMETRI.UI.WebControls.HWMenu" Assembly="DUEMETRI.UI.WebControls.HWMenu" %>
<%@ Page language="c#" Codebehind="EmptyMenuTakesNoSpace.aspx.cs" AutoEventWireup="false" Inherits="DektopPanesTest.EmptyMenuTakesNoSpace" %>
<form runat="server" ID="Form1">
	<P>DataBind (three)</P>
	<P>
		<CC2:DESKTOPPANES id="ThreePanes" runat="server" cellspacing="0" Cellpadding="4" ShowFirstSeparator="False" ShowLastSeparator="False" DESIGNTIMEDRAGDROP="2" BorderStyle="Solid">
			<LeftPaneTemplate>
				<cc1:Menu id="Menu1" runat="server" Horizontal="False">
					<ArrowImageDown Width="10px" Height="5px" ImageUrl="tridown.gif"></ArrowImageDown>
					<ControlSubStyle ForeColor="Black" BorderColor="Black" BackColor="White"></ControlSubStyle>
					<ControlHiStyle ForeColor="White" BackColor="Black"></ControlHiStyle>
					<ControlHiSubStyle ForeColor="White" BackColor="Black"></ControlHiSubStyle>
					<ArrowImage Width="5px" Height="10px" ImageUrl="tri.gif"></ArrowImage>
					<ArrowImageLeft Width="5px" Height="10px" ImageUrl="trileft.gif"></ArrowImageLeft>
				</cc1:Menu>
			</LeftPaneTemplate>
			<ContentPaneStyle VerticalAlign="Top"></ContentPaneStyle>
			<RightPaneStyle Width="230px" VerticalAlign="Top"></RightPaneStyle>
			<VerticalSeparatorTemplate>
|
</VerticalSeparatorTemplate>
			<LeftPaneStyle Width="170px" VerticalAlign="Top"></LeftPaneStyle>
		</CC2:DESKTOPPANES>
	</P>
</form>
