/*
 * This code is released under Duemetri Public License (DPL) Version 1.2.
 * Original Coder: Indah Fuldner [indah@die-seitenweber.de]
 * modified by Mario Hartmann [mario@hartmann.net // http://mario.hartmann.net/]
 * modified by Manu [manudea@duemetri.net]
 * Version: C#
 * Product name: Rainbow
 * Official site: http://www.rainbowportal.net
 * Last updated Date: 04/JUN/2004
 * Derivate works, translation in other languages and binary distribution
 * of this code must retain this copyright notice and include the complete 
 * licence text that comes with product.
*/
using System;
using System.Drawing;
using System.IO;
using Osmosis.Web.UI;
using Rainbow.Framework;
using Rainbow.Framework.DataTypes;
using Rainbow.Framework.Web.UI.WebControls;
using History=Rainbow.Framework.History;
using Path=Rainbow.Framework.Settings.Path;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// 
    /// </summary>
    [History("mario@hartmann.net", "2004/08/24", "Bug fixed: Unexisting flash movie freezes IE")]
    [History("mario@hartmann.net", "2004/06/04", "Changed Flash movie control]")]
    [History("mario@hartmann.net", "2004/05/25", "Bug fixed:[ 877886 ] Flash Module - Background Color does not work]")]
    public partial class FlashModule : PortalModuleControl
    {
        public string FileSource;

        /// <summary>
        /// FlashModule
        /// </summary>
        public FlashModule()
        {
            // modidfied by Hongwei Shen
            SettingItemGroup group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
            int groupBase = (int) group;

            SettingItem src = new SettingItem(new StringDataType());
            src.Required = true;
            src.Group = group;
            src.Order = groupBase + 20; //1;
            _baseSettings.Add("src", src);

            SettingItem width = new SettingItem(new StringDataType());
            //	width.MinValue = 1;
            //	width.MaxValue = 400;
            width.Group = group;
            width.Order = groupBase + 25; //2;
            _baseSettings.Add("width", width);

            SettingItem height = new SettingItem(new StringDataType());
            //	height.MinValue = 1;
            //	height.MaxValue = 200;
            height.Group = group;
            height.Order = groupBase + 30; //3;
            _baseSettings.Add("height", height);

            SettingItem backColor = new SettingItem(new StringDataType());
            backColor.Required = false;
            backColor.Value = "#FFFFFF";
            backColor.Group = group;
            backColor.Order = groupBase + 35; //4;
            _baseSettings.Add("backcolor", backColor);

            SettingItem FlashPath = new SettingItem(new PortalUrlDataType());
            FlashPath.Required = true;
            FlashPath.Value = "FlashGallery";
            FlashPath.Group = group;
            FlashPath.Order = groupBase + 40; //5;
            _baseSettings.Add("FlashPath", FlashPath);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
//			if (!IsPostBack) //Or it does not work with singon...
//			{
            string flashSrc = ((SettingItem) Settings["src"]).Value;
            flashSrc = flashSrc.Replace("~~", portalSettings.PortalFullPath);
            flashSrc = flashSrc.Replace("~", portalSettings.PortalPath);

            string flashHeight = (SettingItem) Settings["height"];
            string flashWidth = (SettingItem) Settings["width"];
            string flashBGColor = (SettingItem) Settings["backcolor"];

            //Set the output type and Movie
            FlashMovie1.FlashOutputType = FlashOutputType.FlashOnly; //Was ClientScriptVersionDection;

            //Always make sure you have a valid movie or your browser will hang.
            if (flashSrc == null || flashSrc.Length == 0)
                flashSrc = Path.WebPathCombine(portalSettings.PortalFullPath, "/FlashGallery/effect2-marquee.swf");

            string movieName = string.Empty;
            try
            {
                if (File.Exists(Server.MapPath(flashSrc)))
                {
                    movieName = Server.MapPath(flashSrc);
                }
            }
            catch
            {
            }

            //Added existing file check by Manu
            if (movieName.Length != 0)
            {
                FlashMovie1.MovieName = flashSrc;

                // Set the plugin version to check for
                FlashMovie1.MajorPluginVersion = 7;
                FlashMovie1.MajorPluginVersionRevision = 0;
                FlashMovie1.MinorPluginVersion = 0;
                FlashMovie1.MinorPluginVersionRevision = 0;

                //Set some other properties
                if (flashWidth != null && flashWidth.Length != 0)
                    FlashMovie1.MovieWidth = flashWidth;
                if (flashHeight != null && flashHeight.Length != 0)
                    FlashMovie1.MovieHeight = flashHeight;

                if (flashBGColor != null && flashBGColor.Length != 0 && flashBGColor != "0")
                {
                    try
                    {
                        FlashMovie1.MovieBGColor = ColorTranslator.FromHtml(flashBGColor);
                    }
                    catch
                    {
                    }
                }

                //this.FlashMovie1.AutoLoop = false;
                //this.FlashMovie1.AutoPlay = true;
                //this.FlashMovie1.FlashHorizontalAlignment = FlashHorizontalAlignment.Center;
                //this.FlashMovie1.FlashVerticalAlignment = FlashVerticalAlignment.Center;
                //this.FlashMovie1.HtmlAlignment = FlashHtmlAlignment.None;
                //this.FlashMovie1.UseDeviceFonts = false;
                //this.FlashMovie1.WindowMode = FlashMovieWindowMode.Transparent;
                //this.FlashMovie1.ShowMenu = false;
                //this.FlashMovie1.MovieQuality = FlashMovieQuality.AutoHigh;
                //this.FlashMovie1.MovieScale = FlashMovieScale.NoScale;

                // Add some variables
                //this.FlashMovie1.MovieVariables.Add("MyVar1","MyValue1");
                //this.FlashMovie1.MovieVariables.Add("MyVar2","MyValue2");
                //this.FlashMovie1.MovieVariables.Add("MyVar3","MyValue3");

                //Set the NoScript and NoFlash content.  
                //In most situations where
                //html will be displayed the content is the same for both

                //this.FlashMovie1.NoFlashContainer.Controls.Add("No flash found!");
                //this.FlashMovie1.NoScriptContainer.Controls.Add("Scripting is disabled");
            }
            else
            {
                FlashMovie1.Visible = false;
                ErrorLabel.Text = "File '" + flashSrc + "' not found!";
                ErrorLabel.Visible = true;
            }
//			}
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Raises OnInit event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            this.Load += new EventHandler(this.Page_Load);
            // Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
            // Set here title properties
            // Add support for the edit page
            this.EditUrl = "~/DesktopModules/CommunityModules/FlashModule/FlashEdit.aspx";
            // Add title ad the very beginning of 
            // the control's controls collection
//			Controls.AddAt(0, ModuleTitle);
            base.OnInit(e);
        }

        #endregion

        #region General Implementation

        /// <summary>
        /// GUID of module (mandatory)
        /// </summary>
        /// <value></value>
        public override Guid GuidID
        {
            get { return new Guid("{623EC4DD-BA40-421c-887D-D774ED8EBF02}"); }
        }

        #endregion
    }
}