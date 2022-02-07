using System.Threading.Tasks;
using Weelo.Abstrations.Types.Properties;

namespace Weelo.Abstrations.Repositories
{
    public interface IPropertyTraceRepository
    {
        /// <summary>
        /// It's implemented for the creation of the property trace.
        /// </summary>
        /// <param name="property"></param>
        void Create(IProperty property);
        /// <summary>
        /// It's implemented for the update of the price of the property.
        /// </summary>
        /// <param name="propertyTrace"></param>
        /// <returns></returns>
        public Task<string> PriceUpdate(IPropertyTrace propertyTrace);
    }
}
