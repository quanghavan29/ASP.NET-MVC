using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Luxstay.Dao
{
    public class HomeDao
    {
        DataProvide dataProvide = new DataProvide();
        SqlConnection cnn; //Ket noi DB
        SqlDataAdapter da; //Xu ly cac cau lenh sql: select
        SqlCommand cmd; //Thuc thi cau lenh insert update

        // Get all home in database
        public List<Home> findAll(int pageIndex, int pageSize)
        {
            connect(); // connect with DB
            List<Home> homes = new List<Home>();
            int first = (pageIndex * pageSize) - (pageSize - 1);
            int max = pageIndex * pageSize;
            String query = "SELECT * FROM "
                            + "(SELECT ROW_NUMBER() OVER (ORDER BY home_id ASC) "
                            + "AS rownum, * from Home h) tbl "
                            + "JOIN Place p ON tbl.place_id = p.place_id "
                            + "WHERE rownum BETWEEN " + first + " and " + max;
            da = new SqlDataAdapter(query, cnn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Home home = new Home();
                home.home_id = Int32.Parse(dataTable.Rows[i]["home_id"].ToString());
                home.home_name = dataTable.Rows[i]["home_name"].ToString();
                home.home_type = dataTable.Rows[i]["home_type"].ToString();
                home.room_number = Int32.Parse(dataTable.Rows[i]["room_number"].ToString());
                home.price = Int32.Parse(dataTable.Rows[i]["price"].ToString());
                home.image_intro = dataTable.Rows[i]["image_intro"].ToString();

                Place place = new Place();
                place.place_id = dataTable.Rows[i]["place_id"].ToString();
                place.place_name = dataTable.Rows[i]["place_name"].ToString();
                place.image = dataTable.Rows[i]["image"].ToString();
                place.total_home = Int32.Parse(dataTable.Rows[i]["total_home"].ToString());

                home.place = place;
                homes.Add(home);
            }
            return homes;
        }

        // Count all of home in database
        public int count()
        {
            connect(); // connect with DB
            String query = "SELECT COUNT(*) AS [total_home] FROM Home";
            da = new SqlDataAdapter(query, cnn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            return Int32.Parse(dataTable.Rows[0]["total_home"].ToString());
        }

        // Get all home by place id in database
        public List<Home> findAllByPlaceId(string place_id)
        {
            connect(); // connect with DB
            List<Home> homes = new List<Home>();
            String query = "Select * from Home h "
                        + "join Place p "
                        + "on h.place_id = p.place_id "
                        + "where p.place_id = '"+ place_id +"'";
            da = new SqlDataAdapter(query, cnn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Home home = new Home();
                home.home_id = Int32.Parse(dataTable.Rows[i]["home_id"].ToString());
                home.home_name = dataTable.Rows[i]["home_name"].ToString();
                home.home_type = dataTable.Rows[i]["home_type"].ToString();
                home.room_number = Int32.Parse(dataTable.Rows[i]["room_number"].ToString());
                home.price = Int32.Parse(dataTable.Rows[i]["price"].ToString());
                home.image_intro = dataTable.Rows[i]["image_intro"].ToString();

                Place place = new Place();
                place.place_id = dataTable.Rows[i]["place_id"].ToString();
                place.place_name = dataTable.Rows[i]["place_name"].ToString();
                place.image = dataTable.Rows[i]["image"].ToString();
                place.total_home = Int32.Parse(dataTable.Rows[i]["total_home"].ToString());

                home.place = place;
                homes.Add(home);
            }
            return homes;
        }

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