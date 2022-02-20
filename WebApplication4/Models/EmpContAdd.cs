using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication4.Models
{
    public partial class EmpContAdd
    {
        public decimal EmpId { get; set; }
        public string EmpName { get; set; }
        public decimal? ContId { get; set; }
        public decimal? AddresId { get; set; }
        public string Phone { get; set; }
        public string AddName { get; set; }
    }
}
