namespace Server
{
    partial class ServerForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.listboxStatus = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbServerport = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer_auto = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // listboxStatus
            // 
            this.listboxStatus.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.listboxStatus.FormattingEnabled = true;
            this.listboxStatus.ItemHeight = 12;
            this.listboxStatus.Location = new System.Drawing.Point(2, 2);
            this.listboxStatus.Name = "listboxStatus";
            this.listboxStatus.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listboxStatus.Size = new System.Drawing.Size(498, 244);
            this.listboxStatus.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器地址：";
            // 
            // txbServerIP
            // 
            this.txbServerIP.Location = new System.Drawing.Point(85, 267);
            this.txbServerIP.Name = "txbServerIP";
            this.txbServerIP.Size = new System.Drawing.Size(112, 21);
            this.txbServerIP.TabIndex = 2;
            this.txbServerIP.TextChanged += new System.EventHandler(this.txbServerIP_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "：";
            // 
            // txbServerport
            // 
            this.txbServerport.Location = new System.Drawing.Point(212, 267);
            this.txbServerport.Name = "txbServerport";
            this.txbServerport.Size = new System.Drawing.Size(45, 21);
            this.txbServerport.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(345, 265);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(53, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(404, 265);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(54, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timer_auto
            // 
            this.timer_auto.Interval = 60000;
            this.timer_auto.Tick += new System.EventHandler(this.timer_auto_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "KTC会议室服务端";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 294);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txbServerport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbServerIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listboxStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServerForm";
            this.Text = "会议系统服务器";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Resize += new System.EventHandler(this.ServerForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listboxStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbServerport;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer_auto;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

