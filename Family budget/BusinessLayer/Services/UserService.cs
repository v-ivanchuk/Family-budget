using AutoMapper;
using Family_budget.BusinessLayer.DTO;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.DataAccessLayer;
using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var user = await _unitOfWork.GetUserRepository.CheckLoginDataAsync(memberEntity);
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

        public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {

            var checkUser = await _unitOfWork.GetUserRepository.FindByIdAsync(userDTO.Id);
            if (userDTO == null || checkUser == null)
            {
                return false;
            }

            var userEntity = _mapper.Map<User>(userDTO);

            await _unitOfWork.GetUserRepository.UpdateAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            await _unitOfWork.GetUserRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserLoginDataAsync(UserDTO userDTO)
        {
            var checkUser = await _unitOfWork.GetUserRepository.FindByIdAsync(userDTO.Id);
            if (userDTO == null || checkUser == null)
            {
                return false;
            }

            var usertempEntity = _mapper.Map<User>(userDTO);

            if (usertempEntity.Login != checkUser.Login)
            {
                var isLoginAvailable = await _unitOfWork.GetUserRepository.IsLoginAvailableAsync(usertempEntity.Login);
                if (!isLoginAvailable)
                {
                    return false;
                }
                else
                {
                    checkUser.Login = usertempEntity.Login;
                }
            }

            checkUser.DateUpdated = DateTime.Now;
            checkUser.PreviousPasswords += "..OlDpAsS:" + checkUser.Password;
            checkUser.Password = usertempEntity.Password;
            checkUser.PasswordDate = DateTime.Now;

            await _unitOfWork.GetUserRepository.UpdateAsync(checkUser);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
