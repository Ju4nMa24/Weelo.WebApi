using Weelo.Abstrations.Types.Properties;

namespace Weelo.Abstrations.Repositories
{
    /// <summary>
    /// Property Image repository (contains the definition of the actions to perform).
    /// </summary>
    public interface IPropertyImageRepository
    {
        /// <summary>
        /// It's implemented for the adding of the image for the property.
        /// </summary>
        /// <param name="propertyImage"></param>
        void Create(IPropertyImage propertyImage);
    }
}
