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
    /// The main application window.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Fields

        // Create a new instance of Hotkey Listener.
        internal static HotkeyListener hotkeyListener = new HotkeyListener();

        // Create a new instance of the Hotkey Settings form.
        internal HotkeySettings settingsForm = new HotkeySettings();

        // Define the default text-clipping hotkey.
        internal static Hotkey clippingHotkey = new Hotkey(Keys.Control, Keys.Q);

        #endregion

        #region Methods

        /// <summary>
        /// This will be used to update the clipping hotkey's information label.
        /// </summary>
        /// <param name="clippingHotkey">
        /// The current clipping hotkey value.
        /// </param>
        public void SetClippingHotkeyInfo(Hotkey clippingHotkey)
        {
            lblClipppingHotkeyInfo.Text = $"The current clipping hotkey is \"{clippingHotkey}\"";
        }

        /// <summary>
        /// This will provide a "no available" status 
        /// message if no texts have been clipped yet.
        /// </summary>
        public void RequireStatusLabelIfNothing()
        {
            if (lstClippedTexts.Items.Count == 0)
                lblNoClippedTexts.Show();
            else
                lblNoClippedTexts.Hide();
        }

        #endregion

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Register the clipping hotkey.
            hotkeyListener.Add(clippingHotkey);

            // Displays a "no available clipped texts" status.
            RequireStatusLabelIfNothing();

            // Suspend listening to hotkeys when the Settings Form is active.
            hotkeyListener.SuspendOn(settingsForm);

            // This event is used to listen to any hotkey presses.
            hotkeyListener.HotkeyPressed += HotkeyListener_HotkeyPressed;

            // This event is used to listen to any updated hotkeys.
            hotkeyListener.HotkeyUpdated += HotkeyListener_HotkeyUpdated;

            // This is used to display to the user the current clipping hotkey.
            SetClippingHotkeyInfo(clippingHotkey);
        }

        private void lstClippedTexts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    // Delete any selected item when the user hits 'Delete'.
                    lstClippedTexts.Items.RemoveAt(lstClippedTexts.SelectedIndex);
                }
                catch (Exception) { }
            }
        }

        private void HotkeyListener_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            if (e.Hotkey == clippingHotkey)
            {
                // If the clipping hotkey is pressed, get the selected text 
                // and add it to the list of clipped texts in the ListBox.
                string selection = hotkeyListener.SelectedText;

                if (!string.IsNullOrWhiteSpace(selection))
                    lstClippedTexts.Items.Add(selection);
            
                // Hide status label if the user adds some clipped text.
                RequireStatusLabelIfNothing();
            }
        }

        private void HotkeyListener_HotkeyUpdated(object sender, HotkeyListener.HotkeyUpdatedEventArgs e)
        {
            if (e.UpdatedHotkey == clippingHotkey)
            {
                // If the clipping hotkey is updated, inform the user of 
                // the change using the via an information label.
                SetClippingHotkeyInfo(e.NewHotkey);
            }
        }

        private void lstClippedTexts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // Double-clicking on any clipped text 
                // should copy it to the Clipboard.
                if (lstClippedTexts.SelectedItem != null)
                {
                    string selectedText = lstClippedTexts.SelectedItem.ToString();

                    if (!string.IsNullOrWhiteSpace(selectedText))
                    {
                        Clipboard.SetText(selectedText);

                        MessageBox.Show("Clipped text copied to the clipboard!");
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnChangeHotkey_Click(object sender, EventArgs e)
        {
            // Show the Hotkey Settings dialog.
            settingsForm.ShowDialog();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion
    }
}