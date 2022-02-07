using System.Threading.Tasks;
using Weelo.Abstrations.Types.Authentication;

namespace Weelo.Abstrations.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string> Generate(IAuth auth);
    }
}
