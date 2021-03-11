using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Luxstay.Dao
{
    public class PlaceDao
    {
        SqlConnection cnn; //Ket noi DB
        SqlDataAdapter da; //Xu ly cac cau lenh sql: select
        SqlCommand cmd; //Thuc thi cau lenh insert update

        // Get all place in database
        public List<Place> findAll()
        {
            connect();
            List<Place> places = new List<Place>();
            String query = "Select * from Place";
            da = new SqlDataAdapter(query, cnn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Place place = new Place();
                place.place_id = dataTable.Rows[i]["place_id"].ToString();
                place.place_name = dataTable.Rows[i]["place_name"].ToString();
                place.image = dataTable.Rows[i]["image"].ToString();
                place.total_home = Int32.Parse(dataTable.Rows[i]["total_home"].ToString());
                places.Add(place);
            }
            return places;
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