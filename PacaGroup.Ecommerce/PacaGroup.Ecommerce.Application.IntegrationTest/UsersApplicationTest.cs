using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Main;
using PacaGroup.Ecommerce.Application.Validator;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Transversal.Common;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace PacaGroup.Ecommerce.Application.IntegrationTest
{
    [TestClass]
    public class UsersApplicationTest
    {
        private Mock<IMapper> _mapperMock = null!;
        private Mock<IUsersDomain> _usersDomainMock = null!;
        //private Mock<UsersDtoValidator> _validatorMock = null!;
        private Mock<IValidator<UsersDto>> _validatorMock;
        private UsersApplication _usersApplication = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _usersDomainMock = new Mock<IUsersDomain>();
            _validatorMock = new Mock<IValidator<UsersDto>>();

            _usersApplication = new UsersApplication(
                _mapperMock.Object,
                _usersDomainMock.Object,
                _validatorMock.Object
            );
        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            // Arrange: Simular validación inválida
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<UsersDto>()))
                .Returns(new ValidationResult(new[] { new ValidationFailure("UserName", "Requerido") }));

            // Act
            var result = _usersApplication.Authenticate("", "");

            // Assert
            Assert.AreEqual("Errores de Validación", result.Message);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito()
        {
            // Arrange: Validación correcta
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<UsersDto>()))
                .Returns(new ValidationResult());

            var domainUser = new PacaGroup.Ecommerce.Domain.Entity.Users
            {
                userName = "admin",
                Password = "123"
            };

            _usersDomainMock
                .Setup(d => d.Authenticate("admin", "123"))
                .Returns(domainUser);

            _mapperMock
                .Setup(m => m.Map<UsersDto>(domainUser))
                .Returns(new UsersDto { UserName = "admin", Password = "123" });

            // Act
            var result = _usersApplication.Authenticate("admin", "123");

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Autenticación Exitosa!!!", result.Message);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste()
        {
            // Arrange: Validación correcta
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<UsersDto>()))
                .Returns(new ValidationResult());

            _usersDomainMock
                .Setup(d => d.Authenticate("invalido", "xxx"))
                .Throws<InvalidOperationException>();

            // Act
            var result = _usersApplication.Authenticate("invalido", "xxx");

            // Assert
            Assert.IsTrue(result.IsSuccess); // tu código lo marca como true
            Assert.AreEqual("Usuario no existe", result.Message);
        }
    }
}
