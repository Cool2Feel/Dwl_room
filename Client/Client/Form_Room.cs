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
using System.Threading;

namespace Client
{
    public partial class Form_Room : Form
    {
        private string selfRoomName;
        private IPEndPoint peerUserIPEndPoint;
        //private UdpClient sendUdpClient;
        private string selfUserNember;
        private string roomIndex = "1";
        private LoginForm f;
        private bool Scheduled = false;

        public Form_Room(LoginForm f)
        {
            this.ControlBox = false; 
            InitializeComponent();
            this.f = f;
            dateTimePicker_time.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker_time.CustomFormat = "yyyy-MM-dd HH:mm:00";
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:00";
            //dateTimeInput1.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            //dateTimeInput1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }

        public void SetUserInfo(string selfName,string peerName, IPEndPoint peerIPEndPoint,string index)
        {
            selfRoomName = selfName;
            selfUserNember = peerName;
            peerUserIPEndPoint = peerIPEndPoint;
            roomIndex = index;
        }


        private void button_colse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 界面加载显示记录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Room_Load(object sender, EventArgs e)
        {
            textBox_name.Text = selfRoomName;
            if (selfRoomName.Contains("会议室"))
                label_roomane.Text = selfRoomName;
            else
                label_roomane.Text = selfRoomName + "会议室";
            string str = roomIndex + "_" + selfRoomName;
            //Console.WriteLine(str);
            DataTable tables = AccessFunction.GetClientProtocol(str);
            int Rows = tables.Rows.Count;
            if (tables != null && Rows > 0)
            { 
                for (int r = 0; r < Rows; r++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Add(tables.Rows[r][3].ToString());
                    item.SubItems.Add(tables.Rows[r][5].ToString());
                    item.SubItems.Add(tables.Rows[r][6].ToString());
                    item.SubItems.Add(tables.Rows[r][4].ToString());
                    listView_Schedul.Items.Add(item);
                }
                listView_Schedul.EndUpdate();
            }
            textBox_people.Focus();
            dateTimePicker_time.MaxDate = DateTime.Now.AddDays(15);
            //dateTimePicker_time.MinDate = DateTime.Now;
            dateTimePicker1.MaxDate = DateTime.Now.AddDays(15);
            //dateTimePicker1.MinDate = DateTime.Now;
        }
        /// <summary>
        /// 确认操作的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            if (textBox_business.Text.Equals("") && (textBox_people.Text.Equals("") || textBox_customer.Text.Equals("")))
            {
                MessageBox.Show("提示：会议室的安排的信息不能为空，请输入正确的信息！", "Tips");
                return;
            }
            Room_Info r = new Room_Info(textBox_name.Text, textBox_people.Text, textBox_customer.Text, textBox_business.Text, dateTimePicker_time.Value.ToString(),dateTimePicker1.Value.ToString(), richTextBox_tips.Text, false);
            if (Scheduled)//预约确认
            {
                string[] roomInfo = new string[8];
                roomInfo[0] = roomIndex + "_" + textBox_name.Text;
                roomInfo[1] = textBox_name.Text;
                roomInfo[2] = textBox_people.Text;
                roomInfo[3] = textBox_customer.Text;
                roomInfo[4] = textBox_business.Text;
                roomInfo[5] = dateTimePicker_time.Value.ToString("yyyy-MM-dd HH:mm:00");
                roomInfo[6] = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:00");
                roomInfo[7] = richTextBox_tips.Text;
                if (ModifyFlag)//预约信息修改确认操作
                {
                    AccessFunction.DeleteClientProtocol(roomIndex + "_" + textBox_name.Text);
                    ModifyFlag = false;
                }
                AccessFunction.InsertClientProtocol(roomInfo);
                f.OtherSend(string.Format("reservate,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", selfUserNember, peerUserIPEndPoint, roomIndex, textBox_name.Text, textBox_people.Text, textBox_customer.Text, textBox_business.Text, dateTimePicker_time.Value.ToString("yyyy-MM-dd HH:mm:00"), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:00"), richTextBox_tips.Text));
            }
            else
            {
                if (!DeleteFlag)//预约信息删除确认操作
                {
                    r.Setstate(true);//设置需要使用状态
                    f.SetRoom(textBox_name.Text, r);

                    f.OtherSend(string.Format("addin,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", selfUserNember, peerUserIPEndPoint, roomIndex, textBox_name.Text, textBox_people.Text, textBox_customer.Text, textBox_business.Text, dateTimePicker_time.Value.ToString("yyyy-MM-dd HH:mm:00"), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:00"), richTextBox_tips.Text));
                }
            }
            this.Close();
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
                if (dateTimePicker_time.Value > DateTime.Now)
                {
                    if ((dateTimePicker_time.Value.Date - DateTime.Now.Date).Days < 15)
                    {
                        Scheduled = true;
                        if(!ModifyFlag && SetFlag) 
                            MessageBox.Show("会议时间为未来的时间，已默认标记为预定的内容！", "提示");
                    }
                    else
                    {
                        Scheduled = false;
                        MessageBox.Show("会议室设置为预定的会议日期只能为未来15天内！", "提示");
                        checkBoxX1.Checked = false;
                    }
                }
                else
                {
                    Scheduled = false;
                    MessageBox.Show("会议室设置为预定的会议日期要大于当前的日期时间！", "提示");
                    dateTimePicker_time.Value = DateTime.Now;
                    checkBoxX1.Checked = false;
                }
            }
            else
                Scheduled = false;
        }

        private void dateTimePicker_time_ValueChanged(object sender, EventArgs e)
        {
            string str = roomIndex + "_" + selfRoomName;
            DataTable tables = AccessFunction.GetClientProtocol(str);
            int Rows = tables.Rows.Count;
            if (tables != null && Rows > 0)
            {
                for (int r = 0; r < Rows; r++)
                {
                    if (DateTime.Parse(tables.Rows[r][5].ToString()) <= dateTimePicker_time.Value && DateTime.Parse(tables.Rows[r][6].ToString()) >= dateTimePicker_time.Value)
                    {
                        MessageBox.Show(" 当前预定会议室开始时间已有预约记录(" + DateTime.Parse(tables.Rows[r][5].ToString()).ToString("HH:mm") + "-" + DateTime.Parse(tables.Rows[r][6].ToString()).ToString("HH:mm") + ")，请重新预约的安排其他时间段 !", " Tips");
                        return;
                    }
                }
            }

            if (dateTimePicker_time.Value.Date > DateTime.Now.Date)
            {
                DialogResult t;
                //if (Chinese_English)
                t = MessageBox.Show(" 当前设置的会议室开始时间不是今天，是否自动标记为预约的安排!", " Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                //else
                 //   t = MessageBox.Show(" 确定对选中的屏幕单元进行复位操作！", " 提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (t == DialogResult.Yes || t == DialogResult.OK)
                {
                    checkBoxX1.Checked = true;
                }
            }
        }
        private bool ModifyFlag = false;//修改标记
        /// <summary>
        /// 修改预约信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = listView_Schedul.SelectedItems[0].SubItems[1].Text;
            DataTable tables = AccessFunction.SearchClientProtocol(roomIndex + "_" + textBox_name.Text,s);
            //int Rows = tables.Rows.Count;
            listView_Schedul.Items.Remove(listView_Schedul.SelectedItems[0]);
            if (tables != null)
            {
                ModifyFlag = true;
                //textBox_name.Text = tables.Rows[r][3].ToString();
                textBox_people.Text = tables.Rows[0][2].ToString();
                textBox_customer.Text = tables.Rows[0][3].ToString();
                textBox_business.Text = tables.Rows[0][4].ToString();
                dateTimePicker_time.Value = DateTime.Parse(tables.Rows[0][5].ToString());
                dateTimePicker1.Value = DateTime.Parse(tables.Rows[0][6].ToString());
                richTextBox_tips.Text = tables.Rows[0][7].ToString();
                checkBoxX1.Checked = true;
            }
            //AccessFunction.DeleteClientProtocol(roomIndex + "_" + textBox_name.Text);
        }
        private bool DeleteFlag = false;//修改标记
        /// <summary>
        /// 删除预约
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFlag = true;
            string s = listView_Schedul.SelectedItems[0].SubItems[1].Text;
            DataTable tables = AccessFunction.SearchClientProtocol(roomIndex + "_" + textBox_name.Text, s);
            //int Rows = tables.Rows.Count;
            if (tables != null && tables.Rows.Count > 0)
            {
                string s1 = listView_Schedul.SelectedItems[0].SubItems[2].Text;
                string s2 = listView_Schedul.SelectedItems[0].SubItems[3].Text;
                //Console.WriteLine(s1);
                if (!s1.Equals(""))
                {
                    try
                    {
                        f.OtherSend(string.Format("delete,{0},{1},{2},{3},{4},{5}", selfUserNember, peerUserIPEndPoint, roomIndex, textBox_name.Text, s1, s2));
                        listView_Schedul.Items.Remove(listView_Schedul.SelectedItems[0]);
                        //AccessFunction.DeleteTerminalProtocol(roomIndex + "_" + textBox_name.Text, s);
                        AccessFunction.DeleteClientProtocol(roomIndex + "_" + textBox_name.Text);
                        //string message = roomIndex + "_" + textBox_name.Text + "," + s;
                        //Thread DeleteThread = new Thread(DeleteInfoFromTable);
                        //DeleteThread.Start(message);
                    }
                    catch
                    { 
                        return;
                    }
                }
            }
        }
        private bool SetFlag = true;//修改标记
        /// <summary>
        /// 安排设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = listView_Schedul.SelectedItems[0].SubItems[1].Text;
                DataTable tables = AccessFunction.SearchClientProtocol(roomIndex + "_" + textBox_name.Text, s);
                //int Rows = tables.Rows.Count;
                listView_Schedul.Items.Remove(listView_Schedul.SelectedItems[0]);
                if (tables != null && tables.Rows.Count > 0)
                {
                    DeleteFlag = false;
                    ModifyFlag = false;
                    SetFlag = false;
                    //textBox_name.Text = tables.Rows[r][3].ToString();
                    textBox_people.Text = tables.Rows[0][2].ToString();
                    textBox_customer.Text = tables.Rows[0][3].ToString();
                    textBox_business.Text = tables.Rows[0][4].ToString();
                    dateTimePicker_time.Value = DateTime.Parse(tables.Rows[0][5].ToString());
                    dateTimePicker1.Value = DateTime.Parse(tables.Rows[0][6].ToString());
                    richTextBox_tips.Text = tables.Rows[0][7].ToString();
                    checkBoxX1.Checked = false;
                }
                //AccessFunction.DeleteTerminalProtocol(roomIndex + "_" + textBox_name.Text, s);
                AccessFunction.DeleteClientProtocol(roomIndex + "_" + textBox_name.Text);
            }
            catch
            {
                return;
            }
        }

        private void DeleteInfoFromTable(object obj)
        {
            string message = (string)obj;
            string[] splitstring = message.Split(',');
            try
            {
                Console.WriteLine(message);
                string s = splitstring[0];
                AccessFunction.DeleteTerminalProtocol(s, splitstring[1]);
            }
            catch
            {
                return;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string str = roomIndex + "_" + selfRoomName;
            DataTable tables = AccessFunction.GetClientProtocol(str);
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
