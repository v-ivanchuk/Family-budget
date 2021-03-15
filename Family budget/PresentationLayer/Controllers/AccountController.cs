using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Family_budget.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userView)
        {
            try
            {
                var userDTO = _mapper.Map<UserViewModel, UserDTO>(userView);
                userDTO = await _userService.CheckUserLoginDataAsync(userDTO);

                if(userDTO != null)
                {
                    var memberView = _mapper.Map<UserViewModel>(userDTO);
                    await Authenticate(memberView.Login); // аутентификация

                    return RedirectToAction("Index", "Home");
                }

                //return RedirectToAction("Login");
                ModelState.AddModelError(/*"Login"*/"", "User not found");
                return View();
            }
            catch
            {
                return RedirectToAction("Login");
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userView)
        {
            try
            {
                var userDTO = _mapper.Map<UserViewModel, UserDTO>(userView);

                var creationResult = await _userService.CreateUserAsync(userDTO);
                if (creationResult)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Register");
                }

            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        private async Task Authenticate(string userLogin)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Test()
        {
            return Content("Name: " + User.Identity.Name);
        }
    }
}
