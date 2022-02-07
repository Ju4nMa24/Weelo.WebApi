using System;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Properties;
using Weelo.Repository.SqlServer.DataContext;

namespace Weelo.Repository.SqlServer.Services
{
    /// <summary>
    /// Property Image repository (contains the definition of the actions to perform).
    /// </summary>
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly WeeloContext _weeloContext;
        public PropertyImageRepository(WeeloContext weeloContext) => _weeloContext = weeloContext;
        /// <summary>
        /// It's implemented for the adding of the image for the property.
        /// </summary>
        /// <param name="propertyImage"></param>

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
