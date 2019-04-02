using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Repositories;

namespace PixBlocks_Addition.Domain
{
    public class PixBlocksContext:DbContext, IEntities
    {
        public PixBlocksContext(DbContextOptions<PixBlocksContext> options) : base(options)
        {
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
    }
}
