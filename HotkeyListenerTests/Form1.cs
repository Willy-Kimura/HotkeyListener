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

        private Hotkey hotkey1 = new Hotkey("Control+Shift+D4");
        private Hotkey hotkey2 = new Hotkey(Keys.Control | Keys.Shift, Keys.Y);

        private Form2 form2 = new Form2();

        public Form1()
        {
            InitializeComponent();
            
            // hkl.Add(new[] { hotkey1, hotkey2 });
            hkl.Add(hotkey2);
            
            hkl.HotkeyPressed += Hkl_HotkeyPressed;

            hks.Enable(textBox1, hotkey2);
            hks.Enable(textBox2, new Hotkey("Control+Alt+T"));

            // var hotkey = HotkeyListener.Convert("Control+Alt+D1");
            // MessageBox.Show(hotkey.ToString());

            form2.Activated += 
                (s, e) => { Text = "Hotkey selection active.";
            };

            hkl.SuspendOn(form2, 
                () => { Text = "Settings updated"; }
            );
        }

        private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            if (e.Hotkey == hotkey2)
            {
                textBox3.Text = hkl.GetSelection();
                Activate();

                // MessageBox.Show(
                //     $"You pressed: {e.Hotkey.ToString()}\n" +
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
            // MessageBox.Show(hks.Convert(hotkey1));
            
            // hkl.SuspendHotkeys();
            // hks.Enable(textBox1, Keys.F1, Keys.Shift | Keys.Alt);
            // hks.Set(textBox1, Keys.F2, Keys.Shift | Keys.Control);
            hkl.Update(ref hotkey1, hotkey2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hkl.Resume();

            // hkl.ResumeHotkeys();
            // hks.Reset(textBox2);
            // MessageBox.Show(hks.IsEnabled(textBox2).ToString());
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hkl.ResumeOn(form2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hkl.Remove(hotkey1);
        }
    }
}