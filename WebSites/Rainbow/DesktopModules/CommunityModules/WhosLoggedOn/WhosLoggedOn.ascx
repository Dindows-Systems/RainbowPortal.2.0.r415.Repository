<%@ control autoeventwireup="false" codefile="WhosLoggedOn.ascx.cs" inherits="Rainbow.Content.Web.Modules.WhosLoggedOn"
    language="c#" %>
<rbfwebui:localize id="Literal1" runat="server" text="There are currently:" textkey="WHOS_LOGGED_ON_1">
</rbfwebui:localize><br />
<b>
    <rbfwebui:label id="LabelAnonUsersCount" runat="server">xx</rbfwebui:label></b>
<rbfwebui:localize id="Literal2" runat="server" text="anonymous users online" textkey="WHOS_LOGGED_ON_2">
</rbfwebui:localize><br />
<b>
    <rbfwebui:label id="LabelRegUsersOnlineCount" runat="server">xx</rbfwebui:label></b>
<rbfwebui:localize id="Literal3" runat="server" text=" registered users online: "
    textkey="WHOS_LOGGED_ON_3">
</rbfwebui:localize>
<b>
    <rbfwebui:label id="LabelRegUserNames" runat="server">users list</rbfwebui:label></b>
