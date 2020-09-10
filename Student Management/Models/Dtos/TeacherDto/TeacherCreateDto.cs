using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models.Dtos.TeacherDto
{
    public class TeacherCreateDto
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

        public byte[] Picture { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}