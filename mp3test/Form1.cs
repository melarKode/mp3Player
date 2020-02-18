using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace mp3test
{
    public partial class Form1 : Form
    {
        int _prevState;
        System.Timers.Timer _monitorTimer;
        public Form1()
        {
            InitializeComponent();
            //run a timer once every second
            _monitorTimer = new System.Timers.Timer(1000);
            _monitorTimer.Elapsed += (s, e) =>
            {
                //every tick of timer, check if media playing previously has ended
                if (_prevState == 1)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    //set URL to next song in the queue or default next song
                    axWindowsMediaPlayer1.URL = @"C:\Users\prave\Music\Playlist\BMK201-Sobillu-Jaganmohini.mp3";
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    _prevState = 0;
                }
            };
            _monitorTimer.Start();
            axWindowsMediaPlayer1.PlayStateChange += (s, e) =>
            {
                if (e.newState == 8)//mediaEnded
                {
                    _prevState = 1;
                }
            };
            axWindowsMediaPlayer1.URL = @"C:\Users\prave\Music\Playlist\BMK206-Bangarumurali-Neelambari.mp3";
        }
        string[] files, paths, paths2, files2;
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i;
            i = listBox1.SelectedIndex;
            if(listBox2.SelectedIndex==-1)
                axWindowsMediaPlayer1.URL = paths[i];
            label1.Text = String.Format("\n {0}", files[i]);
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                listBox2.Items.Add(files[listBox1.SelectedIndex]);
            button2.Visible = false;
            button3.Visible = false;
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            button2.Visible = false;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            button3.Visible = false;
        }

        public void button1_Click(object sender, EventArgs e)
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
