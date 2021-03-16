using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.DataAccessLayer;
using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var user = await _unitOfWork.GetUserRepository.CheckLoginPasswordAsync(memberEntity);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> CreateUserAsync(UserDTO userDTO)
        {

            if (userDTO == null)
            {
                return false;
            }

            var userEntity = _mapper.Map<User>(userDTO);

            var isLoginAvailable = await _unitOfWork.GetUserRepository.IsLoginAvailableAsync(userEntity.Login);

            if (isLoginAvailable)
            {
                userEntity.DateCreated = DateTime.Now;
                userEntity.DateUpdated = DateTime.Now;
                userEntity.PasswordDate = DateTime.Now;

                await _unitOfWork.GetUserRepository.AddAsync(userEntity);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return _mapper.Map<List<User>, List<UserDTO>>(await _unitOfWork.GetUserRepository.FindAllAsync());
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.GetUserRepository.FindByIdAsync(id));
        }

        public async Task<UserDTO> GetUserByLoginAsync(string login)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork
                .GetUserRepository
                .FindByCondition(u => u.Login == login)
                .FirstOrDefaultAsync());
        }
    }
}
