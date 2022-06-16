using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Models;

namespace StudentCourse.Data
{
    public class StudentCourseContext : DbContext
    {
        public StudentCourseContext (DbContextOptions<StudentCourseContext> options)
            : base(options)
        {
        }

        public DbSet<StudentCourse.Models.Students>? Students { get; set; }

        public DbSet<StudentCourse.Models.Courses>? Courses { get; set; }

        public DbSet<StudentCourse.Models.StudentCourses>? StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasIndex(s => s.StudentId)
                .IsUnique();

            modelBuilder.Entity<Courses>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<StudentCourses>()
                .HasIndex(c => new { c.CourseId, c.StudentId })
                .IsUnique();
        }
    }
}
