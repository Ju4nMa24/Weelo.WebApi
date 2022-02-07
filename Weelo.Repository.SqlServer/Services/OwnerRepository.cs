using System;
using System.Linq;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Owners;
using Weelo.Common.Types.Owners;
using Weelo.Repository.SqlServer.DataContext;

namespace Weelo.Repository.SqlServer.Services
{
    /// <summary>
    /// Owner repository (contains the definition of the actions to perform).
    /// </summary>
    public class OwnerRepository : IOwnerRepository
    {
        private readonly WeeloContext _weeloContext;
        public OwnerRepository(WeeloContext weeloContext) => _weeloContext = weeloContext;
        /// <summary>
        /// It's implemented for the creation of the owner.
        /// </summary>
        /// <param name="property"></param>

        public async Task<bool> Create(IOwner owner)
        {
            try
            {
                await Task.Run(() => _weeloContext.Add(owner));
                _weeloContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// It's implemented for the find of the owner.
        /// </summary>
        /// <param name="property"></param>
        public async Task<IOwner> Find(string identificationNumber) => await Task.Run(() => _weeloContext.Owners.Where(o => o.IdentificationNumber == identificationNumber).FirstOrDefault());
        /// <summary>
        /// It's implemented for the get of the owner.
        /// </summary>
        /// <param name="property"></param>
        public bool GetOne(string identificationNumber) => (!string.IsNullOrEmpty(_weeloContext.Owners.Where(o => o.IdentificationNumber == identificationNumber).FirstOrDefault()?.IdentificationNumber));
    }
}
