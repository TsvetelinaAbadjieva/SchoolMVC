using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Models
{
    /*[MetadataType(typeof(TeacherMetaData))]

    public partial class Teacher : Person
    {

    }*/
    public class Teacher : Person
    {

        [ScaffoldColumn(true)]
        [Display(Name = "Номер")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Учебен предмет")]
        public String Subject { get; set; }
        
        [Display(Name = "Втори учебен предмет")]
        public string Subject2 { get; set; }

       
    }
}