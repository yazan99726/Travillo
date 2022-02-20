using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication4.Models
{
    public partial class Client
    {
        public Client()
        {
            Contacts = new HashSet<Contact>();
            Logins = new HashSet<Login>();
            Paymants = new HashSet<Paymant>();
            Reservations = new HashSet<Reservation>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
       
        public string Name { get; set; }
     
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Adress { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Paymant> Paymants { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
