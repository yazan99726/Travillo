using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Login
    {
        public decimal Id { get; set; }
     
        public string Username { get; set; }
     
        public string Password { get; set; }
        public decimal? Rolid { get; set; }
        public decimal? Clientid { get; set; }
        public decimal? Accountid { get; set; }
        public decimal? Adminid { get; set; }

        public virtual Accountenst Account { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Client Client { get; set; }
        public virtual Role Rol { get; set; }
    }
}
