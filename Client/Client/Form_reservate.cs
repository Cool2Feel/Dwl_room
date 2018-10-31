using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Form_reservate : Form
    {
        private string roomIndex = "1";
        private IPEndPoint peerUserIPEndPoint;
        //private UdpClient sendUdpClient;
        private string selfUserNember;
        private LoginForm f;
        public Form_reservate(LoginForm f)
        {
            InitializeComponent();
            this.f = f;
            dateTimePicker_time.Format = DateTimePickerFormat.Custom;
            dateTimePicker_time.CustomFormat = "yyyy-MM-dd HH:mm:00";

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:00";
        }

        public void SetUserInfo(string peerName, IPEndPoint peerIPEndPoint, string index)
        {
            selfUserNember = peerName;
            peerUserIPEndPoint = peerIPEndPoint;
            roomIndex = index;
        }

        private void Form_reservate_Load(object sender, EventArgs e)
        {
            comboBox_type.SelectedIndex = int.Parse(roomIndex) - 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_name.Items.Clear();
            if (comboBox_type.SelectedIndex == 0)
            {
                comboBox_name.Items.Add("研发会议室一");
                comboBox_name.Items.Add("会议室1");
                comboBox_name.Items.Add("会议室3");
                comboBox_name.Items.Add("电气部对面");
                comboBox_name.Items.Add("软件部");
                comboBox_name.Items.Add("结构部");
                comboBox_name.Items.Add("皓丽部");
                comboBox_name.Items.Add("会议室2");
                comboBox_name.Items.Add("大客户六部");
                comboBox_name.Items.Add("业务部");
                comboBox_name.Items.Add("电气部");
                comboBox_name.Items.Add("技术部会议室");
                comboBox_name.Items.Add("会议室11");
                roomIndex = "1";
            }
            else if(comboBox_type.SelectedIndex == 1)
            {
                comboBox_name.Items.Add("1号");
                comboBox_name.Items.Add("2号");
                comboBox_name.Items.Add("3号");
                comboBox_name.Items.Add("C号");
                comboBox_name.Items.Add("5号");
                comboBox_name.Items.Add("6号");
                comboBox_name.Items.Add("会议室A");
                comboBox_name.Items.Add("多功能厅");
                comboBox_name.Items.Add("大客户六部");
                comboBox_name.Items.Add("7号");
                comboBox_name.Items.Add("8号");
                comboBox_name.Items.Add("9号");
                comboBox_name.Items.Add("10号");
                comboBox_name.Items.Add("11号");
                comboBox_name.Items.Add("会议室B");
                comboBox_name.Items.Add("12号");
                comboBox_name.Items.Add("13号");
                comboBox_name.Items.Add("14号");
                comboBox_name.Items.Add("15号");
                comboBox_name.Items.Add("17号");
                comboBox_name.Items.Add("18号");
                comboBox_name.Items.Add("16号");
                comboBox_name.Items.Add("19号");
                comboBox_name.Items.Add("20号");
                comboBox_name.Items.Add("21号");
                roomIndex = "2";
            }
            else if (comboBox_type.SelectedIndex == 2)
            {
                comboBox_name.Items.Add("1号");
                comboBox_name.Items.Add("2号");
                comboBox_name.Items.Add("3号");
                comboBox_name.Items.Add("5号");
                comboBox_name.Items.Add("6号");
                comboBox_name.Items.Add("7号");
                comboBox_name.Items.Add("会议室A");
                comboBox_name.Items.Add("技术部会议室");
                comboBox_name.Items.Add("8号");
                comboBox_name.Items.Add("9号");
                comboBox_name.Items.Add("10号");
                comboBox_name.Items.Add("11号");
                comboBox_name.Items.Add("12号");
                comboBox_name.Items.Add("13号");
                comboBox_name.Items.Add("15号");
                comboBox_name.Items.Add("多功能厅");
                roomIndex = "3";
            }
            comboBox_name.SelectedIndex = 0;
            dateTimePicker_time.MaxDate = DateTime.Now.AddDays(15);
            //dateTimePicker_time.MinDate = DateTime.Now;
            dateTimePicker1.MaxDate = DateTime.Now.AddDays(15);
            //dateTimePicker1.MinDate = DateTime.Now;
        }

        private void button_colse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认预约，上传信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            if (textBox_business.Text.Equals("") && textBox_people.Text.Equals("") || textBox_customer.Text.Equals(""))
            {
                MessageBox.Show("提示：会议室的安排的信息不能为空，请输入正确的信息！","Tips");
                return;
            }
            string[] roomInfo = new string[9];
            roomInfo[0] = roomIndex + "_" + comboBox_name.Text;
            roomInfo[1] = comboBox_name.Text;
            roomInfo[2] = textBox_people.Text;
            roomInfo[3] = textBox_customer.Text;
            roomInfo[4] = textBox_business.Text;
            roomInfo[5] = dateTimePicker_time.Value.ToString("yyyy-MM-dd HH:mm:00");
            roomInfo[6] = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:00");
            roomInfo[7] = richTextBox_tips.Text;
            roomInfo[8] = roomIndex;
            try
            {
                AccessFunction.InsertClientProtocol(roomInfo);
                f.OtherSend(string.Format("reservate,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", selfUserNember, peerUserIPEndPoint, roomIndex, comboBox_name.Text, textBox_people.Text, textBox_customer.Text, textBox_business.Text, dateTimePicker_time.Value.ToString("yyyy-MM-dd HH:mm:00"), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:00"), richTextBox_tips.Text));
            }
            catch
            { }
            this.Close();
        }

        private void dateTimePicker_time_ValueChanged(object sender, EventArgs e)
        {
            string s = roomIndex + "_" + comboBox_name.Text;
            DataTable tables = AccessFunction.GetClientProtocol(s);
            int Rows = tables.Rows.Count;
            if (tables != null && Rows > 0)
            {
                for (int r = 0; r < Rows; r++)
                {
                    if (DateTime.Parse(tables.Rows[r][5].ToString()) <= dateTimePicker_time.Value && DateTime.Parse(tables.Rows[r][6].ToString()) >= dateTimePicker_time.Value)
                    {
                        MessageBox.Show(" 当前预定会议室开始时间已有预约记录(" + DateTime.Parse(tables.Rows[r][5].ToString()).ToString("HH:mm:00") + "-" + DateTime.Parse(tables.Rows[r][6].ToString()).ToString("HH:mm:00") + ")，请重新预约的安排其他时间段 !", " Tips");
                        return;
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string s = roomIndex + "_" + comboBox_name.Text;
            DataTable tables = AccessFunction.GetClientProtocol(s);
            int Rows = tables.Rows.Count;
            if (tables != null && Rows > 0)
            {
                for (int r = 0; r < Rows; r++)
                {
                    if (DateTime.Parse(tables.Rows[r][5].ToString()) <= dateTimePicker1.Value && DateTime.Parse(tables.Rows[r][6].ToString()) >= dateTimePicker1.Value)
                    {
                        MessageBox.Show(" 当前预定会议室开始时间已有预约记录(" + DateTime.Parse(tables.Rows[r][5].ToString()).ToString("HH:mm:00") + "-" + DateTime.Parse(tables.Rows[r][6].ToString()).ToString("HH:mm:00") + ")，请重新预约的安排其他时间段 !", " Tips");
                        return;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                dateTimePicker_time.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:30:00"));
                dateTimePicker1.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 12:00:00"));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                dateTimePicker_time.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 14:00:00"));
                dateTimePicker1.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 17:30:00"));
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
                dateTimePicker_time.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:30:00"));
                dateTimePicker1.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 17:30:00"));
            }
        }
    }
}
