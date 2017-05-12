using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Models
{
   

    public class Student : Person
    {
       
        public int StudentID { get; set; }
        [Required]
        [Display(Name = "Номер в класа")]
        public int Number { get; set; }
        [Required]
        [Display(Name = "Степен")]
        public int Degree { get; set; }
        [Required]
        [Display(Name = "Паралелка")]
        public string Letter { get; set; }
        [Required]
        [Display(Name = "Име на майката")]
        [DisplayFormat(NullDisplayText = "Не е посочено")]
        public String MotherName { get; set; }
        [Required]
        [Display(Name = "Адрес на майката")]
        [DisplayFormat(NullDisplayText ="Не е посочено")]
        public String MotherAddress { get; set; }
        [Required]
        [Display(Name = "Име на бащата")]
        [DisplayFormat(NullDisplayText = "Не е посочено")]
        public String FatherName { get; set; }
        [Required]
        [Display(Name = "Адрес на бащата")]
        [DisplayFormat(NullDisplayText = "Не е посочено")]
        public String FatherAddress { get; set; }

        
    }

    

}