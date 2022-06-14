using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentCourse.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string Name { get; set; }
        [Required]
        [Range(1, 10000)]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^([A-Z]{2,3})\-([0-9]{3,4})$")]
        public string Group { get; set; }
    }
}  