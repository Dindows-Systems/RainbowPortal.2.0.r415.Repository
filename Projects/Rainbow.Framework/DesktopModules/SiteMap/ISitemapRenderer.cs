using System.Web.UI.WebControls;
using Rainbow.Framework.Web.UI.WebControls;

namespace Rainbow.Content.Web.Modules.Sitemap
{
	/// <summary>
	/// This defines an interface for a Sitemap renderer.
	/// </summary>
	public interface ISitemapRenderer
	{

        /// <summary>
        /// The Render interface function
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
		WebControl Render(SitemapItems list);
	}
}
