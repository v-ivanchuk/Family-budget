
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> CheckAsync(User user);
    }
}
