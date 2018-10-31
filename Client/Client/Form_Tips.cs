using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Form_Tips : Form
    {
        private string mess = "";
        public Form_Tips()
        {
            InitializeComponent();
        }

        public void SetInit(string str)
        {
            mess = str;
            //richTextBox1.Text = mess;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Tips_Load(object sender, EventArgs e)
        {
            string me = mess.Substring(7, mess.Length - 7);
            string[] smess = me.Split(',');
            Console.WriteLine(smess[0] + "==" + smess[1]);
            switch (smess[0])
            { 
                case "107":
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = global::Client.Properties.Resources.茶水;
                    break;
                case "110":
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = global::Client.Properties.Resources.清理缓存;
                    break;
                case "113":
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = global::Client.Properties.Resources.water;
                    break;
                case "116":
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = global::Client.Properties.Resources.茶;
                    break;
                default:
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = global::Client.Properties.Resources.icon_通知提示;
                    break;
            }
            richTextBox1.Text = smess[1];
        }
    }
}
