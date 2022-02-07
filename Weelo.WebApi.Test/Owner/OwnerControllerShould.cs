using AutoFixture.Xunit2;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Weelo.Business.Commands.Owner;
using Weelo.WebApi.Controllers;
using Xunit;

namespace Weelo.WebApi.Test.Owner
{
    public class OwnerControllerShould
    {

        [Theory]
        [AutoData]
        public async Task FindOwner(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OwnerConsultResponse()).Verifiable("Error al consultar el propietario.");
            OwnerController ownerController = new(mock.Object);
            #endregion
            #region Act
            string identificationNumber = "123456789";
            IActionResult result = await ownerController.FindOwner(identificationNumber);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }

        [Theory]
        [AutoData]
        public async Task OwnerRegister(Mock<IMediator> mock)
        {
            #region Arrange
            mock.Setup(m => m.Send(It.IsAny<OwnerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OwnerResponse()).Verifiable("Error al crear el propietario.");
            OwnerController ownerController = new(mock.Object);
            #endregion
            #region Act
            OwnerCommand owner = new()
            {
                Address = "Transversal 15 No. 34-30",
                Birthday = "20-12-1990",
                IdentificationNumber = "1005148790",
                Name = "Juan Cardenas",
                Photo = string.Empty,
            };

            IActionResult result = await ownerController.OwnerRegister(owner);
            #endregion
            #region Assert
            string expected = "{ 'StatusCode': 'OK','innerContext': { 'Result': { 'Success': true,'ReasonCode': '00','ReasonPhrase': 'Transaccion Exitosa.'}}}";
            Assert.Equal(expected, ((ObjectResult)result).Value);
            #endregion
        }
    }
}
