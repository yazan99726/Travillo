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

namespace WebApplication4.Controllers
{
    public class AboutusController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AboutusController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: Aboutus
        public async Task<IActionResult> Index()
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            return View(await _context.Aboutus.ToListAsync());
        }

        // GET: Aboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: Aboutus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Imagepath,ImageFile")] Aboutu aboutu)
        {
            if (ModelState.IsValid)
            {
                if (aboutu.ImageFile != null)
                {
                    string wwwroot = webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;//get image name only
                    string Pathof = Path.Combine(wwwroot + "/Image/" + fileName);
                    using (var filestream = new FileStream(Pathof, FileMode.Create))
                    {
                        await aboutu.ImageFile.CopyToAsync(filestream);
                    }
                    aboutu.Imagepath = fileName;
                    _context.Add(aboutu);
                    await _context.SaveChangesAsync();
                }
                else /*ele if category.Imagepath!=null*/
                {

                    return null;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aboutu);
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.cou = _context.Contacts.Include(a => a.IdCusNavigation).Count();

            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }
            return View(aboutu);
        }

        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Description,Imagepath,ImageFile")] Aboutu aboutu)
        {
            if (id != aboutu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        string wwwroot = webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;//get image name only
                        string Pathof = Path.Combine(wwwroot + "/Image/" + fileName);
                        string OldPath = Path.Combine(wwwroot + "/Image/" + aboutu.Imagepath);

                        using (var filestream = new FileStream(Pathof, FileMode.Create))
                        {
                            System.IO.File.Delete(OldPath);
                            await aboutu.ImageFile.CopyToAsync(filestream);
                        }
                        aboutu.Imagepath = fileName;
                        _context.Update(aboutu);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.Id))
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
            return View(aboutu);
        }
        string Uploadfile(IFormFile file, string img)
        {
            if (file != null)
            {
                string uploads = Path.Combine(webHostEnvironment.WebRootPath, "Image");
                string newpath = Path.Combine(uploads, file.FileName);
                string OldPath = Path.Combine(uploads, img);
                if (newpath != OldPath)
                {
                    System.IO.File.Delete(OldPath);
                    file.CopyTo(new FileStream(newpath, FileMode.Create));
                }
                return file.FileName;
            }
            return img;
        }

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var aboutu = await _context.Aboutus.FindAsync(id);
            _context.Aboutus.Remove(aboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(decimal id)
        {
            return _context.Aboutus.Any(e => e.Id == id);
        }
    }
}
