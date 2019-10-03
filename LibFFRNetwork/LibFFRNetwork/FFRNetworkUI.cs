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
        private bool tabsEnabled = false;
        public FFRNetworkUI()
        {
            InitializeComponent();
            Random rand = new Random();
            int usernameRandInt = rand.Next(1000, 9999);
            txtPlayername.Text = $"Lazyracer{usernameRandInt}";
            var statusTask = new Task(() => UpdateImage());
            statusTask.Start();
            tabControl1.Selecting += TabControl1_Selecting;
        }
        public void switchToTab(int i)
        {
            tabsEnabled = true;
            tabControl1.SelectedTab = tabControl1.TabPages[i];
            tabsEnabled = false;
        }
        private void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !tabsEnabled;
        }
        public int getPlayerLimit()
        {
            if (checkBox1.Checked)
            {
                return decimal.ToInt32(txtPlayerLimit.Value);
            }
            else
            {
                return 0;
            }
        }
        public void setInitText(string team)
        {
            txtTeamOutput.Text = team;
            switchToTab(3);
        }
        public string getTeamText()
        {
            return txtTeamJoin.Text;
        }
        public void disableContinueButton()
        {
            btnTab1Continue.Enabled = false;
        }

        public string getPlayername()
        {
            string player = txtPlayername.Text.Replace('{', '[');
            player = player.Replace('\\', '_');
            player = player.Replace('/', '_');
            player = player.Replace('&', '_');
            player = player.Replace('?', '_');
            return player;
        }
        public void setStatusLine(string s)
        {
            this.lblStatusLine.Text = s;
        }
        public PictureBox getStatusImage()
        {
            return imgStatus;
        }
        public void UpdateImage()
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

        private void BtnTab1Continue_Click(object sender, EventArgs e)
        {
            switchToTab(1);
        }

        private void TxtPlayername_TextChanged(object sender, EventArgs e)
        {
            if (txtPlayername.Text == "")
            {
                btnTab1Continue.Enabled = false;
            }
            else
            {
                btnTab1Continue.Enabled = true;
            }
        }

        private void BtnInit_Click_1(object sender, EventArgs e)
        {
            btnInit.Enabled = false;
            initEvent();
        }

        private void BtnJoinChoice_Click(object sender, EventArgs e)
        {
            switchToTab(2);
        }

        private void BtnJoin_Click_1(object sender, EventArgs e)
        {
            btnJoin.Enabled = false;
            btnJoinBack.Enabled = false;
            joinEvent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPlayerLimit.Enabled = checkBox1.Checked;
        }
        public void joinFailed()
        {
            btnJoin.Enabled = true;
            btnJoinBack.Enabled = true;
        }

        private void BtnJoinBack_Click(object sender, EventArgs e)
        {
            switchToTab(1);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/BrianCumminger/FFR-Coop/releases");
        }

        private void TxtTeamJoin_TextChanged(object sender, EventArgs e)
        {
            if (txtTeamJoin.Text == "")
            {
                btnJoin.Enabled = false;
            }
            else
            {
                btnJoin.Enabled = true;
            }
        }
        public void showDownloadLink()
        {
            linkLabel1.Text = "Update to new version";
        }

        private void ImgStatus_Click(object sender, EventArgs e)
        {
            var credits = new Credits();
            credits.Show();
        }
    }
}
