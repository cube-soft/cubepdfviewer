/* ------------------------------------------------------------------------- */
/*
 *  Program.cs
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
 *  Last-modified: Thu 07 Oct 2010 23:26:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cube {
    static class Program {
        /* ----------------------------------------------------------------- */
        ///
        /// Main
        /// 
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [STAThread]
        static void Main(string[] args) {
            Process proc = Process.GetCurrentProcess();

            // 多重起動時の処理
            // NOTE: デバッガ上で起動した場合、通常起動とはプロセス名が違う。
            // そのため、デバッガ上でテストする方法が今の所不明
            if (Process.GetProcessesByName(proc.ProcessName).Length > 1) {
                if (args.Length > 0) {
                    var prevHwnd = FindPrevProcess(proc);
                    var msg = args[0];
                    var cds = new COPYDATASTRUCT();
                    cds.dwData = IntPtr.Zero;
                    cds.lpData = msg;
                    cds.cbData = System.Text.Encoding.Default.GetBytes(msg).Length + 1;
                    SendMessage(prevHwnd, WM_COPYDATA, IntPtr.Zero, ref cds);
                }
            }
            else {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length > 0) Application.Run(new MainForm(args[0]));
                else Application.Run(new MainForm());
            }
        }

        /* ----------------------------------------------------------------- */
        /// FindPrevProcess
        /* ----------------------------------------------------------------- */
        private static IntPtr FindPrevProcess(Process proc) {
            Process[] processes = Process.GetProcessesByName(proc.ProcessName);
            int iThisProcessId = proc.Id;

            foreach (Process item in processes) {
                if (item.Id != iThisProcessId) {
                    if (IsIconic(item.MainWindowHandle)) ShowWindow(item.MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(item.MainWindowHandle);
                    return item.MainWindowHandle;
                }
            }
            return IntPtr.Zero;
        }

        [DllImport("User32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        public const int WM_COPYDATA = 0x0000004A;
        public const int WM_USER = 0x400;

        //COPYDATASTRUCT構造体
        public struct COPYDATASTRUCT {
            public IntPtr dwData;    // 送信する32ビット値
            public int cbData;　　   // lpDataのバイト数
            public string lpData;　  // 送信するデータへのポインタ(0も可能)
        }
        
        private const int SW_RESTORE = 9;
    }
}
