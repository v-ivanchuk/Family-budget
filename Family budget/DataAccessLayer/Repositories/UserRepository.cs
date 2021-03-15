using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BudgetContext budgetContext)
            : base(budgetContext)
        {
        }

        public Task<User> CheckAsync(User user)
        {
            var checkUser = context.Users
                .FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);

            if (checkUser != null)
            {
                return checkUser;
            }
            return null;
        }
    }
}
