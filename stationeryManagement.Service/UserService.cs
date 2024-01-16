using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;
using stationeryManagement.Service.Utils;

namespace stationeryManagement.Service;

public class UserService : EntityService<User>, IUserService
{
    private IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.UserRepository)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _unitOfWork.UserRepository.GetUserWithRole().ToListAsync();
    }

    public async Task<User?> GetUser(Guid id)
    {
        return await _unitOfWork.UserRepository.GetByIdAsync(id);
    }

    public async Task<User?> CreateUser(UserDto userDto)
    {
        if (userDto.FileUpload != null && userDto.FileUpload.Length > 0)
        {
            var path = await FileUtils.AddFile("avatar", userDto.FileUpload);
            userDto.Image = path;
        }
        else
        {
            userDto.Image = "/images/avatar/default_avatar.jpg";
        }

        var newUser = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            RoleId = userDto.RoleId,
            Image = userDto.Image,
            RegistrationDate = DateTime.Now,
            SuperiorId = userDto.SuperiorId,
            Password = PasswordUtils.HashPassword(userDto.Password),
        };
        var userD = await _unitOfWork.UserRepository.AddAsync(newUser);
        return await _unitOfWork.CommitAsync() > 0 ? userD : null;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user != null)
        {
            _unitOfWork.UserRepository.Delete(user);
            var result = await _unitOfWork.CommitAsync() > 0;
            if (result)
            {
                if (!string.IsNullOrWhiteSpace(user.Image))
                {
                    FileUtils.RemoveFile(user.Image);
                }
            }

            return result;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> UpdateUser(UserDto userDto, Guid id)
    {
        var findUser = await _unitOfWork.UserRepository.GetByIdAsync(id);

        if (findUser != null)
        {
            if (userDto.FileUpload != null && userDto.FileUpload.Length > 0)
            {
                var path = await FileUtils.AddFile("avatar", userDto.FileUpload);
                if (!string.IsNullOrWhiteSpace(findUser.Image))
                {
                    FileUtils.RemoveFile(findUser.Image);
                }

                userDto.Image = path;
            }
            else
            {
                userDto.Image = "/images/avatar/default_avatar.jpg";
            }

            var updateUser = new User
            {
                UserId = findUser.UserId,
                Name = string.IsNullOrWhiteSpace(userDto.Name) ? findUser.Name : userDto.Name,
                Email = userDto.Email,
                RoleId = userDto.RoleId,
                Image = userDto.Image,
                RegistrationDate = DateTime.Now,
                SuperiorId = userDto.SuperiorId,
                Password = PasswordUtils.HashPassword(userDto.Password),
            };
            _unitOfWork.UserRepository.UpdateAsync(updateUser);
            return await _unitOfWork.CommitAsync() > 0;
        }

        return false;
    }

    public async Task<User?> Login(UserLoginDto userLoginDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserWithRole().FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
        if (user != null && PasswordUtils.VerifyPassword( userLoginDto.Password,user.Password))
        {
            return user;
        }
        return null;
    }
    
}