using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.DataAccessLayer;
using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateExpenseAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO == null)
            {
                return false;
            }

            var expenseEntity = _mapper.Map<Expense>(expenseDTO);
            expenseEntity.DateCreated = DateTime.UtcNow;
            expenseEntity.DateUpdated = DateTime.UtcNow;
            await _unitOfWork.GetExpenseRepository.AddAsync(expenseEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            await _unitOfWork.GetExpenseRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<ExpenseDTO>> GetAllExpenseAsync()
        {
            return _mapper.Map<List<Expense>, List<ExpenseDTO>>(await _unitOfWork.GetExpenseRepository.FindAllAsync());
        }

        public async Task<List<ExpenseDTO>> GetExpenseByMemberIdAsync(int memberId)
        {
            var listOfExpenses = await _unitOfWork.GetExpenseRepository.FindByCondition(u => u.Member.Id == memberId).ToListAsync();
            return _mapper.Map<List<Expense>, List<ExpenseDTO>>(listOfExpenses);
        }
        public async Task<ExpenseDTO> GetExpenseByIdAsync(int id)
        {
            return _mapper.Map<ExpenseDTO>(await _unitOfWork.GetExpenseRepository.FindByIdAsync(id));
        }

        public Task<bool> UpdateExpenseAsync(ExpenseDTO expenseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
