using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Models
{
    public class ConfirmationForm
    {   //Index =0 -success; 1-warning; 2-confirmation to delete/update
        public int Index { get; set; }
        public string Message { get; set; }        
        public string ActionName { get; set; }
        public ConfirmationForm(int Index, string Message,string ActionName)
        {
            this.Index = Index;
            this.Message = Message;
            this.ActionName = ActionName;
        }
    }
}