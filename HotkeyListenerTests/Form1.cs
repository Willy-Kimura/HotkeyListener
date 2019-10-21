using System;
using System.Drawing;
using System.Windows.Forms;

using WK.Libraries.HotkeyListenerNS;
using WK.Libraries.HotkeyListenerNS.Helpers;

namespace HotkeyListenerTests
{
    public partial class Form1 : Form
    {
        public HotkeyListener hkl = new HotkeyListener();
        public HotkeySelector hks = new HotkeySelector();

        private string hotkey1 = "Control+Shift+E";
        private string hotkey2 = "Control+Y";

        public Form1()
        {
            InitializeComponent();
            
            hkl.AddHotkey(hotkey1);
            
            hkl.HotkeyPressed += Hkl_HotkeyPressed;

            hks.Enable(textBox1, "Control+Shift+E");
        }

        private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            if (e.Hotkey == hotkey1)
            {
                textBox3.Text = hkl.GetSelection();
                
                Activate();

                // MessageBox.Show(
                //     $"You pressed: {e.Hotkey}\n" +
                //     $"Name: {e.SourceApplication.Name}\n" +
                //     $"Title: {e.SourceApplication.Title}\n" +
                //     $"ID: {e.SourceApplication.ID}\n" +
                //     $"Handle: {e.SourceApplication.Handle}\n" +
                //     $"Path: {e.SourceApplication.Path}"
                // );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // hkl.SuspendHotkeys();
            // hks.Enable(textBox1, Keys.F1, Keys.Shift | Keys.Alt);
            // hks.Set(textBox1, Keys.F2, Keys.Shift | Keys.Control);
            hkl.SuspendHotkeys();
            hkl.ModifyHotkey(ref hotkey1, hotkey2);
            hkl.ResumeHotkeys();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // hkl.ResumeHotkeys();
            hks.Reset(textBox2);
            MessageBox.Show(hks.IsEnabled(textBox2).ToString());
        }
    }
}