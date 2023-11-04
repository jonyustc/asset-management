using Core.Model;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user,List<string> roles);
    }
}