<%@ control language="c#" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }
</script>
<rbfwebui:desktoppanes id="ThreePanes" runat="server" cellpadding="4" cellspacing="0"
    showfirstseparator="False" showlastseparator="False">
    <contentpanetemplate>
        <p class="Normal"><rbfwebui:label id="Label1" runat="server" textkey="IMAGEMENU_INSTRUCTIONS">This is a sample use for image menus. You can select ahother image for this tab changing the default "Custom Image Menu" in PageLayout settings.</rbfwebui:label></p>
    </contentpanetemplate>
    <contentpanestyle cssclass="ContentPane" horizontalalign="left" verticalalign="Top" />
    <rightpanestyle cssclass="RightPane" verticalalign="Top" />
    <leftpanestyle cssclass="LeftPane" verticalalign="Top" width="1px" />
</rbfwebui:desktoppanes>
