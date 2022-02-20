using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Images = new HashSet<Image>();
            Reservations = new HashSet<Reservation>();
        }

        public decimal Id { get; set; }
        
        public string Tripname { get; set; }
    
        public DateTime? Datefrom { get; set; }
        
        public DateTime? Dateto { get; set; }       
        public string Imagepath { get; set; }
        [NotMapped]
       
        public IFormFile ImageFile { get; set; }
        
        public string Price { get; set; }
        public string Cost { get; set; }

        public string Itinerary { get; set; }
      
        public string Descriptoin { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
