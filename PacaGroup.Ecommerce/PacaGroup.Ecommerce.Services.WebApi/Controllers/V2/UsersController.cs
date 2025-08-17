using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Services.WebApi.Helpers;
using PacaGroup.Ecommerce.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PacaGroup.Ecommerce.Services.WebApi.Controllers.V2
{
    [Authorize]
    //[Route("api/[controller]")] // QueryStringApiVersionReader o HeaderApiVersionReader
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _usersApplication;
        private readonly AppSettingCors _appSettingCors;
        private readonly AppSettingJWT _appSettingJWT;

        public UsersController(IUsersApplication authApplication, IOptions<AppSettingCors> appSettingsCors, IOptions<AppSettingJWT> appSettingsJWT)
        {
            _usersApplication = authApplication;
            _appSettingCors = appSettingsCors.Value;
            _appSettingJWT = appSettingsJWT.Value;
        }

        /// <summary>
        /// Autentica a un usuario en el sistema y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite el acceso anónimo y recibe un objeto <see cref="UsersDto"/> 
        /// con el nombre de usuario y contraseña.  
        /// Si la autenticación es exitosa, se devuelve un objeto de respuesta que incluye 
        /// los datos del usuario y un token JWT.
        /// </remarks>
        /// <param name="usersDto">
        /// Objeto que contiene el nombre de usuario (<c>UserName</c>) y la contraseña (<c>Password</c>).
        /// Ejemplo: Usuario: <c>slopezb</c>, Contraseña: <c>123456</c>.
        /// </param>
        [AllowAnonymous]
        [HttpPost]
        // GET: api/<UsersController>
        public IActionResult Authenticate([FromBody] UsersDto usersDto)
        {
            var response = _usersApplication.Authenticate(usersDto.UserName, usersDto.Password);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                    return NotFound(response);
            }

            return BadRequest(response);
        }

        //private string BuildToken(Response<UsersDto> usersDto)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_appSettingJWT.Key);

        //    // Claims principales del usuario (puedes añadir más si lo deseas)
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, usersDto.Data.UserId.ToString()),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, usersDto.Data.UserName),
        //        new Claim(ClaimTypes.NameIdentifier, usersDto.Data.UserId.ToString()),
        //        new Claim(ClaimTypes.Name, usersDto.Data.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddMinutes(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        Issuer = _appSettingJWT.Issuer,
        //        Audience = _appSettingJWT.Audience
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        private string BuildToken(Response<UsersDto> usersDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettingJWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettingJWT.Issuer,
                Audience = _appSettingJWT.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
