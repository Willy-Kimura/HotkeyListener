using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WK.Libraries.HotkeyListenerNS;
using WK.Libraries.HotkeyListenerNS.Models;

namespace HotkeyListenerTests
{
    public partial class Form1 : Form
    {
        public HotkeyListener hkl = new HotkeyListener();
        public HotkeySelector hks = new HotkeySelector();

        public Form1()
        {
            InitializeComponent();
            
            hkl.AddHotkey("S");
            hkl.AddHotkey("Control+Shift+E");

            hks.Bind(textBox2);

            hkl.HotkeyPressed += Hkl_HotkeyPressed;
        }

        private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            Activate();
            
            MessageBox.Show(
                $"You pressed: {e.Hotkey}\n" +
                $"Name: {e.SourceApplication.Name}\n" +
                $"Title: {e.SourceApplication.Title}\n" +
                $"ID: {e.SourceApplication.ID}\n" +
                $"Handle: {e.SourceApplication.Handle}\n" +
                $"Path: {e.SourceApplication.Path}");
        }
    }
}
