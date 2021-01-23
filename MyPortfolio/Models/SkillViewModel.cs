using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortfolio.Models
{
    public class SkillViewModel
    {
        public int id_skill { get; set; }
        [Required(ErrorMessage = "This skill name be cannot empty")]
        public string skill_name { get; set; }
        [Range(0, 100)]
        [Required(ErrorMessage = "This skill value be cannot empty")]
        public Nullable<int> skill_value { get; set; }
    }
}