using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models.Dtos.DepartmentDto
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}