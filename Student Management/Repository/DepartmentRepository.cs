using Student_Management.Data;
using Student_Management.Models;
using Student_Management.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateDepartment(Department department)
        {
            _db.Departments.Add(department);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _db.Remove(department);
            return Save();
        }

        public Department GetDepartment(int departmentId)
        {
            var department = _db.Departments.FirstOrDefault(x => x.Id == departmentId);
            return department;
        }

        public ICollection<Department> GetDepartments()
        {
            return _db.Departments.OrderBy(x => x.Name).ToList();
        }

        public bool isDepartmentExists(string name)
        {
            bool value = _db.Departments.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool isDepartmentExists(int id)
        {
            bool value = _db.Departments.Any(x => x.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateDepartment(Department department)
        {
            _db.Departments.Update(department);
            return Save();
        }
    }
}
