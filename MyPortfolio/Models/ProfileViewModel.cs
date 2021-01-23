using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Models
{
    public class ProfileViewModel
    {
        public int id_user { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image_title { get; set; }
        public byte[] image_byte { get; set; }
        public string image_path { get; set; }
        public int id_role { get; set; }
        public string role_name { get; set; }
        public string name { get; set; }

        public HttpPostedFile image_upload { get; set; }

        public virtual role role { get; set; }
    }
}