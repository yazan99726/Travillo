using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Contact
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Expdate { get; set; }
        public decimal? IdCus { get; set; }

        public virtual Client IdCusNavigation { get; set; }
    }
}
