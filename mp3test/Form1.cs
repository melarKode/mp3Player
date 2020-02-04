using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mp3test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] files, paths;
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            int i;
            if(axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                i = listBox1.SelectedIndex;
                i = (i + 1) % (files.Length);
                axWindowsMediaPlayer1.URL = paths[i];
                axWindowsMediaPlayer1.playState.Equals(WMPLib.WMPPlayState.wmppsPlaying);
                label1.Text = String.Format("\n {0}", files[i]);
                
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i;
            i = listBox1.SelectedIndex;
            axWindowsMediaPlayer1.URL = paths[i];
            label1.Text = String.Format("\n {0}", files[i]);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            if(openFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                files = openFileDialog1.SafeFileNames;
                paths = openFileDialog1.FileNames;
                for(i=0; i<files.Length; i++)
                {
                    listBox1.Items.Add(files[i]);
                }
            }
        }
    }
}
