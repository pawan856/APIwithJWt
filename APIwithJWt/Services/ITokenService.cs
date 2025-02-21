using System.Security.Claims;


namespace APIwithJWt.Services
{
    public interface ITokenService
    {

       public  string GenerateToken(String UserID, string Usermail, string Role);
    }
}
