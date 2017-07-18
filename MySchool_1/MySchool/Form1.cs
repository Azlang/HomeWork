using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MySchool
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string loginId = this.txtName.Text.Trim();
            string loginPwd = this.txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(loginId))
            {
                MessageBox.Show("请输入用户名！");
                this.txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(loginPwd))
            {
                MessageBox.Show("请输入密码！");
                this.txtPwd.Focus();
                return;
            }

            string connstring = "Data Source=.;Initial Catalog=MySchool;Persist Security Info=True;User ID=sa;Password=1";
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            string sql = string.Format("select count(*) from Admin where loginId='{0}' and loginPwd='{1}'",loginId,loginPwd);
            SqlCommand comm = new SqlCommand(sql,conn);
            int result=Convert.ToInt32(comm.ExecuteScalar());
            conn.Close();
            if (result > 0)
            {
                MainForm mainForm = new MainForm();
                mainForm.loginId = loginId;
                this.Hide();
                mainForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("登录失败！");
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



    }
}
