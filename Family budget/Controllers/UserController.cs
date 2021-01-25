using Family_budget.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.Controllers
{
    public class UserController : Controller
    {
        private readonly BudgetContext userContext;

        public UserController(BudgetContext budgetContext)
        {
            userContext = budgetContext;
        }

        // GET: User
        public ActionResult Index()
        {
            var users = userContext.Users;
            return View(users);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            var user = await userContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    userContext.Users.Add(user);
                    await userContext.SaveChangesAsync();
                    return View("Index", userContext.Users);
                }
                return View("Index", userContext.Users);
            }
            catch
            {
                return View("Index", userContext.Users);
            }
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var user = await userContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    userContext.Users.Update(user);
                    await userContext.SaveChangesAsync();
                    return View("Details", user);
                }
                return View("Index", userContext.Users);
            }
            catch
            {
                return View("Index", userContext.Users);
            }
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user != null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User user)
        {
            try
            {
                userContext.Users.Remove(user);
                await userContext.SaveChangesAsync();
                return View("Index", userContext.Users);
            }
            catch
            {
                return View("Index", userContext.Users);
            }
        }
    }
}
