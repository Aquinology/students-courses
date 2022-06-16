using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentCourse.Models
{
    public class StudentCourses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Remote(action: "StudentsInCourseUnique", controller: "StudentCourses", 
            AdditionalFields = nameof(CourseId), ErrorMessage = "Product name already exists under the chosen category. Please enter a different product name.")]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Students Students { get; set; }
        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [ValidateNever]
        [Remote(action: "StudentIdUnique", controller: "Students")]
        public Courses Courses { get; set; }
    }
}
