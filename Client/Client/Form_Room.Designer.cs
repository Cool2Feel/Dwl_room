namespace Client
{
    partial class Form_Room
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ModifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_colse = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView_Schedul = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.label_roomane = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dateTimePicker_time = new System.Windows.Forms.DateTimePicker();
            this.richTextBox_tips = new System.Windows.Forms.RichTextBox();
            this.textBox_business = new System.Windows.Forms.TextBox();
            this.textBox_customer = new System.Windows.Forms.TextBox();
            this.textBox_people = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_tips = new System.Windows.Forms.Label();
            this.label_datetime = new System.Windows.Forms.Label();
            this.label_business = new System.Windows.Forms.Label();
            this.label_customer = new System.Windows.Forms.Label();
            this.label_people = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.SetToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 70);
            // 
            // ModifyToolStripMenuItem
            // 
            this.ModifyToolStripMenuItem.Name = "ModifyToolStripMenuItem";
            this.ModifyToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ModifyToolStripMenuItem.Text = "修改";
            this.ModifyToolStripMenuItem.Visible = false;
            this.ModifyToolStripMenuItem.Click += new System.EventHandler(this.ModifyToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // SetToolStripMenuItem
            // 
            this.SetToolStripMenuItem.Name = "SetToolStripMenuItem";
            this.SetToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.SetToolStripMenuItem.Text = "安排";
            this.SetToolStripMenuItem.Click += new System.EventHandler(this.SetToolStripMenuItem_Click);
            // 
            // button_ok
            // 
            this.button_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ok.Location = new System.Drawing.Point(209, 422);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 31);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "确认";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_colse
            // 
            this.button_colse.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_colse.Location = new System.Drawing.Point(363, 422);
            this.button_colse.Name = "button_colse";
            this.button_colse.Size = new System.Drawing.Size(75, 31);
            this.button_colse.TabIndex = 3;
            this.button_colse.Text = "关闭";
            this.button_colse.UseVisualStyleBackColor = true;
            this.button_colse.Click += new System.EventHandler(this.button_colse_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Client.Properties.Resources.back;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.button_ok);
            this.panel1.Controls.Add(this.button_colse);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label_roomane);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBoxX1);
            this.panel1.Controls.Add(this.dateTimePicker_time);
            this.panel1.Controls.Add(this.richTextBox_tips);
            this.panel1.Controls.Add(this.textBox_business);
            this.panel1.Controls.Add(this.textBox_customer);
            this.panel1.Controls.Add(this.textBox_people);
            this.panel1.Controls.Add(this.textBox_name);
            this.panel1.Controls.Add(this.label_tips);
            this.panel1.Controls.Add(this.label_datetime);
            this.panel1.Controls.Add(this.label_business);
            this.panel1.Controls.Add(this.label_customer);
            this.panel1.Controls.Add(this.label_people);
            this.panel1.Controls.Add(this.label_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 465);
            this.panel1.TabIndex = 1;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.BackColor = System.Drawing.Color.Transparent;
            this.checkBox3.Location = new System.Drawing.Point(477, 167);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(51, 21);
            this.checkBox3.TabIndex = 21;
            this.checkBox3.Text = "全天";
            this.checkBox3.UseVisualStyleBackColor = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(477, 146);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(51, 21);
            this.checkBox2.TabIndex = 20;
            this.checkBox2.Text = "下午";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(477, 125);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 21);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "上午";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.listView_Schedul);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(106, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 138);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前会议已预约记录：";
            // 
            // listView_Schedul
            // 
            this.listView_Schedul.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5});
            this.listView_Schedul.ContextMenuStrip = this.contextMenuStrip;
            this.listView_Schedul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Schedul.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_Schedul.FullRowSelect = true;
            this.listView_Schedul.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_Schedul.Location = new System.Drawing.Point(3, 22);
            this.listView_Schedul.Name = "listView_Schedul";
            this.listView_Schedul.Size = new System.Drawing.Size(420, 113);
            this.listView_Schedul.TabIndex = 14;
            this.listView_Schedul.UseCompatibleStateImageBehavior = false;
            this.listView_Schedul.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "预约宾客";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "开始时间";
            this.columnHeader2.Width = 114;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "结束时间";
            this.columnHeader4.Width = 126;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "预定人员";
            this.columnHeader5.Width = 84;
            // 
            // label_roomane
            // 
            this.label_roomane.AutoSize = true;
            this.label_roomane.BackColor = System.Drawing.Color.Transparent;
            this.label_roomane.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_roomane.Location = new System.Drawing.Point(259, 15);
            this.label_roomane.Name = "label_roomane";
            this.label_roomane.Size = new System.Drawing.Size(84, 28);
            this.label_roomane.TabIndex = 18;
            this.label_roomane.Text = "xxxxxx";
            this.label_roomane.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Location = new System.Drawing.Point(189, 158);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(267, 29);
            this.dateTimePicker1.TabIndex = 17;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(77, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "会议结束时间：";
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.Class = "";
            this.checkBoxX1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxX1.Location = new System.Drawing.Point(473, 197);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(135, 32);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.checkBoxX1.TabIndex = 13;
            this.checkBoxX1.Text = "设为预定信息";
            this.checkBoxX1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkBoxX1.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // dateTimePicker_time
            // 
            this.dateTimePicker_time.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_time.Location = new System.Drawing.Point(189, 125);
            this.dateTimePicker_time.Name = "dateTimePicker_time";
            this.dateTimePicker_time.Size = new System.Drawing.Size(267, 29);
            this.dateTimePicker_time.TabIndex = 12;
            this.dateTimePicker_time.ValueChanged += new System.EventHandler(this.dateTimePicker_time_ValueChanged);
            // 
            // richTextBox_tips
            // 
            this.richTextBox_tips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_tips.Location = new System.Drawing.Point(189, 197);
            this.richTextBox_tips.MaxLength = 225;
            this.richTextBox_tips.Name = "richTextBox_tips";
            this.richTextBox_tips.Size = new System.Drawing.Size(267, 59);
            this.richTextBox_tips.TabIndex = 11;
            this.richTextBox_tips.Text = "";
            // 
            // textBox_business
            // 
            this.textBox_business.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_business.Location = new System.Drawing.Point(189, 57);
            this.textBox_business.MaxLength = 50;
            this.textBox_business.Name = "textBox_business";
            this.textBox_business.Size = new System.Drawing.Size(111, 29);
            this.textBox_business.TabIndex = 9;
            // 
            // textBox_customer
            // 
            this.textBox_customer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_customer.Location = new System.Drawing.Point(189, 92);
            this.textBox_customer.MaxLength = 50;
            this.textBox_customer.Name = "textBox_customer";
            this.textBox_customer.Size = new System.Drawing.Size(267, 29);
            this.textBox_customer.TabIndex = 8;
            // 
            // textBox_people
            // 
            this.textBox_people.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_people.Location = new System.Drawing.Point(419, 57);
            this.textBox_people.MaxLength = 10;
            this.textBox_people.Name = "textBox_people";
            this.textBox_people.Size = new System.Drawing.Size(99, 29);
            this.textBox_people.TabIndex = 7;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(189, 21);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.ReadOnly = true;
            this.textBox_name.Size = new System.Drawing.Size(23, 23);
            this.textBox_name.TabIndex = 6;
            this.textBox_name.TabStop = false;
            this.textBox_name.Visible = false;
            // 
            // label_tips
            // 
            this.label_tips.AutoSize = true;
            this.label_tips.BackColor = System.Drawing.Color.Transparent;
            this.label_tips.Location = new System.Drawing.Point(101, 197);
            this.label_tips.Name = "label_tips";
            this.label_tips.Size = new System.Drawing.Size(68, 17);
            this.label_tips.TabIndex = 5;
            this.label_tips.Text = "备注信息：";
            // 
            // label_datetime
            // 
            this.label_datetime.AutoSize = true;
            this.label_datetime.BackColor = System.Drawing.Color.Transparent;
            this.label_datetime.Location = new System.Drawing.Point(77, 131);
            this.label_datetime.Name = "label_datetime";
            this.label_datetime.Size = new System.Drawing.Size(92, 17);
            this.label_datetime.TabIndex = 4;
            this.label_datetime.Text = "会议开始时间：";
            // 
            // label_business
            // 
            this.label_business.AutoSize = true;
            this.label_business.BackColor = System.Drawing.Color.Transparent;
            this.label_business.Location = new System.Drawing.Point(113, 63);
            this.label_business.Name = "label_business";
            this.label_business.Size = new System.Drawing.Size(56, 17);
            this.label_business.TabIndex = 3;
            this.label_business.Text = "预定人：";
            // 
            // label_customer
            // 
            this.label_customer.AutoSize = true;
            this.label_customer.BackColor = System.Drawing.Color.Transparent;
            this.label_customer.Location = new System.Drawing.Point(101, 98);
            this.label_customer.Name = "label_customer";
            this.label_customer.Size = new System.Drawing.Size(68, 17);
            this.label_customer.TabIndex = 2;
            this.label_customer.Text = "开会宾客：";
            // 
            // label_people
            // 
            this.label_people.AutoSize = true;
            this.label_people.BackColor = System.Drawing.Color.Transparent;
            this.label_people.Location = new System.Drawing.Point(331, 63);
            this.label_people.Name = "label_people";
            this.label_people.Size = new System.Drawing.Size(68, 17);
            this.label_people.TabIndex = 1;
            this.label_people.Text = "参会人数：";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.BackColor = System.Drawing.Color.Transparent;
            this.label_name.Location = new System.Drawing.Point(89, 21);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(80, 17);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "会议室名称：";
            // 
            // Form_Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(635, 465);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Room";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会议室信息";
            this.Load += new System.EventHandler(this.Form_Room_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_business;
        private System.Windows.Forms.TextBox textBox_customer;
        private System.Windows.Forms.TextBox textBox_people;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_tips;
        private System.Windows.Forms.Label label_datetime;
        private System.Windows.Forms.Label label_business;
        private System.Windows.Forms.Label label_customer;
        private System.Windows.Forms.Label label_people;
        private System.Windows.Forms.RichTextBox richTextBox_tips;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_colse;
        private System.Windows.Forms.DateTimePicker dateTimePicker_time;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private System.Windows.Forms.ListView listView_Schedul;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ModifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_roomane;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}