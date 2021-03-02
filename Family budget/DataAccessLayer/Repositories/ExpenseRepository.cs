using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(BudgetContext budgetContext)
            : base(budgetContext)
        {
        }

        public new IQueryable<Expense> FindAll()
        {
            return context.Expenses
                     .Include(member => member.Member);
        }
        public new IQueryable<Expense> FindByCondition(Expression<Func<Expense, bool>> expression)
        {
            return context.Expenses
                     .Include(member => member.Member)
                     .Where(expression);
        }

        public new Expense GetById(int id)
        {
            return context.Expenses
                     .Include(member => member.Member)
                     .FirstOrDefault(entity => entity.Id == id);
        }

        public new Task<Expense> GetByIdAsync(int id)
        {
            return context.Expenses
                     .Include(member => member.Member)
                     .FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
