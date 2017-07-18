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
    public partial class UpdatePwdForm : Form
    {

        public string loginId;
        public UpdatePwdForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtOldPwd.Text.Trim()=="")
            {
                this.error.SetError(this.txtOldPwd,"请输入原密码");
                this.txtOldPwd.Focus();
                return;
            }
            this.error.Clear();
            if (this.txtNewPwd.Text.Trim() == "")
            {
                this.error.SetError(this.txtNewPwd, "请输入新密码");
                this.txtNewPwd.Focus();
                return;
            }
            this.error.Clear();
            if (this.txtAgainPwd.Text.Trim() != this.txtNewPwd.Text.Trim())
            {
                this.error.SetError(this.txtAgainPwd, "输入的密码与新密码有误");
                this.txtAgainPwd.Focus();
                return;
            }
            this.error.Clear();

            string connstring = "Data Source=.;Initial Catalog=MySchool;Persist Security Info=True;User ID=sa;Password=1";
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            string sql = string.Format("select count(*) from Admin where LoginId='{0}' and LoginPwd='{1}'",loginId,this.txtOldPwd.Text.Trim());
            SqlCommand comm = new SqlCommand(sql,conn);
            int result=Convert.ToInt32( comm.ExecuteScalar());
            conn.Close();
            if (result > 0)
            {
                conn.Open();
                sql = string.Format("update Admin set LoginPwd='{0}' where LoginId='{1}'",this.txtNewPwd.Text.Trim(),loginId);
                comm = new SqlCommand(sql,conn);
                result=comm.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("修改成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }
            }
            else
            {
                this.error.SetError(this.txtOldPwd,"原密码不正确！");
                this.txtOldPwd.Focus();
                this.txtOldPwd.SelectAll();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
