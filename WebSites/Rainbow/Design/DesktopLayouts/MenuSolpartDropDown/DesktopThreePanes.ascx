<%@ control language="c#" %>
<%@ register assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" tagprefix="rbfwebui" %>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }
</script>

<rbfwebui:desktoppanes id="ThreePanes" runat="server" cellpadding="0" cellspacing="2"
    showfirstseparator="False" showlastseparator="False">
    <horizontalseparatortemplate>
        <asp:image id="IMAGE1" runat="server" height="4" imageurl="<%=LayoutManager.WebPath%>/MenuTopSolpart/images/spacer.gif" /></horizontalseparatortemplate>
    <verticalseparatortemplate>
    </verticalseparatortemplate>
    <contentpanestyle cssclass="ContentPane" horizontalalign="left" verticalalign="Top" />
    <rightpanestyle cssclass="RightPane" verticalalign="Top" />
    <leftpanestyle cssclass="LeftPane" verticalalign="Top" />
</rbfwebui:desktoppanes>
