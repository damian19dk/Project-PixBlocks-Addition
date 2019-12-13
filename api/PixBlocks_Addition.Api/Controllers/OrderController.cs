using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("courses")]
        public async Task OrderCourses([FromBody]OrderCoursesResource courses)
        {
            await _orderService.OrderCourses(courses.Courses);
        }

        [HttpPost("videos")]
        public async Task OrderVideos([FromBody]OrderVideosResource resource)
        {
            await _orderService.OrderVideos(resource.Videos, resource.CourseId);
        }
    }
}