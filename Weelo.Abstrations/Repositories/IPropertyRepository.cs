using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weelo.Abstrations.Specifications;
using Weelo.Abstrations.Types.Properties;

namespace Weelo.Abstrations.Repositories
{
    /// <summary>
    /// Property repository (contains the definition of the actions to perform).
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// It's implemented for the creation of the property.
        /// </summary>
        /// <param name="property"></param>
        Task<string> Create(IProperty property);
        Task<string> Find(string codeInternal);
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
        /// <summary>
        /// It's implemented for the update of the price.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="codeInternal"></param>
        /// <returns></returns>
        Task<IProperty> PriceUpdate(string price, string codeInternal);
        /// <summary>
        /// It's implemented for the purchase.
        /// </summary>
        /// <param name="property"></param>
        Task<bool> PurchaseRecord(IPropertyTrace propertyTrace, string codeInternal);
    }
}
