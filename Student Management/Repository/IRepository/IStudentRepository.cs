using Student_Management.Models;
using System.Collections.Generic;

namespace Student_Management.Repository.IRepository
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();

        Student GetStudent(int studentId);

        bool isStudentExists(string name);

        bool isStudentExists(int id);

        bool CreateStudent(Student student);

        bool UpdateStudent(Student student);

        bool DeleteStudent(Student student);

        bool Save();
    }
}