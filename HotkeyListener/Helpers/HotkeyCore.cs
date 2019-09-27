using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WK.Libraries.HotkeyListenerNS.Helpers
{
    /// <summary>
    /// The core class that helps in the management  
    /// of system-wide Hotkeys within applications.
    /// </summary>
    internal sealed class HotkeyCore
    {
        #region Enumerations

        /// <summary>
        /// Provides a list of supported Hotkey modifiers.
        /// </summary>
        [Flags]
        internal enum HotKeyModifiers
        {
            /// <summary>
            /// Represents no key.
            /// </summary>
            None = 0,

            /// <summary>
            /// The <see cref="Alt"/> modifier key.
            /// </summary>
            Alt = 1,

            /// <summary>
            /// The <see cref="Control"/> modifier key.
            /// </summary>
            Control = 2,

            /// <summary>
            /// The <see cref="Shift"/> modifier key.
            /// </summary>
            Shift = 4,

            /// <summary>
            /// The <see cref="Windows"/> key.
            /// </summary>
            Windows = 8
        }

        #endregion

        #region Methods

        #region Private

        #region Win32 Management

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern ushort GlobalDeleteAtom(int nAtom);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int keyId, HotKeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Hotkey Management

        /// <summary>
        /// Checks whether a specified Hotkey modifier is 
        /// available within a provided Hotkey.
        /// </summary>
        /// <param name="key">The key(s).</param>
        /// <param name="keyModifier">The key modifier from <see cref="Keys"/> enumeration.</param>
        /// <param name="hotkeyModifier">The Hotkey modifier.</param>
        /// <returns>Matched Hotkeymodifier</returns>
        private static HotKeyModifiers CheckModifier(Keys key, Keys keyModifier, HotKeyModifiers hotkeyModifier)
        {
            if ((key & keyModifier) == keyModifier)
            {
                return hotkeyModifier;
            }

            return HotKeyModifiers.None;
        }

        #endregion

        #endregion

        #region Public

        /// <summary>
        /// Parses and registers any provided Hotkey.
        /// </summary>
        /// <param name="handle">A window handle for the Hotkey event receiver.</param>
        /// <param name="hotkeyID">The global atom ID for the given Hotkey.</param>
        /// <param name="strKey">The valid Hotkey.</param>
        public static bool RegisterKey(Control handle, int hotkeyID, string strKey)
        {
            if (strKey == null)
                strKey = "";

            Keys key = Keys.None;

            try
            {
                if (string.IsNullOrEmpty(strKey))
                    throw new Exception("No Hotkey registered.");
                else
                {
                    string[] strKeys = strKey.Split(new char[] { '+' });
                    bool isFirst = true;

                    foreach (string strKeyItem in strKeys)
                    {
                        if (string.IsNullOrEmpty(strKeyItem))
                            throw new Exception("Please provide a valid Hotkey.");

                        if (isFirst)
                        {
                            key = (Keys)Enum.Parse(typeof(Keys), strKeyItem.Trim());
                            isFirst = false;
                        }
                        else
                            key |= (Keys)Enum.Parse(typeof(Keys), strKeyItem.Trim());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            if (key == Keys.None)
                return false;

            return RegisterKey(handle, hotkeyID, key);
        }

        /// <summary>
        /// Registers any provided Hotkey.
        /// </summary>
        /// <param name="handle">A window handle for the Hotkey event receiver.</param>
        /// <param name="hotkeyID">The global atom ID for the given Hotkey.</param>
        /// <param name="key">The valid Hotkey.</param>
        public static bool RegisterKey(Control handle, int hotkeyID, Keys key)
        {
            HotKeyModifiers keyModifier = HotKeyModifiers.None;

            keyModifier |= CheckModifier(key, Keys.Control, HotKeyModifiers.Control);
            keyModifier |= CheckModifier(key, Keys.Alt, HotKeyModifiers.Alt);
            keyModifier |= CheckModifier(key, Keys.Shift, HotKeyModifiers.Shift);
            keyModifier |= CheckModifier(key, Keys.LWin, HotKeyModifiers.Windows);
            keyModifier |= CheckModifier(key, Keys.RWin, HotKeyModifiers.Windows);

            return RegisterHotKey(handle.Handle, hotkeyID, keyModifier, key & Keys.KeyCode);
        }

        /// <summary>
        /// Unregisters any already active Hotkey.
        /// </summary>
        /// <param name="handle">A window handle for the Hotkey event receiver.</param>
        /// <param name="hotkeyID">A global atom ID for the given Hotkey.</param>
        public static bool UnregisterKey(Control handle, int hotkeyID)
        {
            return UnregisterHotKey(handle.Handle, hotkeyID);
        }

        #endregion

        #endregion
    }
}