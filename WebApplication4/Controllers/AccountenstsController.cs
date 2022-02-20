using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AccountenstsController : Controller
    {
        private readonly ModelContext _context;

        public AccountenstsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Accountensts
        public async Task<IActionResult> Index()
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            return View(await _context.Accountensts.ToListAsync());
        }

        // GET: Accountensts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var accountenst = await _context.Accountensts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountenst == null)
            {
                return NotFound();
            }

            return View(accountenst);
        }

        // GET: Accountensts/Create
        public IActionResult Create()
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            return View();
        }

        // POST: Accountensts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Adress,Salary")] Accountenst accountenst, string username, string password)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountenst);
                await _context.SaveChangesAsync();
                Login login = new Login();
                login.Username = username;
                login.Password = password;
                login.Rolid = 3;
                var lastid = _context.Accountensts.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                login.Accountid = lastid;
                _context.Logins.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountenst);
        }

        // GET: Accountensts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var accountenst = await _context.Accountensts.FindAsync(id);
            if (accountenst == null)
            {
                return NotFound();
            }
            return View(accountenst);
        }

        // POST: Accountensts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Phone,Adress,Salary")] Accountenst accountenst)
        {
            if (id != accountenst.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountenst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountenstExists(accountenst.Id))
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
            return View(accountenst);
        }

        // GET: Accountensts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var accountenst = await _context.Accountensts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountenst == null)
            {
                return NotFound();
            }

            return View(accountenst);
        }

        // POST: Accountensts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var log = _context.Logins.Where(x => x.Accountid == id);
            _context.Logins.RemoveRange(log);
            await _context.SaveChangesAsync();

            var accountenst = await _context.Accountensts.FindAsync(id);
            _context.Accountensts.Remove(accountenst);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AccountenstExists(decimal id)
        {
            return _context.Accountensts.Any(e => e.Id == id);
        }
        public IActionResult Report()
        {
            var dtTo = DateTime.Now.Month;
            var dto = DateTime.Now.Day;
            var dtT = DateTime.Now.Year;

            var month = _context.Reservations.Include(x => x.Trip)
                .Where(x => x.Trip.Datefrom.Value.Month == dtTo).ToList();
            var sum = 0;
            var cost = 0;
            var salary = 0;
            List<Reservation> finalM = new List<Reservation>();
            foreach (var item in month.GroupBy(m => m.Tripid).Select(g => g.First()))
            {
                var count = item.Trip.Reservations.Count;
                item.Trip.Price = (int.Parse(item.Trip.Price) * count).ToString();
                finalM.Add(item);
                sum += int.Parse(item.Trip.Price);
                cost += int.Parse(item.Trip.Cost);
            }
            var sal = _context.Accountensts.ToList();
            foreach (var sa in sal)
            {
                salary += int.Parse(sa.Salary);
            }
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            ViewBag.sum = sum;
            ViewBag.salary = sum - salary - cost;
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
            ViewBag.salaryY = sum - (salary * 12) - cost;
            var Tupple = Tuple.Create<IEnumerable<WebApplication4.Models.Reservation>, IEnumerable<WebApplication4.Models.Reservation>>(finalM, finalY);
            return View(Tupple);
        }
        public IActionResult users()
        {
            ViewBag.countoftrip = _context.Trips.DefaultIfEmpty().Count();
            ViewBag.countofclient = _context.Clients.DefaultIfEmpty().Count();
            ViewBag.countofreversation = _context.Reservations.DefaultIfEmpty().Count();
         var Client = _context.Clients.ToList();

            return View(Client);
        }
        public IActionResult contact()
        {
            var contact = _context.Contacts.Include(a => a.IdCusNavigation).ToList();
            return View(contact);
        }

    }
}
