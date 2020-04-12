#region COPYRIGHT

/*
 * The MIT License

 * Copyright (c) 2010-2020, Willy Kimura.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 */

#endregion


using System;
using System.Drawing;
using System.Windows.Forms;

using WK.Libraries.HotkeyListenerNS;

namespace TextClipper.Views
{
    /// <summary>
    /// The Hotkey Settings window.
    /// </summary>
    public partial class HotkeySettings : Form
    {
        #region Constructor

        public HotkeySettings()
        {
            InitializeComponent();
        }

        #endregion

        #region Fields

        // Declare an instance of the Hotkey Selector class.
        internal static HotkeySelector hotkeySelector = new HotkeySelector();

        #endregion

        #region Events

        private void HotkeySettings_Load(object sender, EventArgs e)
        {
            // Enable the default textbox for hotkey-selection.
            hotkeySelector.Enable(txtClippingHotkey, MainForm.clippingHotkey);
        }

        private void HotkeySettings_Shown(object sender, EventArgs e)
        {
            // Set focus to the hotkey selection input.
            txtClippingHotkey.Focus();
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            // Update the default clipping hotkey 
            // to the new user-defined hotkey.
            MainForm.hotkeyListener.Update
            (
                // Reference the current clipping hotkey for directly updating 
                // the hotkey without a need for restarting your application.
                ref MainForm.clippingHotkey, 

                // Convert the selected hotkey's text representation 
                // to a Hotkey object and update it.
                HotkeyListener.Convert(txtClippingHotkey.Text)
            );

            // Close the settings form.
            Close();
        }

        #endregion
    }
}