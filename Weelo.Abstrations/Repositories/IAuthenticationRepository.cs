using System.Threading.Tasks;
using Weelo.Abstrations.Types.Authentication;

namespace Weelo.Abstrations.Repositories
{
    /// <summary>
    /// Authentication repository (contains the definition of the actions to perform).
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// JWT Create
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        Task<string> Generate(IAuth auth);
    }
}
