using System.Security.Claims;
using API.Dto;
using API.Errors;
using AutoMapper;
using Core.Interfaces;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<Role> roleManager,
                                 ITokenService tokenService,
                                 IMapper mapper)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var userRoles = _userManager.GetRolesAsync(user).Result;


            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user,userRoles.ToList()),
                Roles = userRoles.ToArray()
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailAlreadyExists(registerDto.Email).Result.Value == true){
                return new BadRequestObjectResult(new ValidationErrorResponse{Errors=new[]{"Email Already Exists"}});
            }

            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var checkRoleExist = _roleManager.FindByNameAsync(registerDto.Role).Result;

            if(checkRoleExist == null){
                var role = new Role{ Name = registerDto.Role};
                await _roleManager.CreateAsync(role);


                 await _userManager.AddToRoleAsync(user,registerDto.Role);
            }

           

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var userRoles = _userManager.GetRolesAsync(user).Result;

           return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user,userRoles.ToList()),
                Roles = userRoles.ToArray()
            };
        }

        

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type == ClaimTypes.Email).Value;

            var user = await _userManager.FindByEmailAsync(email);

           var userRoles = _userManager.GetRolesAsync(user).Result;

           return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user,userRoles.ToList()),
                Roles = userRoles.ToArray()
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailAlreadyExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

      


       


    }
}