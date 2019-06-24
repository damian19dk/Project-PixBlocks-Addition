using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
    {
        private readonly PixBlocksContext _entities;

        public ExerciseRepository(PixBlocksContext context): base(context)
        {
            _entities = context;
        }

        public async Task<Exercise> GetAsync(Guid id)
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Exercise>> GetAsync(string name)
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags)
                     .Where(x => x.Title == name).ToListAsync();

        public async Task<IEnumerable<Exercise>> GetAllAsync(int page, int count = 10)
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                    .Include(c => c.Tags).Skip((page - 1) * count).Take(count).ToListAsync();

        public async Task<IEnumerable<Exercise>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags)
                     .Where(c => c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
    }
}
