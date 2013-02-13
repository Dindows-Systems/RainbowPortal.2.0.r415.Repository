using System;
using Rainbow.Framework.Web.UI.WebControls;
using History=Rainbow.Framework.History;

namespace Rainbow.Content.Web.Modules
{
    /// <summary>
    /// PageKeyPhrase is a module that uses the PageKeyPhrase Control to allow users to position the menu keyphrase without the need to touch the layout.
    /// </summary>
    [History("John.Mandia@whitelightsolutions.com", "2003/10/25", "Created module and the control")]
    public class PageKeyPhraseModule : PortalModuleControl
    {
        protected PageKeyPhrase _thisTabKeyPhrase;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageKeyPhraseModule"/> class.
        /// </summary>
        public PageKeyPhraseModule()
        {
        }

        /// <summary>
        /// Handles the Load event of the PageKeyPhraseModule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PageKeyPhraseModule_Load(object sender, EventArgs e)
        {
            //_thisTabKeyPhrase.DataBind();
        }

        #region General Implementation

        /// <summary>
        /// GuidID
        /// </summary>
        /// <value></value>
        public override Guid GuidID
        {
            get { return new Guid("{ED58A5FB-D041-4a4b-826E-654250B61E7C}"); }
        }

        #endregion

        #region Web Form Designer generated code

        /// <summary>
        /// Raises Init event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(this.PageKeyPhraseModule_Load);
            base.OnInit(e);
        }

        #endregion
    }
}