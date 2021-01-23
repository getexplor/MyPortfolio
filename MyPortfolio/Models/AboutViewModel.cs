using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortfolio.Models
{
    public class AboutViewModel
    {
        public int id_about { get; set; }
        public string about_me { get; set; }
        public string image_title { get; set; }
        public byte[] image_byte { get; set; }
        public string image_path { get; set; }
        public string current_position { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> birthday { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public Nullable<int> age { get; set; }
        public string degree { get; set; }
    }
}