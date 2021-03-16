
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> CheckLoginDataAsync(User user);
        public Task<bool> IsLoginAvailableAsync(string userLogin);
    }
}
