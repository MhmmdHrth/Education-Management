using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Models.Dtos.StudentDto
{
    public class StudentCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public int PhoneNumber { get; set; }

        [Required]
        public int TeacherId { get; set; }
    }
}
