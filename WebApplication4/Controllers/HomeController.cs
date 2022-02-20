using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
           
            _logger = logger;
            this.context = context;
        }
        //int? id
        public IActionResult Index()
        {
            // var id = HttpContext.Session.GetInt32("id");
            ViewBag.countoftrip = context.Trips.DefaultIfEmpty().Count();
            ViewBag.countofclient = context.Clients.DefaultIfEmpty().Count();
            ViewBag.countofreversation = context.Reservations.DefaultIfEmpty().Count();
            //var contact = context.Clients.Include((a => a.Contacts)).ToList();
            //var contact3 = context.Contacts.Include(a => a.IdCusNavigation).Where(b => b.IdCus == id).ToList();
            // var Client = context.Clients.ToList();
            //var contact = context.Contacts.Include(a => a.IdCusNavigation).Where(x => x.IdCus == id).ToList();
            //var Client3 = context.Contacts.Include(z => z.IdCusNavigation).Where(x => x.IdCus == id).ToList();
            var Client = context.Clients.ToList();
            ViewBag.cou=context.Contacts.Include(a => a.IdCusNavigation).Count();

            return View(Client);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult contact()
        {
            //var contact = context.Clients.Include((a => a.Contacts)).ToList();
            //var contact3 = context.Contacts.ToList();
            //var Client = context.Clients.ToList();
            // var contact3 = context.Clients.Include(a => a.Contacts).ToList();
            //var tu = Tuple.Create<IEnumerable<WebApplication4.Models.Client>, IEnumerable<WebApplication4.Models.Contact>>(Client, contact);
            //return View(nameof(Index),tu);
            ViewBag.cou = context.Contacts.Include(a => a.IdCusNavigation).Count();

            var contact = context.Contacts.Include(a => a.IdCusNavigation).ToList();
            return View(contact);
        }
        public IActionResult Rating()
        {
            ViewBag.cou = context.Contacts.Include(a => a.IdCusNavigation).Count();

            var test = context.Testimonials.Include(x => x.IdCusNavigation).ToList();
            return View(test);
        }


        public IActionResult Updat(int id)
        {
            var update = context.Testimonials.Where(x => x.Id == id).SingleOrDefault();
            update.Accept = "1";
            context.Update(update);
            context.SaveChanges();
            return RedirectToAction("Rating", "Home");
        }
        public IActionResult Reject(int id)
        {
            var update = context.Testimonials.Where(x => x.Id == id).SingleOrDefault();
            update.Accept = "0";
            context.Update(update);
            context.SaveChanges();
            return RedirectToAction("Rating", "Home");
        }
        public IActionResult Delete(int id)
        {
            var update = context.Testimonials.Where(x => x.Id == id).SingleOrDefault();
            context.Remove(update);
            context.SaveChanges();
            return RedirectToAction("Rating", "Home");
        }       
        public IActionResult Report()
        {
            var dtTo = DateTime.Now.Month;
            var dto = DateTime.Now.Day;
            var dtT = DateTime.Now.Year;
     
            var month = context.Reservations.Include(x => x.Trip)
                .Where(x => x.Trip.Datefrom.Value.Month == dtTo).ToList();
            var sum = 0;
            var cost = 0;
            var salary = 0;
            List<Reservation> finalM = new List<Reservation>();
            foreach (var item in month.GroupBy(m=>m.Tripid).Select(g=>g.First()))
            {
                    var count = item.Trip.Reservations.Count;
                    item.Trip.Price = (int.Parse(item.Trip.Price) * count).ToString();
                    finalM.Add(item);
                    sum +=int.Parse(item.Trip.Price);
                    cost += int.Parse(item.Trip.Cost);                    
            }
            var sal = context.Accountensts.ToList();
            foreach (var sa in sal) 
            {
                salary +=int.Parse(sa.Salary);
            }
            ViewBag.cou = context.Contacts.Include(a => a.IdCusNavigation).Count();

            ViewBag.sum = sum;
            ViewBag.salary = sum -salary-cost;
            sum = 0; cost = 0; salary = 0;
            //************************************************
            ModelContext model = new ModelContext();
            var year = model.Reservations.Include(x => x.Trip)
               .Where(x => x.Trip.Datefrom.Value.Year == dtT).ToList();
            List<Reservation> finalY = new List<Reservation>();
            foreach (var item in year.GroupBy(m => m.Tripid).Select(g => g.First()))
            {
                var count = item.Trip.Reservations.Count;
                item.Trip.Price = (int.Parse(item.Trip.Price) * count).ToString();
                finalY.Add(item);
                sum += int.Parse(item.Trip.Price);
                cost += int.Parse(item.Trip.Cost);
            }
            foreach (var sa in sal)
            {
                salary += int.Parse(sa.Salary);
            }
            ViewBag.sumY = sum;
            ViewBag.salaryY = sum - (salary*12) - cost;
            var Tupple = Tuple.Create<IEnumerable<WebApplication4.Models.Reservation>, IEnumerable<WebApplication4.Models.Reservation>>(finalM, finalY);
            return View(Tupple);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "HomePage");
        }
    }
}