using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.DataAccessLayer.Entities;
using Family_budget.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Family_budget.PresentationLayer.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel userView)
        {
            try
            {
                var userDTO = _mapper.Map<UserViewModel, UserDTO>(userView);
                userDTO = await _userService.CheckUserLoginDataAsync(userDTO);

                if(userDTO != null)
                {
                    var memberView = _mapper.Map<UserViewModel>(userDTO);
                    await Authenticate(memberView.Login, memberView.Role.ToString());

                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("Login", "Login or password is incorrect. " +
                    "If you are not registered yet, please do this");

                return View();
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
                    ModelState.AddModelError("Login", $"Login {userView.Login} is not available. " +
                        $"Please use another one");
                    return View(userView);
                }
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        private async Task Authenticate(string userLogin, string userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin),
                new Claim(ClaimTypes.Role, userRole)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(20)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var usersDTO = await _userService.GetAllUsersAsync();
            var usersView = _mapper.Map<List<UserDTO>, List<UserViewModel>>(usersDTO);
            return View(usersView);
        }

        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var usersDTO = await _userService.GetUserByIdAsync(id);
            var usersView = _mapper.Map<UserDTO, UserViewModel>(usersDTO);

            if (usersView == null)
            {
                return NotFound();
            }

            return View(usersView);
        }

        public async Task<IActionResult> MyDetails()
        {
            var usersDTO = await _userService.GetUserByLoginAsync(User.Identity.Name);
            var usersView = _mapper.Map<UserDTO, UserViewModel>(usersDTO);

            if (usersView == null)
            {
                return NotFound();
            }

            return View("Details", usersView);
        }

        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var usersDTO = await _userService.GetUserByIdAsync(id);
            var usersView = _mapper.Map<UserDTO, UserViewModel>(usersDTO);

            if (usersView == null)
            {
                return NotFound();
            }

            return View(usersView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userView)
        {
            try
            {
                var userDTO = _mapper.Map<UserViewModel, UserDTO>(userView);
                await _userService.UpdateUserAsync(userDTO);
                if (User.IsInRole(UserRole.Admin.ToString()))
                {
                    return RedirectToAction("Details", new { userView.Id });
                }
                else
                {
                    return RedirectToAction("MyDetails");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> MyEdit()
        {
            var usersDTO = await _userService.GetUserByLoginAsync(User.Identity.Name);
            var usersView = _mapper.Map<UserDTO, UserViewModel>(usersDTO);

            if (usersView == null)
            {
                return NotFound();
            }

            return View("Edit", usersView);
        }

        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> CheckDelete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var usersDTO = await _userService.GetUserByIdAsync(id);
            var usersView = _mapper.Map<UserDTO, UserViewModel>(usersDTO);

            if (usersView != null)
            {
                return View(usersView);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Policy = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }

                await _userService.DeleteUserAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> EditLoginData()
        {
            var usersDTO = await _userService.GetUserByLoginAsync(User.Identity.Name);
            var usersView = _mapper.Map<UserDTO, UserViewModel>(usersDTO);

            if (usersView == null)
            {
                return NotFound();
            }

            return View(usersView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLoginData(UserViewModel userView)
        {
            try
            {
                if(userView.Password == userView.ConfirmPassword 
                    && userView.Login != null
                    && userView.Password != null
                    && userView?.Login?.Trim()?.Length != 0
                    && userView?.Password?.Trim()?.Length != 0)
                {
                    var userDTO = _mapper.Map<UserViewModel, UserDTO>(userView);
                    var updateResult = await _userService.UpdateUserLoginDataAsync(userDTO);

                    return View("EditLoginDataResult", updateResult);
                }
                else
                {
                    return View(userView);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
