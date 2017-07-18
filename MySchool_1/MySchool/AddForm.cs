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
    public partial class AddForm : Form
    {
        string connstring = "Data Source=.;Initial Catalog=MySchool;Persist Security Info=True;User ID=sa;Password=1";

        public string type;
        public int StudentNo;

        SeacherForm seacher;
        public AddForm(SeacherForm seacher)
        {
            this.seacher = seacher;

        }
        public AddForm()
        {
            InitializeComponent();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connstring);
            string sql = "select * from Grade";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(ds,"Grade");
            this.cbxGrade.DisplayMember = "GradeName";
            this.cbxGrade.ValueMember = "GradeId";
            this.cbxGrade.DataSource=ds.Tables["Grade"];

            if (type=="修改")
            {
                this.label1.Visible = true;
                this.txtStudentNo.Visible = true;
                this.txtStudentNo.Text = StudentNo.ToString();
                this.Text = "修改学生信息";
                this.btnAdd.Text = "修改";


                sql = string.Format("select * from Student where StudentNo='{0}'",StudentNo);
                conn.Open();
                SqlCommand comm = new SqlCommand(sql,conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    this.txtPwd.Text = reader["Pwd"].ToString();
                    this.txtStudentName.Text = reader["StudentName"].ToString();
                    if (reader["Gender"].ToString().Trim()=="男")
                    {
                        this.rbtnNan.Checked = true;
                    }
                    else
                    {
                        this.rbtnNv.Checked = true; 
                    }
                    this.cbxGrade.SelectedValue =Convert.ToInt32( reader["GradeId"]);
                    this.txtPhone.Text = reader["Phone"].ToString();
                    this.dateTime.Value =Convert.ToDateTime( reader["BornDate"]);
                    this.txtEmail.Text = reader["Email"].ToString();
                    this.txtIdentityCard.Text = reader["IdentityCard"].ToString();

                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string loginPwd = this.txtPwd.Text.Trim();
            string name = this.txtStudentName.Text.Trim();
            string gender = this.rbtnNan.Checked ? "男" : "女";
            int gradeId = Convert.ToInt32( this.cbxGrade.SelectedValue);
            string phone = this.txtPhone.Text.Trim();
            DateTime borndate = this.dateTime.Value;
            string email = this.txtEmail.Text.Trim();
            string identityCard = this.txtIdentityCard.Text.Trim();

            string sql = string.Format("insert into Student(LoginPwd,StudentName,Gender,GradeId,Phone,BornDate,Email,IdentityCard) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",loginPwd,name,gender,gradeId,phone,borndate,email,identityCard);


            if (type=="修改")
            {
                sql = string.Format("update Student set Pwd='{0}',StudentName='{1}',Gender='{2}',GradeId='{3}',Phone='{4}',BornDate='{5}',Email='{6}',IdentityCard='{7}'",loginPwd,name,gender,gradeId,phone,borndate,email,identityCard);
            }
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            SqlCommand comm = new SqlCommand(sql,conn);
            int result=comm.ExecuteNonQuery();
            conn.Close();

            if (type!="修改")
            {
                if (result>0)
                {
                    MessageBox.Show("添加成功！");
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
            }
            else
            {
                if (result > 0)
                {
                    MessageBox.Show("修改成功！");
                    
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }
            }
          
        }
    }
}
