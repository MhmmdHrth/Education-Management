using Student_Management.Models;
using System.Collections.Generic;

namespace Student_Management.Repository.IRepository
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetTeachers();

        Teacher GetTeacher(int teacherId);

        bool isTeacherExists(string name);

        bool isTeacherExists(int id);

        bool CreateTeacher(Teacher teacher);

        bool UpdateTeacher(Teacher teacher);

        bool DeleteTeacher(Teacher teacher);

        bool Save();
    }
}