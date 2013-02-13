using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Rainbow.Framework.Content;
using Rainbow.Framework.Content.ItemTypes;
using Rainbow.Framework.Content.Items;
using Rainbow.Framework.Content.Web;


namespace Rainbow.Framework.Content.Web.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Links runat=server></{0}:Links>")]
    public class Links : DataList
    {
        private Rainbow.Framework.Content.ItemTypes.Links _links;
        private long _moduleID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Links"/> class.
        /// </summary>
        /// <param name="ModuleID">The module ID.</param>
        public Links(long ModuleID)
        {
            _moduleID = ModuleID;
            _links = new Rainbow.Framework.Content.ItemTypes.Links(_moduleID);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return _links.Items.Count; }
        }

        /// <summary>
        /// Gets or sets the max links.
        /// </summary>
        /// <value>The max links.</value>
        public int MaxLinks
        {
            get 
            {
                throw new NotImplementedException();
            }
            set { }
        }


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        /// <summary>
        /// Gets or sets the source containing a list of values used to populate the items within the control.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Collections.IEnumerable"></see> or <see cref="T:System.ComponentModel.IListSource"></see> that contains a collection of values used to supply data to this control. The default value is null.</returns>
        /// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource"></see> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID"></see> property. </exception>
        public override object DataSource
        {
            get
            {
                return _links;
            }
            set
            {
                if (value is Rainbow.Framework.Content.ItemTypes.Links)
                    _links = (Rainbow.Framework.Content.ItemTypes.Links)value;
                else
                    throw new Exception("value must be of type 'Rainbow.Framework.Content.ItemTypes.Links'");
            }
        }


        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
    }
}
