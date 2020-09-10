using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models.Dtos.StudentDto
{
    public class StudentDto
    {
        public int Id { get; set; }

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