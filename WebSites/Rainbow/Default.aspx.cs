using System;
using System.Web.UI;

namespace Rainbow
{
    /// <summary>
    /// The Default.aspx page simply tests 
    /// the browser type and redirects either to
    /// the DesktopDefault or MobileDefault pages, 
    /// depending on the device type.
    /// </summary>
    public class Default : Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser["IsMobileDevice"] == "true")
            {
                Server.Transfer("MobileDefault.aspx", false);
                //Response.Redirect("MobileDefault.aspx");
            }
            else
            {
                Server.Transfer("DesktopDefault.aspx", true);
                // Response.Redirect("DesktopDefault.aspx");
            }
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(this.Page_Load);

            base.OnInit(e);
        }

        #endregion
    }
}