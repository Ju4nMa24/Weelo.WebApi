using AutoFixture.Xunit2;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weelo.Business.Commands.Property;
using Weelo.WebApi.Controllers;
using Xunit;

namespace Weelo.WebApi.Test.Properties
{
    /// <summary>
    /// Unit tests for the properties api.
    /// </summary>
    public class PropertiesControllerShould
    {
        /// <summary>
        /// Unit tests of get all properties.
        /// </summary>
        /// <param name="mock"></param>
        /// <returns></returns>
        [Theory]
        [AutoData]
        public async Task GetAllProperties(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<PropertyFiltersCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PropertyFiltersResponse()).Verifiable("Error al consultar las propiedades.");
            PropertiesController propertiesController = new(mock.Object);
            #endregion
            #region Act
            PropertyFiltersCommand propertyFilters = new();
            IActionResult result = await propertiesController.GetAllProperties(propertyFilters);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
        /// <summary>
        /// Unit tests of property register.
        /// </summary>
        /// <param name="mock"></param>
        /// <returns></returns>
        [Theory]
        [AutoData]
        public async Task PropertyRegister(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<PropertyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PropertyResponse()).Verifiable("Error al crear la propiedad.");
            PropertiesController propertiesController = new(mock.Object);
            #endregion
            #region Act
            PropertyCommand propertyFilters = new()
            {
                Address = "Cra. 15 No. 10-39",
                CodeInternal = "111",
                IdentificationNumner = "1547889965",
                IdOwner = Guid.Parse("020E3BE0-634A-476E-DE25-08D9E858D6EA"),
                Name = "Edificio Luxury",
                Price = 35250,
                Year = "2020"
            };
            IActionResult result = await propertiesController.PropertyRegister(propertyFilters);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
        /// <summary>
        /// Unit tests of purchase record.
        /// </summary>
        /// <param name="mock"></param>
        /// <returns></returns>
        [Theory]
        [AutoData]
        public async Task PurchaseRecord(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<PropertyPurchaseCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PropertyPurchaseResponse()).Verifiable("Error al crear la compra.");
            PropertiesController propertiesController = new(mock.Object);
            #endregion
            #region Act
            PropertyPurchaseCommand propertyFilters = new()
            {
                CodeInternal = "111",
                DateSale = DateTime.UtcNow,
                Name = "Juan",
                Tax = "19",
                Value = 36500
            };
            IActionResult result = await propertiesController.PurchaseRecord(propertyFilters);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
        /// <summary>
        /// Unit tests of add image.
        /// </summary>
        /// <param name="mock"></param>
        /// <returns></returns>
        [Theory]
        [AutoData]
        public async Task AddImage(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<PropertyAddImageCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PropertyAddImageResponse()).Verifiable("Error al crear la imagen.");
            PropertiesController propertiesController = new(mock.Object);
            #endregion
            #region Act
            PropertyAddImageCommand propertyFilters = new()
            {
                CodeInternal = "111",
                Files = new List<Files>()
            };
            IActionResult result = await propertiesController.AddImage(propertyFilters);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
        /// <summary>
        /// Unit tests of price update.
        /// </summary>
        /// <param name="mock"></param>
        /// <returns></returns>
        [Theory]
        [InlineAutoData]
        public async Task PriceUpdate(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PricePropertyResponse()).Verifiable("Error al actualizar el precio.");
            PropertiesController propertiesController = new(mock.Object);
            #endregion
            #region Act
            string codeInternal = "111";
            string price = "552000";
            IActionResult result = await propertiesController.PriceUpdate(codeInternal,price);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
        /// <summary>
        /// Unit tests of update.
        /// </summary>
        /// <param name="mock"></param>
        /// <returns></returns>
        [Theory]
        [InlineAutoData]
        public async Task Update(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<PropertyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PropertyResponse()).Verifiable("Error al actualizar el precio.");
            PropertiesController propertiesController = new(mock.Object);
            #endregion
            #region Act
            PropertyCommand property = new()
            {
                Address = "Cra. 15 No. 10-40",
                CodeInternalOrigin = "111",
                Name = "Edificio Luxury",
                Price = 35250,
                Year = "2020"
            };
            IActionResult result = await propertiesController.Update("111",property);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
    }
}
