using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MySchoolBase
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = null;
            try
            {
                string connstring = "Data Source=.;DataBase=MySchool;User id=sa;pwd=1";
                conn = new SqlConnection(connstring);
                conn.Open();
                Console.WriteLine("请输入用户名：");
                string name = Console.ReadLine();
                Console.WriteLine("请输入密码：");
                string pwd = Console.ReadLine();
                string sql = string.Format("select count(*) from Admin where LoginId='{0}' and LoginPwd='{1}'", name, pwd);
                SqlCommand comm = new SqlCommand(sql, conn);
                int result = Convert.ToInt32(comm.ExecuteScalar());
                if (result > 0)
                {
                        Console.WriteLine("登录成功！");

                        Console.WriteLine("==========请选择操作键==========");
                        Console.WriteLine("1.查询学生数量");
                        Console.WriteLine("2.查看学生用户列表");
                        Console.WriteLine("3.查询指定学生姓名");
                        Console.WriteLine("4.查询指定学生的所有基本信息");
                        Console.WriteLine("5.插入年级信息");
                        Console.WriteLine("6.修改学生生日信息");
                        Console.WriteLine("7.删除学生信息");
                        Console.WriteLine("8.退出");
                        Console.WriteLine("================================");
                        int number = Convert.ToInt32(Console.ReadLine());
                        switch (number)
                        {
                            case 1:
                                sql ="select count(*) from Student";
                                comm = new SqlCommand(sql, conn);
                                result = Convert.ToInt32(comm.ExecuteScalar());
                                try
                                {
                                    Console.WriteLine("学生数量为：" + result);
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex.Message); 
                                }

                                break;
                            case 2:
                                sql = "select StudentNo,StudentName from Student";
                                comm = new SqlCommand(sql,conn);
                                SqlDataReader reader = comm.ExecuteReader();
                                Console.WriteLine("学号\t姓名");
                                while (reader.Read())
                                {
                                    Console.WriteLine("{0}\t{1}", reader["StudentNo"],reader["StudentName"]);

                                }
                                reader.Close();
                                break;
                            case 3:
                                Console.WriteLine("请输入指定学生学号：");
                                string id = Console.ReadLine();
                                sql = string.Format("select StudentName from Student where StudentNo='{0}'",id);
                                comm = new SqlCommand(sql,conn);
                                string StudentName=comm.ExecuteScalar().ToString();
                                Console.WriteLine("学生学号为："+StudentName);
                                break;
                            case 4:
                                Console.WriteLine("请输入指定学生学号：");
                                id = Console.ReadLine();
                                sql = string.Format("select StudentName,Sex,Phone,BornDate,Address,Email,IdentityCard from Student where StudentNo='{0}'",id);
                                comm = new SqlCommand(sql,conn);
                                reader = comm.ExecuteReader();
                                Console.WriteLine("姓名\t性别\t手机号\t出生日期\t地址\t邮箱\t身份证号码");
                                while (reader.Read())
                                {
                                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", reader["StudentName"], reader["Sex"], reader["Phone"], reader["BornDate"], reader["Address"], reader["Email"], reader["IdentityCard"]);

                                }
                                reader.Close();
                                break;
                            case 5:
                                Console.WriteLine("请输入插入的年级名称：");
                                string GradeName=Console.ReadLine();
                                sql = string.Format("insert into Grade(GradeName) values('{0}')",GradeName);
                                comm = new SqlCommand(sql,conn);
                                result=comm.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    Console.WriteLine("插入成功！");
                                }
                                else
                                {
                                    Console.WriteLine("插入失败！");
                                }
                                break;
                            case 6:
                                Console.WriteLine("请输入学生学号：");
                                id = Console.ReadLine();
                                Console.WriteLine("请输入需要修改的学生生日：");
                                string BornDate = Console.ReadLine();
                                sql = string.Format("update Student set BornDate='{0}' where StudentNo='{1}'",BornDate,id);
                                comm = new SqlCommand(sql,conn);
                                result=comm.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    Console.WriteLine("修改成功！");
                                }
                                else
                                {
                                    Console.WriteLine("修改失败！");
                                }
                                break;
                            case 7:
                                Console.WriteLine("请输入需要删除的学生学号：");
                                id = Console.ReadLine();
                                string sql1 = string.Format("select StudentName from Student where StudentNo='{0}'",id);
                                comm = new SqlCommand(sql1,conn);
                                StudentName=comm.ExecuteScalar().ToString();
                                Console.WriteLine("确认删除学生为{0}的学生信息？（Y/N）",StudentName);
                                string num = Console.ReadLine();
                                if (num=="Y")
                                {
                                    sql = string.Format("delete from Result where StudentNo='{0}'", id);
                                    comm = new SqlCommand(sql, conn);
                                    comm.ExecuteNonQuery();
                                    sql = string.Format("delete from Student where StudentNo='{0}'", id);
                                    comm.CommandText = sql;
                                    comm = new SqlCommand(sql, conn);
                                    result = comm.ExecuteNonQuery();
                                    if (result > 0)
                                    {
                                        Console.WriteLine("删除成功！");
                                    }
                                    else
                                    {
                                        Console.WriteLine("删除失败！");
                                    }
                                }
                                else if(num=="N")
                                {
                                    
                                }
                                
                                break;
                            case 8:
                               
                                break;
                        }
                    }

                
                else
                {
                    Console.WriteLine("登录失败！");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                Console.ReadLine();
            }
        }
    }
}
