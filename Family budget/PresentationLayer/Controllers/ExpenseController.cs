using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.PresentationLayer.Controllers
{
    //[Authorize(Policy = "Administrator")]
    [Authorize]
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

        public async Task<IActionResult> GetIndex()
        {
            var membersDTO = await _memberService.GetAllMembersAsync();
            var membersView = _mapper.Map<List<MemberDTO>, List<MemberViewModel>>(membersDTO);
            ViewBag.Category = membersView.Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() });
            return View();
        }

        public async Task<IActionResult> Index(int memberId)
        {
            var expensesDTO = await _expenseService.GetExpenseByMemberIdAsync(memberId);
            var expensesView = _mapper.Map<List<ExpenseDTO>, List<ExpenseViewModel>>(expensesDTO);

            if (expensesView.Count() == 0)
            {
                return RedirectToAction(nameof(Create), new { memberId });
            }

            return View(expensesView);
        }

        public async Task<IActionResult> Details(int id)
        {
            var expenseDTO = await _expenseService.GetExpenseByIdAsync(id);
            var expenseView = _mapper.Map<ExpenseDTO, ExpenseViewModel>(expenseDTO);
            return View(expenseView);
        }

        public ActionResult Create(int memberId)
        {
            ViewData["MemberId"] = memberId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseViewModel expenseView)
        {
            try
            {
                if (expenseView != null || expenseView.Value != 0m)
                {
                    var expenseDTO = _mapper.Map<ExpenseViewModel, ExpenseDTO>(expenseView);
                    await _expenseService.CreateExpenseAsync(expenseDTO);
                    return RedirectToAction("Index", new { expenseView.MemberId });
                }
                return RedirectToAction("GetIndex");
            }
            catch
            {
                return RedirectToAction("GetIndex");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expenseDTO = await _expenseService.GetExpenseByIdAsync(id);
            var expenseView = _mapper.Map<ExpenseDTO, ExpenseViewModel>(expenseDTO);

            if(expenseView != null)
            {
                return View(expenseView);
            }
            else
            {
                return RedirectToAction("GetIndex");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseViewModel expenseView)
        {
            try
            {
                var expenseDTO = _mapper.Map<ExpenseViewModel, ExpenseDTO>(expenseView);
                await _expenseService.UpdateExpenseAsync(expenseDTO);
                return RedirectToAction("Details", new { expenseView.Id });
            }
            catch
            {
                return RedirectToAction("GetIndex");
            }
        }

        public async Task<IActionResult> CheckDeleteAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var expenseDTO = await _expenseService.GetExpenseByIdAsync(id);
            var expenseView = _mapper.Map<ExpenseDTO, ExpenseViewModel>(expenseDTO);

            if (expenseView != null)
            {
                return View(expenseView);
            }
            else
            {
                return RedirectToAction("GetIndex");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }

                var expenseDTO = await _expenseService.GetExpenseByIdAsync(id);
                var expenseView = _mapper.Map<ExpenseDTO, ExpenseViewModel>(expenseDTO);

                await _expenseService.DeleteExpenseAsync(id);

                return RedirectToAction("Index", new { expenseView.MemberId });
            }
            catch
            {
                return RedirectToAction("GetIndex");
            }
        }
    }
}
