using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ReversationController : Controller
    {
        private readonly ModelContext context;

        public ReversationController(ModelContext context)
        {
            this.context = context;
        }
        public int Identi;
        public IActionResult Index(int id)
        {
            //Identi = id;
            var c = context.Trips.Where(x=>x.Id==id).SingleOrDefault();
            var image = context.Images.Where(x=>x.TripId==id).ToList();
            var model2 = Tuple.Create<WebApplication4.Models.Trip, IEnumerable<WebApplication4.Models.Image>>(c, image);

            //var sees = HttpContext.Session.GetInt32("id");         
            //var found = context.Reservations.Where(x => x.Tripid == id && x.Customerid == sees).SingleOrDefault();
            //ViewBag.x = found;

            return View(model2);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Trip trip,string ctype, string cnumber, int ccvv, DateTime cexpdate)
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                var sees = HttpContext.Session.GetInt32("id");         
                var found = context.Reservations.Where(x => x.Tripid == trip.Id && x.Customerid == sees).SingleOrDefault();


                if (found == null)
                {
                    Reservation c = new Reservation()
                    {

                        Customerid = HttpContext.Session.GetInt32("id"),
                        Tripid = trip.Id,
                    };
                    context.Add(c);
                    await context.SaveChangesAsync();
                    Paymant p = new Paymant()
                    {
                        Cardtype = ctype,
                        Cardnumber = cnumber,
                        Cvv = ccvv,
                        Expdate = cexpdate,
                        IdCus = HttpContext.Session.GetInt32("id"),
                    };
                    context.Add(p);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index", "HomePage");
                }
                else 
                {
                    ViewBag.mssg = "This Trip alredy exist";
                    var c = context.Trips.Where(x => x.Id == trip.Id).SingleOrDefault();
                    var image = context.Images.Where(x => x.TripId == trip.Id).ToList();
                    var model2 = Tuple.Create<WebApplication4.Models.Trip, IEnumerable<WebApplication4.Models.Image>>(c, image);
                    return View(model2);
                }
            }          
           return RedirectToAction("Login", "Login");           
        }



        public IActionResult MyTrip()
        {
            var id = HttpContext.Session.GetInt32("id");
            ViewBag.x = id;
            var cat = context.Clients.Where(x => x.Id == id).SingleOrDefault();
            var trip = context.Reservations.Include(a => a.Trip).Where(b => b.Customerid== id).ToList();
            var model3 = Tuple.Create<IEnumerable<WebApplication4.Models.Reservation>, WebApplication4.Models.Client>(trip, cat);
            return View(model3);
        }
        public IActionResult RemoveTrip(int id)
        {
            var sess = HttpContext.Session.GetInt32("id");
            var c = context.Reservations.Where(x => x.Tripid == id && x.Customerid==sess).SingleOrDefault();            
            context.Reservations.Remove(c);
            context.SaveChanges();
            return RedirectToAction("MyTrip", "Reversation");
        }
    }
}