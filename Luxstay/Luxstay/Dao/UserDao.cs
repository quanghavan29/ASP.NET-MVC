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

        public List<User> findAll()
        {
            String query = "SELECT * FROM [User]";
            List<User> users = new List<User>();
            try
            {
                DataTable dataTable = dataProvider.excuteQuery(query);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    User user = new User();
                    user.user_id = Int32.Parse(dataTable.Rows[i]["user_id"].ToString());
                    user.email = dataTable.Rows[i]["email"].ToString();
                    user.password = dataTable.Rows[i]["password"].ToString();
                    user.name = dataTable.Rows[i]["name"].ToString();
                    user.role = dataTable.Rows[i]["role"].ToString();
                    user.phone = dataTable.Rows[i]["phone"].ToString();
                    user.address = dataTable.Rows[i]["address"].ToString();
                    user.gender = bool.Parse(dataTable.Rows[i]["gender"].ToString());
                    user.verify = bool.Parse(dataTable.Rows[i]["verify"].ToString());
                    users.Add(user);
                }
                return users;
            }
            catch
            {
                return null;
            }
        }

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
                + "', '" + user.password + "', 1, N'" + user.address + "', 'ROLE_USER', 1)";
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public User findById(int user_id)
        {
            String query = "SELECT * FROM [User] "
                        + "WHERE user_id = " + user_id;
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
        public void update(User user)
        {
            try
            {
                String query = "update [User] "
                    + "set phone = '" + user.phone + "', [name] = N'" + user.name + "', "
                    + "[address] = N'" + user.address + "', [role] = '" + user.role + "', "
                    + "[password] = '" + user.password + "' where[user_id] = " + user.user_id;
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public void delete(int user_id)
        {
            try
            {
                string query = "Delete from [User] Where user_id = " + user_id;
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public void updatePasswordByEmail(string email, string password)
        {
            try
            {
                string query = "Update [User] set [password] = '" + password + "' "
                                + "where [email] = '" + email + "'";
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi update password: " + ex.Message);
            }
        }
    }
}