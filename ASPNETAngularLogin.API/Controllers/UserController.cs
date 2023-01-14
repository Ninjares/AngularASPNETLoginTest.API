using ASPNETAngularLogin.Core.Models.User;
using ASPNETAngularLogin.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Route("User")]


    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterForm form)
        {
            if (form.Password != form.ConfirmPassword) 
                return BadRequest(new  { Error = "Passwords not matching" });

            await userManager.CreateAsync(new User
            {
                UserName = form.Username,
                Email = form.Email,
            }, 
            form.Password);

            return Ok(new { Message = $"User \"{userManager.FindByNameAsync(form.Username).Result.UserName}\" created"});
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            if (signInManager.IsSignedIn(this.User)) return BadRequest("Already signed in");
            var signin = await signInManager.PasswordSignInAsync(form.Username, form.Password, true, true);
            if (signin.Succeeded)
                return Ok($"User {form.Username} successfuly signed in");
            else return BadRequest(new { Error = "Username/password invalid" });
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            if (!signInManager.IsSignedIn(this.User)) return BadRequest("Nobody signed in");
            await signInManager.SignOutAsync();
            return Ok($"Successfuly singed out");
        }
    }
}
