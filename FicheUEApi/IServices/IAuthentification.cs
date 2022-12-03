namespace FicheUEAuthApi.IServices;

public interface IAuthentification
{
    Task<string> AuthenticateUser(string email, string password);
}
