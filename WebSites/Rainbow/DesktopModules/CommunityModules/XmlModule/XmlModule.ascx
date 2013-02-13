<%@ register assembly="Rainbow.Framework" namespace="Rainbow.Framework.Web.UI.WebControls"
    tagprefix="rbfwebui" %>
<%@ control autoeventwireup="false" codefile="XmlModule.ascx.cs" inherits="Rainbow.Content.Web.Modules.XmlModule"
    language="c#" %>
<rbfwebui:desktopmoduletitle id="ModuleTitle" runat="server" propertiestext="PROPERTIES"
    propertiesurl="~/DesktopModules/CoreModules/Admin/PropertyPage.aspx">
</rbfwebui:desktopmoduletitle>
<span class="Normal"><asp:xml id="xml1" runat="server"></asp:xml></span>