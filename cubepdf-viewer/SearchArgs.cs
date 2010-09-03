/* ------------------------------------------------------------------------- */
/*
 *  SearchArgs.cs
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
 *  Last-modified: Mon 02 Aug 2010 22:59:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;

namespace Cube {
    /* --------------------------------------------------------------------- */
    //  SearchArgs
    /* --------------------------------------------------------------------- */
    public class SearchArgs : EventArgs {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public SearchArgs() {
            text_ = "";
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public SearchArgs(string s) {
            text_ = s;
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  Text
        ///  
        ///  <summary>
        ///  検索キーワード
        ///  </summary>
        ///  
        /* ----------------------------------------------------------------- */
        public string Text {
            get { return text_; }
            set { text_ = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  FromBegin
        ///  
        ///  <summary>
        ///  先頭から検索するかどうか．true の場合は文書（または現在の
        ///  ページ）の先頭から，false の場合は現在の位置から検索を行う．
        ///  </summary>
        ///  
        /* ----------------------------------------------------------------- */
        public bool FromBegin {
            get { return position_; }
            set { position_ = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  WholeDocument
        ///  
        ///  <summary>
        ///  文書全体を検索範囲にするかどうか．true の場合は検索範囲は
        ///  文書全体，false の場合は検索範囲は現在のページ．
        ///  </summary>
        ///  
        /* ----------------------------------------------------------------- */
        public bool WholeDocument {
            get { return range_; }
            set { range_ = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  IgnoreCase
        ///  
        ///  <summary>
        ///  大文字・小文字を区別するか．
        ///  </summary>
        ///  
        /* ----------------------------------------------------------------- */
        public bool IgnoreCase {
            get { return ignore_case_; }
            set { ignore_case_ = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  FindNext
        ///  
        ///  <summary>
        ///  検索方法．true の場合は現在の位置よりも後の文章を検索範囲に，
        ///  false の場合は現在の位置よりも前の文章を検索範囲に指定する．
        ///  </summary>
        ///  
        /* ----------------------------------------------------------------- */
        public bool FindNext {
            get { return vector_; }
            set { vector_ = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        ///  WholeWord
        ///  
        ///  <summary>
        ///  完全一致したもののみを扱うかどうか．
        ///  </summary>
        ///  
        /* ----------------------------------------------------------------- */
        public bool WholeWord {
            get { return complete_; }
            set { complete_ = value; }
        }

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private string text_;
        private bool position_ = true;      // true: begin / false: the current position
        private bool range_ = true;         // true: whole document / false: the current page
        private bool ignore_case_ = true;   // true: ignore case / false: case sensitive
        private bool vector_ = true;        // true: find next / false: find previous
        private bool complete_ = true;      // true: completed match word
        #endregion
    }
}
