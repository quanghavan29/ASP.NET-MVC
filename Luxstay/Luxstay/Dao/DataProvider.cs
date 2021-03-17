using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Luxstay.Dao
{


    public class DataProvider
    {
        //Khai bao het noi va xu ly database
        SqlConnection cnn; //Ket noi DB
        SqlDataAdapter da; //Xu ly cac cau lenh sql: select
        SqlCommand cmd; //Thuc thi cau lenh insert update

        public DataProvider()
        {
            connect();
        }

        public void connect()
        {
            try
            {
                string strCnn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                cnn = new SqlConnection(strCnn);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                cnn.Open();
                Console.WriteLine("Connect success !");
            }
            catch (Exception ex)
            {
            }
        }


        // Hàm execute 1 câu lệnh select
        public DataTable excuteQuery(string strSelect)
        {
            DataTable dt = new DataTable(); //Chua du lieu sau khi select
            try
            {
                da = new SqlDataAdapter(strSelect, cnn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("loi execute query: " + ex.Message);
            }
            return dt;
        }

        public void ExcuteNonQuery(string query)
        {
            try
            {
                cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi excute non query: " + ex.Message);
            }
        }
    }

}