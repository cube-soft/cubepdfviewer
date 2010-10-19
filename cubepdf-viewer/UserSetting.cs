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
using System.Drawing;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// Navigaion
    /* --------------------------------------------------------------------- */
    public enum NavigationCondition {
        None = 0x00, Thumbnail = 0x01, Bookmark = 0x02
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
            pos_.X = (int)registry.GetValue(REG_X, 30);
            pos_.Y = (int)registry.GetValue(REG_Y, 30);
            size_.Width = (int)registry.GetValue(REG_WIDTH, 0);
            size_.Height = (int)registry.GetValue(REG_HEIGHT, 0);
            thumb_width_ = (int)registry.GetValue(REG_THUMBWIDTH, 0);
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
                registry.SetValue(REG_X, pos_.X);
                registry.SetValue(REG_Y, pos_.Y);
                registry.SetValue(REG_WIDTH, size_.Width);
                registry.SetValue(REG_HEIGHT, size_.Height);
                registry.SetValue(REG_THUMBWIDTH, thumb_width_);
                int x = menu_ ? 1 : 0;
                registry.SetValue(REG_MENU, x);
                x = adobe_ ? 1 : 0;
                registry.SetValue(REG_ADOBE, x);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
            }
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
        /// Position
        /* ----------------------------------------------------------------- */
        public Point Position {
            get { return pos_; }
            set { pos_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Size
        /* ----------------------------------------------------------------- */
        public Size Size {
            get { return size_; }
            set { size_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// ThumbWidth
        /* ----------------------------------------------------------------- */
        public int ThumbWidth {
            get { return thumb_width_; }
            set { thumb_width_ = value; }
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
        private Point pos_ = new Point(0, 0);
        private Size size_ = new Size(0, 0);
        private int thumb_width_ = 0;
        private bool menu_ = true;
        private bool adobe_ = true;
        #endregion

        /* ----------------------------------------------------------------- */
        //  レジストリの名前の定義
        /* ----------------------------------------------------------------- */
        #region Registry settings
        private static string REG_ROOT          = @"Software\CubeSoft\CubePDF Viewer";
        private static string REG_NAVIGATION    = "Navigation";
        private static string REG_FIT           = "Fit";
        private static string REG_X             = "X";
        private static string REG_Y             = "Y";
        private static string REG_WIDTH         = "Width";
        private static string REG_HEIGHT        = "Height";
        private static string REG_THUMBWIDTH    = "ThumbWidth";
        private static string REG_MENU          = "MenuWarnDialog";
        private static string REG_ADOBE         = "AdobeExtension";
        #endregion
    }
}
