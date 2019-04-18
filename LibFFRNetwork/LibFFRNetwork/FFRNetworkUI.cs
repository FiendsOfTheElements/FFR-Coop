using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibFFRNetwork
{
    public partial class FFRNetworkUI : Form
    {
        public delegate void initDelegate();
        public event initDelegate initEvent;
        public delegate void joinDelegate();
        public event joinDelegate joinEvent;
        public int status = 1;
        public FFRNetworkUI()
        {
            InitializeComponent();
            Random rand = new Random();
            int usernameRandInt = rand.Next(1000, 9999);
            txtPlayername.Text = $"Lazyracer{usernameRandInt}";
            var statusTask = new Task(() => UpdateImage());
            statusTask.Start();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            btnInit.Enabled = false;
            btnJoin.Enabled = false;
            initEvent();
        }
        
        public void setInitText(string team)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("sans", 30.0f);
            richTextBox1.AppendText(team);
            panel1.Visible = true;
            panel2.Visible = false;
        }
        public string getTeamText()
        {
            return txtJoinTeam.Text;
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            btnInit.Enabled = false;
            btnJoin.Enabled = false;
            joinEvent();
        }
        public string getPlayername()
        {
            string player = txtPlayername.Text.Replace('{', '[');
            player = player.Replace('\\', '_');
            player = player.Replace('/', '_');
            player = player.Replace('&', '_');
            player = player.Replace('?', '_');
            return txtPlayername.Text;
        }
        public void setStatusLine(string s)
        {
            this.lblStatusLine.Text = s;
        }
        public PictureBox getStatusImage()
        {
            return imgStatus;
        }
        public async void UpdateImage()
        {
            do
            {
                switch (status)
                {
                    case 1:
                        imgStatus.Image = LibFFRNetwork.Properties.Resources.th1;
                        status = 2;
                        break;
                    case 2:
                        imgStatus.Image = LibFFRNetwork.Properties.Resources.th2;
                        status = 1;
                        break;
                    case 3:
                        imgStatus.Image = LibFFRNetwork.Properties.Resources.th3;
                        break;
                    case 4:
                        imgStatus.Image = LibFFRNetwork.Properties.Resources.th4;
                        break;
                    default:
                        break;
                }
                System.Threading.Thread.Sleep(500);
            } while (true);
            

        }
    }
}
