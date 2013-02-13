<%@ Control Language="c#" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }
</script>
<rbfwebui:DESKTOPPANES id="ThreePanes" height="100%" ShowLogon="False" ShowLastSeparator="False" ShowFirstSeparator="False" Cellpadding="0" cellspacing="0" runat="server">
    <LEFTPANESTYLE cssclass="LeftPane" width="190px" verticalalign="Top"></LEFTPANESTYLE>
    <CONTENTPANESTYLE cssclass="ContentPane" verticalalign="Top" horizontalalign="left"></CONTENTPANESTYLE>
    <RIGHTPANESTYLE cssclass="RightPane" verticalalign="Top"></RIGHTPANESTYLE>
</rbfwebui:DESKTOPPANES>
