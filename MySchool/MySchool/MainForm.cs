using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchool
{
    public partial class MainForm : Form
    {
        public string loginId;
        public MainForm()
        {
            InitializeComponent();
        }


        private void tsmiUpdatePwd_Click(object sender, EventArgs e)
        {
            UpdatePwdForm update = new UpdatePwdForm();
            update.loginId = loginId;
            update.ShowDialog();

        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm();
            add.ShowDialog();
        }

        private void tsmiSeacherStudent_Click(object sender, EventArgs e)
        {
            SeacherForm seacher = new SeacherForm();
            seacher.ShowDialog();
        }

    }
}
