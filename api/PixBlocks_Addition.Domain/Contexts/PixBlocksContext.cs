using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionString,
                options=> 
                {
                    options.MigrationsAssembly("PixBlocks_Addition.Api");
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(x => x.UserId);
        }
    }
}
