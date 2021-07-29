#region Copyright

/*
 * Developer    : Willy Kimura (WK).
 * Library      : HotkeyListener.
 * License      : MIT.
 * 
 */

#endregion


using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WK.Libraries.HotkeyListenerNS.Helpers
{
    /// <summary>
    /// Gathers the list of application attributes 
    /// derived from the currently active application.
    /// </summary>
    internal static class SourceAttributes
    {
        #region Fields

        private static string _executableName = string.Empty;
        private static string _executablePath = string.Empty;

        // We will use this to get the selected 
        // text from any active application.
        internal static TextSelectionReader _reader = new TextSelectionReader();

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Gets the source application's process ID.
        /// </summary>
        public static int GetID()
        {
            int processID = 1;
            GetWindowThreadProcessId(GetForegroundWindow().ToInt32(), out processID);

            return processID;
        }

        /// <summary>
        /// Gets the source application's Window Handle.
        /// </summary>
        public static IntPtr GetHandle()
        {
            return GetForegroundWindow();
        }

        /// <summary>
        /// Gets the source application's executable name.
        /// </summary>
        public static string GetName()
        {
            try
            {
                int hwnd = 0;
                hwnd = GetForegroundWindow().ToInt32();

                _executablePath = Process.GetProcessById(GetID()).MainModule.FileName;
                _executableName = _executablePath.Substring(_executablePath.LastIndexOf(@"\") + 1);
            }
            catch (Exception) { }

            return _executableName;
        }

        /// <summary>
        /// Gets the source application's executable path.
        /// </summary>
        public static string GetPath()
        {
            return _executablePath;
        }

        /// <summary>
        /// Gets the source application's window title text.
        /// </summary>
        public static string GetTitle()
        {
            const int capacity = 256;
            StringBuilder content = null;
            IntPtr handle = IntPtr.Zero;

            try
            {
                content = new StringBuilder(capacity);
                handle = GetForegroundWindow(); // windowHandle;
            }
            catch (Exception) { }

            if (GetWindowText(handle, content, capacity) > 0)
                return content.ToString();

            return null;
        }

        /// <summary>
        /// Gets the currently selected text in the active application.
        /// </summary>
        /// <returns>The selected text, if any.</returns>
        public static string GetSelection()
        {
            try
            {
                string app = GetName().ToLower();
                string selection = _reader.TryGetSelectedTextFromActiveControl();

                if (app == "chrome.exe" || app == "firefox.exe")
                    selection = _reader.GetTextViaClipboard();

                if (!string.IsNullOrWhiteSpace(selection))
                    return selection;
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        #region Private

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindowPtr();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        #endregion

        #endregion
    }
}