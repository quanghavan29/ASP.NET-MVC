﻿using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Luxstay.Dao
{
    public class ImagesDetailDao
    {
        DataProvider dataProvider = new DataProvider();
       
        // get all images from table images_detail by home_id
        public List<ImagesDetail> findAllByHomeId(int home_id)
        {
            List<ImagesDetail> imagesDetails = new List<ImagesDetail>();
            String query = "SELECT * FROM Images_Detail "
                        + "WHERE home_id = " + home_id;
            DataTable dataTable = dataProvider.excuteQuery(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                ImagesDetail imagesDetail = new ImagesDetail();
                imagesDetail.image_id = Int32.Parse(dataTable.Rows[i]["image_id"].ToString());
                imagesDetail.home_id = dataTable.Rows[i]["home_id"].ToString();
                imagesDetail.image_url = dataTable.Rows[i]["image_url"].ToString();
                imagesDetails.Add(imagesDetail);
            }
            return imagesDetails;
        }
    }
}