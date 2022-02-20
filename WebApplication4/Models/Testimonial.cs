using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Description { get; set; }
        public string Accept { get; set; }
        public decimal? IdCus { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]

        public IFormFile ImageFile { get; set; }

        public string Rate { get; set; }


        public virtual Client IdCusNavigation { get; set; }
    }
}
