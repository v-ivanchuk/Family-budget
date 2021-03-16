using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> IsLoginAvailableAsync(string userLogin)
        {
            var checkUser = await context.Users
                .FirstOrDefaultAsync(u => u.Login == userLogin);
            return checkUser == null;
        }

        public Task<User> CheckLoginDataAsync(User user)
        {
            var checkUser = context.Users
                .FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);

            if (checkUser != null)
            {
                return checkUser;
            }
            return null;
        }

        public new async Task UpdateAsync(User user)
        {
            var checkUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (checkUser == null)
            {
                return;
            }

            checkUser.Address = user.Address;
            checkUser.City = user.City;
            checkUser.DateUpdated = DateTime.Now;
            checkUser.Name = user.Name;
            checkUser.Email = user.Email;
            checkUser.PhoneNumber = user.PhoneNumber;
            checkUser.Surname = user.Surname;
            checkUser.Role = user.Role;

            await Task.Run(() => context.Users.Update(checkUser));
        }
    }
}
