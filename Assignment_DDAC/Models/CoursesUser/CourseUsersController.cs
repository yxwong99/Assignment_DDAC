using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment_DDAC.Data;
using Assignment_DDAC.Models;

namespace Assignment_DDAC.Views.CoursesUser
{
    public class CourseUsersController : Controller
    {
        private readonly Assignment_DDACContext _context;

        public CourseUsersController(Assignment_DDACContext context)
        {
            _context = context;
        }

        // GET: CourseUsers
        public async Task<IActionResult> Index(string Level, string Type, string CourseName, string UniversityName, string Location)
        {
            var cour = from m in _context.CourseAdmin select m;

            //filter course level
            IQueryable<string> LevelQuery = from m in _context.CourseAdmin
                                           orderby m.Level
                                           select m.Level;
            IEnumerable<SelectListItem> lvls = new SelectList(await LevelQuery.Distinct().ToListAsync());
            ViewBag.Level = lvls;

            if (!string.IsNullOrEmpty(Level))
            {
                cour = cour.Where(s=>s.Level.Equals(Level));  
            }


            //filter course type
            IQueryable<string> TypeQuery = from m in _context.CourseAdmin
                                           orderby m.Type
                                           select m.Type;
            IEnumerable<SelectListItem> types = new SelectList(await TypeQuery.Distinct().ToListAsync());
            ViewBag.Type = types;

            if (!string.IsNullOrEmpty(Type))
            {
                cour = cour.Where(s => s.Type.Equals(Type));
            }


            //filter course name
            IQueryable<string> CnQuery = from m in _context.CourseAdmin
                                           orderby m.CourseName
                                           select m.CourseName;
            IEnumerable<SelectListItem> cns = new SelectList(await CnQuery.Distinct().ToListAsync());
            ViewBag.CourseName = cns;

            if (!string.IsNullOrEmpty(CourseName))
            {
                cour = cour.Where(s => s.CourseName.Equals(CourseName));
            }


            //filter university name
            IQueryable<string> UnQuery = from m in _context.CourseAdmin
                                         orderby m.UniversityName
                                         select m.UniversityName;
            IEnumerable<SelectListItem> uns = new SelectList(await UnQuery.Distinct().ToListAsync());
            ViewBag.UniversityName = uns;

            if (!string.IsNullOrEmpty(UniversityName))
            {
                cour = cour.Where(s => s.UniversityName.Equals(UniversityName));
            }


            //filter location
            IQueryable<string> LocQuery = from m in _context.CourseAdmin
                                         orderby m.Location
                                         select m.Location;
            IEnumerable<SelectListItem> locs = new SelectList(await LocQuery.Distinct().ToListAsync());
            ViewBag.Location = locs;

            if (!string.IsNullOrEmpty(Location))
            {
                cour = cour.Where(s => s.Location.Equals(Location));
            }

            return View(await cour.ToListAsync());
            return View(await _context.CourseAdmin.ToListAsync());
        }

        // GET: CourseUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseUser = await _context.CourseAdmin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courseUser == null)
            {
                return NotFound();
            }

            return View(courseUser);
        }

        // GET: CourseUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Level,Type,CourseName,UniversityName,Location,URL")] CourseAdmin courseUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseUser);
        }

        // GET: CourseUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseUser = await _context.CourseUser.FindAsync(id);
            if (courseUser == null)
            {
                return NotFound();
            }
            return View(courseUser);
        }

        // POST: CourseUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Level,Type,CourseName,UniversityName,Location,URL")] CourseAdmin courseUser)
        {
            if (id != courseUser.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseUserExists(courseUser.ID))
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
            return View(courseUser);
        }

        // GET: CourseUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseUser = await _context.CourseUser
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courseUser == null)
            {
                return NotFound();
            }

            return View(courseUser);
        }

        // POST: CourseUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseUser = await _context.CourseUser.FindAsync(id);
            _context.CourseUser.Remove(courseUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseUserExists(int id)
        {
            return _context.CourseUser.Any(e => e.ID == id);
        }
    }
}
