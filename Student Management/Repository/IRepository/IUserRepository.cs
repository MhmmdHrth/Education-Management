using Student_Management.Models;
using Student_Management.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);

        User Authenticate(UserSignInDto modelDto);

        User Register(UserRegisterDto modelDto);
    }
}
