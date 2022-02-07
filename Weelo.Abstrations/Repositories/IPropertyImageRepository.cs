using Weelo.Abstrations.Types.Properties;

namespace Weelo.Abstrations.Repositories
{
    public interface IPropertyImageRepository
    {
        /// <summary>
        /// It's implemented for the adding of the image for the property.
        /// </summary>
        /// <param name="propertyImage"></param>
        void Create(IPropertyImage propertyImage);
    }
}
