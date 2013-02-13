using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Rainbow.Framework.Providers.RainbowSiteMapProvider {
	
	public abstract class RainbowSiteMapProvider : StaticSiteMapProvider {

        public abstract void ClearCache();

        public static void ClearAllRainbowSiteMapCaches() {
            // Removing Sitemap Cache
            foreach (SiteMapProvider siteMap in SiteMap.Providers) {
                if (siteMap is RainbowSiteMapProvider) {
                    ((RainbowSiteMapProvider)siteMap).ClearCache();
                }
            }
        }
    }
}
