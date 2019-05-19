using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public class ExercisieRepository : GenericRepository<Exercise>, IExerciseRepository
    {
        private readonly PixBlocksContext _entities;

        public ExercisieRepository(PixBlocksContext context): base(context)
        {
            _entities = context;
        }

        public async Task<Exercise> GetAsync(Guid id)
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Exercise> GetAsync(string name)
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags).SingleOrDefaultAsync(x => x.Title == name);

        public async Task<IEnumerable<Exercise>> GetAllAsync()
            => await _entities.Exercises.Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags).ToListAsync();
    }
}
