using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace WebApplication4.Controllers
{
    public class TripsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TripsController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            webHostEnvironment = _webHostEnvironment;

        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            return View(await _context.Trips.ToListAsync());
        }
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Index(DateTime datefrom, DateTime dateto)
        {
            var xx = await _context.Trips.Where(x => x.Datefrom >= datefrom && x.Dateto <= dateto).ToListAsync();
            return View(xx);
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tripname,Datefrom,Dateto,Imagepath,Price,Cost,Itinerary,Descriptoin,ImageFile")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                if (trip.ImageFile != null)
                {
                    string wwwroot = webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + trip.ImageFile.FileName;//get image name only
                    string Pathof = Path.Combine(wwwroot + "/Image/" + fileName);
                    using (var filestream = new FileStream(Pathof, FileMode.Create))
                    {
                        await trip.ImageFile.CopyToAsync(filestream);
                    }
                    trip.Imagepath = fileName;
                    _context.Add(trip);
                    await _context.SaveChangesAsync();
                }
                else /*ele if category.Imagepath!=null*/
                {

                    return null;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Tripname,Datefrom,Dateto,Imagepath,Price,Cost,Itinerary,ImageFile,Descriptoin")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (trip.ImageFile != null)
                    {
                        string wwwroot = webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + trip.ImageFile.FileName;//get image name only
                        string Pathof = Path.Combine(wwwroot + "/Image/" + fileName);
                        string OldPath = Path.Combine(wwwroot + "/Image/" + trip.Imagepath);

                        using (var filestream = new FileStream(Pathof, FileMode.Create))
                        {
                            System.IO.File.Delete(OldPath);
                            await trip.ImageFile.CopyToAsync(filestream);
                        }
                        trip.Imagepath = fileName;
                    }
                        _context.Update(trip);
                        await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }
        //string Uploadfile(IFormFile file, string img)
        //{
        //    if (file != null)
        //    {
        //        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //        string uploads = webHostEnvironment.WebRootPath;
        //        string newpath = Path.Combine(uploads + "/Image/" + fileName);
        //        string OldPath = Path.Combine(uploads+ "/Image/"+ img);
        //        if (newpath != OldPath)
        //        { 
        //            System.IO.File.Delete(OldPath);
        //            file.CopyTo(new FileStream(newpath, FileMode.Create));         
        //            //file.CopyTo(new FileStream(newpath, FileMode.Create));
        //        }
        //        return file.FileName;
        //    }
        //    return img;
        //}

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var trip = await _context.Trips.FindAsync(id);
            var rever = await _context.Reservations.Where(r => r.Tripid == id).ToListAsync();
            if (rever == null)
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var reveer = await _context.Reservations.Where(r => r.Tripid == id).Include(c=>c.Customer).ToListAsync();
                foreach (var item in reveer)
                {
                    SendEmail(item);
                }
                _context.Reservations.RemoveRange(rever);
                await _context.SaveChangesAsync();

                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
        public void SendEmail(Reservation client)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("odayalsanad6374@gmail.com");
                mail.To.Add(client.Customer.Email);
                mail.Subject = "Hello "+client.Customer.Name;
                mail.Body = $"<h1>hallo deare the {client.Trip.Tripname} is remove please check account</h1>";
                mail.IsBodyHtml = true;

                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("odayalsanad6374@gmail.com", "*****");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            //MailMessage message = new MailMessage();
            //message.Subject = "This is email subject";
            //message.Body ="this is maa";
            //message.IsBodyHtml = false;
            //message.From = new MailAddress("odayalsanad8787@gmail.com", "odayalsanad8787@gmail.com");
            //message.To.Add(new MailAddress("yazantayem26@gmail.com", "yazantayem26@gmail.com"));

            //System.Net.Mail.SmtpClient smp = new System.Net.Mail.SmtpClient();

            //smp.Host = "smtp.gmail.com";
            //smp.EnableSsl = true;

            //NetworkCredential fff = new NetworkCredential("odayalsanad8787@gmail.com", "oday87199588");
            //smp.UseDefaultCredentials = true;
            //smp.Credentials = fff;
            //smp.Port = 587;
            //smp.Send(message);
            

        }
        private bool TripExists(decimal id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
