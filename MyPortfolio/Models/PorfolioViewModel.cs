using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Models
{
    public class PorfolioViewModel
    {
        public int id_portfolio { get; set; }
        public string portfo_name { get; set; }
        public string image_title { get; set; }
        public byte[] image_byte { get; set; }
        public string image_path { get; set; }
        public int id_category { get; set; }

        public virtual category category { get; set; }

        public HttpPostedFile image_upload { get; set; }
        public string category_name { get; set; }
    }
}