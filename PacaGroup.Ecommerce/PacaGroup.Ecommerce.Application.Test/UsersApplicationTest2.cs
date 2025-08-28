using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Application.Main;
using PacaGroup.Ecommerce.Application.UseCases;
using PacaGroup.Ecommerce.Application.Validator;

namespace PacaGroup.Ecommerce.Application.IntegrationTest
{
    [TestClass]
    public sealed class UsersApplicationTest2
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            // Registrar IConfiguration
            services.AddSingleton<IConfiguration>(_configuration);

            // Registrar validadores de FluentValidation
            services.AddValidatorsFromAssemblyContaining<UsersDtoValidator>();

            // Registrar tus servicios de la aplicación
            services.AddApplicationServices();

            _scopeFactory = services
                .AddLogging()
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            using var scope = _scopeFactory.CreateScope();

            // Ejemplo: obtener un servicio real desde DI
            var usersApp = scope.ServiceProvider.GetRequiredService<IUsersApplication>();

            var result = usersApp.Authenticate("", "");

            Assert.AreEqual("Errores de Validación", result.Message);
        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion2()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersApplication>();

            // Arrange
            var userName = string.Empty;
            var password = string.Empty;
            var expected = "Errores de Validación";

            // Act            
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito2()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersApplication>();

            // Arrange
            var userName = "ALEX";
            var password = "123456";
            var expected = "Autenticación Exitosa!!!";

            // Act
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste2()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersApplication>();

            // Arrange
            var userName = "ALEX";
            var password = "123456899";
            var expected = "Usuario no existe";

            // Act
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private static IUsersApplication GetUsersApp() =>
            _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUsersApplication>();


        [DataTestMethod]
        [DataRow("", "", "Errores de Validación")]
        [DataRow("ALEX", "123456", "Autenticación Exitosa!!!")]
        [DataRow("ALEX", "123456899", "Usuario no existe")]
        public void Authenticate_TestCases(string user, string pass, string expected)
        {
            var context = GetUsersApp();
            var result = context.Authenticate(user, pass);
            Assert.AreEqual(expected, result.Message);
        }

    }
}
