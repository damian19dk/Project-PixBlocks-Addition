using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IOrderService
    {
        Task OrderCourses(IEnumerable<Guid> courses);
        Task OrderVideos(IEnumerable<Guid> videos, Guid courseId);
    }
}
