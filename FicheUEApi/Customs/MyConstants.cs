using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FicheUEAuthApi.Customs;

public static class MyConstants
{

    private const string SECRET_KEY = "EverythingAsCodeByWicode";
    public const string ISSUER = "projetcSharp";
    public const string AUDIANCE = "CSharp";
    public const int EXPIRE = 20;
    public static readonly SymmetricSecurityKey SIGN_KEY = new(Encoding.UTF8.GetBytes(SECRET_KEY));

}
