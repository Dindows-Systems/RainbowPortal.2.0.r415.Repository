using System;
using System.Text;
using Rainbow.Framework;
using Rainbow.Framework.DataTypes;
using Rainbow.Framework.Web.UI.WebControls;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// The IframeModule provides an IFRAME where you can set the
    /// source URL and the height of the frame using the settings system.
    /// Default height is 200px and URL is http://www.rainbowportal.net
    /// Written by: Jakob Hansen, hansen3000@hotmail
    /// </summary>
    public partial class IframeModule : PortalModuleControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            string strURL = Settings["URL"].ToString();
            string height = Settings["Height"].ToString();
            string width = Settings["Width"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<iframe");
            sb.Append(" src='");
            sb.Append(strURL);
            sb.Append("'");
            sb.Append(" width='");
            sb.Append(width);
            sb.Append("'");
            sb.Append(" height='");
            sb.Append(height);
            sb.Append("'");
            sb.Append(" title='");
            sb.Append(TitleText);
            sb.Append("'");
            sb.Append(">");
            sb.Append("</iframe>");

            LiteralIframe.Text = sb.ToString();
        }

        /// <summary>
        /// General module GUID
        /// </summary>
        /// <value></value>
        public override Guid GuidID
        {
            get { return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531005}"); }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public IframeModule()
        {
            // modified by Hongwei Shen
            SettingItemGroup group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            int groupBase = (int) group;

            //MH:canged to support relativ url
            //SettingItem url = new SettingItem(new UrlDataType());
            SettingItem url = new SettingItem(new StringDataType());
            url.Required = true;
            url.Group = group;
            url.Order = groupBase + 20; //1;
            url.Value = "http://www.rainbowportal.net";
            _baseSettings.Add("URL", url);

            //MH: added to support width values
            SettingItem width = new SettingItem(new StringDataType());
            width.Required = true;
            width.Group = group;
            width.Order = groupBase + 25; //2;
            width.Value = "250";
            //width.MinValue = 1;
            //width.MaxValue = 2000;
            _baseSettings.Add("Width", width);

            //MH: changed to StringDataType to support  percent or pixel values
            //SettingItem width = new SettingItem(new IntegerDataType());
            SettingItem height = new SettingItem(new StringDataType());
            height.Required = true;
            height.Group = group;
            height.Order = groupBase + 30; //3;
            height.Value = "250";
            //height.MinValue = 1;
            //height.MaxValue = 2000;
            _baseSettings.Add("Height", height);
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Raises Init event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(this.Page_Load);

            // Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
            // Add title ad the very beginning of 
            // the control's controls collection
//			Controls.AddAt(0, ModuleTitle);

            base.OnInit(e);
        }

        #endregion
    }
}