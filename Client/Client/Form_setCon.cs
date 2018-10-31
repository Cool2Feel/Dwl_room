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
    public partial class Form_setCon : Form
    {
        private InitFiles settingFile;//配置文件
        public Form_setCon()
        {
            InitializeComponent();
        }

        private void Form_setCon_Load(object sender, EventArgs e)
        {
            IPAddress[] localIP = Dns.GetHostAddresses("");
            txtserverIP.Text = localIP[0].ToString();
            txtLocalIP.Text = localIP[0].ToString();
            // 随机指定本地端口
            //Random random = new Random();
            //int port = random.Next(1024, 65500);
            //txtlocalport.Text = port.ToString();
            txtServerport.Text = "7088";

            settingFile = new InitFiles(Application.StartupPath + "\\setting.ini");
            txtserverIP.Text = settingFile.ReadString("SETTING", "SERVERIP", txtserverIP.Text);
            txtLocalIP.Text = settingFile.ReadString("SETTING", "LOCALIP", txtLocalIP.Text);
            int index = settingFile.ReadInteger("SETTING", "INDEX", 0);
            comboBox_user.SelectedIndex = index;

            bool check = settingFile.ReadBool("SETTING", "AUTORUN", false);
            checkBox_auto.Checked = check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            settingFile.WriteString("SETTING", "SERVERIP", txtserverIP.Text);
            settingFile.WriteString("SETTING", "LOCALIP", txtLocalIP.Text);
            settingFile.WriteInteger("SETTING", "INDEX", comboBox_user.SelectedIndex);
            settingFile.WriteBool("SETTING", "AUTORUN", checkBox_auto.Checked);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
