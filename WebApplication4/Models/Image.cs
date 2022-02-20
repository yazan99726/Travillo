using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Image
    {
        public decimal Id { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public decimal? TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
