using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Web.UI.Design;
using System.ComponentModel;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DUEMETRI.UI.WebControls;

namespace DUEMETRI.UI.Design.WebControls
{
    /// <summary>
    /// DesktopPanes design support class for Visual Studio. Pane Template.
    /// </summary>
	public class DesktopPanesTemplate : Control, INamingContainer
	{
		private DesktopPanes parent;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
		public DesktopPanesTemplate(DesktopPanes parent)
		{
			this.parent = parent;
		}
	}
}