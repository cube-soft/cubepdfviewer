/* ------------------------------------------------------------------------- */
/*
 *  CustomToolStripRenderer.cs
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
 *  Last-modified: Mon 04 Oct 2010 12:02:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// CustomToolStripRenderer
    /* --------------------------------------------------------------------- */
    class CustomToolStripRenderer : ToolStripProfessionalRenderer {

        /* --------------------------------------------------------------------- */
        /// OnRenderToolStripBorder
        /* --------------------------------------------------------------------- */
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
            // base.OnRenderToolStripBorder(e);
        }

        /* --------------------------------------------------------------------- */
        /// OnRenderButtonBackground
        /* --------------------------------------------------------------------- */
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            //base.OnRenderButtonBackground(e);

            var button = e.Item as ToolStripButton;
            if (button != null) {
                if (button.BackgroundImage != null) {
                    e.Graphics.DrawImage(button.BackgroundImage, new Point(0, 0));
                }
            }
        }

        /* --------------------------------------------------------------------- */
        /// OnRenderDropDownButtonBackground
        /* --------------------------------------------------------------------- */
        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
            base.OnRenderDropDownButtonBackground(e);
        }

        /* --------------------------------------------------------------------- */
        /// OnRenderSeparator
        /* --------------------------------------------------------------------- */
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            //base.OnRenderSeparator(e);

            var separator = e.Item as ToolStripSeparator;
            if (separator != null) {
                var pen = new Pen(Color.Black);
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, 1, separator.Height));
            }
        }
    }
}
