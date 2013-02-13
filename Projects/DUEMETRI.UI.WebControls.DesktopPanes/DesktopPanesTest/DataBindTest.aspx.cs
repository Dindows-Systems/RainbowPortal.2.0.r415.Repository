using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DektopPanesTest
{
	/// <summary>
	/// Descrizione di riepilogo per WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected DUEMETRI.UI.WebControls.DesktopPanes DESKTOPPANES1;
		protected DUEMETRI.UI.WebControls.DesktopPanes DESKTOPPANES2;
		protected DUEMETRI.UI.WebControls.DesktopPanes ThreePanes;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			AddModule("leftpane", new LiteralControl("LeftPane"), ThreePanes);
			AddModule("rightpane", new LiteralControl("RightPane"), ThreePanes);
			AddModule("contentpane", new LiteralControl("ContentPane"), ThreePanes);
			ThreePanes.DataBind();


			AddModule("leftpane", new LiteralControl("LeftPane"), DESKTOPPANES1);
			AddModule("contentpane", new LiteralControl("ContentPane"), DESKTOPPANES1);
			DESKTOPPANES1.DataBind();

			AddModule("rightpane", new LiteralControl("RightPane"), DESKTOPPANES2);
			AddModule("contentpane", new LiteralControl("ContentPane"), DESKTOPPANES2);
			DESKTOPPANES2.DataBind();
		}

		private void AddModule(string pane, Control control, DUEMETRI.UI.WebControls.DesktopPanes panes)
		{
			ArrayList arrayData;

			switch(pane)
			{
				case "leftpane":
					arrayData = panes.DataSource[0];
					break;
				case "rightpane":
					arrayData = panes.DataSource[2];
					break;
				case "contentpane":
				default:
					arrayData = panes.DataSource[1];
					break;
			}		
			arrayData.Add(control);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: questa chiamata è richiesta da Progettazione Web Form ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
