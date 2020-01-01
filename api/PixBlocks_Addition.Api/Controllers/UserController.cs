using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
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
        private readonly IUserCourseHistoryService _userCourseHistoryService;

        public UserController(IUserService userService, IUserCourseHistoryService userCourseHistoryService)
        {
            _userService = userService;
            _userCourseHistoryService = userCourseHistoryService;
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

        [HttpDelete("deleteUserHistoryId")]
        public async Task RemoveUserhistory(Guid userId)
        {
            await _userCourseHistoryService.RemoveAsync(userId);
        }

        [HttpDelete("deleteUserHistory")]
        public async Task RemoveUserhistory(string login)
        {
            await _userCourseHistoryService.RemoveAsync(login);
        }

        [HttpPost("addCourseToHistoryId")]
        public async Task AddCourseToHistory(Guid userId, Guid courseId)
        {
            await _userCourseHistoryService.AddHistoryAsync(userId, courseId);
        }

        [HttpPost("addCourseToHistory")]
        public async Task AddCourseToHistory(string login, Guid courseId)
        {
            await _userCourseHistoryService.AddHistoryAsync(login, courseId);
        }

        [HttpPost("getUserHistoryId")]
        public async Task<IEnumerable<CourseDto>> GetUserHistory(Guid userId)
        {
            return await _userCourseHistoryService.GetAllAsync(userId);
        }

        [HttpPost("getUserHistory")]
        public async Task<IEnumerable<CourseDto>> GetUserHistory(string login)
        {
            return await _userCourseHistoryService.GetAllAsync(login);
        }
    }
}
