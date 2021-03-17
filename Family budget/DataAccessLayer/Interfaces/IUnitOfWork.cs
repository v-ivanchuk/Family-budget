using System;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IMemberRepository GetMemberRepository { get; }
        public IExpenseRepository GetExpenseRepository { get; }
        public IUserRepository GetUserRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
