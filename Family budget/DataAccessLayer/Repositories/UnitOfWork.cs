using Family_budget.DataAccessLayer.Interfaces;
using System;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BudgetContext _budgetContext;
        private IMemberRepository _memberRepository;
        private IExpenseRepository _expenseRepository;
        private IUserRepository _userRepository;
        private bool _disposed = false;

        public UnitOfWork(BudgetContext budgetContext)
        {
            _budgetContext = budgetContext;
        }

        public IMemberRepository GetMemberRepository
        {
            get
            {
                return _memberRepository = _memberRepository
                    ?? new MemberRepository(_budgetContext);
            }
        }

        public IExpenseRepository GetExpenseRepository
        {
            get
            {
                return _expenseRepository = _expenseRepository
                    ?? new ExpenseRepository(_budgetContext);
            }
        }

        public IUserRepository GetUserRepository
        {
            get
            {
                return _userRepository = _userRepository
                    ?? new UserRepository(_budgetContext);
            }
        }

        public void SaveChanges() => _budgetContext.SaveChanges();

        public Task SaveChangesAsync() => _budgetContext.SaveChangesAsync();


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _budgetContext.Dispose();
            }
            _disposed = true;
        }
    }
}
