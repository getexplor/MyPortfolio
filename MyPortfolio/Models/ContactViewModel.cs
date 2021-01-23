using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Models
{
    public class ContactViewModel
    {
        public int id_contact { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string instagram { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string linkedin { get; set; }
        public string github { get; set; }
    }
}