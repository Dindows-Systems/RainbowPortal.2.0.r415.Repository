namespace DektopPanesTest
{
	using System;
	using System.Data;
	using System.Collections;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Descrizione di riepilogo per testcontrol.
	/// </summary>
	public abstract class testcontrol : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataList defsList;
		protected System.Web.UI.WebControls.Button Button1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ArrayList l =new ArrayList();
				l.Add("First");
				l.Add("Second");
				l.Add("Third");
				defsList.DataSource = l;
				defsList.DataBind();
			}
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
		
		///		Metodo necessario per il supporto della finestra di progettazione. Non modificare
		///		il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.defsList.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.defsList_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("Button clicked: " + this.ID);
		}

		private void defsList_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			Response.Write("ItemCommand clicked: " + this.ID + " on " + e.Item.ItemIndex.ToString());		
		}
	}
}
