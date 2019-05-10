namespace LibFFRNetwork
{
    partial class FFRNetworkUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFRNetworkUI));
            this.txtPlayername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatusLine = new System.Windows.Forms.Label();
            this.imgStatus = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabName = new System.Windows.Forms.TabPage();
            this.btnTab1Continue = new System.Windows.Forms.Button();
            this.tabChoice = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnJoinChoice = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.tabJoin = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtPlayerLimit = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTeamOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTeamJoin = new System.Windows.Forms.TextBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnJoinBack = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabName.SuspendLayout();
            this.tabChoice.SuspendLayout();
            this.tabJoin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlayerLimit)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPlayername
            // 
            this.txtPlayername.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlayername.Location = new System.Drawing.Point(6, 110);
            this.txtPlayername.MaxLength = 14;
            this.txtPlayername.Name = "txtPlayername";
            this.txtPlayername.Size = new System.Drawing.Size(400, 38);
            this.txtPlayername.TabIndex = 1;
            this.txtPlayername.TextChanged += new System.EventHandler(this.TxtPlayername_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter a name:";
            // 
            // lblStatusLine
            // 
            this.lblStatusLine.AutoSize = true;
            this.lblStatusLine.Location = new System.Drawing.Point(49, 338);
            this.lblStatusLine.Name = "lblStatusLine";
            this.lblStatusLine.Size = new System.Drawing.Size(98, 13);
            this.lblStatusLine.TabIndex = 5;
            this.lblStatusLine.Text = "Checking version...";
            // 
            // imgStatus
            // 
            this.imgStatus.Location = new System.Drawing.Point(12, 324);
            this.imgStatus.Name = "imgStatus";
            this.imgStatus.Size = new System.Drawing.Size(35, 27);
            this.imgStatus.TabIndex = 6;
            this.imgStatus.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabName);
            this.tabControl1.Controls.Add(this.tabChoice);
            this.tabControl1.Controls.Add(this.tabJoin);
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(423, 306);
            this.tabControl1.TabIndex = 7;
            // 
            // tabName
            // 
            this.tabName.Controls.Add(this.btnTab1Continue);
            this.tabName.Controls.Add(this.txtPlayername);
            this.tabName.Controls.Add(this.label1);
            this.tabName.Location = new System.Drawing.Point(4, 22);
            this.tabName.Name = "tabName";
            this.tabName.Padding = new System.Windows.Forms.Padding(3);
            this.tabName.Size = new System.Drawing.Size(415, 280);
            this.tabName.TabIndex = 2;
            this.tabName.Text = "Name";
            this.tabName.UseVisualStyleBackColor = true;
            // 
            // btnTab1Continue
            // 
            this.btnTab1Continue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTab1Continue.Location = new System.Drawing.Point(6, 182);
            this.btnTab1Continue.Name = "btnTab1Continue";
            this.btnTab1Continue.Size = new System.Drawing.Size(400, 92);
            this.btnTab1Continue.TabIndex = 2;
            this.btnTab1Continue.Text = "Continue";
            this.btnTab1Continue.UseVisualStyleBackColor = true;
            this.btnTab1Continue.Click += new System.EventHandler(this.BtnTab1Continue_Click);
            // 
            // tabChoice
            // 
            this.tabChoice.Controls.Add(this.label3);
            this.tabChoice.Controls.Add(this.txtPlayerLimit);
            this.tabChoice.Controls.Add(this.checkBox1);
            this.tabChoice.Controls.Add(this.label2);
            this.tabChoice.Controls.Add(this.btnJoinChoice);
            this.tabChoice.Controls.Add(this.btnInit);
            this.tabChoice.Location = new System.Drawing.Point(4, 22);
            this.tabChoice.Name = "tabChoice";
            this.tabChoice.Padding = new System.Windows.Forms.Padding(3);
            this.tabChoice.Size = new System.Drawing.Size(415, 280);
            this.tabChoice.TabIndex = 0;
            this.tabChoice.Text = "Mode";
            this.tabChoice.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "or";
            // 
            // btnJoinChoice
            // 
            this.btnJoinChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinChoice.Location = new System.Drawing.Point(6, 126);
            this.btnJoinChoice.Name = "btnJoinChoice";
            this.btnJoinChoice.Size = new System.Drawing.Size(403, 87);
            this.btnJoinChoice.TabIndex = 1;
            this.btnJoinChoice.Text = "Join Existing Team";
            this.btnJoinChoice.UseVisualStyleBackColor = true;
            this.btnJoinChoice.Click += new System.EventHandler(this.BtnJoinChoice_Click);
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInit.Location = new System.Drawing.Point(6, 6);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(403, 90);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "Create New Team";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.BtnInit_Click_1);
            // 
            // tabJoin
            // 
            this.tabJoin.BackColor = System.Drawing.Color.Transparent;
            this.tabJoin.Controls.Add(this.btnJoinBack);
            this.tabJoin.Controls.Add(this.btnJoin);
            this.tabJoin.Controls.Add(this.txtTeamJoin);
            this.tabJoin.Controls.Add(this.label5);
            this.tabJoin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabJoin.Location = new System.Drawing.Point(4, 22);
            this.tabJoin.Name = "tabJoin";
            this.tabJoin.Padding = new System.Windows.Forms.Padding(3);
            this.tabJoin.Size = new System.Drawing.Size(415, 280);
            this.tabJoin.TabIndex = 1;
            this.tabJoin.Text = "Join";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 255);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Limit game to";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // txtPlayerLimit
            // 
            this.txtPlayerLimit.Enabled = false;
            this.txtPlayerLimit.Location = new System.Drawing.Point(96, 254);
            this.txtPlayerLimit.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtPlayerLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPlayerLimit.Name = "txtPlayerLimit";
            this.txtPlayerLimit.Size = new System.Drawing.Size(39, 20);
            this.txtPlayerLimit.TabIndex = 4;
            this.txtPlayerLimit.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "players.";
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.txtTeamOutput);
            this.tabInfo.Controls.Add(this.label4);
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(415, 280);
            this.tabInfo.TabIndex = 3;
            this.tabInfo.Text = "Info";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 37);
            this.label4.TabIndex = 0;
            this.label4.Text = "Your team is";
            // 
            // txtTeamOutput
            // 
            this.txtTeamOutput.Enabled = false;
            this.txtTeamOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTeamOutput.Location = new System.Drawing.Point(6, 115);
            this.txtTeamOutput.Name = "txtTeamOutput";
            this.txtTeamOutput.Size = new System.Drawing.Size(403, 49);
            this.txtTeamOutput.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(300, 37);
            this.label5.TabIndex = 0;
            this.label5.Text = "Enter team number:";
            // 
            // txtTeamJoin
            // 
            this.txtTeamJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTeamJoin.Location = new System.Drawing.Point(6, 68);
            this.txtTeamJoin.Name = "txtTeamJoin";
            this.txtTeamJoin.Size = new System.Drawing.Size(403, 49);
            this.txtTeamJoin.TabIndex = 1;
            this.txtTeamJoin.TextChanged += new System.EventHandler(this.TxtTeamJoin_TextChanged);
            // 
            // btnJoin
            // 
            this.btnJoin.Enabled = false;
            this.btnJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoin.Location = new System.Drawing.Point(6, 136);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(403, 85);
            this.btnJoin.TabIndex = 2;
            this.btnJoin.Text = "Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.BtnJoin_Click_1);
            // 
            // btnJoinBack
            // 
            this.btnJoinBack.Location = new System.Drawing.Point(7, 251);
            this.btnJoinBack.Name = "btnJoinBack";
            this.btnJoinBack.Size = new System.Drawing.Size(75, 23);
            this.btnJoinBack.TabIndex = 3;
            this.btnJoinBack.Text = "<-   Back";
            this.btnJoinBack.UseVisualStyleBackColor = true;
            this.btnJoinBack.Click += new System.EventHandler(this.BtnJoinBack_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(317, 338);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // FFRNetworkUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 360);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.imgStatus);
            this.Controls.Add(this.lblStatusLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FFRNetworkUI";
            this.Text = "FFRNetworkUI";
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabName.ResumeLayout(false);
            this.tabName.PerformLayout();
            this.tabChoice.ResumeLayout(false);
            this.tabChoice.PerformLayout();
            this.tabJoin.ResumeLayout(false);
            this.tabJoin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlayerLimit)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tabInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPlayername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatusLine;
        private System.Windows.Forms.PictureBox imgStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChoice;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.TabPage tabJoin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnJoinChoice;
        private System.Windows.Forms.TabPage tabName;
        private System.Windows.Forms.Button btnTab1Continue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtPlayerLimit;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.TextBox txtTeamJoin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TextBox txtTeamOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnJoinBack;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}