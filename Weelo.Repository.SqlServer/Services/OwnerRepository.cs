using System;
using System.Linq;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Owners;
using Weelo.Common.Types.Owners;
using Weelo.Repository.SqlServer.DataContext;

namespace Weelo.Repository.SqlServer.Services
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly WeeloContext _weeloContext;
        public OwnerRepository(WeeloContext weeloContext) => _weeloContext = weeloContext;
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
        public async Task<IOwner> Find(string identificationNumber) => await Task.Run(() => _weeloContext.Owners.Where(o => o.IdentificationNumber == identificationNumber).FirstOrDefault());
        public bool GetOne(string identificationNumber) => (!string.IsNullOrEmpty(_weeloContext.Owners.Where(o => o.IdentificationNumber == identificationNumber).FirstOrDefault()?.IdentificationNumber));
    }
}
