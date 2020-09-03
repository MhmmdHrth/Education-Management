using Microsoft.VisualBasic;
using Student_Management.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();

        Department GetDepartment(int departmentId);

        bool isDepartmentExists(string name);

        bool isDepartmentExists(int id);

        bool CreateDepartment(Department department);

        bool UpdateDepartment(Department department);

        bool DeleteDepartment(Department department);

        bool Save();
    }
}
