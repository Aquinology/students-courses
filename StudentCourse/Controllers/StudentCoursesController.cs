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
            if (_context.StudentCourses == null || _context.Courses == null)
            {
                Problem("Entity set 'StudentCourseContext.StudentCourses' or 'StudentCourseContext.Courses'  is null.");
            }
            var studentCourseContext = _context.StudentCourses
                .Include(s => s.Students).Where(s => s.CourseId == courseid);
            ViewBag.CourseId = courseid;
            var course = _context.Courses.FirstOrDefault(s => s.Id == courseid);
            if (course != null)
            {
                ViewBag.CourseName = course.Name;
            }
            return View(await studentCourseContext.ToListAsync());
        }

        public IActionResult StudentsInCourseUnique(int courseid, int studentid)
        {
            if (_context.StudentCourses == null)
            {
                return Problem("Entity set 'StudentCourseContext.StudentCourses'  is null.");
            }
            if (_context.StudentCourses.Any(x => x.CourseId == courseid && x.StudentId == studentid))
            {
                return Json($"This student already in course.");
            }
            return Json(true);
        }

        // GET: StudentCourses/Create
        public IActionResult Create(int courseid)
        {
            if (_context.StudentCourses == null)
            {
                return Problem("Entity set 'StudentCourseContext.StudentCourses'  is null.");
            }
            var students = _context.StudentCourses.Where(x => x.CourseId == courseid).Select(x => x.StudentId);
            //var s = _context.Students.Where(x => x.Group == "IT-119");
            ViewData["StudentId"] = new SelectList(_context.Students.Where(x => !students.Contains(x.Id)), "Id", "Name");
            ViewBag.CourseId = courseid;
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
                return RedirectToAction(nameof(Index), new { courseid = studentCourses.CourseId });
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

            ViewBag.CourseId = studentCourses.CourseId;
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
            return RedirectToAction(nameof(Index), new { courseid = studentCourses.CourseId });
        }

        private bool StudentCoursesExists(int id)
        {
          return (_context.StudentCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
