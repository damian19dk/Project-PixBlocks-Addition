using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("passwordChange")]
        public async Task ChangePassword(string login, string newPassword, string oldPassword)
        {
            await _userService.ChangePassword(login, newPassword, oldPassword);
        }

        [HttpPut("emailChange")]
        public async Task ChangeEmail(string login, string email)
        {
            await _userService.ChangeEmail(login, email);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("setPremium")]
        public async Task SetPremium(string login)
        {
            await _userService.SetPremium(login);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("takePremium")]
        public async Task TakePremium(string login)
        {
            await _userService.TakePremium(login);
        }
    }
}
