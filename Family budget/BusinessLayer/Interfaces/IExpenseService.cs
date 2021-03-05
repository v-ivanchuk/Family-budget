using Family_budget.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseDTO> GetExpenseByIdAsync(int id);
        Task<List<ExpenseDTO>> GetAllExpenseAsync();
        Task<List<ExpenseDTO>> GetExpenseByMemberIdAsync(int memberId);
        Task<bool> CreateExpenseAsync(ExpenseDTO expenseDTO);
        Task<bool> UpdateExpenseAsync(ExpenseDTO expenseDTO);
        Task<bool> DeleteExpenseAsync(int id);
    }
}
