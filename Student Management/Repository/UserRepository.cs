using AutoMapper;
using BCrypt.Net;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student_Management.Data;
using Student_Management.Models;
using Student_Management.Models.Dtos;
using Student_Management.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly AppSettings appSettings;

        public UserRepository(ApplicationDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        public User Authenticate(UserSignInDto modelVm)
        {
            var user = context.Users.SingleOrDefault
                                (x => x.Username == modelVm.Username);

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(modelVm.Password, user.Password);

            if (isValidPassword)
            {
                //generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretBytes = Encoding.UTF8.GetBytes(appSettings.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                user.Token = tokenHandler.WriteToken(token);

                return user;
            }

            return null;
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Register(UserRegisterDto modelVm)
        {
            modelVm.Password = BCrypt.Net.BCrypt.HashPassword(modelVm.Password);

            var obj = mapper.Map<User>(modelVm);
            context.Add(obj);
            context.SaveChanges();

            return obj;
        }
    }
}
