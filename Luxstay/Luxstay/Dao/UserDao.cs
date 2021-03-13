using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Luxstay.Dao
{
    public class UserDao
    {
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

        public User findByEmailAndPassword(string email, string password)
        {
            connect();
            String query = "SELECT * FROM [User] "
                        + "WHERE email = '" + email + "' and password = '" + password + "'";
            da = new SqlDataAdapter(query, cnn);
            try
            {
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                User user = new User();
                user.user_id = Int32.Parse(dataTable.Rows[0]["user_id"].ToString());
                user.email = dataTable.Rows[0]["email"].ToString();
                user.password = dataTable.Rows[0]["password"].ToString();
                user.name = dataTable.Rows[0]["name"].ToString();
                user.role = dataTable.Rows[0]["role"].ToString();
                user.phone = dataTable.Rows[0]["phone"].ToString();
                user.address = dataTable.Rows[0]["address"].ToString();
                user.gender = bool.Parse(dataTable.Rows[0]["gender"].ToString());
                user.verify = bool.Parse(dataTable.Rows[0]["verify"].ToString());
                return user;
            }
            catch
            {
                return null;
            }
        }

        public User findByEmail(string email)
        {
            connect();
            String query = "SELECT * FROM [User] "
                        + "WHERE email = '" + email + "'";
            da = new SqlDataAdapter(query, cnn);
            try
            {
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                User user = new User();
                user.user_id = Int32.Parse(dataTable.Rows[0]["user_id"].ToString());
                user.email = dataTable.Rows[0]["email"].ToString();
                user.password = dataTable.Rows[0]["password"].ToString();
                user.name = dataTable.Rows[0]["name"].ToString();
                user.role = dataTable.Rows[0]["role"].ToString();
                user.phone = dataTable.Rows[0]["phone"].ToString();
                user.address = dataTable.Rows[0]["address"].ToString();
                user.gender = bool.Parse(dataTable.Rows[0]["gender"].ToString());
                user.verify = bool.Parse(dataTable.Rows[0]["verify"].ToString());
                return user;
            }
            catch
            {
                return null;
            }
        }

        public void insert(User user)
        {
            try
            {
                String query = "INSERT INTO [User] "
                + "VALUES('" + user.email + "', '" + user.phone + "', N'" + user.name 
                + "', '" + user.password + "', 1, '', 'ROLE_USER', 1)";
                cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }
    }
}