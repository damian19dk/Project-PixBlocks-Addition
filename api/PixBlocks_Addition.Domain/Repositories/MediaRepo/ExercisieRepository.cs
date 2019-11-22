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
        private readonly IQueryable<Exercise> _exercises;
        public string ContextLanguage { get; }

        public ExerciseRepository(PixBlocksContext context, ILocalizationService localizer) : base(context)
        {
            _exercises = context.Exercises
                        .Include(c => c.ExerciseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                        .Include(c => c.Tags);
            ContextLanguage = localizer.Language;
        }

        public async Task<Exercise> GetAsync(Guid id)
            => await _exercises.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Exercise>> GetAsync(string name)
            => await _exercises.Where(x => x.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase) 
                                      && x.Title == name).ToListAsync();

        public async Task<IEnumerable<Exercise>> GetAllAsync(int page, int count = 10)
            => await _exercises.Where(x => x.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase))
                     .Skip((page - 1) * count).Take(count).ToListAsync();

        public async Task<IEnumerable<Exercise>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _exercises.Where(e => e.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase) 
                                      && e.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
    }
}
