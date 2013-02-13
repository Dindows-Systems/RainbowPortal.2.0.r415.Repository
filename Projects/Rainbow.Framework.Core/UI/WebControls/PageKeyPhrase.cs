using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using Rainbow.Framework.Site.Configuration;

namespace Rainbow.Framework.Web.UI.WebControls
{
    /// <summary>
    /// Created By john.mandia@whitelightsolutions.com - This control is used by the PageKeyPhrase Module.
    /// </summary>
    public class PageKeyPhrase : Label
    {
        // Obtain PortalSettings from Current Context
        private PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
        private string _tabKeyPhrase;
        private string currentLanguage;

        /// <summary>
        /// Stores current Tab Key Phrase
        /// </summary>
        /// <value>The tab key phrase.</value>
        public string TabKeyPhrase
        {
            get
            {
                currentLanguage = "TabKeyPhrase_" + portalSettings.PortalContentLanguage.Name.ToString();
                if (portalSettings.PortalContentLanguage != CultureInfo.InvariantCulture
                    && portalSettings.ActivePage.CustomSettings[currentLanguage] != null)
                {
                    _tabKeyPhrase =
                        (string) ViewState["TabKeyPhrase_" + portalSettings.PortalContentLanguage.Name.ToString()];
                    if (_tabKeyPhrase != null)
                        return _tabKeyPhrase;
                    else
                    {
                        // Try to get this tab's key phrase
                        _tabKeyPhrase = portalSettings.ActivePage.CustomSettings[currentLanguage].ToString();

                        if (_tabKeyPhrase == string.Empty && portalSettings.CustomSettings != null)
                        {
                            _tabKeyPhrase = portalSettings.ActivePage.CustomSettings["TabKeyPhrase"].ToString();

                            if (_tabKeyPhrase == string.Empty)
                                _tabKeyPhrase = portalSettings.CustomSettings["SITESETTINGS_PAGE_KEY_PHRASE"].ToString();
                        }

                        return _tabKeyPhrase;
                    }
                }
                else
                {
                    _tabKeyPhrase = (string) ViewState["TabKeyPhrase"];
                    if (_tabKeyPhrase != null)
                        return _tabKeyPhrase;
                    else
                    {
                        if (portalSettings.ActivePage.CustomSettings["TabKeyPhrase"] != null)
                        {
                            // Try to get this tab's key phrase
                            _tabKeyPhrase = portalSettings.ActivePage.CustomSettings["TabKeyPhrase"].ToString();

                            if (_tabKeyPhrase == string.Empty && portalSettings.CustomSettings != null)
                            {
                                _tabKeyPhrase = portalSettings.CustomSettings["SITESETTINGS_PAGE_KEY_PHRASE"].ToString();
                            }

                            return _tabKeyPhrase;
                        }
                        return string.Empty;
                    }
                }
            }
            set
            {
                if (portalSettings.PortalContentLanguage != CultureInfo.InvariantCulture)
                {
                    ViewState["TabKeyPhrase_" + portalSettings.PortalContentLanguage.Name.ToString()] = value;
                }
                else
                {
                    ViewState["TabKeyPhrase"] = value;
                }
            }
        }

        /// <summary>
        /// Load event handler
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            Text = TabKeyPhrase;

            base.DataBind();
        }
    }
}