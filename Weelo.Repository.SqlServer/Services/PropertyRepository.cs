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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly WeeloContext _weeloContext;

        public PropertyRepository(WeeloContext weeloContext) => _weeloContext = weeloContext;

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

        public async Task<Guid> Find(string codeInternal) => await Task.Run(() =>_weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault().IdProperty);

        public async Task<IEnumerable<IProperty>> GetAll(IProperty property) =>
            await Task.Run(() => _weeloContext.Properties.OrderByDescending(
                p => p.Name.Contains(property.Name) ||
                p.Address.Contains(property.Address) ||
                p.CodeInternal.ToString().Contains(property.CodeInternal.ToString()) ||
                p.Price.ToString().Contains(property.Price.ToString())
                ));

        public async Task<IProperty> PriceUpdate(string price, string codeInternal)
        {
            Common.Types.Properties.Property property = _weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault();
            property.Price = float.Parse(price);
            _weeloContext.SaveChanges();
            return await Task.Run(() => _weeloContext.Properties.Where(p => p.CodeInternal == codeInternal).FirstOrDefault());
        }

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
