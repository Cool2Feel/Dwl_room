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
    public partial class Form_Timing : Form
    {
        private string mess = "";
        private LoginForm form;
        private int num;
        public Form_Timing(LoginForm form)
        {
            InitializeComponent();
            this.form = form;
        }

        public void SetInit(string str,int key)
        {
            mess = str;
            num = key;
            //richTextBox1.Text = mess;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.Dictionary[num] = true;
            this.Close();
        }

        private void Form_Timing_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = mess;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
