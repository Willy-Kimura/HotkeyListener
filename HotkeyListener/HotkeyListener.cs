#region README

/*
 * Developer    : Willy Kimura (WK).
 * Library      : HotkeyListener.
 * License      : MIT.
 * 
 * I've had the privilege of building "pervasive" Desktop 
 * applications for products of my own. However, one of the 
 * key features required in most of them was the ability to 
 * invoke features whenever a user triggered a certain key or 
 * combination of keys. After looking around, I found one really 
 * functional library, "SmartHotkey", and it worked really well. 
 * However, there was a need for some few additional features 
 * in my products which led me to rebuilding the project and 
 * improving it even further. And thus came "HotkeyListener". 
 * 
 * This project combines two open-source libraries:
 * 
 *  (1) SmartHotKey: https://www.codeproject.com/Articles/100199/Smart-Hotkey-Handler-NET
 *  (2) HotkeySelection Control: https://www.codeproject.com/Articles/15085/A-simple-hotkey-selection-control-for-NET
 *  
 *  I've added some few improvements such as:
 *  
 *  (1) Ability to fetch source application info from where a hotkey is triggered.
 *  (2) Ability to enable any Windows control to provide Hotkey selection features.
 * 
 * Improvements are welcome.
 * 
 */

#endregion


using System;
using System.Linq;
using System.Collections.Generic;

using WK.Libraries.HotkeyListenerNS.Models;
using WK.Libraries.HotkeyListenerNS.Helpers;

namespace WK.Libraries.HotkeyListenerNS
{
    /// <summary>
    /// A library that provides support for registering and 
    /// attaching events to global hotkeys in .NET applications.
    /// </summary>
    public class HotkeyListener
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyListener"/> class.
        /// </summary>
        public HotkeyListener()
        {
            SetDefaults();
        }

        #endregion

        #region Fields

        // This is the handle that will be used to register, 
        // unregister, and listen to the hotkey triggers.
        private HotkeyHandle _handle = new HotkeyHandle();

        /// <summary>
        /// Saves the list of hotkeys suspended.
        /// </summary>
        private Dictionary<int, string> _suspendedKeys = 
            new Dictionary<int, string>();

        #endregion

        #region Properties

        #region Public

        /// <summary>
        /// Gets a value indicating whether hotkeys have been suspended.
        /// </summary>
        public bool HotkeysSuspended { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Adds a hotkey to the global Key watcher.
        /// </summary>
        /// <param name="strHotKey">The hotkey to add.</param>
        public bool AddHotkey(string hotkey)
        {
            return _handle.AddKey(hotkey);
        }

        /// <summary>
        /// Adds a list of hotkeys to the global Key watcher.
        /// </summary>
        /// <param name="hotkeys">The hotkeys to add.</param>
        public void AddHotkeys(string[] hotkeys)
        {
            foreach (string key in hotkeys)
            {
                AddHotkey(key);
            }
        }

        /// <summary>
        /// Removes any specific hotkey from the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The hotkey to remove.</param>
        public void RemoveHotkey(string hotkey)
        {
            _handle.RemoveKey(hotkey);
        }

        /// <summary>
        /// Remove all registered hotkeys from the global Key watcher.
        /// </summary>
        public void RemoveAllHotkeys()
        {
            _handle.RemoveAllKeys();
        }

        /// <summary>
        /// Suspends the hotkey(s) set from the global Key watcher.
        /// </summary>
        public void SuspendHotkeys()
        {
            if (!HotkeysSuspended)
            {
                foreach (var item in _handle.Hotkeys)
                {
                    _suspendedKeys.Add(item.Key, item.Value);
                }

                foreach (var key in _handle.Hotkeys.Values.ToList())
                {
                    RemoveHotkey(key);
                }

                HotkeysSuspended = true;
            }
        }

        /// <summary>
        /// Resumes using the hotkey(s) set in the global Key watcher.
        /// </summary>
        public void ResumeHotkeys()
        {
            if (HotkeysSuspended)
            {
                foreach (var key in _suspendedKeys.Values.ToList())
                {
                    AddHotkey(key);
                }
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Applies the library-default options & settings.
        /// </summary>
        private void SetDefaults()
        {
            AttachEvents();
        }

        /// <summary>
        /// Attaches the major hotkey events 
        /// to the Hotkey Listener.
        /// </summary>
        private void AttachEvents()
        {
            _handle.HotkeyPressed += (s, e) =>
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
                        Hotkey = e.Hotkey,
                        SourceApplication = new SourceApplication(
                        SourceAttributes.GetID(),
                        SourceAttributes.GetHandle(),
                        SourceAttributes.GetName(),
                        SourceAttributes.GetTitle(),
                        SourceAttributes.GetPath())
                    });
            };
        }

        #endregion

        #endregion

        #region Events

        #region Public

        /// <summary>
        /// Raised whenever a registered Hotkey is pressed.
        /// </summary>
        public event HotkeyEventHandler HotkeyPressed;
    
        /// <summary>
        /// Represents the method that will handle a <see cref="HotkeyPressed"/> 
        /// event that has no event data.
        /// </summary>
        /// <param name="sender">The hotkey sender object.</param>
        /// <param name="e">The <see cref="HotkeyEventArgs"/> data.</param>
        public delegate void HotkeyEventHandler(object sender, HotkeyEventArgs e);

        #endregion

        #endregion
    }

    /// <summary>
    /// Provides data for the <see cref="HotkeyListener.HotkeyPressed"/> event.
    /// </summary>
    public class HotkeyEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyEventArgs"/> class.
        /// </summary>
        public HotkeyEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyEventArgs"/> class.
        /// </summary>
        /// <param name="source">
        /// The source application from where 
        /// the Hotkey was triggered.
        /// </param>
        public HotkeyEventArgs(SourceApplication source)
        {
            SourceApplication = new SourceApplication(
                source.ID, source.Handle, source.Name,
                source.Title, source.Path);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the hotkey that was pressed.
        /// </summary>
        public string Hotkey { get; internal set; }

        /// <summary>
        /// Gets the details of the source application 
        /// from where the hotkey was triggered.
        /// </summary>
        public SourceApplication SourceApplication { get; internal set; }

        #endregion
    }
}