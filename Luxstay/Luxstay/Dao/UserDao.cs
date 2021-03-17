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
        DataProvider dataProvider = new DataProvider();
        public User findByEmailAndPassword(string email, string password)
        {
            String query = "SELECT * FROM [User] "
                        + "WHERE email = '" + email + "' and password = '" + password + "'";
            try
            {
                DataTable dataTable = dataProvider.excuteQuery(query);
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
            String query = "SELECT * FROM [User] "
                        + "WHERE email = '" + email + "'";
            try
            {
                DataTable dataTable = dataProvider.excuteQuery(query);
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
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }
    }
}