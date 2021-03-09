using Family_budget.BusinessLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDTO> GetMemberByIdAsync(int id);
        Task<List<MemberDTO>> GetAllMembersAsync();
        Task<bool> CreateMemberAsync(MemberDTO memberDTO);
        Task<bool> UpdateMemberAsync(MemberDTO memberDTO);
        Task<bool> DeleteMemberAsync(int id);
    }
}
