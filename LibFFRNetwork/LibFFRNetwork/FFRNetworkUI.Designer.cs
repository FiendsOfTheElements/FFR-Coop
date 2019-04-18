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
            this.btnInit = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblInit = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtJoinTeam = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlayername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatusLine = new System.Windows.Forms.Label();
            this.imgStatus = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(13, 80);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(154, 49);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "Initialize New Team";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(282, 199);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(154, 49);
            this.btnJoin.TabIndex = 1;
            this.btnJoin.Text = "Join Existing Team";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.lblInit);
            this.panel1.Location = new System.Drawing.Point(13, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 112);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 21);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(194, 77);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // lblInit
            // 
            this.lblInit.AutoSize = true;
            this.lblInit.Location = new System.Drawing.Point(4, 4);
            this.lblInit.Name = "lblInit";
            this.lblInit.Size = new System.Drawing.Size(97, 13);
            this.lblInit.TabIndex = 0;
            this.lblInit.Text = "New team created.";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtJoinTeam);
            this.panel2.Location = new System.Drawing.Point(282, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 113);
            this.panel2.TabIndex = 3;
            // 
            // txtJoinTeam
            // 
            this.txtJoinTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJoinTeam.Location = new System.Drawing.Point(4, 4);
            this.txtJoinTeam.Name = "txtJoinTeam";
            this.txtJoinTeam.Size = new System.Drawing.Size(147, 29);
            this.txtJoinTeam.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlayername);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 50);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player";
            // 
            // txtPlayername
            // 
            this.txtPlayername.Location = new System.Drawing.Point(239, 13);
            this.txtPlayername.MaxLength = 14;
            this.txtPlayername.Name = "txtPlayername";
            this.txtPlayername.Size = new System.Drawing.Size(178, 20);
            this.txtPlayername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter a name that others on your team will see:";
            // 
            // lblStatusLine
            // 
            this.lblStatusLine.AutoSize = true;
            this.lblStatusLine.Location = new System.Drawing.Point(42, 269);
            this.lblStatusLine.Name = "lblStatusLine";
            this.lblStatusLine.Size = new System.Drawing.Size(98, 13);
            this.lblStatusLine.TabIndex = 5;
            this.lblStatusLine.Text = "Checking version...";
            // 
            // imgStatus
            // 
            this.imgStatus.Location = new System.Drawing.Point(8, 258);
            this.imgStatus.Name = "imgStatus";
            this.imgStatus.Size = new System.Drawing.Size(35, 27);
            this.imgStatus.TabIndex = 6;
            this.imgStatus.TabStop = false;
            // 
            // FFRNetworkUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 292);
            this.Controls.Add(this.imgStatus);
            this.Controls.Add(this.lblStatusLine);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.btnInit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FFRNetworkUI";
            this.Text = "FFRNetworkUI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblInit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtJoinTeam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlayername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatusLine;
        private System.Windows.Forms.PictureBox imgStatus;
    }
}