using System;
using System.IO;
using System.Net;
using System.Xml;
using Rainbow.Framework;
using Rainbow.Framework.DataTypes;
using Rainbow.Framework.Web.UI.WebControls;

namespace Rainbow.Content.Web.ModulesVersion
{
    /// <summary>
    ///		Summary description for VisaClassic.
    /// </summary>
    public partial class WeatherCom : PortalModuleControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            DisplayWeather();
        }

        /// <summary>
        /// Displays the weather.
        /// </summary>
        private void DisplayWeather()
        {
            try
            {
                // Request URL
                string wsUrl = "http://xoap.weather.com/weather/local/" +
                               // City Code
                               Settings["CityCode"].ToString() +
                               "?cc=*" +
                               // Forecast Days
                               "&dayf=" + Settings["Forecast"].ToString() +
                               "&prod=xoap&par=1010760847&key=36e1f14b468962e2" +
                               // Set Unit
                               "&unit=" + Settings["Unit"].ToString();

                // Contact service for content
                HttpWebRequest wrq = (HttpWebRequest) WebRequest.Create(wsUrl);
                // Load response
                WebResponse resp = wrq.GetResponse();
                // Create new stream for XmlTextReader
                Stream str = resp.GetResponseStream();
                XmlTextReader reader = new XmlTextReader(str);
                reader.XmlResolver = null;
                // Create Xml document
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                Xml1.Document = doc;
                if (Settings["Unit"].ToString() == "m")
                    Xml1.TransformSource = "WeatherComM.xslt";
                else
                    Xml1.TransformSource = "WeatherCom.xslt";
            }
            catch (Exception ex)
            {
                ErrorHandler.Publish(LogLevel.Warn, "Weather.com Error", ex);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WeatherCom"/> class.
        /// </summary>
        public WeatherCom()
        {
            SettingItem cityCode = new SettingItem(new StringDataType());
            cityCode.Required = false;
            cityCode.Value = "BKXX0001";
            _baseSettings.Add("CityCode", cityCode);

            SettingItem forecast = new SettingItem(new StringDataType());
            forecast.Required = false;
            forecast.Value = "3";
            BaseSettings.Add("Forecast", forecast);

            SettingItem setUnit = new SettingItem(new StringDataType());
            setUnit.Required = false;
            setUnit.Value = "m";
            _baseSettings.Add("Unit", setUnit);
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Raises OnInit event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(this.Page_Load);
            base.OnInit(e);
        }

        #endregion

        /// <summary>
        /// GUID of module (mandatory)
        /// </summary>
        /// <value></value>
        public override Guid GuidID
        {
            get { return new Guid("{078FDE24-45A0-4f70-A6C8-E7F2E498B9BC}"); }
        }
    }
}