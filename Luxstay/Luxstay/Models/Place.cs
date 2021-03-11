using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luxstay.Models
{
    public class Place
    {
        public string place_id { get; set; }

        public string place_name { get; set; }

        public string image { get; set; }

        public int total_home { get; set; }
    }
}