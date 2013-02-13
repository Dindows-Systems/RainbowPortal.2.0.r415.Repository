<%@ Control Language="c#" %>
<script runat="server">

    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }

</script>
<rbfwebui:DESKTOPPANES id="ThreePanes" runat="server" cellspacing="0" Cellpadding="0" ShowFirstSeparator="False" ShowLastSeparator="False" ShowLogon="False" height="100%">
    <LEFTPANESTYLE cssclass="LeftPane" verticalalign="Top" width="200"></LEFTPANESTYLE>
    <CONTENTPANESTYLE cssclass="ContentPane" verticalalign="Top"></CONTENTPANESTYLE>
    <RIGHTPANESTYLE cssclass="RightPane" verticalalign="Top"></RIGHTPANESTYLE>
</rbfwebui:DESKTOPPANES>
