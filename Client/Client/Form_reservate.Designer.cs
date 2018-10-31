namespace Client
{
    partial class Form_reservate
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
            this.dateTimePicker_time = new System.Windows.Forms.DateTimePicker();
            this.richTextBox_tips = new System.Windows.Forms.RichTextBox();
            this.textBox_business = new System.Windows.Forms.TextBox();
            this.textBox_customer = new System.Windows.Forms.TextBox();
            this.textBox_people = new System.Windows.Forms.TextBox();
            this.label_tips = new System.Windows.Forms.Label();
            this.label_datetime = new System.Windows.Forms.Label();
            this.label_business = new System.Windows.Forms.Label();
            this.label_customer = new System.Windows.Forms.Label();
            this.label_people = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.button_colse = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.comboBox_name = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dateTimePicker_time
            // 
            this.dateTimePicker_time.CalendarMonthBackground = System.Drawing.SystemColors.Menu;
            this.dateTimePicker_time.CalendarTitleBackColor = System.Drawing.Color.DarkSalmon;
            this.dateTimePicker_time.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_time.Location = new System.Drawing.Point(142, 171);
            this.dateTimePicker_time.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker_time.Name = "dateTimePicker_time";
            this.dateTimePicker_time.Size = new System.Drawing.Size(255, 29);
            this.dateTimePicker_time.TabIndex = 24;
            this.dateTimePicker_time.ValueChanged += new System.EventHandler(this.dateTimePicker_time_ValueChanged);
            // 
            // richTextBox_tips
            // 
            this.richTextBox_tips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_tips.Location = new System.Drawing.Point(142, 269);
            this.richTextBox_tips.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox_tips.MaxLength = 225;
            this.richTextBox_tips.Name = "richTextBox_tips";
            this.richTextBox_tips.Size = new System.Drawing.Size(291, 73);
            this.richTextBox_tips.TabIndex = 23;
            this.richTextBox_tips.Text = "";
            // 
            // textBox_business
            // 
            this.textBox_business.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_business.Location = new System.Drawing.Point(142, 79);
            this.textBox_business.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_business.MaxLength = 50;
            this.textBox_business.Name = "textBox_business";
            this.textBox_business.Size = new System.Drawing.Size(96, 29);
            this.textBox_business.TabIndex = 22;
            // 
            // textBox_customer
            // 
            this.textBox_customer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_customer.Location = new System.Drawing.Point(142, 125);
            this.textBox_customer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_customer.MaxLength = 50;
            this.textBox_customer.Name = "textBox_customer";
            this.textBox_customer.Size = new System.Drawing.Size(255, 29);
            this.textBox_customer.TabIndex = 21;
            // 
            // textBox_people
            // 
            this.textBox_people.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_people.Location = new System.Drawing.Point(337, 79);
            this.textBox_people.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_people.MaxLength = 10;
            this.textBox_people.Name = "textBox_people";
            this.textBox_people.Size = new System.Drawing.Size(96, 29);
            this.textBox_people.TabIndex = 20;
            // 
            // label_tips
            // 
            this.label_tips.AutoSize = true;
            this.label_tips.BackColor = System.Drawing.Color.Transparent;
            this.label_tips.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tips.Location = new System.Drawing.Point(55, 269);
            this.label_tips.Name = "label_tips";
            this.label_tips.Size = new System.Drawing.Size(68, 17);
            this.label_tips.TabIndex = 18;
            this.label_tips.Text = "备注信息：";
            // 
            // label_datetime
            // 
            this.label_datetime.AutoSize = true;
            this.label_datetime.BackColor = System.Drawing.Color.Transparent;
            this.label_datetime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_datetime.Location = new System.Drawing.Point(31, 177);
            this.label_datetime.Name = "label_datetime";
            this.label_datetime.Size = new System.Drawing.Size(92, 17);
            this.label_datetime.TabIndex = 17;
            this.label_datetime.Text = "会议开始时间：";
            // 
            // label_business
            // 
            this.label_business.AutoSize = true;
            this.label_business.BackColor = System.Drawing.Color.Transparent;
            this.label_business.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_business.Location = new System.Drawing.Point(67, 85);
            this.label_business.Name = "label_business";
            this.label_business.Size = new System.Drawing.Size(56, 17);
            this.label_business.TabIndex = 16;
            this.label_business.Text = "预定人：";
            // 
            // label_customer
            // 
            this.label_customer.AutoSize = true;
            this.label_customer.BackColor = System.Drawing.Color.Transparent;
            this.label_customer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_customer.Location = new System.Drawing.Point(55, 131);
            this.label_customer.Name = "label_customer";
            this.label_customer.Size = new System.Drawing.Size(68, 17);
            this.label_customer.TabIndex = 15;
            this.label_customer.Text = "开会宾客：";
            // 
            // label_people
            // 
            this.label_people.AutoSize = true;
            this.label_people.BackColor = System.Drawing.Color.Transparent;
            this.label_people.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_people.Location = new System.Drawing.Point(250, 85);
            this.label_people.Name = "label_people";
            this.label_people.Size = new System.Drawing.Size(68, 17);
            this.label_people.TabIndex = 14;
            this.label_people.Text = "参会人数：";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.BackColor = System.Drawing.Color.Transparent;
            this.label_name.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(250, 39);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(80, 17);
            this.label_name.TabIndex = 13;
            this.label_name.Text = "会议室名称：";
            // 
            // button_colse
            // 
            this.button_colse.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_colse.Location = new System.Drawing.Point(294, 374);
            this.button_colse.Name = "button_colse";
            this.button_colse.Size = new System.Drawing.Size(75, 31);
            this.button_colse.TabIndex = 26;
            this.button_colse.Text = "关闭";
            this.button_colse.UseVisualStyleBackColor = true;
            this.button_colse.Click += new System.EventHandler(this.button_colse_Click);
            // 
            // button_ok
            // 
            this.button_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ok.Location = new System.Drawing.Point(148, 374);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 31);
            this.button_ok.TabIndex = 25;
            this.button_ok.Text = "确认";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(43, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "会议室形式：";
            // 
            // comboBox_type
            // 
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "内部会议室",
            "坂田会议室",
            "惠南会议室"});
            this.comboBox_type.Location = new System.Drawing.Point(142, 35);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(96, 25);
            this.comboBox_type.TabIndex = 28;
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox_name
            // 
            this.comboBox_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_name.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_name.FormattingEnabled = true;
            this.comboBox_name.Items.AddRange(new object[] {
            "内部会议室",
            "坂田会议室",
            "惠州会议室"});
            this.comboBox_name.Location = new System.Drawing.Point(337, 35);
            this.comboBox_name.Name = "comboBox_name";
            this.comboBox_name.Size = new System.Drawing.Size(96, 25);
            this.comboBox_name.TabIndex = 29;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Location = new System.Drawing.Point(142, 217);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(255, 29);
            this.dateTimePicker1.TabIndex = 31;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(31, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 30;
            this.label2.Text = "会议结束时间：";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.BackColor = System.Drawing.Color.Transparent;
            this.checkBox3.Location = new System.Drawing.Point(418, 221);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(51, 21);
            this.checkBox3.TabIndex = 34;
            this.checkBox3.Text = "全天";
            this.checkBox3.UseVisualStyleBackColor = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(418, 197);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(51, 21);
            this.checkBox2.TabIndex = 33;
            this.checkBox2.Text = "下午";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(418, 173);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 21);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "上午";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form_reservate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Client.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(519, 427);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_name);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_colse);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.dateTimePicker_time);
            this.Controls.Add(this.richTextBox_tips);
            this.Controls.Add(this.textBox_business);
            this.Controls.Add(this.textBox_customer);
            this.Controls.Add(this.textBox_people);
            this.Controls.Add(this.label_tips);
            this.Controls.Add(this.label_datetime);
            this.Controls.Add(this.label_business);
            this.Controls.Add(this.label_customer);
            this.Controls.Add(this.label_people);
            this.Controls.Add(this.label_name);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_reservate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会议室预约安排";
            this.Load += new System.EventHandler(this.Form_reservate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_time;
        private System.Windows.Forms.RichTextBox richTextBox_tips;
        private System.Windows.Forms.TextBox textBox_business;
        private System.Windows.Forms.TextBox textBox_customer;
        private System.Windows.Forms.TextBox textBox_people;
        private System.Windows.Forms.Label label_tips;
        private System.Windows.Forms.Label label_datetime;
        private System.Windows.Forms.Label label_business;
        private System.Windows.Forms.Label label_customer;
        private System.Windows.Forms.Label label_people;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Button button_colse;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.ComboBox comboBox_name;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}