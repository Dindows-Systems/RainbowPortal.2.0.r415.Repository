<%@ Control Language="c#" AutoEventWireup="false" Inherits="Rainbow.Framework.Design.PictureItem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register Assembly="Rainbow.Framework.Core" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<%@ Register Assembly="Rainbow.Framework.Web.UI.WebControls" Namespace="Rainbow.Framework.Web.UI.WebControls" TagPrefix="rbfwebui" %>
<table width="100%" height="100%">
 <tr><td><br></td></tr>
	<tr>
		<td valign="top" width="1%">
			<a href='<%# Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/Pictures/PictureView.aspx","ItemID=" + GetMetadata("ItemID") + "&mid=" + GetMetadata("ModuleID") + "&wversion=" + GetMetadata("WVersion"))%>' runat="server">
				<img border='0' width='<%#GetMetadata("ThumbnailWidth")%>' height='<%#GetMetadata("ThumbnailHeight")%>' alt='<%#GetMetadata("Caption")%>' src='<%# GetMetadata("AlbumPath") + "/" + GetMetadata("ThumbnailFilename")%>' />
			</a>
		</td>
		<td align="left" valign="middle" class="Normal">
			
			<rbfwebui:HyperLink id="editLink" ImageUrl='<%# GetCurrentImageFromTheme("Buttons_Edit", "Edit.gif") %>' NavigateUrl='<%# Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/Pictures/PicturesEdit.aspx","ItemID=" + GetMetadata("ItemID") + "&mid=" + GetMetadata("ModuleID"))%>' Visible='<%# GetMetadata("IsEditable") == "True"%>' runat="server" />
		</td>
	</tr>
	<tr>
		<td valign="top" align="center" class="PictureTitle" valign="top">
			<%#GetMetadata("ShortDescription")%><hr align=center width=30%>
		</td>
	</tr>
</table>
