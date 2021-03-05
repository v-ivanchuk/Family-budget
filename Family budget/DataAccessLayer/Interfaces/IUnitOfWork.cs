using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IMemberRepository GetMemberRepository { get; }
        public IExpenseRepository GetExpenseRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
