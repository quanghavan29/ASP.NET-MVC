using Luxstay.Models;
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
        DataProvide dataProvide = new DataProvide();
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

        // get all images from table images_detail by home_id
        public List<ImagesDetail> findAllByHomeId(string home_id)
        {
            connect();
            List<ImagesDetail> imagesDetails = new List<ImagesDetail>();
            String query = "SELECT * FROM Images_Detail "
                        + "WHERE home_id = '" + home_id + "'";
            da = new SqlDataAdapter(query, cnn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
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