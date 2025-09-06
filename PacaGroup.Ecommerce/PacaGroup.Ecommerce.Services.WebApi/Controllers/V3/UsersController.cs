using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;
using PacaGroup.Ecommerce.Services.WebApi.Helpers;
using PacaGroup.Ecommerce.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PacaGroup.Ecommerce.Services.WebApi.Controllers.V3
{
    [Authorize]
    //[Route("api/[controller]")] // QueryStringApiVersionReader o HeaderApiVersionReader
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class UsersController : ControllerBase
    {
        private readonly AppSettingJWT _appSettingJWT;
        private readonly IMediator _mediator;

        public UsersController(IOptions<AppSettingJWT> appSettingsJWT, IMediator mediator)
        {
            _appSettingJWT = appSettingsJWT.Value;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] CreateUserTokenCommand command)
        {
            var response = await _mediator.Send(command);
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

        private string BuildToken(Response<UserDto> usersDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettingJWT.Key);
            //var claims = new Dictionary<string, object>
            //{
            //    { "userid", usersDto.Data.UserId.ToString() },
            //    { "username", usersDto.Data.UserName }
            //};

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettingJWT.Issuer,
                Audience = _appSettingJWT.Audience
                //Claims = claims
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
