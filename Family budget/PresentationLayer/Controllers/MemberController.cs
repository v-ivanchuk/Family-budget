﻿using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Family_budget.PresentationLayer.Controllers
{
    //[Authorize(Policy = "HeadOfFamily")]
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var membersDTO = await _memberService.GetAllMembersAsync();
            var membersView = _mapper.Map<List<MemberDTO>, List<MemberViewModel>>(membersDTO);
            return View(membersView);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            var memberDTO = await _memberService.GetMemberByIdAsync(id);
            var memberView = _mapper.Map<MemberDTO, MemberViewModel>(memberDTO);

            if (memberView == null)
            {
                return NotFound();
            }

            return View(memberView);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel memberView)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(memberView.Name))
                {
                    var memberDTO = _mapper.Map<MemberViewModel, MemberDTO>(memberView);
                    await _memberService.CreateMemberAsync(memberDTO);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var memberDTO = await _memberService.GetMemberByIdAsync(id);
            var memberView = _mapper.Map<MemberDTO, MemberViewModel>(memberDTO);

            if (memberView == null)
            {
                return NotFound();
            }

            return View(memberView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberViewModel memberView)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(memberView.Name))
                {
                    var memberDTO = _mapper.Map<MemberViewModel, MemberDTO>(memberView);
                    await _memberService.UpdateMemberAsync(memberDTO);
                    return RedirectToAction("Details", new { memberView.Id });
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> CheckDelete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var memberDTO = await _memberService.GetMemberByIdAsync(id);
            var memberView = _mapper.Map<MemberDTO, MemberViewModel>(memberDTO);

            if (memberView != null)
            {
                return View(memberView);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }

                await _memberService.DeleteMemberAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
