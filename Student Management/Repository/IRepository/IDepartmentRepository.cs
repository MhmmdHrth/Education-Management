using Student_Management.Models;
using System.Collections.Generic;

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