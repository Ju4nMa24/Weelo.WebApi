using System;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Properties;
using Weelo.Repository.SqlServer.DataContext;

namespace Weelo.Repository.SqlServer.Services
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly WeeloContext _weeloContext;
        public PropertyImageRepository(WeeloContext weeloContext) => _weeloContext = weeloContext;
        public void Create(IPropertyImage propertyImage)
        {
            try
            {
                _weeloContext.Add(propertyImage);
                _weeloContext.SaveChanges();
            }
            catch
            {
            }
        }
    }
}
