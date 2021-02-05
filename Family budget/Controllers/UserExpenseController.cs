using Family_budget.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.Controllers
{
    public class UserExpenseController : Controller
    {
        private readonly BudgetContext _context;

        public UserExpenseController(BudgetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetIndex()
        {
            ViewBag.Category = _context.Users.Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() });
            return View();
        }

        //[HttpPost]
        public ActionResult Index(int id)
        {
            var expenses = _context.UserExpenses.Include(u => u.User).Where(user => user.User.Id == id);
            if(expenses.Count() == 0)
                return RedirectToAction(nameof(Create), new { id });
            return View(expenses);
        }

        // GET: UserExpenseController/Details/5
        public ActionResult Details(int id)
        {
            var expense = _context.UserExpenses.Include(u => u.User).FirstOrDefault(ex => ex.Id == id);
            return View(expense);
        }

        // GET: UserExpenseController/Create
        public ActionResult Create(int id)
        {
            var user = _context.UserExpenses.Include(u => u.User).FirstOrDefault(user => user.User.Id == id);
            return View(user);
        }

        // POST: UserExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, UserExpense expenses)
        {
            try
            {
                if(expenses != null && expenses.Expense != 0m)
                {
                    expenses.User = _context.Users.FirstOrDefault(u => u.Id == id);
                    expenses.CountDate = DateTime.Now;
                    expenses.Id = 0;
                    _context.UserExpenses.Add(expenses);
                    await _context.SaveChangesAsync();
                    return View("Details", expenses);
                }
                var user = _context.UserExpenses.Include(u => u.User).FirstOrDefault(user => user.User.Id == id);
                return View(user);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: UserExpenseController/Edit/5
        public ActionResult Edit(int id)
        {
            var expense = _context.UserExpenses.Include(u => u.User).FirstOrDefault(ex => ex.Id == id);
            return View(expense);
        }

        // POST: UserExpenseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserExpense expenses)
        {
            try
            {
                _context.UserExpenses.Update(expenses);
                await _context.SaveChangesAsync();
                return View("Details", expenses);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: UserExpenseController/Delete/5
        public ActionResult Delete(int id)
        {
            var expense = _context.UserExpenses.Include(u => u.User).FirstOrDefault(ex => ex.Id == id);
            return View(expense);
        }

        // POST: UserExpenseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserExpense expenses)
        {
            try
            {
                _context.UserExpenses.Remove(expenses);
                await _context.SaveChangesAsync();
                return View("Index", _context.UserExpenses.Where(ex => ex.User.Id == expenses.User.Id));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
