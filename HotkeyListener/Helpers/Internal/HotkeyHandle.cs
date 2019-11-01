#region Copyright

/*
 * Developer    : Willy Kimura (WK).
 * Library      : HotkeyListener.
 * License      : MIT.
 * 
 */

#endregion


using System;
using System.Windows.Forms;
using System.Collections.Generic;

using WK.Libraries.HotkeyListenerNS.Models;

namespace WK.Libraries.HotkeyListenerNS.Helpers
{
    /// <summary>
    /// Provides the base Hotkey handle for intercepting 
    /// and receiving all registered global Hotkey events.
    /// </summary>
    internal class HotkeyHandle : Control
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyHandle"/> class.
        /// </summary>
        public HotkeyHandle()
        {
            SuspendLayout();
            Visible = false;

            ResumeLayout(false);

            Hotkeys = new Dictionary<int, string>();
        }

        #endregion

        #region Fields

        private const int WM_HOTKEY = 0x312;
        public Dictionary<int, string> Hotkeys;

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Adds a Hotkey to the global Key watcher.
        /// </summary>
        /// <param name="strHotKey">The Hotkey string.</param>
        public bool AddKey(string hotkey)
        {
            if (!base.IsHandleCreated)
            {
                base.CreateControl();
            }

            return this.Register(hotkey);
        }

        /// <summary>
        /// Removes any specified Hotkey from the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The Hotkey to remove.</param>
        public void RemoveKey(string hotkey)
        {
            this.Unregister(hotkey);
        }

        /// <summary>
        /// Remove all the registered Hotkeys from the global Key watcher.
        /// </summary>
        public void RemoveAllKeys()
        {
            this.Unregister();
        }

        #endregion

        #region Private

        /// <summary>
        /// Validates and registers any given Hotkey.
        /// </summary>
        /// <param name="hotkey">The Hotkey string.</param>
        private bool Register(string hotkey)
        {
            this.Unregister(hotkey);

            int hotKeyId = HotkeyCore.GlobalAddAtom("RE:" + hotkey);

            if (hotKeyId == 0)
                throw new Exception(
                    string.Format(
                        "Could not register atom for {0} Hotkey. " +
                        "Please try another Hotkey.", hotkey));

            if (HotkeyCore.RegisterKey(this, hotKeyId, hotkey))
            {
                Hotkeys.Add(hotKeyId, hotkey);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Unregisters any registered Hotkeys.
        /// </summary>
        /// <param name="hotkey">The registered Hotkey.</param>
        private void Unregister(string hotkey)
        {
            int intKey = 0;

            foreach (KeyValuePair<int, string> hotKey in Hotkeys)
            {
                if (hotKey.Value == hotkey)
                {
                    intKey = hotKey.Key;

                    HotkeyCore.UnregisterKey(this, hotKey.Key);
                    HotkeyCore.GlobalDeleteAtom(hotKey.Key);

                    break;
                }
            }

            if (intKey > 0)
                Hotkeys.Remove(intKey);
        }

        /// <summary>
        /// Unregisters all the registered Hotkeys.
        /// </summary>
        private void Unregister()
        {
            foreach (KeyValuePair<int, string> hotKey in Hotkeys)
            {
                HotkeyCore.UnregisterKey(this, hotKey.Key);
                HotkeyCore.GlobalDeleteAtom(hotKey.Key);
            }

            Hotkeys.Clear();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides the default window message processing 
        /// to detect the registered Hotkeys when pressed.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                HotkeyPressed?.Invoke(
                    new SourceApplication(
                        SourceAttributes.GetID(),
                        SourceAttributes.GetHandle(),
                        SourceAttributes.GetName(),
                        SourceAttributes.GetTitle(),
                        SourceAttributes.GetPath()),
                    new HotkeyEventArgs
                    {
                        Hotkey = this.Hotkeys[m.WParam.ToInt32()],
                        SourceApplication = new SourceApplication(
                        SourceAttributes.GetID(),
                        SourceAttributes.GetHandle(),
                        SourceAttributes.GetName(),
                        SourceAttributes.GetTitle(),
                        SourceAttributes.GetPath())
                    });
            }
            else
                base.WndProc(ref m);
        }

        #endregion

        #endregion

        #region Events

        #region Public

        /// <summary>
        /// Raised whenever a registered Hotkey is pressed.
        /// </summary>
        public event EventHandler<HotkeyEventArgs> HotkeyPressed;

        #endregion

        #endregion
    }
}