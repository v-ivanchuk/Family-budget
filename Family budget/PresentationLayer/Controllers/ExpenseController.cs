using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.Models;
using Family_budget.PresentationLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.PresentationLayer.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService, IMemberService memberService, IMapper mapper)
        {
            _expenseService = expenseService;
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIndex()
        {
            var membersDTO = await _memberService.GetAllMembersAsync();
            var membersView = _mapper.Map<List<MemberDTO>, List<MemberViewModel>>(membersDTO);
            ViewBag.Category = membersView.Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() });
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            //var expenses = _context.UserExpenses.Include(u => u.User).Where(user => user.User.Id == id);
            var expensesDTO = await _expenseService.GetExpenseByMemberIdAsync(id);
            var expensesView = _mapper.Map<List<ExpenseDTO>, List<ExpenseViewModel>>(expensesDTO);

            if (expensesView.Count() == 0)
                return RedirectToAction(nameof(Create), new { id });
            return View(expensesView);
        }

        //// GET: UserExpenseController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var expense = _context.UserExpenses.Include(u => u.User).FirstOrDefault(ex => ex.Id == id);
        //    return View(expense);
        //}

        // GET: UserExpenseController/Create
        public async Task<ActionResult> Create(int id)
        {
            //var user = _context.UserExpenses.Include(u => u.User).FirstOrDefault(user => user.User.Id == id);
            var expensesDTO = await _expenseService.GetExpenseByMemberIdAsync(id);
            var expensesView = _mapper.Map<List<ExpenseDTO>, List<ExpenseViewModel>>(expensesDTO);
            return View(expensesView.FirstOrDefault());
        }

        // POST: UserExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, ExpenseViewModel expenseView)
        {
            //try
            {
                if (expenseView != null || expenseView.Value != 0m)
                {
                    var memberDTO = await _memberService.GetMemberByIdAsync(id);
                    var memberView = _mapper.Map<MemberDTO, MemberViewModel>(memberDTO);

                    expenseView.Member = memberView;
                    expenseView.Id = 0;
                    var expenseDTO = _mapper.Map<ExpenseViewModel, ExpenseDTO>(expenseView);
                    await _expenseService.CreateExpenseAsync(expenseDTO);
                    return RedirectToAction("GetIndex");

                    //expenses.User = _context.Users.FirstOrDefault(u => u.Id == id);
                    //expenses.CountDate = DateTime.Now;
                    //expenses.Id = 0;
                    //_context.UserExpenses.Add(expenses);
                    //await _context.SaveChangesAsync();
                    //return View("Details", expenses);
                }
                //var user = _context.UserExpenses.Include(u => u.User).FirstOrDefault(user => user.User.Id == id);
                //return View(user);
                return RedirectToAction("GetIndex");
            }
            //catch
            //{
            //    return NotFound();
            //}
        }

        //// GET: UserExpenseController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var expense = _context.UserExpenses.Include(u => u.User).FirstOrDefault(ex => ex.Id == id);
        //    return View(expense);
        //}

        //// POST: UserExpenseController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(UserExpense expenses)
        //{
        //    try
        //    {
        //        _context.UserExpenses.Update(expenses);
        //        await _context.SaveChangesAsync();
        //        return View("Details", expenses);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}

        //// GET: UserExpenseController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var expense = _context.UserExpenses.Include(u => u.User).FirstOrDefault(ex => ex.Id == id);
        //    return View(expense);
        //}

        //// POST: UserExpenseController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(UserExpense expenses)
        //{
        //    try
        //    {
        //        _context.UserExpenses.Remove(expenses);
        //        await _context.SaveChangesAsync();
        //        return View("Index", _context.UserExpenses.Where(ex => ex.User.Id == expenses.User.Id));
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
