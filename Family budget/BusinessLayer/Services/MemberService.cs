using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.DataAccessLayer;
using Family_budget.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MemberDTO>> GetAllMembersAsync()
        {
            return _mapper.Map<List<Member>, List<MemberDTO>>(await _unitOfWork.GetMemberRepository.FindAllAsync());
        }

        public async Task<MemberDTO> GetMemberByIdAsync(int id)
        {
            return _mapper.Map<MemberDTO>(await _unitOfWork.GetMemberRepository.FindByIdAsync(id));
        }

        public async Task<bool> CreateMemberAsync(MemberDTO memberDTO)
        {
            if(memberDTO == null)
            {
                return false;
            }

            var memberEntity = _mapper.Map<Member>(memberDTO);
            memberEntity.DateCreated = DateTime.UtcNow;
            memberEntity.DateUpdated = DateTime.UtcNow;
            await _unitOfWork.GetMemberRepository.AddAsync(memberEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMemberAsync(MemberDTO memberDTO)
        {
            var checkMember = _unitOfWork.GetMemberRepository.FindById(memberDTO.Id);
            if(memberDTO == null || checkMember == null)
            {
                return false;
            }

            var memberEntity = _mapper.Map<Member>(memberDTO);
            await _unitOfWork.GetMemberRepository.UpdateAsync(memberEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var expenses = _unitOfWork.GetExpenseRepository.FindByCondition(ex => ex.Member.Id == id);
            _unitOfWork.GetExpenseRepository.DeleteRange(expenses);

            await _unitOfWork.GetMemberRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
