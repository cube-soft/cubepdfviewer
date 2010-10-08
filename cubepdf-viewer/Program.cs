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
            Process hThisProcess = Process.GetCurrentProcess();
            var pn = hThisProcess.ProcessName;
            var mutex = new System.Threading.Mutex(false, pn);

            // 多重起動時の処理
            // NOTE: デバッガ上で起動した場合、通常起動とはプロセス名が違う。
            // そのため、デバッガ上でテストする方法が今の所不明
            if (mutex.WaitOne(0, false) == false) {
                if (args.Length > 0) {
                    var prevHwnd = FindPrevProcess();
                    var msg = args[0];
                    var cds = new COPYDATASTRUCT();
                    cds.dwData = 0;
                    cds.lpData = msg;
                    cds.cbData = System.Text.Encoding.Default.GetBytes(msg).Length + 1;
                    SendMessage(prevHwnd.ToInt32(), WM_COPYDATA, 0, ref cds);
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
        private static IntPtr FindPrevProcess() {
            Process hThisProcess = Process.GetCurrentProcess();
            Process[] hProcesses = Process.GetProcessesByName(hThisProcess.ProcessName);
            int iThisProcessId = hThisProcess.Id;

            foreach (Process hProcess in hProcesses) {
                if (hProcess.Id != iThisProcessId) {
                    // メインウインドウを最前面にする
                    ShowWindow(hProcess.MainWindowHandle, SW_NORMAL);
                    SetForegroundWindow(hProcess.MainWindowHandle);
                    return hProcess.MainWindowHandle;
                }
            }
            return IntPtr.Zero;
        }

        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(System.IntPtr hWnd, int nCmdShow);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern Int32 SendMessage(Int32 hWnd, Int32 Msg, Int32 wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern Int32 SendMessage(Int32 hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        public const Int32 WM_COPYDATA = 0x4A;
        public const Int32 WM_USER = 0x400;

        //COPYDATASTRUCT構造体
        public struct COPYDATASTRUCT {
            public Int32 dwData;    // 送信する32ビット値
            public Int32 cbData;　　// lpDataのバイト数
            public string lpData;　 // 送信するデータへのポインタ(0も可能)
        }

        private const int SW_NORMAL = 1;
    }
}
