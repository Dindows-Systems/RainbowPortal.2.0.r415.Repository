// Esperantus - The Web translator
// Copyright (C) 2003 Emmanuele De Andreis
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Emmanuele De Andreis (manu-dea@hotmail dot it)

using System;
using System.Globalization;

namespace Rainbow.Framework.Web.UI.WebControls
{
    /// <summary>
    /// LanguageSwitcherEventArgs
    /// </summary>
    public class LanguageSwitcherEventArgs : EventArgs
    {
        private LanguageCultureItem cultureItem;

        public LanguageSwitcherEventArgs(CultureInfo uiCulture, CultureInfo culture) : base()
        {
            cultureItem = new LanguageCultureItem(uiCulture, culture);
        }

        public LanguageSwitcherEventArgs(LanguageCultureItem cultureItem) : base()
        {
            this.cultureItem = cultureItem;
        }

        /// <summary>
        /// Returns the language to change
        /// </summary>
        public LanguageCultureItem CultureItem
        {
            get { return cultureItem; }
        }
    }
}