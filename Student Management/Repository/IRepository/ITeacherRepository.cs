using Student_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Repository.IRepository
{
    interface ITeacherRepository
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
