using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Aboutu
    {
        public decimal Id { get; set; }
        public string Description { get; set; }
        [NotMapped]

        public IFormFile ImageFile { get; set; }
        public string Imagepath { get; set; }
    }
}
