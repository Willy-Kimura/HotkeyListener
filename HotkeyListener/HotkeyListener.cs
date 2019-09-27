using System;

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
        // unregister, and listen to the Hotkey triggers.
        private HotkeyHandle _handle = new HotkeyHandle();

        #endregion

        #region Properties

        #region Browsable

        #endregion

        #region Non-browsable

        #endregion

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Adds a Hotkey to the global Key watcher.
        /// </summary>
        /// <param name="strHotKey">The Hotkey string.</param>
        public bool AddHotkey(string hotkey)
        {
            return _handle.AddKey(hotkey);
        }

        /// <summary>
        /// Removes any specified Hotkey from the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The Hotkey to remove.</param>
        public void RemoveHotkey(string hotkey)
        {
            _handle.RemoveKey(hotkey);
        }

        /// <summary>
        /// Remove all the registered Hotkeys from the global Key watcher.
        /// </summary>
        public void RemoveAllHotkeys()
        {
            _handle.RemoveAllKeys();
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
        /// Attaches the major Hotkey events 
        /// to the Hotkey listener.
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
        public event EventHandler<HotkeyEventArgs> HotkeyPressed;

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
        /// Gets the Hotkey that was pressed.
        /// </summary>
        public string Hotkey { get; internal set; }

        /// <summary>
        /// Gets the details of the source application 
        /// from where the Hotkey was triggered.
        /// </summary>
        public SourceApplication SourceApplication { get; internal set; }

        #endregion
    }
}