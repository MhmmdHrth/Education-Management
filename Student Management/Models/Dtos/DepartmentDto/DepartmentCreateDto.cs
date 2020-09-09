using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Models.Dtos.DepartmentDto
{
    public class DepartmentCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
