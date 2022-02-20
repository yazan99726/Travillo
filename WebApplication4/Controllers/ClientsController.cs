using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ModelContext _context;

        public ClientsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Adress")] Client client, string username, string password)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                Login login = new Login();
                login.Username = username;
                login.Password = password;
                login.Rolid = 2;
                var lastid = _context.Clients.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                login.Clientid = lastid;
                _context.Logins.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "HomePage");
            }
            return View(client);
        }

        public ActionResult EditProfile()
        {
            var id = HttpContext.Session.GetInt32("id");
            ViewBag.x = id;
            var client1 = _context.Clients.Where(x=>x.Id==id).SingleOrDefault();
            var login1= _context.Logins.Where(x => x.Clientid == client1.Id).SingleOrDefault();
            Client client = new Client();
            client.Name = client1.Name;
            client.Phone=client1.Phone;
            client.Adress=client1.Adress;
            Login login = new Login();
            login.Username=login1.Username;
            var model3 = Tuple.Create<WebApplication4.Models.Client, WebApplication4.Models.Login>(client, login);
            return View(model3);
        }
        [HttpPost]
        public ActionResult EditProfile(string Name, string Phone, string Adress, string Username,string cpass, string npass)
        {
            var id = HttpContext.Session.GetInt32("id");
            var Newdata = _context.Clients.Where(x => x.Id == id).SingleOrDefault();
            var login1 = _context.Logins.Where(x => x.Clientid == Newdata.Id).SingleOrDefault();
            if (cpass != login1.Password)
            {
                ViewBag.mssg = "The password is incorrect";
            }
            else { 
            Newdata.Name = Name;
            Newdata.Phone= Phone;
            Newdata.Adress=Adress;
            _context.Update(Newdata);
            //_context.Entry(Newdata).State=EntityState.Modified;
            _context.SaveChanges();
            login1.Username = Username;
            login1.Password = npass;
            _context.Update(login1);
           // _context.Entry(login1).State=EntityState.Modified;
            _context.SaveChanges();
            ViewBag.upd = "Update completed successfully";
            }
            var model3 = Tuple.Create<WebApplication4.Models.Client, WebApplication4.Models.Login>(Newdata,login1);
            return View(model3);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Phone,Adress")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(decimal id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
        //Contact Client 
        public ActionResult Contact()
        {
            var id = HttpContext.Session.GetInt32("id");
            ViewBag.x = id;
            var client1 = _context.Clients.Where(x => x.Id == id).SingleOrDefault();
            Client client = new Client();
            client.Name = client1.Name;

            return View(client);
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var id = HttpContext.Session.GetInt32("id");
                Contact contact1 = new Contact();
                contact.Name = contact.Name;
                contact.Email = contact.Email;
                contact.Message = contact.Message;
                contact.IdCus = id;
                _context.Contacts.Add(contact);
                _context.SaveChangesAsync();
                return RedirectToAction("Index", "HomePage");
                //return View(contact);
            }
            return RedirectToAction("Login", "Login");

        }

    }
}
