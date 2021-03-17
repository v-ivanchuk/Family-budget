using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.DataAccessLayer;
using Family_budget.PresentationLayer.Models;

namespace Family_budget.BusinessLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Expense, ExpenseDTO>().ReverseMap();
            CreateMap<MemberDTO, MemberViewModel>().ReverseMap();
            CreateMap<ExpenseDTO, ExpenseViewModel>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
        }
    }
}
