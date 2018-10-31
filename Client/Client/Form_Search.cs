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
    public partial class Form_Search : Form
    {
        private string selfSearchSatate = "";
        public Form_Search()
        {
            InitializeComponent();
        }

        public void SetSearchInfo(string selfInfo)
        {
            selfSearchSatate = selfInfo;
        }

        private void Form_Search_Load(object sender, EventArgs e)
        {
            label_Info.Text = selfSearchSatate;
        }

        public void ChangeStateInfo(string content)
        {
            label_Info.Text = content;
            label_Info.Refresh();
        }

        public void CloseForm()
        {
            this.Close();
        }

    }
}
