using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using System.Data.Linq;

namespace SchoolMVC.Models

{   
    public class Login
    {
        
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password, ErrorMessage = "Expects 8 symbols [0-9],[a-z],special characters")]
        public string Password { get; set; }
    }
    public class LoginDetails:Login
    {
        //[Key]
        public int Id { get; set; }
        public int PersonID { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        [ScaffoldColumn(false)]
        public bool IsActive { get; set; }
        [ScaffoldColumn(false)]
        public string Role { get; set;}
        public string Message { get; set; }

    }
}