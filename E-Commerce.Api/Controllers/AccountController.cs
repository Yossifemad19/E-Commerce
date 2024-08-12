using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Api.Errors;
using E_Commerce.Api.Extensions;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Api.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ItokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,ItokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult> GetCurrentUser()
        {

            var user= await _userManager.GetUserByClaims(User);


            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.GetToken(user),
            });
        }


        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginDto loginForm)
        {
            var user = await _userManager.FindByEmailAsync(loginForm.Email);

            if (user == null || ! await _userManager.CheckPasswordAsync(user,loginForm.Password))
            {
                return Unauthorized(new ApiResponse(401));
            }

            var returnUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.GetToken(user),
            };


            return Ok(returnUser);
        }

        [Authorize]
        [HttpGet("Address")]

        public async Task<ActionResult> GetAddress()
        {
            var user = await _userManager.GetUserWithAddresByEmail(User);

            return Ok(_mapper.Map<Address,AddressDto>(user.Address));
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult> CheckEmailExist(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            return Ok(user != null);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterDto registerForm)
        {
            var user = new AppUser()
            {
                DisplayName = registerForm.DisplayName,
                Email = registerForm.Email,
                UserName = registerForm.DisplayName,
            };

            var result = await _userManager.CreateAsync(user,registerForm.Password);

            if(!result.Succeeded) {
                return BadRequest(new ApiResponse(400));
            }

            var returnUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.GetToken(user),
            };

            return Ok(returnUser);
        }
    }
}
