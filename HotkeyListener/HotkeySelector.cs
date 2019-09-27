using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WK.Libraries.HotkeyListenerNS
{
    public class HotkeySelector
    {
        #region Fields

        // These variables store the current 
        // Hotkey and modifier key(s).
        private Keys _hotkey = Keys.None;
        private Keys _modifiers = Keys.None;

        // ArrayLists used to enforce the use of proper modifiers.
        // Shift+A isn't a valid hotkey, for instance, as it would screw up when the user is typing.
        private ArrayList _needNonShiftModifier = null;
        private ArrayList _needNonAltGrModifier = null;

        public List<Control> _hkSelectionControls = new List<Control>();

        #endregion

        #region Properties

        #region Browsable

        public List<Control> Controls
        {
            get => _hkSelectionControls;
            set {

                _hkSelectionControls = value;

                Refresh();
                BindAll(value);

            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public

        public bool Bind(Control control)
        {
            try
            {
                control.Text = "None";
                
                control.KeyPress += new KeyPressEventHandler(OnKeyPress);
                control.KeyDown += new KeyEventHandler(OnKeyDown);
                control.KeyUp += new KeyEventHandler(OnKeyUp);

                _needNonShiftModifier = new ArrayList();
                _needNonAltGrModifier = new ArrayList();

                PopulateModifierLists();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BindAll(List<Control> controls)
        {
            try
            {
                foreach (var control in controls)
                {
                    Bind(control);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(Control control)
        {
            try
            {
                Controls.Remove(control);
                
                Refresh();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Refresh()
        {
            // Fill the ArrayLists that contain  
            // all invalid hotkey combinations.
            _needNonShiftModifier = new ArrayList();
            _needNonAltGrModifier = new ArrayList();

            PopulateModifierLists();
        }

        /// <summary>
        /// Resets any bound control's Text to None.
        /// </summary>
        public void Clear(Control control)
        {
            control.Text = "None";
        }

        /// <summary>
        /// Resets all the bound controls' Text to None.
        /// </summary>
        public void ClearAll()
        {
            foreach (var control in Controls)
            {
                control.Text = "None";
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Populates the ArrayLists specifying disallowed Hotkeys 
        /// such as Shift+A, Ctrl+Alt+4 (produces 'dollar' sign) etc.
        /// </summary>
        private void PopulateModifierLists()
        {
            // Shift + 0 - 9, A - Z.
            for (Keys k = Keys.D0; k <= Keys.Z; k++)
                _needNonShiftModifier.Add((int)k);

            // Shift + Numpad keys.
            for (Keys k = Keys.NumPad0; k <= Keys.NumPad9; k++)
                _needNonShiftModifier.Add((int)k);

            // Shift + Misc (,;<./ etc).
            for (Keys k = Keys.Oem1; k <= Keys.OemBackslash; k++)
                _needNonShiftModifier.Add((int)k);

            // Shift + Space, PgUp, PgDn, End, Home.
            for (Keys k = Keys.Space; k <= Keys.Home; k++)
                _needNonShiftModifier.Add((int)k);

            // Misc keys that we can't loop through.
            _needNonShiftModifier.Add((int)Keys.Insert);
            _needNonShiftModifier.Add((int)Keys.Help);
            _needNonShiftModifier.Add((int)Keys.Multiply);
            _needNonShiftModifier.Add((int)Keys.Add);
            _needNonShiftModifier.Add((int)Keys.Subtract);
            _needNonShiftModifier.Add((int)Keys.Divide);
            _needNonShiftModifier.Add((int)Keys.Decimal);
            _needNonShiftModifier.Add((int)Keys.Return);
            _needNonShiftModifier.Add((int)Keys.Escape);
            _needNonShiftModifier.Add((int)Keys.NumLock);
            _needNonShiftModifier.Add((int)Keys.Scroll);
            _needNonShiftModifier.Add((int)Keys.Pause);

            // Ctrl+Alt + 0 - 9.
            for (Keys k = Keys.D0; k <= Keys.D9; k++)
                _needNonAltGrModifier.Add((int)k);
        }

        #endregion

        #endregion

        #region Events

        #region Private

        /// <summary>
        /// Fires when a key is pushed down. Here, we'll want to update the text in the box
        /// to notify the user what combination is currently pressed.
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete || e.KeyData == (Keys.Control | Keys.Delete))
                ResetHotkey((Control)sender);

            if (e.KeyData == (Keys.Shift | Keys.Insert))
            {
                this._modifiers = Keys.Shift;

                e.Handled = true;
            }

            // Clear the current hotkey.
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                ResetHotkey((Control)sender);

                return;
            }
            else
            {
                this._modifiers = e.Modifiers;
                this._hotkey = e.KeyCode;

                Redraw((Control)sender);
            }
        }

        /// <summary>
        /// Fires when all keys are released. If the current hotkey isn't valid, reset it.
        /// Otherwise, do nothing and keep the text and hotkey as it was.
        /// </summary>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (this._hotkey == Keys.None && Control.ModifierKeys == Keys.None)
            {
                ResetHotkey((Control)sender);

                return;
            }
        }

        /// <summary>
        /// Prevents anything entered in Input controls from being displayed.
        /// Without this, a "A" key press would appear as "aControl, Alt + A".
        /// </summary>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #endregion

        /// <summary>
        /// Clears the current hotkey and resets the TextBox
        /// </summary>
        public void ResetHotkey(Control control)
        {
            this._hotkey = Keys.None;
            this._modifiers = Keys.None;

            Redraw(control);
        }

        /// <summary>
        /// Helper function
        /// </summary>
        private void Redraw(Control control)
        {
            Redraw(control, false);
        }

        /// <summary>
        /// Redraws the bound control when necessary.
        /// </summary>
        /// <param name="internalCall">
        /// Specifies whether this function is 
        /// called internally or by the user.
        /// </param>
        private void Redraw(Control control, bool internalCall)
        {
            try
            {
                // No hotkey set.
                if (this._hotkey == Keys.None)
                {
                    control.Text = "None";

                    return;
                }

                // LWin/RWin don't work as hotkeys...
                // (neither do they work as modifier keys in .NET 2.0).
                if (this._hotkey == Keys.LWin || this._hotkey == Keys.RWin)
                {
                    control.Text = "None";

                    return;
                }

                // Only validate input if it comes from the user.
                if (internalCall == false)
                {
                    // No modifier or shift only, and a hotkey that needs another modifier.
                    if ((this._modifiers == Keys.Shift || this._modifiers == Keys.None) &&
                        this._needNonShiftModifier.Contains((int)this._hotkey))
                    {
                        if (this._modifiers == Keys.None)
                        {
                            // Set Ctrl+Alt as the modifier unless Ctrl+Alt+<key> won't work.
                            if (_needNonAltGrModifier.Contains((int)this._hotkey) == false)
                            {
                                this._modifiers = Keys.Alt | Keys.Control;
                            }
                            else
                            {
                                // ...In that case, use Shift+Alt instead.
                                this._modifiers = Keys.Alt | Keys.Shift;
                            }
                        }
                        else
                        {
                            // User pressed Shift and an invalid key (e.g. a letter or a number),
                            // that needs another set of modifier keys.
                            this._hotkey = Keys.None;
                            control.Text = this._modifiers.ToString() + " + (Unsupported)";

                            return;
                        }
                    }
                    // Check all Ctrl+Alt keys.
                    if ((this._modifiers == (Keys.Alt | Keys.Control)) &&
                        this._needNonAltGrModifier.Contains((int)this._hotkey))
                    {
                        // Ctrl+Alt+4 etc won't work; reset hotkey and tell the user.
                        this._hotkey = Keys.None;
                        control.Text = this._modifiers.ToString() + " + (Unsupported)";

                        return;
                    }
                }

                if (this._modifiers == Keys.None)
                {
                    if (this._hotkey == Keys.None)
                    {
                        control.Text = "None";

                        return;
                    }
                    else
                    {
                        // We get here if we've got a hotkey that is valid without a modifier,
                        // like F1-F12, Media-keys etc.
                        control.Text = this._hotkey.ToString();

                        return;
                    }
                }

                // Without this code, pressing only Ctrl 
                // will show up as "Control + ControlKey", etc.
                if (this._hotkey == Keys.Menu || /* Alt */
                    this._hotkey == Keys.ShiftKey ||
                    this._hotkey == Keys.ControlKey)
                {
                    this._hotkey = Keys.None;
                }

                control.Text = this._modifiers.ToString() + " + " + this._hotkey.ToString();
            }
            catch (Exception) { }
        }
    }
}
