namespace FicheUEAuthApi.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Passwd { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    

    public User(Guid id, string email, string passwd, string role)
    {
        Id = id;
        Email = email;
        Passwd = passwd;
        Role = role;
    }
}
