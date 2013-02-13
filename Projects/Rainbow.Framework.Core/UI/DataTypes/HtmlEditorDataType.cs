using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using ActiveUp.WebControls.HtmlTextBox.Tools;
using Rainbow.Framework.Settings;
using Rainbow.Framework.Site.Configuration;
using Rainbow.Framework.Web.UI.WebControls;

namespace Rainbow.Framework.DataTypes
{
    /// <summary>
    /// List of available HTML editors
    /// </summary>
    public class HtmlEditorDataType : ListDataType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlEditorDataType"/> class.
        /// </summary>
        public HtmlEditorDataType()
        {
            InnerDataType = PropertiesDataType.List;
            InitializeComponents();
        }

        /// <summary>
        /// HTMLs the editor settings.
        /// </summary>
        /// <param name="editorSettings">The editor settings.</param>
        /// <param name="group">The group.</param>
        public static void HtmlEditorSettings(Hashtable editorSettings, SettingItemGroup group)
        {
            PortalSettings pS = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            SettingItem Editor = new SettingItem(new HtmlEditorDataType());
            Editor.Order = (int) group + 1; //1; modified by Hongwei Shen(hongwei.shen@gmail.com) 11/9/2005
            Editor.Group = group;
            Editor.EnglishName = "Editor";
            Editor.Description = "Select the Html Editor for Module";

            SettingItem ControlWidth = new SettingItem(new IntegerDataType());
            ControlWidth.Value = "700";
            ControlWidth.Order = (int) group + 2; // 2; modified by Hongwei Shen
            ControlWidth.Group = group;
            ControlWidth.EnglishName = "Editor Width";
            ControlWidth.Description = "The width of editor control";

            SettingItem ControlHeight = new SettingItem(new IntegerDataType());
            ControlHeight.Value = "400";
            ControlHeight.Order = (int) group + 3; //3; modified by Hongwei Shen
            ControlHeight.Group = group;
            ControlHeight.EnglishName = "Editor Height";
            ControlHeight.Description = "The height of editor control";

            SettingItem ShowUpload = new SettingItem(new BooleanDataType());
            ShowUpload.Value = "true";
            ShowUpload.Order = (int) group + 4; // 4;  modified by Hongwei Shen
            ShowUpload.Group = group;
            ShowUpload.EnglishName = "Upload?";
            ShowUpload.Description = "Only used if Editor is ActiveUp HtmlTextBox";

            SettingItem ModuleImageFolder = null;
            if (pS != null)
            {
                if (pS.PortalFullPath != null)
                {
                    ModuleImageFolder =
                        new SettingItem(
                            new FolderDataType(HttpContext.Current.Server.MapPath(pS.PortalFullPath + "/images"),
                                               "default"));
                    ModuleImageFolder.Value = "default";
                    ModuleImageFolder.Order = (int) group + 5; // 5;  modified by Hongwei Shen
                    ModuleImageFolder.Group = group;
                    ModuleImageFolder.EnglishName = "Default Image Folder";
                    ModuleImageFolder.Description =
                        "This folder is used for editor in this module to take and upload images";
                }

                // Set the portal default values
                if (pS.CustomSettings != null)
                {
                    if (pS.CustomSettings["SITESETTINGS_DEFAULT_EDITOR"] != null)
                        Editor.Value = pS.CustomSettings["SITESETTINGS_DEFAULT_EDITOR"].ToString();
                    if (pS.CustomSettings["SITESETTINGS_EDITOR_WIDTH"] != null)
                        ControlWidth.Value = pS.CustomSettings["SITESETTINGS_EDITOR_WIDTH"].ToString();
                    if (pS.CustomSettings["SITESETTINGS_EDITOR_HEIGHT"] != null)
                        ControlHeight.Value = pS.CustomSettings["SITESETTINGS_EDITOR_HEIGHT"].ToString();
                    if (pS.CustomSettings["SITESETTINGS_EDITOR_HEIGHT"] != null)
                        ControlHeight.Value = pS.CustomSettings["SITESETTINGS_EDITOR_HEIGHT"].ToString();
                    if (pS.CustomSettings["SITESETTINGS_SHOWUPLOAD"] != null)
                        ShowUpload.Value = pS.CustomSettings["SITESETTINGS_SHOWUPLOAD"].ToString();
                    if (pS.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"] != null)
                        ModuleImageFolder.Value = pS.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"].ToString();
                }
            }

            editorSettings.Add("Editor", Editor);
            editorSettings.Add("Width", ControlWidth);
            editorSettings.Add("Height", ControlHeight);
            editorSettings.Add("ShowUpload", ShowUpload);
            if (ModuleImageFolder != null)
                editorSettings.Add("MODULE_IMAGE_FOLDER", ModuleImageFolder);
        }

        /// <summary>
        /// </summary>
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            // Default
            Value = "FreeTextBox";
            // Change the default value to Portal Default Editor Value by jviladiu@portalServices.net 13/07/2004

            if (HttpContext.Current != null && HttpContext.Current.Items["PortalSettings"] != null)
            {
                PortalSettings pS = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
                if (pS.CustomSettings != null)
                {
                    if (pS.CustomSettings["SITESETTINGS_DEFAULT_EDITOR"] != null)
                        Value = pS.CustomSettings["SITESETTINGS_DEFAULT_EDITOR"].ToString();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        public override object DataSource
        {
            get { return "Plain Text;FCKEditor;FCKEditor V2;FreeTextBox;ActiveUp HtmlTextBox".Split(';'); }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description
        {
            get { return "HtmlEditor List"; }
        }

        /// <summary>
        /// Gets the FTB language.
        /// </summary>
        /// <param name="rainbowLanguage">The rainbow language.</param>
        /// <returns></returns>
        private static string getFtbLanguage(string rainbowLanguage)
        {
            switch (rainbowLanguage.Substring(rainbowLanguage.Length - 2).ToLower())
            {
                case "en":
                    return "en-US";
                case "us":
                    return "en-US";
                case "es":
                    return "es-ES";
                case "cn":
                    return "zh-cn";
                case "cz":
                    return "cz-CZ";
                case "fi":
                    return "fi-fi";
                case "nl":
                    return "nl-NL";
                case "de":
                    return "de-de";
                case "il":
                    return "he-IL";
                case "it":
                    return "it-IT";
                case "jp":
                    return "ja-JP";
                case "kr":
                    return "ko-kr";
                case "no":
                    return "nb-NO";
                case "pt":
                    return "pt-pt";
                case "ro":
                    return "ro-RO";
                case "ru":
                    return "ru-ru";
                case "se":
                    return "sv-se";
                case "tw":
                    return "zh-TW";
                default:
                    return "en-US";
            }
        }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <param name="PlaceHolderHTMLEditor">The place holder HTML editor.</param>
        /// <param name="moduleID">The module ID.</param>
        /// <param name="showUpload">if set to <c>true</c> [show upload].</param>
        /// <param name="portalSettings">The portal settings.</param>
        /// <returns></returns>
        public IHtmlEditor GetEditor(Control PlaceHolderHTMLEditor, int moduleID, bool showUpload,
                                     PortalSettings portalSettings)
        {
            IHtmlEditor DesktopText;
            string moduleImageFolder = ModuleSettings.GetModuleSettings(moduleID)["MODULE_IMAGE_FOLDER"].ToString();

            // Grabs ID from the place holder so that a unique editor is on the page if more than one
            // But keeps same ID so that the information can be submitted to be saved. [CDT]
            string uniqueID = PlaceHolderHTMLEditor.ID;

            switch (Value)
            {
                case "FCKEditor V2": // jviladiu@portalservices.net 2004/11/09.
                    FCKTextBoxV2 fckv2 = new FCKTextBoxV2();
                    fckv2.ImageFolder = moduleImageFolder;
                    fckv2.BasePath = Path.WebPathCombine(Path.ApplicationRoot, "aspnet_client/FCKEditorV2/");
                    fckv2.AutoDetectLanguage = false;
                    fckv2.DefaultLanguage = portalSettings.PortalUILanguage.Name.Substring(0, 2);
//					fckv2.EditorAreaCSS = portalSettings.GetCurrentTheme().CssFile;
                    fckv2.ID = string.Concat("FCKTextBox", uniqueID);
                    string conector = Path.ApplicationRootPath("/app_support/FCKconnectorV2.aspx");
                    fckv2.ImageBrowserURL =
                        Path.WebPathCombine(Path.ApplicationRoot,
                                            "aspnet_client/FCKEditorV2/editor/filemanager/browser.html?Type=Image&Connector=" +
                                            conector);
                    fckv2.LinkBrowserURL =
                        Path.WebPathCombine(Path.ApplicationRoot,
                                            "aspnet_client/FCKEditorV2/editor/filemanager/browser.html?Connector=" +
                                            conector);
                    DesktopText = ((IHtmlEditor) fckv2);
                    break;

                case "FreeTextBox":
                    FreeTextBox freeText = new FreeTextBox();
                    freeText.ToolbarLayout =
                        "ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorPicker,FontBackColorPicker,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat;CreateLink,Unlink|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Delete,Cut,Copy,Paste;Undo,Redo,Print;InsertTable,InsertTableColumnAfter,InsertTableColumnBefore,InsertTableRowAfter,InsertTableRowBefore,DeleteTableColumn,DeleteTableRow,InsertImageFromGallery";
                    freeText.ImageGalleryUrl =
                        Path.WebPathCombine(Path.ApplicationFullPath,
                                            "app_support/ftb.imagegallery.aspx?rif={0}&cif={0}&mID=" +
                                            moduleID.ToString());
                    freeText.ImageFolder = moduleImageFolder;
                    freeText.ImageGalleryPath = Path.WebPathCombine(portalSettings.PortalFullPath, freeText.ImageFolder);
                    freeText.ID = string.Concat("FreeText", uniqueID);
                    freeText.Language = getFtbLanguage(portalSettings.PortalUILanguage.Name);
                    DesktopText = ((IHtmlEditor) freeText);
                    break;

                case "ActiveUp HtmlTextBox":
                    HtmlTextBox h = new HtmlTextBox();
                    h.ImageFolder = moduleImageFolder;
                    DesktopText = (IHtmlEditor) h;

                    // Allow content editors to see the content with the same style that it is displayed in
                    h.ContentCssFile = portalSettings.GetCurrentTheme().CssFile;

                    // Custom Properties must come after control is added to placeholder
                    h.EnsureToolsCreated();

                    // Set the icons folder
                    h.IconsDir = Path.WebPathCombine(Path.ApplicationRoot, "aspnet_client/ActiveUp/icons/");

                    // Add the Help icon
                    StringBuilder sbHelp = new StringBuilder();
                    sbHelp.Append("var Help=window.open('");
                    sbHelp.Append(Path.ApplicationRoot);
                    sbHelp.Append("/aspnet_client/ActiveUp/");
                    sbHelp.Append(
                        "htmltextbox_help.html', 'HTML_TextBox_Help', 'height=520, width=520, resizable=yes, scrollbars=yes, menubar=no, toolbar=no, directories=no, location=no, status=no');Help.moveTo('20', '20');");
                    Custom openPage = new Custom();
                    openPage.IconOff = "help_off.gif";
                    openPage.IconOver = "help_off.gif";
                    openPage.ClientSideOnClick = sbHelp.ToString();
                    h.Toolbars[0].Tools.Add(openPage);

                    // Add the image library			
                    Image imageLibrary = (Image) h.Toolbars[0].Tools["Image"];
                    imageLibrary.AutoLoad = true;

                    // Clear the directories collection because it is stored in ViewState and must be cleared or upload will result in display of multiple directories of the same name
                    imageLibrary.Directories.Clear();
                    imageLibrary.Directories.Add("images",
                                                 HttpContext.Current.Server.MapPath(portalSettings.PortalFullPath +
                                                                                    h.ImageFolder),
                                                 portalSettings.PortalFullPath + h.ImageFolder + "/");

                    if (!showUpload)
                    {
                        imageLibrary.UploadDisabled = true;
                    }
                    break;

                case "Plain Text":
                default:
                    DesktopText = (new TextEditor());
                    break;
            }
            PlaceHolderHTMLEditor.Controls.Add(((Control) DesktopText));
            return DesktopText;
        }
    }
}