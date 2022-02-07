using System.Threading.Tasks;
using Weelo.Abstrations.Types.Owners;

namespace Weelo.Abstrations.Repositories
{
    public interface IOwnerRepository
    {
        /// <summary>
        /// It's implemented for the creation of the owner.
        /// </summary>
        /// <param name="property"></param>
        Task<bool> Create(IOwner owner);
        Task<IOwner> Find(string identificationNumber);
        bool GetOne(string identificationNumber);
    }
}
