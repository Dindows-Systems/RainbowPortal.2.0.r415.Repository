using System.Web;
using ActiveUp.WebControls.HtmlTextBox;
using Rainbow.Framework.Site.Configuration;

namespace Rainbow.Framework.Web.UI.WebControls
{
    /// <summary>
    /// HtmlTextBox is a wrapper for ActiveUp.HtmlTextBox.HtmlTextBox.
    /// </summary>
    public class HtmlTextBox : Editor, IHtmlEditor
    {
        private string _imageFolder = string.Empty;

        /// <summary>
        /// Control Image Folder
        /// </summary>
        /// <value></value>
        public string ImageFolder
        {
            get
            {
                if (_imageFolder == string.Empty)
                {
                    PortalSettings pS = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
                    if (pS.CustomSettings != null)
                    {
                        if (pS.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"] != null)
                        {
                            _imageFolder = pS.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"].ToString();
                        }
                    }
                }
                return "/images/" + _imageFolder;
            }
            set { _imageFolder = value; }
        }
    }
}