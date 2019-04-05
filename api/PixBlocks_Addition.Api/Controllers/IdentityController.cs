using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Api.ResourceModels;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ICancellationTokenService _cancellationService;

        public IdentityController(IIdentityService identityService, ICancellationTokenService cancellationTokenService)
        {
            _identityService = identityService;
            _cancellationService = cancellationTokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<JwtDto> Login([FromBody]LoginModel login)
        {
            return await _identityService.Login(login.Login, login.Password);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<JwtDto> RefreshToken(string token)
        {
            return await _identityService.RefreshAccessToken(token);
        }

        [HttpPost("revoke")]
        public async Task RevokeToken(string token)
        {
            await _identityService.RevokeRefreshToken(token);
        }

        [HttpPost("cancel")]
        public async Task CancelToken()
        {
            await _cancellationService.DeactivateCurrentAsync();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task Register([FromBody] RegisterModel register)
        {
            await _identityService.Register(register.Id, register.Login, register.Password, register.E_mail, register.RoleId);
        }
    }
}