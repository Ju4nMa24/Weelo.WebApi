using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weelo.Abstrations.Specifications;
using Weelo.Abstrations.Types.Properties;

namespace Weelo.Abstrations.Repositories
{
    public interface IPropertyRepository
    {
        /// <summary>
        /// It's implemented for the creation of the property.
        /// </summary>
        /// <param name="property"></param>
        Task<string> Create(IProperty property);
        Task<Guid> Find(string codeInternal);
        /// <summary>
        /// It's implemented for the to list of the property.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<IEnumerable<IProperty>> GetAll(IProperty specification);
        /// <summary>
        /// It's implemented for the update of the property.
        /// </summary>
        /// <param name="property"></param>
        Guid Update(IProperty property, string codeInternalOrigin);
        Task<IProperty> PriceUpdate(string price, string codeInternal);
        Task<bool> PurchaseRecord(IPropertyTrace propertyTrace, string codeInternal);
    }
}
