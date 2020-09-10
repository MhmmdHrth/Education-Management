using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models.Dtos.DepartmentDto
{
    public class DepartmentCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}