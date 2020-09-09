using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Models.Dtos.TeacherDto
{
    public class TeacherCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public int PhoneNumber { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
