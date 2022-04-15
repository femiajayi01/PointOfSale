using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PosCore.Models;
using PosCore.ViewModels;

namespace PosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationSettings _applicationSettings;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _applicationSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(RegisterViewModel model)
        {
            model.Role = "Admin";
            var applicationUser = new ApplicationUser()

            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Sync = false
            };
            try
            {
                 var result = await _userManager.CreateAsync(applicationUser, "Password1!");  // await _userManager.CreateAsync(user, model.Password);
                 await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception e)
            {
                //  Console.WriteLine(e);
                throw e;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);   //    FindByNameAsync(model.Email); 
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Get the role assigned to the user
                var role = await _userManager.GetRolesAsync(user);
                var securityKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JwtSecret.ToString()));

                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1), // DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

        }

        [HttpGet]
        [Route("Users")]
        //Get : /api/ApplicationUser/Users
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUser")]
        //Get : /api/ApplicationUser/GetUser
        public async Task<BasicResponse> GetUser(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);
            return user == null ? BasicResponse.FailureResponse("Unable to retrive user") : BasicResponse.SuccessResponse("Success", user);
        }

        [HttpGet]
        [Route("UserProfile")]
       // [Authorize]
        // GET : /api/ApplicationUser/UserProfile
        public async Task<object> GetUserProfile()
        {
            /*            string userId = User.Claims.First(c => c.Type == "UserID").Value;
                        var user = await _userManager.FindByIdAsync(userId);
                        return user;*/

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }

       [HttpPost]
       [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
       {
           if (ModelState.IsValid)
           {
               IdentityRole identityRole = new IdentityRole
               {
                   Name = model.RoleName
               };
               IdentityResult result = await _roleManager.CreateAsync(identityRole);
               if (result.Succeeded)
                   return Ok(result);
               foreach (IdentityError error in result.Errors)
               {
                   ModelState.AddModelError("", error.Description);
               }
           }
           return Ok(model);
       }






    }
}
