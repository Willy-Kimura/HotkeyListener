#region Copyright

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
 *  (2) Hotkey Selection Control: https://www.codeproject.com/Articles/15085/A-simple-hotkey-selection-control-for-NET
 *  
 * Improvements:
 *  
 *  (1) Provides a CRUD-like model for managing hotkeys.
 *  (2) Introduction of a Hotkey class that lets you easily register and manage hotkeys.
 *  (3) Ability to suspend and resume the list of hotkeys registered.
 *  (4) Ability to fetch source application info from where a hotkey is triggered.
 *  (5) Ability to enable any Windows control to provide Hotkey selection features.
 * 
 */

#endregion


using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using WK.Libraries.HotkeyListenerNS.Models;
using WK.Libraries.HotkeyListenerNS.Helpers;

namespace WK.Libraries.HotkeyListenerNS
{
    /// <summary>
    /// A library that provides support for registering and 
    /// attaching events to global hotkeys in .NET applications.
    /// </summary>
    [DebuggerStepThrough]
    public partial class HotkeyListener : Component
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyListener"/> class.
        /// </summary>
        public HotkeyListener()
        {
            SetDefaults();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyListener"/> class.
        /// </summary>
        public HotkeyListener(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            SetDefaults();
        }

        #endregion

        #region Fields

        // This is the handle that will be used to register, 
        // unregister, and listen to the hotkey triggers.
        internal static HotkeyHandle _handle = new HotkeyHandle();

        // Saves the list of hotkeys suspended.
        private List<string> _suspendedKeys = 
            new List<string>();

        // Saves the list of Form suspension actions.
        private Dictionary<Form, Action> _suspendedActions =
            new Dictionary<Form, Action>();
        
        // Saves the list of forms suspended.
        private List<Form> _suspendedForms = new List<Form>();

        // We will use this to convert keys into 
        // their respective string formats.
        private static HotkeySelector _selector = new HotkeySelector();

        #endregion

        #region Properties

        #region Public

        /// <summary>
        /// Gets a value determining whether the 
        /// hotkeys set have been suspended.
        /// </summary>
        public bool Suspended { get; private set; }
        
        #endregion

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Adds a hotkey to the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The hotkey to add.</param>
        /// <returns>
        /// True if successful or false if not. 
        /// Ensure you inform the user if the 
        /// hotkey fails to be registered. This 
        /// is mostly due to a hotkey being 
        /// already in use by another application.
        /// </returns>
        public bool Add(Hotkey hotkey)
        {
            if (hotkey.Modifiers == Keys.LWin || hotkey.Modifiers == Keys.RWin)
                return false;

            return Add(Convert(hotkey));
        }
    
        /// <summary>
        /// Adds a list of hotkeys to the global Key watcher.
        /// </summary>
        /// <param name="hotkeys">The hotkeys to add.</param>
        /// <returns>
        /// The list of hotkeys passed and their 
        /// results when trying to register them.
        /// Their results will each denote a true 
        /// if successful or false if not. 
        /// Ensure you inform the user if one 
        /// hotkey fails to be registered. This 
        /// is mostly due to a hotkey being 
        /// already in use by another application.
        /// </returns>
        public Dictionary<string, bool> Add(Hotkey[] hotkeys)
        {
            Dictionary<string, bool> keyValues = new Dictionary<string, bool>();

            foreach (var key in hotkeys)
            {
                keyValues.Add(key.ToString(), Add(key));
            }

            return keyValues;
        }

        /// <summary>
        /// Updates an existing hotkey 
        /// in the global Key watcher.
        /// </summary>
        /// <param name="currentHotkey">The hotkey to modify.</param>
        /// <param name="newHotkey">The new hotkey to be set.</param>
        public void Update(Hotkey currentHotkey, Hotkey newHotkey)
        {
            Update(currentHotkey.ToString(), newHotkey.ToString());

            HotkeyUpdated?.Invoke(this, new HotkeyUpdatedEventArgs(currentHotkey, newHotkey));
        }

        /// <summary>
        /// Updates an existing hotkey 
        /// in the global Key watcher.
        /// </summary>
        /// <param name="currentHotkey">
        /// A reference to the variable 
        /// containing the hotkey to modify.
        /// </param>
        /// <param name="newHotkey">
        /// The new hotkey to be set.
        /// </param>
        public void Update(ref Hotkey currentHotkey, Hotkey newHotkey)
        {
            currentHotkey = newHotkey;

            Update(currentHotkey.ToString(), newHotkey.ToString());

            HotkeyUpdated?.Invoke(this, new HotkeyUpdatedEventArgs(currentHotkey, newHotkey));
        }

        /// <summary>
        /// Updates an existing hotkey 
        /// in the global Key watcher.
        /// </summary>
        /// <param name="currentHotkey">
        /// A reference to the variable 
        /// containing the hotkey to modify.
        /// </param>
        /// <param name="newHotkey">
        /// A reference to the variable containing 
        /// the new hotkey to be set.
        /// </param>
        public void Update(ref Hotkey currentHotkey, ref Hotkey newHotkey)
        {
            currentHotkey = newHotkey;

            Update(currentHotkey.ToString(), newHotkey.ToString());

            HotkeyUpdated?.Invoke(this, new HotkeyUpdatedEventArgs(currentHotkey, newHotkey));
        }

        /// <summary>
        /// Removes any specific hotkey 
        /// from the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The hotkey to remove.</param>
        public void Remove(Hotkey hotkey)
        {
            _handle.RemoveKey(Convert(hotkey));
        }

        /// <summary>
        /// Removes a list of hotkeys from 
        /// the global Key watcher.
        /// </summary>
        /// <param name="hotkeys">The hotkeys to remove.</param>
        public void Remove(Hotkey[] hotkeys)
        {
            foreach (var key in hotkeys)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// Remove all the registered hotkeys 
        /// from the global Key watcher.
        /// </summary>
        public void RemoveAll()
        {
            _handle.RemoveAllKeys();
        }

        /// <summary>
        /// Suspends the hotkey(s) set 
        /// in the global Key watcher.
        /// </summary>
        public void Suspend()
        {
            if (!Suspended)
            {
                _suspendedKeys.Clear();

                foreach (var item in _handle.Hotkeys)
                {
                    _suspendedKeys.Add(item.Value);
                }

                foreach (var key in _handle.Hotkeys.Values.ToList())
                {
                    Remove(key);
                }

                Suspended = true;
            }
        }
    
        /// <summary>
        /// Suspends the hotkey(s) set whenever a particular Form is active. 
        /// This is useful in Forms where the user requires modifying certain 
        /// hotkeys without triggering them when active.
        /// </summary>
        /// <param name="form">
        /// The Form to suspend listening to hotkeys when active.
        /// </param>
        /// <param name="onDeactivate">
        /// The action to be called when the Form has been deactivated.
        /// </param>
        public void SuspendOn(Form form, Action onDeactivate = null)
        {
            try
            {
                form.Activated += OnActivateForm;
                form.Deactivate += OnDeactivateForm;

                if (onDeactivate != null)
                    _suspendedActions.Add(form, onDeactivate);
                
                _suspendedForms.Add(form);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Suspends the hotkey(s) set whenever a list of Forms are active. 
        /// This is useful in Forms where the user requires modifying certain 
        /// hotkeys without triggering them when active.
        /// </summary>
        /// <param name="forms">
        /// The Forms to suspend listening to hotkeys when active.
        /// </param>
        /// <param name="onDeactivate">
        /// The actions to be called respectively 
        /// when each Form has been deactivated.
        /// </param>
        public void SuspendOn(Form[] forms, Action[] onDeactivate = null)
        {
            try
            {
                for (int i = 0; i < forms.Length; i++)
                {
                    SuspendOn(forms[i], onDeactivate[i]);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Releases a Form from suspending hotkeys when active.
        /// </summary>
        /// <param name="form">
        /// The Form to resume to listening to hotkeys when active.
        /// </param>
        public void ResumeOn(Form form)
        {
            try
            {
                if (_suspendedForms != null)
                {
                    foreach (var addedForm in _suspendedForms)
                    {
                        if (addedForm.GetHashCode() == form.GetHashCode())
                        {
                            _suspendedForms.Remove(addedForm);

                            addedForm.Activated -= OnActivateForm;
                            addedForm.Deactivate -= OnDeactivateForm;

                            if (_suspendedActions.ContainsKey(addedForm))
                                _suspendedActions.Remove(addedForm);
                        }
                    }
                }

                Suspended = false;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Releases a list of Forms from suspending hotkeys when active.
        /// </summary>
        /// <param name="forms">
        /// The Forms to resume to listening to hotkeys when active.
        /// </param>
        public void ResumeOn(Form[] forms)
        {
            try
            {
                foreach (var form in forms)
                {
                    ResumeOn(form);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Resumes using the hotkey(s) that 
        /// were set in the global Key watcher.
        /// </summary>
        public void Resume()
        {
            if (Suspended)
            {
                foreach (var key in _suspendedKeys.ToList())
                {
                    if (!_handle.Hotkeys.ContainsValue(key))
                    {
                        Add(key);
                    }
                }

                Suspended = false;
            }
        }

        /// <summary>
        /// [Special] Converts a hotkey string to its variant <see cref="Hotkey"/> object.
        /// </summary>
        public static Hotkey Convert(string hotkey)
        {
            Keys keyCode = Keys.None;
            Keys modifiers = Keys.None;

            hotkey = hotkey.Replace(" ", "");
            hotkey = hotkey.Replace(",", "");
            hotkey = hotkey.Replace("+", "");

            if (hotkey.Contains("Control"))
            {
                modifiers |= Keys.Control;
                hotkey = hotkey.Replace("Control", "");
            }

            if (hotkey.Contains("Shift"))
            {
                modifiers |= Keys.Shift;
                hotkey = hotkey.Replace("Shift", "");
            }

            if (hotkey.Contains("Alt"))
            {
                modifiers |= Keys.Alt;
                hotkey = hotkey.Replace("Alt", "");
            }

            keyCode = (Keys)Enum.Parse(typeof(Keys), hotkey, true);
            
            return new Hotkey(modifiers, keyCode);
        }
    
        /// <summary>
        /// [Special] Converts keys or key combinations to their string types.
        /// </summary>
        /// <param name="hotkey">The hotkey to convert.</param>
        public static string Convert(Hotkey hotkey)
        {
            return _selector.Convert(hotkey);
        }

        /// <summary>
        /// [Special] Gets the currently selected text in the active application.
        /// </summary>
        /// <returns>The selected text, if any.</returns>
        public static string GetSelection()
        {
            return SourceAttributes.GetSelection();
        }

        #endregion

        #region Private

        /// <summary>
        /// Adds a hotkey to the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The hotkey to add.</param>
        private bool Add(string hotkey)
        {
            return _handle.AddKey(hotkey);
        }

        /// <summary>
        /// Adds a list of hotkeys to the global Key watcher.
        /// </summary>
        /// <param name="hotkeys">The hotkeys to add.</param>
        private void Add(string[] hotkeys)
        {
            foreach (string key in hotkeys)
            {
                Add(key);
            }
        }

        /// <summary>
        /// Updates an existing hotkey 
        /// in the global Key watcher.
        /// </summary>
        /// <param name="currentHotkey">The hotkey to modify.</param>
        /// <param name="newHotkey">The new hotkey to be set.</param>
        private void Update(string currentHotkey, string newHotkey)
        {
            try
            {
                if (!Suspended)
                {
                    Remove(currentHotkey);
                    Add(newHotkey);
                }
                else
                {
                    _suspendedKeys.Remove(currentHotkey);
                    _suspendedKeys.Add(newHotkey);
                }

                currentHotkey = newHotkey;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Updates an existing hotkey 
        /// in the global Key watcher.
        /// </summary>
        /// <param name="currentHotkey">
        /// A reference to the variable 
        /// containing the hotkey to modify.
        /// </param>
        /// <param name="newHotkey">
        /// The new hotkey to be set.
        /// </param>
        private void Update(ref string currentHotkey, string newHotkey)
        {
            try
            {
                if (!Suspended)
                {
                    Remove(currentHotkey);
                    Add(newHotkey);
                }
                else
                {
                    _suspendedKeys.Remove(currentHotkey);
                    _suspendedKeys.Add(newHotkey);
                }

                currentHotkey = newHotkey;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Updates an existing hotkey 
        /// in the global Key watcher.
        /// </summary>
        /// <param name="currentHotkey">
        /// A reference to the variable 
        /// containing the hotkey to modify.
        /// </param>
        /// <param name="newHotkey">
        /// A reference to the variable containing 
        /// the new hotkey to be set.
        /// </param>
        private void Update(ref string currentHotkey, ref string newHotkey)
        {
            try
            {
                if (!Suspended)
                {
                    Remove(currentHotkey);
                    Add(newHotkey);
                }
                else
                {
                    _suspendedKeys.Remove(currentHotkey);
                    _suspendedKeys.Add(newHotkey);
                }

                currentHotkey = newHotkey;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Removes any specific hotkey 
        /// from the global Key watcher.
        /// </summary>
        /// <param name="hotkey">The hotkey to remove.</param>
        private void Remove(string hotkey)
        {
            _handle.RemoveKey(hotkey);
        }

        /// <summary>
        /// Removes a list of hotkeys from 
        /// the global Key watcher.
        /// </summary>
        /// <param name="hotkeys">The hotkeys to remove.</param>
        private void Remove(string[] hotkeys)
        {
            foreach (string key in hotkeys)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// Applies the library's default options and settings.
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
                            SourceAttributes.GetPath(),
                            SourceAttributes.GetSelection()
                    ),
                    new HotkeyEventArgs
                    {
                        Hotkey = e.Hotkey,
                        SourceApplication = new SourceApplication(
                            SourceAttributes.GetID(),
                            SourceAttributes.GetHandle(),
                            SourceAttributes.GetName(),
                            SourceAttributes.GetTitle(),
                            SourceAttributes.GetPath(),
                            SourceAttributes.GetSelection()
                        )
                    });
            };
        }

        #endregion

        #endregion

        #region Events

        #region Public

        #region Event Handlers

        /// <summary>
        /// Raised whenever a registered Hotkey is pressed.
        /// </summary>
        [Category("HotkeyListener Events")]
        [Description("Raised whenever a registered Hotkey is pressed.")]
        public event HotkeyEventHandler HotkeyPressed;

        /// <summary>
        /// Raised whenever a registered Hotkey has been updated.
        /// </summary>
        [Category("HotkeyListener Events")]
        [Description("Raised whenever a registered Hotkey has been updated.")]
        public event EventHandler<HotkeyUpdatedEventArgs> HotkeyUpdated = null;

        /// <summary>
        /// Represents the method that will handle a <see cref="HotkeyPressed"/> 
        /// event that has no event data.
        /// </summary>
        /// <param name="sender">The hotkey sender object.</param>
        /// <param name="e">The <see cref="HotkeyEventArgs"/> data.</param>
        public delegate void HotkeyEventHandler(object sender, HotkeyEventArgs e);

        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="HotkeyListener.HotkeyUpdated"/> event.
        /// </summary>
        public class HotkeyUpdatedEventArgs : EventArgs
        {
            #region Fields

            private Hotkey _updatedHotkey;
            private Hotkey _newHotkey;

            #endregion

            #region Constructor

            /// <summary>
            /// Provides data for the <see cref="HotkeyListener.HotkeyUpdated"/> event.
            /// </summary>
            /// <param name="updatedHotkey">
            /// The hotkey that has been updated.
            /// </param>
            /// <param name="newHotkey">
            /// The hotkey's newly updated value.
            /// </param>
            public HotkeyUpdatedEventArgs(Hotkey updatedHotkey, Hotkey newHotkey)
            {
                _updatedHotkey = updatedHotkey;
                _newHotkey = newHotkey;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the currently updated Hotkey.
            /// </summary>
            public Hotkey UpdatedHotkey
            {
                get => _updatedHotkey;
            }

            /// <summary>
            /// Gets the Hotkey's newly updated value.
            /// </summary>
            public Hotkey NewHotkey
            {
                get => _newHotkey;
            }

            #endregion
        }

        #endregion

        #endregion

        #region Private

        private void OnActivateForm(object sender, EventArgs e)
        {
            Suspend();
        }

        private void OnDeactivateForm(object sender, EventArgs e)
        {
            Resume();

            try
            {
                Form form = (Form)sender;

                if (_suspendedActions.ContainsKey(form))
                    form.Invoke(_suspendedActions[form]);
            }
            catch (Exception) { }
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Creates a standard hotkey for 
    /// use with <see cref="HotkeyListener"/>.
    /// </summary>
    [Serializable]
    public class Hotkey
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotkey"/> class.
        /// </summary>
        public Hotkey() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotkey"/> class.
        /// </summary>
        /// <param name="hotkey">
        /// The hotkey in string format.
        /// </param>
        public Hotkey(string hotkey)
        {
            var hotkeyObj = HotkeyListener.Convert(hotkey);
        
            KeyCode = hotkeyObj.KeyCode;
            Modifiers = hotkeyObj.Modifiers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotkey"/> class.
        /// </summary>
        /// <param name="keyCode">
        /// The hotkey's keyboard code.
        /// </param>
        public Hotkey(Keys keyCode = Keys.None)
        {
            KeyCode = keyCode;
            Modifiers = Keys.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotkey"/> class.
        /// </summary>
        /// <param name="modifiers">
        /// The hotkey's modifier flags. The flags indicate which 
        /// combination of CTRL, SHIFT, and ALT keys will be detected.
        /// </param>
        /// <param name="keyCode">
        /// The hotkey's keyboard code.
        /// </param>
        public Hotkey(Keys modifiers = Keys.None, Keys keyCode = Keys.None)
        {
            KeyCode = keyCode;
            Modifiers = modifiers;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hotkey's keyboard code.
        /// </summary>
        public Keys KeyCode { get; set; }

        /// <summary>
        /// Gets or sets the hotkey's modifier flags. The flags indicate 
        /// which combination of CTRL, SHIFT, and ALT keys will be detected.
        /// </summary>
        public Keys Modifiers { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a string conversion containing the Hotkey's 
        /// <see cref="KeyCode"/> and <see cref="Modifiers"/> keys.
        /// </summary>
        /// <returns><see cref="String"/></returns>
        public override string ToString()
        {
            if (Modifiers == Keys.None)
                return KeyCode.ToString();
            else
                return HotkeyListener.Convert(this);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// true if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Hotkey))
                return false;

            var other = obj as Hotkey;

            if (KeyCode != other.KeyCode || Modifiers != other.Modifiers)
                return false;

            return true;
        }

        /// <summary>
        /// Overrides the system-default object equality operator 
        /// for a customized Hotkey equality-check operator.
        /// </summary>
        /// <returns></returns>
        public static bool operator ==(Hotkey x, Hotkey y)
        {
            try
            {
                return x.Equals(y);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Overrides the system-default object non-equality operator 
        /// for a customized Hotkey non-equality-check operator.
        /// </summary>
        /// <returns></returns>
        public static bool operator !=(Hotkey x, Hotkey y)
        {
            try
            {
                return !(x == y);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

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
                source.ID, 
                source.Handle, 
                source.Name,
                source.Title, 
                source.Path,
                source.Selection
            );
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets the hotkey that was pressed.
        /// </summary>
        public Hotkey Hotkey { get; internal set; }
        
        /// <summary>
        /// Gets the details of the application 
        /// from where the hotkey was pressed.
        /// </summary>
        public SourceApplication SourceApplication { get; internal set; }

        #endregion
    }
}