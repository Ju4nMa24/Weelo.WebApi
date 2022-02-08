using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Specifications;
using Weelo.Abstrations.Types.Properties;
using Weelo.Repository.SqlServer.DataContext;

namespace Weelo.Repository.SqlServer.Services
{
    /// <summary>
    /// Property repository (contains the definition of the actions to perform).
    /// </summary>
    public class PropertyRepository : IPropertyRepository
    {
        private readonly WeeloContext _weeloContext;

        public PropertyRepository(WeeloContext weeloContext) => _weeloContext = weeloContext;
        /// <summary>
        /// It's implemented for the creation of the property.
        /// </summary>
        /// <param name="property"></param>
        public async Task<string> Create(IProperty property)
        {
            try
            {
                _weeloContext.Add(property);
                _weeloContext.SaveChanges();
                return await Task.Run(() => _weeloContext.Properties.Where(p => p.CodeInternal == property.CodeInternal).FirstOrDefault().IdProperty.ToString());
            }
            catch
            {
                return string.Empty;
            }
        }
        public async Task<string> Find(string codeInternal) => await Task.Run(() =>_weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault().IdProperty.ToString());
        /// <summary>
        /// It's implemented for the to list of the property.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IProperty>> GetAll(IProperty property)
        {
            if (property.Price > 0)
            {
                return await Task.Run(() => _weeloContext.Properties.Where(
                                        p => p.Name.Contains(property.Name) &&
                                        p.Address.Contains(property.Address) &&
                                        p.CodeInternal.Contains(property.CodeInternal) &&
                                        p.Price.ToString().Contains(property.Price.ToString())
                                        ).OrderByDescending(p => p.CreationDate).ToList());
            }
            else
            {
                 return await Task.Run(() => _weeloContext.Properties.Where(
                p => p.Name.Contains(property.Name) &&
                p.Address.Contains(property.Address) &&
                p.CodeInternal.Contains(property.CodeInternal)
                ).OrderByDescending(p => p.CreationDate).ToList());
            }
        }

        /// <summary>
        /// It's implemented for the update of the price.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="codeInternal"></param>
        /// <returns></returns>
        public async Task<IProperty> PriceUpdate(string price, string codeInternal)
        {
            Common.Types.Properties.Property property = _weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault();
            property.Price = float.Parse(price);
            _weeloContext.SaveChanges();
            return await Task.Run(() => _weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault());
        }
        /// <summary>
        /// It's implemented for the purchase.
        /// </summary>
        /// <param name="property"></param>
        public async Task<bool> PurchaseRecord(IPropertyTrace propertyTrace, string codeInternal)
        {
            try
            {
                propertyTrace.IdProperty = await Task.Run(() => _weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault().IdProperty);
                _weeloContext.Add(propertyTrace);
                _weeloContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// It's implemented for the update of the property.
        /// </summary>
        /// <param name="property"></param>
        public Guid Update(IProperty property, string codeInternalOrigin)
        {
            Common.Types.Properties.Property propertyChange = _weeloContext.Properties.Where(p => p.CodeInternal == codeInternalOrigin).FirstOrDefault();
            propertyChange.Name = property.Name;
            propertyChange.Address = property.Address;
            propertyChange.Price = property.Price;
            propertyChange.Year = property.Year;
            _weeloContext.SaveChanges();
            return _weeloContext.Properties.Where(p => p.CodeInternal == property.CodeInternal).FirstOrDefault().IdProperty;
        }
    }
}
