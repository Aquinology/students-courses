using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace StudentCourse.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s\-\,\.\&\(\)]*$")]
        [Remote(action: "CourseNameUnique", controller: "Courses")]
        public string Name { get; set; }
    }
}
