using System;
using System.Threading;
using System.Web.UI.WebControls;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Web.UI.WebControls;
using Rainbow.Framework.Settings;

namespace Rainbow.Content.Web.Modules
{
	/// <summary>
	/// Rainbow Version
	/// </summary>
	public partial class RainbowVersion : PortalModuleControl
	{
		/// <summary>
		/// Handles the Load event of the RainbowVersion control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void RainbowVersion_Load(object sender, EventArgs e)
		{
            VersionLabel.Text = PortalSettings.ProductVersion;
			currentLanguage.Text = Thread.CurrentThread.CurrentCulture.Name;
			currentUILanguage.Text = Thread.CurrentThread.CurrentUICulture.Name;
		}
		/// <summary>
		/// GUID of module (mandatory)
		/// </summary>
		/// <value></value>
		public override Guid GuidID
		{
			get
			{
				return new Guid("{72C6F60A-50C4-4f20-8F89-3E8A27820557}");
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
            this.Load += new EventHandler(this.RainbowVersion_Load);
			base.OnInit(e);
		}
		#endregion
	}
}