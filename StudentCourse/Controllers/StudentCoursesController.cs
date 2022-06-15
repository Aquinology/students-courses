using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;

namespace StudentCourse.Controllers
{
    public class StudentCoursesController : Controller
    {
        private readonly StudentCourseContext _context;

        public StudentCoursesController(StudentCourseContext context)
        {
            _context = context;
        }

        // GET: StudentCourses
        public async Task<IActionResult> Index(int courseid)
        {
            var studentCourseContext = _context.StudentCourses
                .Include(s => s.Students).Where(s => s.CourseId == courseid);
            return View(await studentCourseContext.ToListAsync());
        }

        // GET: StudentCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: StudentCourses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId")] StudentCourses studentCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", studentCourses.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", studentCourses.StudentId);
            return View(studentCourses);
        }

        // GET: StudentCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentCourses == null)
            {
                return NotFound();
            }

            return View(studentCourses);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentCourses == null)
            {
                return Problem("Entity set 'StudentCourseContext.StudentCourses'  is null.");
            }
            var studentCourses = await _context.StudentCourses.FindAsync(id);
            if (studentCourses != null)
            {
                _context.StudentCourses.Remove(studentCourses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCoursesExists(int id)
        {
          return (_context.StudentCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
