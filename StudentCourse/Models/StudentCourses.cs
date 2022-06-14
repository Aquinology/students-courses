using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentCourse.Models
{
    public class StudentCourses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Students Students { get; set; }
        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [ValidateNever]
        public Courses Courses { get; set; }
    }
}
