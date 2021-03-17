using Family_budget.BusinessLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CheckUserLoginDataAsync(UserDTO userDTO);
        Task<bool> CreateUserAsync(UserDTO userDTO);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByLoginAsync(string login);
        Task<bool> UpdateUserAsync(UserDTO userDTO);
        Task<bool> UpdateUserLoginDataAsync(UserDTO userDTO);
        Task<bool> DeleteUserAsync(int id);
    }
}
