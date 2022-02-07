using System.Threading.Tasks;
using Weelo.Abstrations.Types.Owners;

namespace Weelo.Abstrations.Repositories
{
    /// <summary>
    /// Owner repository (contains the definition of the actions to perform).
    /// </summary>
    public interface IOwnerRepository
    {
        /// <summary>
        /// It's implemented for the creation of the owner.
        /// </summary>
        /// <param name="property"></param>
        Task<bool> Create(IOwner owner);
        /// <summary>
        /// It's implemented for the find of the owner.
        /// </summary>
        /// <param name="property"></param>
        Task<IOwner> Find(string identificationNumber);
        /// <summary>
        /// It's implemented for the get of the owner.
        /// </summary>
        /// <param name="property"></param>
        bool GetOne(string identificationNumber);
    }
}
