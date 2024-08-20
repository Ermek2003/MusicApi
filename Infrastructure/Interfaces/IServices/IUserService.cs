using Models.DTOs;

namespace Infrastructure.Interfaces.IServices;

public interface IUserService
{
    Task AssignAdminRoleToUser(int userId);
    Task<List<UserDto>> GetAllUsers();
}
