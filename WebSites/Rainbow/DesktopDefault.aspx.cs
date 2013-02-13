using System;
using System.IO;
using Rainbow.Framework;
using Rainbow.Framework.Design;
using Rainbow.Framework.Security;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Web.UI;
using History=Rainbow.Framework.History;

namespace Rainbow
{
    /// <summary>
    /// The DesktopDefault.aspx page is used 
    /// to load and populate each Portal View.
    /// It accomplishes this by reading the layout configuration 
    /// of the portal from the Portal Configuration	system, 
    /// and then using this information to dynamically 
    /// instantiate portal modules (each implemented 
    /// as an ASP.NET User Control), and then inject them into the page.
    /// </summary>
    [History("Ender", "2003/3/31", "Add logentry support the monitoring module written by Paul Yarrow")]
    public partial class DesktopDefault : Page
    {
        #region Web Form Designer generated code

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(this.DesktopDefault_Load);
            base.OnInit(e);
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the DesktopDefault control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DesktopDefault_Load(object sender, EventArgs e)
        {
            // Ensure that the visiting user has access to the current page
            if (PortalSecurity.IsInRoles(portalSettings.ActivePage.AuthorizedRoles) == false)
                PortalSecurity.AccessDenied();
            else
                LoadPage();
        }

        /// <summary>
        /// Loads the page.
        /// </summary>
        private void LoadPage()
        {
            //   Response.Buffer = true;

            const string LAYOUT_BASE_PAGE = "DesktopDefault.ascx";
            string defaultLayoutPath = string.Concat(LayoutManager.WebPath, "/Default/", LAYOUT_BASE_PAGE);

            try
            {
                // Thierry (Tiptopweb), 4 July 2003, all moved to portalSettings
                // new version using custom settings from the Page.cs
                // no need to detect the shop module, add the layout when adding the shop module
                // also useful to switch design for home page or other pages!

                string layoutPath = string.Concat(portalSettings.PortalLayoutPath, LAYOUT_BASE_PAGE);
                LayoutPlaceHolder.Controls.Add(Page.LoadControl(layoutPath));
            }
            catch (System.Web.HttpException ex)
            {
                Rainbow.Framework.ErrorHandler.Publish(Rainbow.Framework.LogLevel.Error, "FileOrDirectoryNotFound", ex);
                LayoutPlaceHolder.Controls.Add(Page.LoadControl(defaultLayoutPath));
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Rainbow.Framework.ErrorHandler.Publish(Rainbow.Framework.LogLevel.Error, "DirectoryNotFound", ex);
                LayoutPlaceHolder.Controls.Add(Page.LoadControl(defaultLayoutPath));
            }
            catch (FileNotFoundException ex)
            {
                Rainbow.Framework.ErrorHandler.Publish(Rainbow.Framework.LogLevel.Error, "FileNotFound", ex);
                LayoutPlaceHolder.Controls.Add(Page.LoadControl(defaultLayoutPath));
            }

             LogEntry();
            //System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(LogEntry()));
            //t.Start();
        }

        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <remarks>
        /// If an error occurs, it doesn't really  matter to the user so we log it and keep going.
        /// </remarks>
        private void LogEntry()
        {
            if (Config.EnableMonitoring)
            {
                // TODO: after next compile, or whenever use MonitoringAction.PageRequest doe "PageRequest"
                try
                {
                    
                    Monitoring.LogEntry((Guid)PortalSettings.CurrentUser.Identity.ProviderUserKey, portalSettings.PortalID,
                                        Convert.ToInt32(PageID), Rainbow.Framework.Monitoring.MonitoringAction.PageRequest, string.Empty);
                }
                catch (System.Data.SqlClient.SqlException sqex)
                {
                    ErrorHandler.Publish(Rainbow.Framework.LogLevel.Debug, "SQL Monitoring LogEntry Error", sqex);
                }
                catch (Exception ex)
                {
                    //
                    ErrorHandler.Publish(Rainbow.Framework.LogLevel.Debug, "Monitoring LogEntry Error", ex);
                }
            }
        }
    }
}