/* ------------------------------------------------------------------------- */
/*
 *  UserSetting.cs
 *
 *  Copyright (c) 2010 CubeSoft Inc. All rights reserved.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see < http://www.gnu.org/licenses/ >.
 *
 *  Last-modified: Tue 05 Oct 2010 12:24:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Text;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// Navigaion
    /* --------------------------------------------------------------------- */
    public enum NavigationCondition {
        None = 0x00, Thumbnail = 0x01, Bookmark
    }

    /* --------------------------------------------------------------------- */
    /// UserSetting
    /* --------------------------------------------------------------------- */
    public class UserSetting {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public UserSetting() {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);
            navi_ = (NavigationCondition)registry.GetValue(REG_NAVIGATION, NavigationCondition.Thumbnail);
            fit_ = (FitCondition)registry.GetValue(REG_FIT, FitCondition.Height);
            menu_ = ((int)registry.GetValue(REG_MENU, 1) != 0);
            adobe_ = ((int)registry.GetValue(REG_ADOBE, 1) != 0);
        }

        /* ----------------------------------------------------------------- */
        /// Save
        /* ----------------------------------------------------------------- */
        public void Save() {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);
            try {
                registry.SetValue(REG_NAVIGATION, (int)navi_);
                registry.SetValue(REG_FIT, (int)fit_);
                int x = menu_ ? 1 : 0;
                registry.SetValue(REG_MENU, x);
                x = adobe_ ? 1 : 0;
                registry.SetValue(REG_ADOBE, x);
            }
            catch (Exception /* err */) { }
        }

        /* ----------------------------------------------------------------- */
        /// Navigation
        /* ----------------------------------------------------------------- */
        public NavigationCondition Navigaion {
            get { return navi_; }
            set { navi_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// FitCondition
        /* ----------------------------------------------------------------- */
        public FitCondition Fit {
            get { return fit_; }
            set { fit_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// ShowMenuWarning
        /* ----------------------------------------------------------------- */
        public bool ShowMenuInfo {
            get { return menu_; }
            set { menu_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// UseAdobeExtension
        /* ----------------------------------------------------------------- */
        public bool UseAdobeExtension {
            get { return adobe_; }
            set { adobe_ = value; }
        }

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private NavigationCondition navi_ = NavigationCondition.Thumbnail;
        private FitCondition fit_ = FitCondition.Height;
        private bool menu_ = true;
        private bool adobe_ = true;
        #endregion

        /* ----------------------------------------------------------------- */
        //  レジストリの名前の定義
        /* ----------------------------------------------------------------- */
        #region Registry settings
        private static string REG_ROOT = @"Software\CubeSoft\CubePDF Viewer";
        private static string REG_NAVIGATION = "Navigation";
        private static string REG_FIT = "Fit";
        private static string REG_MENU = "ShowMenuInfo";
        private static string REG_ADOBE = "AdobeExtension";
        #endregion
    }
}
