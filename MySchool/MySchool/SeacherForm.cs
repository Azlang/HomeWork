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
    public partial class SeacherForm : Form
    {
        DataSet ds;
        string connstring = "Data Source=.;Initial Catalog=MySchool;Persist Security Info=True;User ID=sa;Password=1";
        public SeacherForm()
        {
            InitializeComponent();
        }

        private void SeacherForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connstring);
            string sql = "select StudentNo,StudentName,Grade.GradeName,BornDate,Phone,Address,IdentityCard,Email from Student,Grade where Student.GradeId=Grade.GradeId";
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            ds = new DataSet();
            adapter.Fill(ds,"Student");

            sql = "select * from Grade";
            adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(ds,"Grade");
            DataRow dr =ds.Tables["Grade"].NewRow();
            dr[0] = -1;
            dr[1] = "全部";

            ds.Tables["Grade"].Rows.InsertAt(dr,0);

            this.cbxGrade.DisplayMember = "GradeName";
            this.cbxGrade.ValueMember = "GradeId";
            this.cbxGrade.DataSource = ds.Tables["Grade"];

            this.dgvList.DataSource=ds.Tables["Student"];
        }

        private void btnSeacher_Click(object sender, EventArgs e)
        {
            UpdateSeacher();
        }

        private void UpdateSeacher()
        {
            int gradeId = Convert.ToInt32(this.cbxGrade.SelectedValue);
            string name = this.txtName.Text.Trim();
            string sql = string.Format("select StudentNo,StudentName,Grade.GradeName,BornDate,Phone,Address,IdentityCard,Email from Student,Grade where Student.GradeId=Grade.GradeId and StudentName like '%{0}%' ", name);
            if (gradeId != -1)
            {
                sql += string.Format(" and Grade.GradeId='{0}'", gradeId);
            }
            SqlConnection conn = new SqlConnection(connstring);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            if (ds.Tables["Student"] != null)
            {
                ds.Tables["Student"].Clear();
                adapter.Fill(ds, "Student");

                this.dgvList.DataSource = ds.Tables["Student"];
            }
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            int StudentNo =Convert.ToInt32( this.dgvList.SelectedRows[0].Cells[0].Value);
            AddForm add = new AddForm(this);
            add.type = "修改";
            add.StudentNo = StudentNo;
            add.ShowDialog();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            int StudentNo = Convert.ToInt32(this.dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("是否删除该学生信息","系统提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result==DialogResult.OK)
            {
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                string sql = string.Format("delete from Result where StudentNo='{0}'",StudentNo);
                SqlCommand comm = new SqlCommand(sql,conn);
                comm.ExecuteNonQuery();

                sql = string.Format("delete from Student where StudentNo='{0}'",StudentNo);
                comm.CommandText = sql;
                int count = comm.ExecuteNonQuery();
                conn.Close();

                if (count>0)
                {
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }
    }
}
