using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Settings;
using System.Linq;

namespace PixBlocks_Addition.Domain.Contexts
{
    public class PixBlocksContext : DbContext
    {
        private readonly SqlSettings _settings;

        public PixBlocksContext(DbContextOptions<PixBlocksContext> options, IOptions<SqlSettings> sqlSettings) : base(options)
        {
            _settings = sqlSettings.Value;
        }

        public DbSet<CustomImage> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<Tag>().ToTable("Tags");
            var course = modelBuilder.Entity<Course>();
            course.ToTable("Courses").HasMany(c => c.Lessons).WithOne(l => l.Course);
            course.HasMany(c => c.CourseVideos);
            var lesson = modelBuilder.Entity<Lesson>();
            lesson.ToTable("Lessons").HasMany(l => l.Exercises).WithOne(e => e.Lesson);
            lesson.HasMany(l => l.LessonVideos);
            modelBuilder.Entity<Exercise>().ToTable("Exercises").HasMany(e => e.ExerciseVideos);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<History>().ToTable("History");
            modelBuilder.Entity<Role>().ToTable("Role");

            var videoFK = modelBuilder.Model.FindEntityType(typeof(Video)).GetForeignKeys();
            foreach (var fk in videoFK)
                fk.DeleteBehavior = DeleteBehavior.Cascade;


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionString,
                options =>
                {
                    options.MigrationsAssembly("PixBlocks_Addition.Api");
                });
        }
    }
}
