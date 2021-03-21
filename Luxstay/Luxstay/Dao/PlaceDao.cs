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
        DataProvider dataProvider = new DataProvider();

        // Get all place in database
        public List<Place> findAll()
        {
            List<Place> places = new List<Place>();
            String query = "Select * from Place";
            DataTable dataTable = dataProvider.excuteQuery(query);
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

        public int totalHomestayByPlace(string place_id)
        {
            int total_homestay = 0;
            String query = "Select p.total_home from Place p "
                            + "where place_id = '" + place_id + "'";
            DataTable dataTable = dataProvider.excuteQuery(query);
            total_homestay = Int32.Parse(dataTable.Rows[0]["total_home"].ToString());
            return total_homestay;
        }

        public void updateTotalHomestay(int totalHomestay, string place_id)
        {
            try
            {
                string query = "Update Place set total_home = " + totalHomestay
                            + " where place_id = '" + place_id + "'";
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }
    }
}