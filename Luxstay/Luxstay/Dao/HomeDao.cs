﻿using Luxstay.Models;
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
        DataProvider dataProvider = new DataProvider();

        // Get all home in database
        public List<Home> findAll(int pageIndex, int pageSize)
        {
            List<Home> homes = new List<Home>();
            int first = (pageIndex * pageSize) - (pageSize - 1);
            int max = pageIndex * pageSize;
            String query = "SELECT * FROM "
                            + "(SELECT ROW_NUMBER() OVER (ORDER BY home_id ASC) "
                            + "AS rownum, * from Home h where h.[restore] = 1) tbl "
                            + "JOIN Place p ON tbl.place_id = p.place_id "
                            + "WHERE rownum BETWEEN " + first + " and " + max;
            DataTable dataTable = dataProvider.excuteQuery(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Home home = new Home();
                home.home_id = Int32.Parse(dataTable.Rows[i]["home_id"].ToString());
                home.home_name = dataTable.Rows[i]["home_name"].ToString();
                home.home_type = dataTable.Rows[i]["home_type"].ToString();
                home.room_number = Int32.Parse(dataTable.Rows[i]["room_number"].ToString());
                home.price = Int32.Parse(dataTable.Rows[i]["price"].ToString());
                home.image_intro = dataTable.Rows[i]["image_intro"].ToString();
                home.address = dataTable.Rows[i]["address"].ToString();
                home.short_description = dataTable.Rows[i]["short_description"].ToString();
                home.detail_description = dataTable.Rows[i]["detail_description"].ToString();

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
            String query = "SELECT COUNT(*) AS [total_home] FROM Home where [restore] = 1";
            DataTable dataTable = dataProvider.excuteQuery(query);
            return Int32.Parse(dataTable.Rows[0]["total_home"].ToString());
        }

        // Get all home by place id in database and pagging
        public List<Home> findAllByPlaceId(string place_id, int pageIndex, int pageSize)
        {
            List<Home> homes = new List<Home>();
            int first = (pageIndex * pageSize) - (pageSize - 1);
            int max = pageIndex * pageSize;
            String query = "SELECT * FROM "
                          + "(SELECT ROW_NUMBER() OVER (ORDER BY home_id ASC) "
                          + "AS rownum, * from Home h WHERE h.place_id = '" + place_id + "' and h.[restore] = 1) tbl "
                          + "JOIN Place p ON tbl.place_id = p.place_id "
                          + "WHERE rownum BETWEEN " + first + " and " + max;
            DataTable dataTable = dataProvider.excuteQuery(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Home home = new Home();
                home.home_id = Int32.Parse(dataTable.Rows[i]["home_id"].ToString());
                home.home_name = dataTable.Rows[i]["home_name"].ToString();
                home.home_type = dataTable.Rows[i]["home_type"].ToString();
                home.room_number = Int32.Parse(dataTable.Rows[i]["room_number"].ToString());
                home.price = Int32.Parse(dataTable.Rows[i]["price"].ToString());
                home.image_intro = dataTable.Rows[i]["image_intro"].ToString();
                home.address = dataTable.Rows[i]["address"].ToString();
                home.short_description = dataTable.Rows[i]["short_description"].ToString();
                home.detail_description = dataTable.Rows[i]["detail_description"].ToString();

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

        // Display information of home by id
        public Home findById(int home_id)
        {
            // query select home from table Home in data base by home_id
            String query = "Select * from Home h "
                        + "join Place p "
                        + "on h.place_id = p.place_id "
                        + "where h.home_id = " + home_id;
            DataTable dataTable = dataProvider.excuteQuery(query);
            Home home = new Home();
            home.home_id = Int32.Parse(dataTable.Rows[0]["home_id"].ToString());
            home.home_name = dataTable.Rows[0]["home_name"].ToString();
            home.home_type = dataTable.Rows[0]["home_type"].ToString();
            home.room_number = Int32.Parse(dataTable.Rows[0]["room_number"].ToString());
            home.price = Int32.Parse(dataTable.Rows[0]["price"].ToString());
            home.image_intro = dataTable.Rows[0]["image_intro"].ToString();
            home.address = dataTable.Rows[0]["address"].ToString();
            home.short_description = dataTable.Rows[0]["short_description"].ToString();
            home.detail_description = dataTable.Rows[0]["detail_description"].ToString();


            Place place = new Place();
            place.place_id = dataTable.Rows[0]["place_id"].ToString();
            place.place_name = dataTable.Rows[0]["place_name"].ToString();
            place.image = dataTable.Rows[0]["image"].ToString();
            place.total_home = Int32.Parse(dataTable.Rows[0]["total_home"].ToString());

            home.place = place;
            return home;
        }

        // Count all of home in database by place_id
        public int countByPlace(string place_id)
        {
            String query = "SELECT COUNT(*) AS [total_home] FROM Home "
                        + "WHERE place_id = '" + place_id + "' and [restore] = 1";
            DataTable dataTable = dataProvider.excuteQuery(query);
            return Int32.Parse(dataTable.Rows[0]["total_home"].ToString());
        }

        // Insert Homestay to database

        public void insert(Home home)
        {
            try
            {
                string query = "Insert into Home "
                                + "Values(N'" + home.home_name + "', N'" + home.home_type + "', " + home.room_number + ", " + home.price + ", '" + home.place.place_id + "', " +
                                "'" + home.image_intro + "', N'" + home.address + "', N'" + home.short_description + "', N'" + home.detail_description + "', 1)";
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public void clear(int home_id)
        {
            try
            {
                string query = "Delete from Home Where home_id = " + home_id;
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public void delete(int home_id)
        {
            try
            {
                string query = "update Home "
                    + "set[restore] = 0 Where home_id = " + home_id;
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public void restore(int home_id)
        {
            try
            {
                string query = "update Home "
                    + "set[restore] = 1 Where home_id = " + home_id;
                dataProvider.ExcuteNonQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi insert user: " + ex.Message);
            }
        }

        public List<Home> findAllDeleted()
        {
            List<Home> homes = new List<Home>();
            String query = "SELECT * FROM Home h join Place p on h.place_id = p.place_id "
                         + "WHERE h.[restore] = 0";
            DataTable dataTable = dataProvider.excuteQuery(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Home home = new Home();
                home.home_id = Int32.Parse(dataTable.Rows[i]["home_id"].ToString());
                home.home_name = dataTable.Rows[i]["home_name"].ToString();
                home.home_type = dataTable.Rows[i]["home_type"].ToString();
                home.room_number = Int32.Parse(dataTable.Rows[i]["room_number"].ToString());
                home.price = Int32.Parse(dataTable.Rows[i]["price"].ToString());
                home.image_intro = dataTable.Rows[i]["image_intro"].ToString();
                home.address = dataTable.Rows[i]["address"].ToString();
                home.short_description = dataTable.Rows[i]["short_description"].ToString();
                home.detail_description = dataTable.Rows[i]["detail_description"].ToString();

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

        public int findHomeIdInsert()
        {
            int home_id = 0;
            // query select home from table Home in data base by home_id
            String query = "select top(1) home_id from Home "
                        + "Order By home_id DESC";
            DataTable dataTable = dataProvider.excuteQuery(query);
            home_id = Int32.Parse(dataTable.Rows[0]["home_id"].ToString());
            return home_id;
        }

    }
}