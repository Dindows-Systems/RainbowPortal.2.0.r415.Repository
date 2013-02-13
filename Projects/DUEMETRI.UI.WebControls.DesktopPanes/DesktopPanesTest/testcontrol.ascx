<%@ Control Language="c#" AutoEventWireup="false" Codebehind="testcontrol.ascx.cs" Inherits="DektopPanesTest.testcontrol" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<P>
	<asp:Button id="Button1" Text="Button" runat="server"></asp:Button></P>
<P>
	<asp:datalist id="defsList" runat="server">
		<ItemTemplate>
			&nbsp;
			<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit this item"></asp:ImageButton>&nbsp;
			<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>' CssClass="Normal">
			</asp:Label>
		</ItemTemplate>
	</asp:datalist></P>
