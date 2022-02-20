using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Logins = new HashSet<Login>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
