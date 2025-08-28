using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Application.UseCases;
using PacaGroup.Ecommerce.Application.UseCases.Users;

namespace PacaGroup.Ecommerce.Application.IntegrationTest
{
    [TestClass]
    public class UsersApplicationTest
    {
        private Mock<IMapper> _mapperMock = null!;
        private Mock<IUnitOfWork> _unitOfWorkMock = null!;
        private Mock<IValidator<UserDto>> _validatorMock = null!;
        private UsersApplication _usersApplication = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UserDto>>();

            _usersApplication = new UsersApplication(
                _mapperMock.Object,
                _unitOfWorkMock.Object,   // 👈 Aquí va IUnitOfWork
                _validatorMock.Object
            );
        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<UserDto>()))
                .Returns(new ValidationResult(new[] { new ValidationFailure("UserName", "Requerido") }));

            var result = _usersApplication.Authenticate("", "");

            Assert.AreEqual("Errores de Validación", result.Message);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito()
        {
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<UserDto>()))
                .Returns(new ValidationResult());

            var domainUser = new PacaGroup.Ecommerce.Domain.Entities.User
            {
                userName = "admin",
                Password = "123"
            };

            // Simular el repo dentro de UnitOfWork
            _unitOfWorkMock
                .Setup(u => u.Users.Authenticate("admin", "123"))
                .Returns(domainUser);

            _mapperMock
                .Setup(m => m.Map<UserDto>(domainUser))
                .Returns(new UserDto { UserName = "admin", Password = "123" });

            var result = _usersApplication.Authenticate("admin", "123");

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Autenticación Exitosa!!!", result.Message);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste()
        {
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<UserDto>()))
                .Returns(new ValidationResult());

            _unitOfWorkMock
                .Setup(u => u.Users.Authenticate("invalido", "xxx"))
                .Throws<InvalidOperationException>();

            var result = _usersApplication.Authenticate("invalido", "xxx");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Usuario no existe", result.Message);
        }
    }
}