using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MySchool.DAL
{
   public class SqlHelper
    {

       private static readonly string connstring = "Data Source=.;Initial Catalog=MySchool;Persist Security Info=True;User ID=sa;Password=1";

       //查询单行单列
       public static object ExcuteScalar(string sql)
       {
           SqlConnection conn = new SqlConnection(connstring);
           conn.Open();
           SqlCommand comm = new SqlCommand(sql,conn);
           object result= comm.ExecuteScalar();
           conn.Close();
           return result;
       }

       //查询多行多列
       public static SqlDataReader ExcuteReader(string sql)
       {
           SqlConnection conn = new SqlConnection(connstring);
           conn.Open();
           SqlCommand comm = new SqlCommand(sql,conn);
           SqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
           return reader;
 

       }


       //查询受影响的行数
       public static int ExcuteNonQuery(string sql)
       {
           SqlConnection conn = new SqlConnection(connstring);
           conn.Open();
           SqlCommand comm = new SqlCommand(sql,conn);
           int result=comm.ExecuteNonQuery();
           conn.Close();
           return result;
       }
       
    }
}
