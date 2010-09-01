/* ------------------------------------------------------------------------- */
/*
 *  TabPolicy.cs
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
 *  Last-modified: Wed 01 Sep 2010 00:10:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;
using Canvas = System.Windows.Forms.PictureBox;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// TabPolicy
    /* --------------------------------------------------------------------- */
    public abstract class TabPolicy {
        /* ----------------------------------------------------------------- */
        /// Create
        /* ----------------------------------------------------------------- */
        public static TabPage Create(TabControl parent) {
            var tab = new TabPage();

            // TabPage の設定
            tab.AutoScroll = true;
            tab.VerticalScroll.SmallChange = 3;
            tab.HorizontalScroll.SmallChange = 3;
            tab.BackColor = Color.DimGray;
            tab.BorderStyle = BorderStyle.Fixed3D;
            tab.ContextMenuStrip = new ContextMenuStrip();
            tab.Text = "(無題)";

            parent.Controls.Add(tab);
            parent.SelectedIndex = parent.TabCount - 1;

            return tab;
        }

        /* ----------------------------------------------------------------- */
        /// Destroy
        /* ----------------------------------------------------------------- */
        public static void Destroy(TabPage tab) {
            var parent = (TabControl)tab.Parent;
            CanvasPolicy.Destroy((Canvas)tab.Controls["Canvas"]);
            parent.TabPages.Remove(tab);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ContextMenu
        ///
        /// <summary>
        /// コンテキストメニューを設定する．
        /// TODO: コンテキストメニューから登録元である TabControl の
        /// オブジェクトへ辿る方法の調査．現状では，暫定的にコンテキスト
        /// メニューの Tag に TabControl のオブジェクトを設定しておく
        /// 事で対処している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void ContextMenu(TabControl parent) {
            var menu = new ContextMenuStrip();
            var elem = new ToolStripMenuItem();
            elem.Text = "閉じる";
            elem.Click += new EventHandler(CloseHandler);
            menu.Items.Add(elem);
            menu.Tag = parent; // 暫定
            parent.MouseDown += new MouseEventHandler(MouseDownHandler);
            parent.ContextMenuStrip = menu;

            foreach (TabPage child in parent.TabPages) {
                child.ContextMenuStrip = new ContextMenuStrip();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CloseHandler (private)
        /// 
        /// <summary>
        /// コンテキストメニューの「閉じる」が押された時のイベントハンドラ．
        /// TODO: 「閉じる」が押されたときの座標を取得する方法．現状では，
        /// MouseDown イベントにもハンドラを設定しておき，その時の座標から
        /// 判断している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void CloseHandler(object sender, EventArgs e) {
            var item = (ToolStripMenuItem)sender;
            var menu = (ContextMenuStrip)item.Owner;
            var control = (TabControl)menu.Tag;
            if (control.TabCount <= 1) return;

            for (int i = 0; i < control.TabCount; i++) {
                var rect = control.GetTabRect(i);
                if (position_.X > rect.Left && position_.X < rect.Right &&
                    position_.Y > rect.Top && position_.Y < rect.Bottom) {
                    TabPolicy.Destroy(control.TabPages[i]);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// MouseDownHandler (private)
        /* ----------------------------------------------------------------- */
        private static void MouseDownHandler(object sender, MouseEventArgs e) {
            position_ = e.Location;
        }

        #region Variables
        private static Point position_;
        #endregion
    }
}
