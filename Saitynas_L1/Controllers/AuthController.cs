using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_L1.Auth;
using Saitynas_L1.Auth.Model;
using Saitynas_L1.Data.Dtos.Auth;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<User> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.Username);
            if (user != null)
            {
                return BadRequest("User already exists.");
            }
            var newUser = new User
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email
            };
            var result = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Could not create user.");
            }
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                return BadRequest("Username is invalid.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Password is invalid.");
            }
            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);
            return Ok(new SuccessfulLoginResponseDto(accessToken));
        }
    }
}
