using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DUEMETRI.UI.Design.WebControls;

namespace DUEMETRI.UI.WebControls
{
    /// <summary>
    /// DesktopPanes class for supporting three pane browsing
    /// </summary>
    [
    Designer("DUEMETRI.UI.Design.WebControls.DesktopPanesDesigner")
    ]
    public class DesktopPanes : WebControl, INamingContainer
    {
        #region Member variables
        bool _showFirstSeparator = true;
        bool _showLastSeparator = true;

		private ArrayList[] innerDataSource;

		private TableItemStyle leftPaneStyle;
		private TableItemStyle contentPaneStyle;
		private TableItemStyle rightPaneStyle;

		private Style horizontalSeparatorStyle;
		private TableItemStyle verticalSeparatorStyle;

		private ITemplate leftPaneTemplate;
		private ITemplate contentPaneTemplate;
		private ITemplate rightPaneTemplate;
		private ITemplate horizontalSeparatorTemplate;
		private ITemplate verticalSeparatorTemplate;

		private System.Web.UI.Control leftPaneContainer;
		private System.Web.UI.Control contentPaneContainer;
		private System.Web.UI.Control rightPaneContainer;
		private System.Web.UI.Control verticalSeparatorContainer;
		private System.Web.UI.Control horizontalSeparatorContainer;

		private System.Web.UI.WebControls.TableCell leftPane;
		private System.Web.UI.WebControls.TableCell contentPane;
		private System.Web.UI.WebControls.TableCell rightPane;

		private const int IDX_CONTROL_STYLE = 0;

		private const int IDX_LEFT_PANE_STYLE = 1;
		private const int IDX_CONTENT_PANE_STYLE = 2;
		private const int IDX_RIGHT_PANE_STYLE = 3;

		private const int IDX_HORIZONTAL_SEPARATOR_STYLE = 4;
		private const int IDX_VERTICAL_SEPARATOR_STYLE = 5;

        /// <summary>
        /// Left pane index
        /// </summary>
		protected const int IDX_LEFT_PANE_DATA = 0;
        /// <summary>
        /// Content pane index
        /// </summary>
		protected const int IDX_CONTENT_PANE_DATA = 1;
        /// <summary>
        /// Right pane index
        /// </summary>
		protected const int IDX_RIGHT_PANE_DATA = 2;
		#endregion

        /// <summary>
        /// Show First Separator.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        Description("Show First Separator.")
        ]
        public virtual bool ShowFirstSeparator 
        {
            get 
            {
                return _showFirstSeparator;
            }
            set 
            {
                _showFirstSeparator = value;
            }
        }
        
        /// <summary>
        /// Show Last Separator.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        Description("Show Last Separator.")
        ]
        public virtual bool ShowLastSeparator 
        {
            get 
            {
                return _showLastSeparator;
            }
            set 
            {
                _showLastSeparator = value;
            }
        }

        /// <summary>
        /// The cell padding of the rendered table.
        /// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("The cell padding of the rendered table.")
		]
		public virtual int CellPadding 
		{
			get 
			{
				if (ControlStyleCreated == false) 
				{
					return 0;
				}
				return ((TableStyle)ControlStyle).CellPadding;
			}
			set 
			{
				((TableStyle)ControlStyle).CellPadding = value;
			}
		}

        /// <summary>
        /// The cell spacing of the rendered table.
        /// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("The cell spacing of the rendered table.")
		]
		public virtual int CellSpacing 
		{
			get 
			{
				if (ControlStyleCreated == false) 
				{
					return 0;
				}
				return ((TableStyle)ControlStyle).CellSpacing;
			}
			set 
			{
				((TableStyle)ControlStyle).CellSpacing = value;
			}
		}

        /// <summary>
        /// The width of the rendered table.
        /// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue("100%"),
		Description("The width of the rendered table.")
		]
		public override Unit Width 
		{
			get 
			{
				if (ControlStyleCreated == false) 
				{
					return 0;
				}
				return ((TableStyle)ControlStyle).Width;
			}
			set 
			{
				((TableStyle)ControlStyle).Width = value;
			}
		}

        /// <summary>
        /// The height of the rendered table.
        /// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(null),
		Description("The height of the rendered table.")
		]
		public override Unit Height 
		{
			get 
			{
				if (ControlStyleCreated == false) 
				{
					return 0;
				}
				return ((TableStyle)ControlStyle).Height;
			}
			set 
			{
				((TableStyle)ControlStyle).Height = value;
			}
		}

        /// <summary>
        /// The grid lines to be shown in the rendered table.
        /// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(GridLines.None),
		Description("The grid lines to be shown in the rendered table.")
		]
		public virtual GridLines GridLines 
		{
			get 
			{
				if (ControlStyleCreated == false) 
				{
					return GridLines.None;
				}
				return ((TableStyle)ControlStyle).GridLines;
			}
			set 
			{
				((TableStyle)ControlStyle).GridLines = value;
			}
		}

        /// <summary>
        /// The style to be applied to LeftPane.
        /// </summary>
		[
		Category("Style"),
		Description("The style to be applied to LeftPane."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public virtual TableItemStyle LeftPaneStyle 
		{
			get 
			{
				if (leftPaneStyle == null) 
				{
					leftPaneStyle = new TableItemStyle();
                    
                    //Default
                    leftPaneStyle.Width = new Unit(170);
                    leftPaneStyle.VerticalAlign = VerticalAlign.Top;

					if (IsTrackingViewState)
						((IStateManager)leftPaneStyle).TrackViewState();
				}
				return leftPaneStyle;
			}
		}

        /// <summary>
        /// The style to be applied to RightPane.
        /// </summary>
		[
		Category("Style"),
		Description("The style to be applied to RightPane."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public virtual TableItemStyle RightPaneStyle 
		{
			get 
			{
				if (rightPaneStyle == null) 
				{
					rightPaneStyle = new TableItemStyle();
                    
                    //Default
                    rightPaneStyle.Width = new Unit(230);
                    rightPaneStyle.VerticalAlign = VerticalAlign.Top;

					if (IsTrackingViewState)
						((IStateManager)rightPaneStyle).TrackViewState();
				}
				return rightPaneStyle;
			}        
		}

        /// <summary>
        /// The style to be applied to ContentPane.
        /// </summary>
		[
		Category("Style"),
		Description("The style to be applied to ContentPane."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public virtual TableItemStyle ContentPaneStyle 
		{
			get 
			{
				if (contentPaneStyle == null) 
				{
					contentPaneStyle = new TableItemStyle();
                    
                    //Default
                    contentPaneStyle.VerticalAlign = VerticalAlign.Top;

					if (IsTrackingViewState)
						((IStateManager)contentPaneStyle).TrackViewState();
				}
				return contentPaneStyle;
			}        
		}  

        /// <summary>
        /// The style to be applied to Horizontal Separator.
        /// </summary>
		[
		Category("Style"),
		Description(""),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public virtual Style HorizontalSeparatorStyle 
		{
			get 
			{
				if (horizontalSeparatorStyle == null) 
				{
					horizontalSeparatorStyle = new Style();
					if (IsTrackingViewState)
						((IStateManager)horizontalSeparatorStyle).TrackViewState();
				}
				return horizontalSeparatorStyle;
			}
		}
        
        /// <summary>
        /// The style to be applied to Horizontal Separator.
        /// </summary>
		[
		Category("Style"),
		Description("The style to be applied to Horizontal Separator."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public virtual TableItemStyle VerticalSeparatorStyle 
		{
			get 
			{
				if (verticalSeparatorStyle == null) 
				{
					verticalSeparatorStyle = new TableItemStyle();
					if (IsTrackingViewState)
						((IStateManager)verticalSeparatorStyle).TrackViewState();
				}
				return verticalSeparatorStyle;
			}
		}

        /// <summary>
        /// The DataSource.
        /// </summary>
		[
		Category("Data"),
		Description("The DataSource.")
		]
		public ArrayList[] DataSource 
		{
			get 
			{
				if (innerDataSource == null) 
				{
					InitializeDataSource();
				}
				return innerDataSource;
			}
		}
        
        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls.
        /// </summary>
		public override void DataBind()
		{
			foreach(Control c in DataSource[IDX_LEFT_PANE_DATA])
			{
				LeftPane.Controls.Add(c);
				LeftPane.Controls.Add(GetHorizontalSeparator());
			}

			foreach(Control c in DataSource[IDX_CONTENT_PANE_DATA])
			{
				ContentPane.Controls.Add(c);
				ContentPane.Controls.Add(GetHorizontalSeparator());
			}

			foreach(Control c in DataSource[IDX_RIGHT_PANE_DATA])
			{
				RightPane.Controls.Add(c);
				RightPane.Controls.Add(GetHorizontalSeparator());
			}
        }

        /// <summary>
        /// The Left Pane.
        /// </summary>
		[
		Browsable(false),
		DefaultValue(null),
		Description("The Left Pane."),
		PersistenceMode(PersistenceMode.InnerProperty),
		TemplateContainer(typeof(DesktopPanesTemplate))
		]
		public virtual ITemplate LeftPaneTemplate 
		{
			get 
			{
				return leftPaneTemplate;
			}
			set 
			{
				leftPaneTemplate = value;
			}
		}

        /// <summary>
        /// The Content Pane.
        /// </summary>
		[
		Browsable(false),
		DefaultValue(null),
		Description("The Content Pane."),
		PersistenceMode(PersistenceMode.InnerProperty),
		TemplateContainer(typeof(DesktopPanesTemplate))
		]
		public virtual ITemplate ContentPaneTemplate 
		{
			get 
			{
				return contentPaneTemplate;
			}
			set 
			{
				contentPaneTemplate = value;
			}
		}
        
        /// <summary>
        /// The Right Pane.
        /// </summary>
		[
		Browsable(false),
		DefaultValue(null),
		Description("The Right Pane."),
		PersistenceMode(PersistenceMode.InnerProperty),
		TemplateContainer(typeof(DesktopPanesTemplate))
		]
		public virtual ITemplate RightPaneTemplate 
		{
			get 
			{
				return rightPaneTemplate;
			}
			set 
			{
				rightPaneTemplate = value;
			}
		}

        /// <summary>
        /// The HorizontalSeparator.
        /// </summary>
		[
		Browsable(false),
		DefaultValue(null),
		Description("The HorizontalSeparator."),
		PersistenceMode(PersistenceMode.InnerProperty),
		TemplateContainer(typeof(DesktopPanesTemplate))
		]
		public virtual ITemplate HorizontalSeparatorTemplate 
		{
			get 
			{
				return horizontalSeparatorTemplate;
			}
			set 
			{
				horizontalSeparatorTemplate = value;
			}
		}

        /// <summary>
        /// The VerticalSeparator.
        /// </summary>
		[
		Browsable(false),
		DefaultValue(null),
		Description("The VerticalSeparator."),
		PersistenceMode(PersistenceMode.InnerProperty),
		TemplateContainer(typeof(DesktopPanesTemplate))
		]
		public virtual ITemplate VerticalSeparatorTemplate 
		{
			get 
			{
				return verticalSeparatorTemplate;
			}
			set 
			{
				verticalSeparatorTemplate = value;
			}
		}

        /// <summary>
        /// LeftPane
        /// </summary>
		[
		Browsable(false)
		]
		public TableCell LeftPane
		{
			get
			{
                if (leftPane == null)
                {
                    leftPane = new TableCell();
                }
				return leftPane;
			}
		}

        /// <summary>
        /// ContentPane
        /// </summary>
		[
		Browsable(false)
		]
		public TableCell ContentPane
		{
			get
			{
                if (contentPane == null)
                {
                    contentPane = new TableCell();
                }
				return contentPane;
			}
		}

        /// <summary>
        /// RightPane
        /// </summary>
		[
		Browsable(false)
		]
		public TableCell RightPane
		{
			get
			{
                if (rightPane == null)
                {
                    rightPane = new TableCell();
                }
				return rightPane;
			}
		}

        /// <summary>
        /// This member overrides Control.LoadViewState
        /// </summary>
        /// <param name="savedState"></param>
		protected override void LoadViewState(object savedState) 
		{
			// Customize state management to handle saving state of contained objects.
			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if (myState[IDX_CONTROL_STYLE] != null)
					base.LoadViewState(myState[IDX_CONTROL_STYLE]);
				if (myState[IDX_LEFT_PANE_STYLE] != null)
					((IStateManager)leftPaneStyle).LoadViewState(myState[IDX_LEFT_PANE_STYLE]);
				if (myState[IDX_CONTENT_PANE_STYLE] != null)
					((IStateManager)contentPaneStyle).LoadViewState(myState[IDX_CONTENT_PANE_STYLE]);
				if (myState[IDX_RIGHT_PANE_STYLE] != null)
					((IStateManager)rightPaneStyle).LoadViewState(myState[IDX_RIGHT_PANE_STYLE]);
				if (myState[IDX_HORIZONTAL_SEPARATOR_STYLE] != null)
					((IStateManager)horizontalSeparatorStyle).LoadViewState(myState[IDX_HORIZONTAL_SEPARATOR_STYLE]);
				if (myState[IDX_VERTICAL_SEPARATOR_STYLE] != null)
					((IStateManager)verticalSeparatorStyle).LoadViewState(myState[IDX_VERTICAL_SEPARATOR_STYLE]);
			}
		}
        
        /// <summary>
        /// This member overrides Control.SaveViewState
        /// </summary>
        /// <returns></returns>
		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.
			object baseState = base.SaveViewState();
			object leftPaneStyleState = (leftPaneStyle != null) ? ((IStateManager)leftPaneStyle).SaveViewState() : null;
			object contentPaneStyleState = (contentPaneStyle != null) ? ((IStateManager)contentPaneStyle).SaveViewState() : null;
			object rightPaneStyleState = (rightPaneStyle != null) ? ((IStateManager)rightPaneStyle).SaveViewState() : null;
			object horizontalSeparatorStyleState = (horizontalSeparatorStyle != null) ? ((IStateManager)horizontalSeparatorStyle).SaveViewState() : null;
			object verticalSeparatorStyleState = (verticalSeparatorStyle != null) ? ((IStateManager)verticalSeparatorStyle).SaveViewState() : null;

			object[] myState = new object[6];
			myState[IDX_CONTROL_STYLE] = baseState;
			myState[IDX_LEFT_PANE_STYLE] = leftPaneStyleState;
			myState[IDX_CONTENT_PANE_STYLE] = contentPaneStyleState;
			myState[IDX_RIGHT_PANE_STYLE] = rightPaneStyleState;
			myState[IDX_HORIZONTAL_SEPARATOR_STYLE] = horizontalSeparatorStyleState;
			myState[IDX_VERTICAL_SEPARATOR_STYLE] = verticalSeparatorStyleState;

			return myState;
		}

        /// <summary>
        /// This member overrides Control.TrackViewState.
        /// </summary>
		protected override void TrackViewState() 
		{
			// Customized state management to handle saving state of contained objects such as styles.
			base.TrackViewState();

			if (leftPaneStyle != null)
				((IStateManager)leftPaneStyle).TrackViewState();
			if (contentPaneStyle != null)
				((IStateManager)contentPaneStyle).TrackViewState();
			if (rightPaneStyle != null)
				((IStateManager)rightPaneStyle).TrackViewState();
			if (horizontalSeparatorStyle != null)
				((IStateManager)horizontalSeparatorStyle).TrackViewState();
			if (verticalSeparatorStyle != null)
				((IStateManager)verticalSeparatorStyle).TrackViewState();
		}

		/// <summary>
		/// Web server control can set its control style to 
		/// any class that derives from Style by overriding 
		/// the WebControl.CreateControlStyle method
		/// </summary>
		/// <returns></returns>
		protected override Style CreateControlStyle() 
		{
			// Note that the constructor of Style takes   
			// ViewState as an argument. 
			TableStyle style = new TableStyle(ViewState);

			// Set up default initial state.
			style.CellSpacing = 0;
            style.Width = new Unit("100%");          

            return style;
		}

        /// <summary>
        /// Initialize internal data source
        /// </summary>
		protected virtual void InitializeDataSource()
		{
			innerDataSource = new ArrayList[3];
			innerDataSource[IDX_LEFT_PANE_DATA] = new ArrayList();
			innerDataSource[IDX_CONTENT_PANE_DATA] = new ArrayList();
			innerDataSource[IDX_RIGHT_PANE_DATA] = new ArrayList();
		}

        /// <summary>
        /// This member overrides Control.OnDataBinding
        /// </summary>
        /// <param name="e"></param>
		protected override void OnDataBinding(EventArgs e) 
		{
			EnsureChildControls();
			base.OnDataBinding(e);
		}

        /// <summary>
        /// This member overrides Control.CreateChildControls
        /// </summary>
		protected override void CreateChildControls() 
		{
			Controls.Clear();

			CreateControlHierarchy();
			
			base.CreateChildControls();
		}

        /// <summary>
        /// This member overrides Control.Render
        /// </summary>
        /// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer) 
		{
			// Apply styles to the control hierarchy
			// and then render it out.

			// Apply styles during render phase, so the user can change styles
			// after calling DataBind without the property changes ending
			// up in view state.
			PrepareControlHierarchy();

			RenderContents(writer);
//			base.Render(writer);
		}

        /// <summary>
        /// This member overrides Control.PrepareControlHierarchy
        /// </summary>
		private void PrepareControlHierarchy() 
		{
			if (HasControls() == false) 
				return;

			Debug.Assert(Controls[0] is Table);
			Table table = (Table)Controls[0];

			Debug.Assert(table.Rows.Count == 1);
			TableCellCollection cells = table.Rows[0].Cells;

			table.CopyBaseAttributes(this);
			if (ControlStyleCreated) 
				table.ApplyStyle(ControlStyle);

			Debug.Assert(cells.Count == 7);

			TableCell firstSeparator = cells[0];
			firstSeparator.Visible = ShowFirstSeparator;
			firstSeparator.MergeStyle(this.VerticalSeparatorStyle);

			TableCell leftCell = cells[1];
			leftCell.MergeStyle(this.LeftPaneStyle);

			TableCell leftToContentSeparator = cells[2];
			leftToContentSeparator.MergeStyle(this.VerticalSeparatorStyle);

			if (LeftPane.HasControls() || LeftPane.Text.Trim() != String.Empty)
			{
				LeftPane.Visible = true;
				leftToContentSeparator.Visible = true;
			}
			else
			{
				LeftPane.Visible = false;
				leftToContentSeparator.Visible = false;
			}


			TableCell contentCell = cells[3];
			contentCell.MergeStyle(this.ContentPaneStyle);

			TableCell contentToRightSeparator = cells[4];
			contentToRightSeparator.MergeStyle(this.VerticalSeparatorStyle);

			TableCell rightCell = cells[5];
			rightCell.MergeStyle(this.RightPaneStyle);

			TableCell lastSeparator = cells[6];
			lastSeparator.Visible = ShowLastSeparator;
			lastSeparator.MergeStyle(this.VerticalSeparatorStyle);

			if (RightPane.HasControls() || RightPane.Text.Trim() != String.Empty)
			{
				contentToRightSeparator.Visible = true;
				RightPane.Visible = true;
			}
			else
			{
				contentToRightSeparator.Visible = false;
				RightPane.Visible = false;
			}
		}

        /// <summary>
        /// This member overrides Control.CreateControlHierarchy
        /// </summary>
		private void CreateControlHierarchy() 
		{
			// NEVER hide controls on this routine
			// some events WILL NOT FIRED

			Controls.Clear();

			Table table = new Table();
			Controls.Add(table);

            //Prepare Control Hierarchy
			TableRow contentRow = new TableRow();

			contentRow.Controls.Add(GetVerticalSeparator());

			contentRow.Controls.Add(LeftPane);
			TableCell leftToContentSeparator = GetVerticalSeparator();
			contentRow.Controls.Add(leftToContentSeparator);

			if (LeftPaneTemplate != null)
			{
				leftPaneContainer = new DesktopPanesTemplate(this);
				LeftPaneTemplate.InstantiateIn(leftPaneContainer);
				LeftPane.Controls.AddAt(0, leftPaneContainer);
				LeftPane.Controls.AddAt(1, GetHorizontalSeparator());
			}

			if (ContentPaneTemplate != null)
			{
				contentPaneContainer = new DesktopPanesTemplate(this);
				ContentPaneTemplate.InstantiateIn(contentPaneContainer);
				ContentPane.Controls.AddAt(0, contentPaneContainer);
				ContentPane.Controls.AddAt(1, GetHorizontalSeparator());
			}
			contentRow.Controls.Add(ContentPane);

			TableCell contentToRightSeparator = GetVerticalSeparator();
			contentRow.Controls.Add(contentToRightSeparator);
			contentRow.Controls.Add(RightPane);

			if (RightPaneTemplate != null)
			{
				rightPaneContainer = new DesktopPanesTemplate(this);
				RightPaneTemplate.InstantiateIn(rightPaneContainer);
				RightPane.Controls.AddAt(0, rightPaneContainer);
				RightPane.Controls.AddAt(1, GetHorizontalSeparator());

			}
            contentRow.Controls.Add(GetVerticalSeparator());

			table.Controls.Add(contentRow);
		}    

		/// <summary>
		/// Returns a reference to Horizontal separator
		/// </summary>
		/// <returns></returns>
		protected Control GetHorizontalSeparator()
		{
			if (HorizontalSeparatorTemplate != null)
			{
				horizontalSeparatorContainer = new DesktopPanesTemplate(this);
				HorizontalSeparatorTemplate.InstantiateIn(horizontalSeparatorContainer);
				return horizontalSeparatorContainer;
			}
			else
				return new Control();		
		}

		/// <summary>
		/// Returns a reference to Vertical separator
		/// </summary>
		/// <returns></returns>
		protected TableCell GetVerticalSeparator()
		{
			TableCell tc = new TableCell();

			if (VerticalSeparatorTemplate != null)
			{
				verticalSeparatorContainer = new DesktopPanesTemplate(this);
				VerticalSeparatorTemplate.InstantiateIn(verticalSeparatorContainer);
				tc.Controls.Add(verticalSeparatorContainer);
			}
			return tc;		
		}
	}
}