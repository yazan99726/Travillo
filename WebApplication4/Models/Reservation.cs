using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Reservation
    {
        public decimal Id { get; set; }
        public decimal? Tripid { get; set; }
        public decimal? Customerid { get; set; }

        public virtual Client Customer { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
