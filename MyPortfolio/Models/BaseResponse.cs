using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Models
{
    public class BaseResponse
    {
        public String Message { get; set; }
        public String status { get; set; }
        public object data { get; set; }
    }
}