<%@ Control Language="c#" %>
<script runat="server">
    private void Page_Load( object sender, System.EventArgs e ) {
        ThreePanes.DataBind();
    }
</script>

<rbfwebui:DesktopPanes ID="ThreePanes" runat="server" CellPadding="0" CellSpacing="2"
    ShowFirstSeparator="False" ShowLastSeparator="False">
    <HorizontalSeparatorTemplate>
        <asp:Image ID="IMAGE1" runat="server" ImageUrl="<%=LayoutManager.WebPath%>/CustomMenuSolPart/images/spacer.gif"
            Height="4"></asp:Image></HorizontalSeparatorTemplate>
    <VerticalSeparatorTemplate>
    </VerticalSeparatorTemplate>
    <ContentPaneStyle CssClass="ContentPane" HorizontalAlign="left" VerticalAlign="Top">
    </ContentPaneStyle>
    <RightPaneStyle CssClass="RightPane" VerticalAlign="Top"></RightPaneStyle>
    <LeftPaneStyle CssClass="LeftPane" VerticalAlign="Top" Width="1px"></LeftPaneStyle>
</rbfwebui:DesktopPanes>
