<%@ Page language="c#" Codebehind="IntegrationLeftTest.aspx.cs" AutoEventWireup="false" Inherits="DektopPanesTest.IntegrationLeftTest" %>
<%@ Register TagPrefix="cc1" Namespace="DUEMETRI.UI.WebControls.HWMenu" Assembly="DUEMETRI.UI.WebControls.HWMenu" %>
<%@ Register TagPrefix="CC2" Namespace="DUEMETRI.UI.WebControls" Assembly="DUEMETRI.UI.WebControls.Panes" %>
<form runat="server">
	<P>DataBind (three)</P>
	<P>
		<CC2:DESKTOPPANES id="ThreePanes" runat="server" cellspacing="0" Cellpadding="4" ShowFirstSeparator="False" ShowLastSeparator="False" DESIGNTIMEDRAGDROP="2" BorderStyle="Solid">
			<LeftPaneTemplate>
				<cc1:Menu id="Menu1" runat="server" Horizontal="False">
					<ArrowImageDown Width="10px" Height="5px" ImageUrl="tridown.gif"></ArrowImageDown>
					<Childs>
						<cc1:MenuTreeNode Height="20px" Text="Untitled menu" Width="100px" ID="menuTreeNode1">
							<Childs>
								<cc1:MenuTreeNode Height="20px" Text="Untitled menu" Width="100px" ID="menuTreeNode11"></cc1:MenuTreeNode>
							</Childs>
						</cc1:MenuTreeNode>
						<cc1:MenuTreeNode Height="20px" Text="Untitled menu" Width="100px" ID="menuTreeNode2"></cc1:MenuTreeNode>
						<cc1:MenuTreeNode Height="20px" Text="Untitled menu" Width="100px" ID="menuTreeNode3"></cc1:MenuTreeNode>
					</Childs>
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
