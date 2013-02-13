using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Rainbow.Framework.Security;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Site.Configuration;

namespace Rainbow.Framework.Web.UI.WebControls
{
    [History("Manu", "2003/06/06", "Removed commented legacy code.")]
    [History("Manu", "2003/05/28", "Remove SignIn and LanguageSwitcher auto-placement. Place it as other modules.")]
    [History("Jes1111", "2003/03/08", "Added SignIn auto-placement")]
    [History("Jes1111", "2003/04/23", "Added LanguageSwitcher auto-placement")]
    [History("Jes1111", "2003/04/24", "Improved cache behaviour for CacheTime=-1")]
    public class DesktopPanes : DUEMETRI.UI.WebControls.DesktopPanes
    {
        /// <summary>
        /// This method determines the tab index of the currently
        /// requested portal view, and then dynamically populate the left,
        /// center and right hand sections of the portal tab.
        /// </summary>
        protected override void InitializeDataSource()
        {
            base.InitializeDataSource();

            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            // Dynamically Populate the Left, Center and Right pane sections of the portal page
            if (portalSettings.ActivePage.Modules.Count > 0)
            {
                // Loop through each entry in the configuration system for this tab
                foreach (ModuleSettings _moduleSettings in portalSettings.ActivePage.Modules)
                {
                    if (!_moduleSettings.Cacheable) _moduleSettings.CacheTime = -1; // Disable cache

                    // NEW MODULE_VIEW PERMISSIONS ADDED
                    // Ensure that the visiting user has access to view the current module
                    if (PortalSecurity.IsInRoles(_moduleSettings.AuthorizedViewRoles) == true)
                    {
                        ArrayList arrayData;

                        switch (_moduleSettings.PaneName.ToLower())
                        {
                            case "leftpane":
                                arrayData = DataSource[IDX_LEFT_PANE_DATA];
                                break;
                            case "contentpane":
                                arrayData = DataSource[IDX_CONTENT_PANE_DATA];
                                break;
                            case "rightpane":
                                arrayData = DataSource[IDX_RIGHT_PANE_DATA];
                                break;
                            default:
                                arrayData = DataSource[IDX_CONTENT_PANE_DATA];
                                break;
                        }

                        // If no caching is specified, create the user control instance and dynamically
                        // inject it into the page.  Otherwise, create a cached module instance that
                        // may or may not optionally inject the module into the tree

                        //Cache. If == 0 then override with default cache in web.config
// jes1111
//						if(ConfigurationSettings.AppSettings["ModuleOverrideCache"] != null 
//							&& !_moduleSettings.Admin
//							&& _moduleSettings.CacheTime == 0)
                        if (!_moduleSettings.Admin && _moduleSettings.CacheTime == 0)
                        {
                            //jes1111 - int mCache = Int32.Parse(ConfigurationSettings.AppSettings["ModuleOverrideCache"]);
                            int mCache = Config.ModuleOverrideCache;
                            if (mCache > 0)
                                _moduleSettings.CacheTime = mCache;
                        }

                        // Change 28/Feb/2003 Jeremy Esland - added security settings to condition test so that a user who has 
                        // edit or properties permission will not cause the module output to be cached. 
                        if (
                            ((_moduleSettings.CacheTime) <= 0)
                            || (PortalSecurity.HasEditPermissions(_moduleSettings.ModuleID))
                            || (PortalSecurity.HasPropertiesPermissions(_moduleSettings.ModuleID))
                            || (PortalSecurity.HasAddPermissions(_moduleSettings.ModuleID))
                            || (PortalSecurity.HasDeletePermissions(_moduleSettings.ModuleID))
                            )
                        {
                            try
                            {
                                string portalModuleName =
                                    string.Concat(Path.ApplicationRoot, "/", _moduleSettings.DesktopSrc);
                                PortalModuleControl portalModule =
                                    (PortalModuleControl) Page.LoadControl(portalModuleName);

                                portalModule.PortalID = portalSettings.PortalID;
                                portalModule.ModuleConfiguration = _moduleSettings;

                                //TODO: This is not the best place: should be done early
                                if (portalModule.Cultures == string.Empty ||
                                    (portalModule.Cultures + ";").IndexOf(portalSettings.PortalContentLanguage.Name +
                                                                          ";") >= 0)
                                {
                                    arrayData.Add(portalModule);
                                }
                            }
                            catch (Exception ex)
                            {
                                //ErrorHandler.HandleException("DesktopPanes: Unable to load control '" + _moduleSettings.DesktopSrc + "'!", ex);
                                ErrorHandler.Publish(LogLevel.Error,
                                                     "DesktopPanes: Unable to load control '" +
                                                     _moduleSettings.DesktopSrc + "'!", ex); // jes1111
                                if (PortalSecurity.IsInRoles("Admins"))
                                {
                                    arrayData.Add(
                                        new LiteralControl("<br><span class=NormalRed>" + "Unable to load control '" +
                                                           _moduleSettings.DesktopSrc +
                                                           "'! (Full Error Logged)<br />Error Message: " +
                                                           ex.Message.ToString()));
                                }
                                else
                                {
                                    arrayData.Add(
                                        new LiteralControl("<br><span class=NormalRed>" + "Unable to load control '" +
                                                           _moduleSettings.DesktopSrc + "'!"));
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                using (CachedPortalModuleControl portalModule = new CachedPortalModuleControl())
                                {
                                    portalModule.PortalID = portalSettings.PortalID;
                                    portalModule.ModuleConfiguration = _moduleSettings;

                                    arrayData.Add(portalModule);
                                }
                            }
                            catch (Exception ex)
                            {
                                //ErrorHandler.HandleException("DesktopPanes: Unable to load cached control '" + _moduleSettings.DesktopSrc + "'!", ex);
                                ErrorHandler.Publish(LogLevel.Error,
                                                     "DesktopPanes: Unable to load cached control '" +
                                                     _moduleSettings.DesktopSrc + "'!", ex);
                                if (PortalSecurity.IsInRoles("Admins"))
                                {
                                    arrayData.Add(
                                        new LiteralControl("<br><span class=NormalRed>" +
                                                           "Unable to load cached control '" +
                                                           _moduleSettings.DesktopSrc +
                                                           "'! (Full Error Logged)<br />Error Message: " +
                                                           ex.Message.ToString()));
                                }
                                else
                                {
                                    arrayData.Add(
                                        new LiteralControl("<br><span class=NormalRed>" +
                                                           "Unable to load cached control '" +
                                                           _moduleSettings.DesktopSrc + "'!"));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}