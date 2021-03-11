using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Luxstay.Dao
{


    public class DataProvide
    {
        //Khai bao het noi va xu ly database
        SqlConnection cnn; //Ket noi DB
        SqlDataAdapter da; //Xu ly cac cau lenh sql: select
        SqlCommand cmd; //Thuc thi cau lenh insert update
        public void connect()
        {
            try
            {
                String strCnn = "Data Source=localhost;Initial Catalog=Luxstay;Integrated Security=True";
                /*                string strCnn = ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;*/
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
    }

    
}