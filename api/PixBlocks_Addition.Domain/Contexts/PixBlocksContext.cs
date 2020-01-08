using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Settings;
using System;
using System.Linq;

namespace PixBlocks_Addition.Domain.Contexts
{
    public class PixBlocksContext : DbContext, ICloneable
    {
        private readonly IOptions<SqlSettings> _settings;

        public PixBlocksContext(DbContextOptions<PixBlocksContext> options, IOptions<SqlSettings> sqlSettings) : base(options)
        {
            _settings = sqlSettings;
        }

        public DbSet<CustomResource> Resources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<VideoHistory> VideoHistories { get; set; }
        public DbSet<VideoHistoryHelper> VideoHistoryHelpers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<Video>().Property(e=>e.Resources)
                                        .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<Course>().ToTable("Courses").HasMany(c => c.CourseVideos);
            modelBuilder.Entity<Course>().Property(e => e.Resources)
                                         .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<History>().ToTable("History");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<VideoHistory>().ToTable("VideoHistory");
            modelBuilder.Entity<VideoHistoryHelper>().ToTable("VideoHistoryHelper");

            var videoFK = modelBuilder.Model.FindEntityType(typeof(Video)).GetForeignKeys();
            foreach (var fk in videoFK)
                fk.DeleteBehavior = DeleteBehavior.Cascade;


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_settings.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase("PixBlocks");
                return;
            }
            optionsBuilder.UseSqlServer(_settings.Value.ConnectionString,
                options =>
                {
                    options.MigrationsAssembly("PixBlocks_Addition.Api");
                });
        }

        public object Clone()
        {
            return new PixBlocksContext(new DbContextOptions<PixBlocksContext>(), _settings);
        }
    }
}
