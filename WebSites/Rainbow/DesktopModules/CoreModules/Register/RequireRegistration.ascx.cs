using System;
using System.Web.UI;

namespace Rainbow.Admin
{
    public partial class RequireRegistration : UserControl
    {
        #region Web Form Designer generated code

        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            SignInHyperLink.NavigateUrl = Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/CoreModules/Admin/Logon.aspx");
            RegisterHyperlink.NavigateUrl =
                Rainbow.Framework.HttpUrlBuilder.BuildUrl("~/DesktopModules/CoreModules/Register/Register.aspx");

            base.OnInit(e);
        }

        #endregion
    }
}