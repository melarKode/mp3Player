using AxWMPLib;
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
        }
        /*{
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
                    axWindowsMediaPlayer1.URL = @" ";
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
            axWindowsMediaPlayer1.URL = @" ";
        }*/
        string[] files, paths;
        public static List<string> paths2 = new List<string>();
        public static List<string> files2 = new List<string>();
        int i = 0;
        
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i;
            i = listBox1.SelectedIndex;
            axWindowsMediaPlayer1.URL = paths[i];
            label1.Text = String.Format("\n {0}", files[i]);
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            button2.Visible = true;
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox2.Items.Add(files[listBox1.SelectedIndex]);
                paths2.Add(paths[listBox1.SelectedIndex]);
                files2.Add(files[listBox1.SelectedIndex]);
            }
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
            if (listBox2.SelectedIndex != -1)
            {
                //   for(int j=listBox2.Items.Count -1; j>=listBox2.SelectedIndex; j=j-1)
                paths2.RemoveAt(listBox2.SelectedIndex);
                files2.RemoveAt(listBox2.SelectedIndex);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                
                    
            }
            button3.Visible = false;
        }


        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if(e.newState == 1)
            {
                if (listBox2.Items.Count != 0 && listBox2.SelectedIndex !=listBox2.Items.Count-1)
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (listBox2.SelectedIndex != -1)
                        {
                            paths2.RemoveAt(listBox2.SelectedIndex);
                            files2.RemoveAt(listBox2.SelectedIndex);
                            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                            listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
                            axWindowsMediaPlayer1.URL = paths2[0];
                            label1.Text = String.Format("\n {0}", files2[0]);
                        }
                        else
                        {
                            listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
                            axWindowsMediaPlayer1.URL = paths2[0];
                            label1.Text = String.Format("\n {0}", files2[0]);
                        }
                    }));
                }else if(listBox2.SelectedIndex == listBox2.Items.Count - 1 && listBox2.Items.Count!=0)
                {
                    BeginInvoke(new Action(() =>
                    {
                        paths2.RemoveAt(listBox2.SelectedIndex);
                        files2.RemoveAt(listBox2.SelectedIndex);
                        listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                        listBox1.SelectedIndex = (listBox1.SelectedIndex + 1)%(listBox1.Items.Count);
                        axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                        label1.Text = String.Format("\n {0}", files[listBox1.SelectedIndex]);
                    }));
                }
                else
                {
                    BeginInvoke(new Action(() =>
                    {
                        listBox1.SelectedIndex = (listBox1.SelectedIndex + 1)%(listBox1.Items.Count);
                        if (listBox1.SelectedIndex == -1)
                        {
                            listBox1.SelectedIndex = 0;
                        }
                        axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                        label1.Text = String.Format("\n {0}", files[listBox1.SelectedIndex]);
                    }));
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == 0)
            {
                paths2.RemoveAt(listBox2.SelectedIndex);
                files2.RemoveAt(listBox2.SelectedIndex);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                label1.Text = String.Format("\n {0}", files[listBox1.SelectedIndex]);
            }
            else
            {
                if (listBox1.SelectedIndex == 0)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                    label1.Text = String.Format("\n {0}", files[listBox1.SelectedIndex]);
                }
                else if(listBox1.SelectedIndex>0)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                    axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                    label1.Text = String.Format("\n {0}", files[listBox1.SelectedIndex]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int i;
            if(openFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                listBox1.Items.Clear();
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
