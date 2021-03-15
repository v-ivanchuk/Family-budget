using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.DataAccessLayer;
using Family_budget.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> CheckUserLoginDataAsync(UserDTO userDTO)
        {
            var memberEntity = _mapper.Map<User>(userDTO);
            var user = await _unitOfWork.GetUserRepository.CheckAsync(memberEntity);
            userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<bool> CreateUserAsync(UserDTO userDTO)
        {

            if (userDTO == null)
            {
                return false;
            }

            var userEntity = _mapper.Map<User>(userDTO);
            userEntity.DateCreated = DateTime.Now;
            userEntity.DateUpdated = DateTime.Now;
            userEntity.PasswordDate = DateTime.Now;

            await _unitOfWork.GetUserRepository.AddAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
