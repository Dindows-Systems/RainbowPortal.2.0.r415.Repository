using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rainbow.Framework.Site.Configuration;
using Path = Rainbow.Framework.Settings.Path;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// Default user control placed on top of each administrative page
    /// </summary>
    [Rainbow.Framework.History("john", "2003/03/15", "Some mods")]
    [Rainbow.Framework.History("manu", "2002/11/18", "Testing attributes")]
	[Rainbow.Framework.History("Jes1111", "2003/03/09", "Retrieve ShowTabs attribute and pass into new portalSettings.ShowTabs property")]
    public abstract class DesktopPortalBanner : UserControl
    {
        /// <summary>
        /// Placeholder for current control
        /// </summary>
        protected PlaceHolder LayoutPlaceHolder;

		// jes1111 
		/// <summary>
		/// 
		/// </summary>
		public bool ShowTabs = true;

        #region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();

            base.OnInit(e);
        }
        private void InitializeComponent() 
        {
			this.Load += new EventHandler(this.DesktopPortalBanner_Load);

		}
		#endregion

		[Rainbow.Framework.History("bja@reedtek.com", "2003/05/09", "Validate the control being brought in")]
        private void DesktopPortalBanner_Load(object sender, EventArgs e)
        {
            
            string LayoutBasePage = "DesktopPortalBanner.ascx";
			
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			
			// jes1111 
			portalSettings.ShowPages = ShowTabs;

			// [START] file path -- bja@reedtek.com
			//
			// Validate that the layout file is present. I have found
			// that sometimes they go away in different releases. So let's check
			//string filepath = portalSettings.PortalLayoutPath + LayoutBasePage;
			string filepath = Path.WebPathCombine(portalSettings.PortalLayoutPath, LayoutBasePage);

			// does it exsists
			if (File.Exists(Server.MapPath(filepath)))
				LayoutPlaceHolder.Controls.Add(Page.LoadControl(filepath));
			else 
			{
				// create an exception
				Exception ex = new Exception("Portal cannot find layout ('" + filepath + "')");
				// go log/handle it
				//ErrorHandler.HandleException(ex);
				Rainbow.Framework.ErrorHandler.Publish(Rainbow.Framework.LogLevel.Error, ex);
			}
			// [END] file path -- bja@reedtek.com
        }
    }
}
