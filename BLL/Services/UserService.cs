using Common.Resources;
using Infrastructure.Interfaces.IRepository;
using Infrastructure.Interfaces.IServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AssignAdminRoleToUser(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new InvalidOperationException(string.Format(ErrorMessages.UserNotFound, userId));
        if (user.Role == "User")
        {
            user.Role = "Admin";
            await _userRepository.SaveChangesAsync();
        }
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _userRepository.GetAll().ProjectToType<UserDto>().ToListAsync();
    }
}
