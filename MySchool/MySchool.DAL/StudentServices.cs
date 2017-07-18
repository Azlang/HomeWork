using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MySchool.Model;

namespace MySchool.DAL
{
   public class StudentServices
    {
       public static List<Student> GetList()
       {
           List<Student> list = new List<Student>();
           string sql = "select * from Student";
           SqlDataReader reader = SqlHelper.ExcuteReader(sql);
           while (reader.Read())
           {
               Student student = new Student()
               {
                   StudentNo = Convert.ToInt32(reader["StudentNo"]),
                   Pwd = reader["Pwd"].ToString(),
                   StudentName = reader["StudentName"].ToString(),
                   Sex = reader["Sex"].ToString(),
                   GradeId = Convert.ToInt32(reader["GradeId"]),
                   Phone = reader["Phone"].ToString(),
                   BornDate = Convert.ToDateTime(reader["BornDate"]),
                   Address = reader["Address"].ToString(),
                   Email = reader["Email"].ToString(),
                   IdentityCard = reader["IdentityCard"].ToString()

               };
               list.Add(student);
           }
           reader.Close();
           return list;

       }
    }
}
