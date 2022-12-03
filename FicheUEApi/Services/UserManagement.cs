using FicheUEAuthApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FicheUEAuthApi.Services;

public class UserManagement : IUserManagement
{
    private readonly FicheUEAuthApiContext _context;
    public UserManagement(FicheUEAuthApiContext context)
    {
        _context = context;
    }

    public async Task<User> AddUser(User user)
    {
       var userAdded = _context.Add(user);
        await _context.SaveChangesAsync();
        return userAdded.Entity;
    }

    public async Task<bool> DeleteUser(Guid Id)
    {
        try
        {
            var user = _context.Users.Find(Id);
            if(user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<IEnumerable<User>> GetAllUsers()
      => await _context.Users.ToListAsync();

    public Task<User?> GetUserByEmail(string Email)
    {
            var user = _context.Users.FirstOrDefault(x => x.Email == Email);
            return Task.FromResult(user);  
    }

    public Task<User?> GetUserById(Guid Id)
    {
        var user = _context.Users.Find(Id);
        return Task.FromResult(user);
    }


    public async Task<User> UpdateUser(User user)
    {

        var userAdded = _context.Update(user);
        await _context.SaveChangesAsync();
        return userAdded.Entity;
    }
}
