<%@ control autoeventwireup="false" codefile="FlashModule.ascx.cs" inherits="Rainbow.Content.Web.Modules.FlashModule"
    language="c#" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>
<rbfwebui:flashmovie id="FlashMovie1" runat="server" allowflashautoinstall="False"
    autoloop="False" autoplay="True" flashoutputtype="FlashOnly" htmlalignment="none"
    moviebgcolor="#000000" movieheight="100" moviequality="High" moviescale="ShowAll"
    moviewidth="340" showmenu="False" windowmode="Opaque">
</rbfwebui:flashmovie>
<rbfwebui:label id="ErrorLabel" runat="server" cssclass="Error" visible="False"></rbfwebui:label>
