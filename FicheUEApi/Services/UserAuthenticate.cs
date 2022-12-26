using FicheUEAuthApi.Data;

namespace FicheUEAuthApi.Services;

public class UserAuthenticate : IAuthentification
{
    private readonly FicheUEAuthApiContext _context;
    public UserAuthenticate(FicheUEAuthApiContext context)
    {
        _context = context;
    }

    public Task<string> AuthenticateUser(string email, string password)
    {
        var token = "";
        if(_context.Users.Any(x => x.Email == email && x.Passwd == password))
            token = GetToken(_context.Users.
                Where(x => x.Email == email && x.Passwd == password).First().Role);
        return Task.FromResult(token);
    }

    private string GetToken(string role)
    {
        var credentials = new SigningCredentials(MyConstants.SIGN_KEY, SecurityAlgorithms.HmacSha256);
        if (role == "Admin")
        {
            var section = new JwtSecurityToken(
                issuer: MyConstants.ISSUER,
                audience: MyConstants.AUDIANCE,
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Role,"Admin")
                },
                expires: DateTime.UtcNow.AddMinutes(MyConstants.EXPIRE),
                signingCredentials: credentials
                );
            var handler = new JwtSecurityTokenHandler();

            var stringToken = handler.WriteToken(section);
            return stringToken;

        }
        else
        {
            var section = new JwtSecurityToken(
                issuer: MyConstants.ISSUER,
                audience: MyConstants.AUDIANCE,
                expires: DateTime.UtcNow.AddMinutes(MyConstants.EXPIRE),
                signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();

            var stringToken = handler.WriteToken(section);
            return stringToken;
        }
    }



}
