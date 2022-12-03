namespace FicheUEAuthApi.IServices;

public interface IUserManagement
{
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);

    Task<bool> DeleteUser(Guid Id);

    Task<User?> GetUserByEmail(string Email);

    Task<User?> GetUserById(Guid Id);

    Task<IEnumerable<User>> GetAllUsers();
}
