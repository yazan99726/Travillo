using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Paymant
    {
        public decimal CardId { get; set; }
        public string Cardtype { get; set; }
        public string Cardnumber { get; set; }
        public decimal Cvv { get; set; }
        public DateTime Expdate { get; set; }     
        public decimal? IdCus { get; set; }

        public virtual Client IdCusNavigation { get; set; }
    }
}
