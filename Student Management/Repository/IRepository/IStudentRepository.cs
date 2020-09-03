using Student_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Repository.IRepository
{
    public interface IStudent
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
