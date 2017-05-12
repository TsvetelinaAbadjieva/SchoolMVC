using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Models
{
    

    public class Person
    {
        [Display(Name = "No")]
        [ScaffoldColumn(true)]
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Име")]
        public String FirstName { get; set; }
        [Required]
        [Display(Name = "Презиме")]
        public String MiddleName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public String LastName { get; set; }
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Позиция")]
        public string Position { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата на раждане")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Имейл")]
        public String UserName { get; set; }
        [Display(Name = "No")]
        [ScaffoldColumn(true)]
        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }
    }
}