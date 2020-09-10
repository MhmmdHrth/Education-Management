using Student_Management.Data;
using Student_Management.Models;
using Student_Management.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _db;

        public TeacherRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTeacher(Teacher teacher)
        {
            _db.Teachers.Add(teacher);
            return Save();
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            _db.Teachers.Remove(teacher);
            return Save();
        }

        public Teacher GetTeacher(int teacherId)
        {
            var teacher = _db.Teachers.FirstOrDefault(x => x.Id == teacherId);
            return teacher;
        }

        public ICollection<Teacher> GetTeachers()
        {
            var teachers = _db.Teachers.OrderBy(x => x.Name).ToList();
            return teachers;
        }

        public bool isTeacherExists(string name)
        {
            bool value = _db.Teachers.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool isTeacherExists(int id)
        {
            bool value = _db.Teachers.Any(x => x.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            _db.Teachers.Update(teacher);
            return Save();
        }
    }
}