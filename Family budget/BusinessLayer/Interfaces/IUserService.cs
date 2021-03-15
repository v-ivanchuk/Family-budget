using Family_budget.BusinessLayer.DTO;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CheckUserLoginDataAsync(UserDTO userDTO);
        Task<bool> CreateUserAsync(UserDTO userDTO);
    }
}
