<%@ control language="c#" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<script runat="server">

    private void Page_Load(object sender, System.EventArgs e)
    {
        ThreePanes.DataBind();
    }

</script>

<rbfwebui:desktoppanes id="ThreePanes" runat="server" cellpadding="0" cellspacing="0"
    height="100%" showfirstseparator="False" showlastseparator="False" showlogon="False">
    <leftpanetemplate>
        <rbfwebui:menunavigation id="NavigationMenu" runat="server" autobind="True" autoshopdetect="True"
            bind="BindOptionCurrentChilds" borderwidth="0" height="20" horizontal="False"
            imagespath="/design/Themes/default/img" leftpaddng="5" startleft="0" starttop="0"
            toppaddng="1" visible="True" width="185px">
            <controlitemstyle cssclass="MenuItem" />
            <controlsubstyle cssclass="MenuSub" />
            <controlhistyle cssclass="MenuItemHi" />
            <controlhisubstyle cssclass="MenuSubHi" />
            <arrowimage height="9px" imageurl="arrow.gif" width="7px" />
            <arrowimageleft height="9px" imageurl="arrow_left.gif" width="5px" />
            <arrowimagedown height="5px" imageurl="arrow_down.gif" width="10px" /></rbfwebui:menunavigation>
    </leftpanetemplate>
    <leftpanestyle cssclass="LeftPane" verticalalign="Top" width="190px" />
    <contentpanestyle cssclass="ContentPane" horizontalalign="left" verticalalign="Top" />
    <rightpanestyle cssclass="RightPane" verticalalign="Top" />
</rbfwebui:desktoppanes>
