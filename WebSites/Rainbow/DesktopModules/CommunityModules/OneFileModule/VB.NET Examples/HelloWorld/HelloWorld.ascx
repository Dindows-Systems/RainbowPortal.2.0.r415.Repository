<%@ Control Language="vb" inherits="Rainbow.Content.Web.Modules.OneFileModule" %>
<%@ Register TagPrefix="rbfwebui" Namespace="Rainbow.Framework.Web.UI.WebControls" assembly="Rainbow.Framework" %>

<script language="vb" runat="server">
    Sub Page_Load()
        InitSettings(SettingsType.Str)

        If SettingsExists Then
            lblFirstName.Text = GetSetting("FirstName")
            lblLastName.Text = GetSetting("LastName")
        End If
    End Sub
</script>

<rbfwebui:DesktopModuleTitle EditText="Edit" EditUrl="~/DesktopModules/CoreModules/Admin/PropertyPage.aspx"
    PropertiesText="PROPERTIES" PropertiesUrl="~/DesktopModules/CoreModules/Admin/PropertyPage.aspx"
    runat="server" ID="ModuleTitle" />
Hello Rainbow World from:
<rbfwebui:label ID="lblFirstName" runat="server" />
<rbfwebui:label ID="lblLastName" runat="server" />
