﻿using System;
using System.Collections.Generic;
using System.Text;
using PixBlocks_Addition.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IEntities
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Video> Videos { get; set; }
        DbSet<History> Histories { get; set; }
        DbSet<Category> Categories { get; set; }

        int SaveChanges();
    }
}