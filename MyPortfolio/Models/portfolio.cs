//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPortfolio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class portfolio
    {
        public int id_portfolio { get; set; }
        public string portfo_name { get; set; }
        public string image_title { get; set; }
        public byte[] image_byte { get; set; }
        public string image_path { get; set; }
        public int id_category { get; set; }
    
        public virtual category category { get; set; }
    }
}
