using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment_DDAC.Data;
using Assignment_DDAC.Models;
using System.IO;

namespace Assignment_DDAC.Views.CoursesAdmin
{
    public class CourseAdminsController : Controller
    {
        private readonly Assignment_DDACContext _context;

        public CourseAdminsController(Assignment_DDACContext context)
        {
            _context = context;
        }

        // GET: CourseAdmins
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseAdmin.ToListAsync());
        }

        // GET: CourseAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAdmin = await _context.CourseAdmin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courseAdmin == null)
            {
                return NotFound();
            }

            return View(courseAdmin);
        }

        // GET: CourseAdmins/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Image,Level,Type,CourseName,UniversityName,Location,URL")] CourseAdmin courseAdmin)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    //.UploadImageController = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                }
                _context.Add(courseAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseAdmin);
        }

        // GET: CourseAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAdmin = await _context.CourseAdmin.FindAsync(id);
            if (courseAdmin == null)
            {
                return NotFound();
            }
            return View(courseAdmin);
        }

        // POST: CourseAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Level,Type,CourseName,UniversityName,Location,URL")] CourseAdmin courseAdmin)
        {
            if (id != courseAdmin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseAdminExists(courseAdmin.ID))
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
            return View(courseAdmin);
        }

        // GET: CourseAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAdmin = await _context.CourseAdmin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courseAdmin == null)
            {
                return NotFound();
            }

            return View(courseAdmin);
        }

        // POST: CourseAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseAdmin = await _context.CourseAdmin.FindAsync(id);
            _context.CourseAdmin.Remove(courseAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseAdminExists(int id)
        {
            return _context.CourseAdmin.Any(e => e.ID == id);
        }
    }
}
