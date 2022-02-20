using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomePageController : Controller
    {
        public readonly ModelContext context;
        public HomePageController(ModelContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var trip = context.Trips.ToList();
            var id = HttpContext.Session.GetInt32("id");
            ViewBag.x = id;
            var about = context.Aboutus.ToList();

            var cat = context.Clients.Where(x => x.Id == id).SingleOrDefault();
            var test = context.Testimonials.Where(x => x.Accept == "1").Include(x=>x.IdCusNavigation).ToList();    
            var model3 = Tuple.Create<IEnumerable<WebApplication4.Models.Trip>, IEnumerable<WebApplication4.Models.Trip>, WebApplication4.Models.Client, IEnumerable<WebApplication4.Models.Testimonial>, IEnumerable<WebApplication4.Models.Aboutu>>(null, trip, cat,test,about);
            //var model3 = Tuple.Create<IEnumerable<WebApplication4.Models.Trip>, IEnumerable<WebApplication4.Models.Trip>>(null,trip);

            return View(model3);
        }   
        public IActionResult SearchByProductName()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string tripname, DateTime fdate, DateTime tdata, string Itreation)
        {
            //string c =Convert.ToString(c1);
            // var x = _context.Products.Where(x => x.Proname == proname).FirstOrDefault();
                var x = context.Trips.Where(x => x.Tripname.Contains(tripname)
                                          || x.Datefrom >=fdate
                                          && x.Dateto <=tdata
                                          || x.Itinerary.Contains(Itreation)).ToList();
            var trip = context.Trips.ToList();

            var id = HttpContext.Session.GetInt32("id");
            ViewBag.x = id;

            var cat = context.Clients.Where(x => x.Id == id).SingleOrDefault();

            var test = context.Testimonials.Where(x => x.Accept == "1").Include(x => x.IdCusNavigation).ToList();
            var about = context.Aboutus.ToList();

            var model3 = Tuple.Create<IEnumerable<WebApplication4.Models.Trip>, IEnumerable<WebApplication4.Models.Trip>, WebApplication4.Models.Client, IEnumerable<WebApplication4.Models.Testimonial>, IEnumerable<WebApplication4.Models.Aboutu>>(x, trip, cat,test, about);
            return View(model3);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","HomePage");
        }
       
    }
}