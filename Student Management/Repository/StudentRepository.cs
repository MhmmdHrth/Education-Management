using Student_Management.Data;
using Student_Management.Models;
using Student_Management.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateStudent(Student student)
        {
            _db.Students.Add(student);
            return Save();
        }

        public bool DeleteStudent(Student student)
        {
            _db.Students.Remove(student);
            return Save();
        }

        public Student GetStudent(int studentId)
        {
            var student = _db.Students.FirstOrDefault(x => x.Id == studentId);
            return student;
        }

        public ICollection<Student> GetStudents()
        {
            var students = _db.Students.OrderBy(x => x.Name).ToList();
            return students;
        }

        public bool isStudentExists(string name)
        {
            bool value = _db.Students.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool isStudentExists(int id)
        {
            bool value = _db.Students.Any(x => x.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateStudent(Student student)
        {
            _db.Students.Update(student);
            return Save();
        }
    }
}