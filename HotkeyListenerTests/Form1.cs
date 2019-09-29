using System;
using System.Drawing;
using System.Windows.Forms;

using WK.Libraries.HotkeyListenerNS;

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
            
            hkl.HotkeyPressed += Hkl_HotkeyPressed;
        }

        private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            if (e.Hotkey == "Control+Shift+E")
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

        private void button1_Click(object sender, EventArgs e)
        {
            // hkl.SuspendHotkeys();
            hks.Enable(textBox1, Keys.F1, Keys.Shift | Keys.Alt);
            hks.Set(textBox1, Keys.F2, Keys.Shift | Keys.Control);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // hkl.ResumeHotkeys();
            hks.Reset(textBox2);
            MessageBox.Show(hks.HasEnabled(textBox2).ToString());
        }
    }
}
