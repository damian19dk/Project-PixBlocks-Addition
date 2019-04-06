using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Settings;

namespace PixBlocks_Addition.Domain.Contexts
{
    public class PixBlocksContext : DbContext
    {
        private readonly SqlSettings _settings;

        public PixBlocksContext(DbContextOptions<PixBlocksContext> options, IOptions<SqlSettings> sqlSettings) : base(options)
        {
            _settings = sqlSettings.Value;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<History>().ToTable("History");
            modelBuilder.Entity<Role>().ToTable("Role");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionString,
                options=> 
                {
                    options.MigrationsAssembly("PixBlocks_Addition.Api");
                });
        }
    }
}
