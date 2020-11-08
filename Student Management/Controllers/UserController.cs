using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Models.Dtos;
using Student_Management.Repository.IRepository;

namespace Student_Management.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200, Type = typeof(UserSignInDto))]
        public IActionResult Authenticate(UserSignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.Authenticate(model);
                return Ok(user);
            }

            return BadRequest(new { message = "Not Valid"});
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UserRegisterDto))]
        public IActionResult Register(UserRegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.Register(model);
                return Ok(user);
            }
            return BadRequest(new { message = "Not Valid" });
        }
    }
}
