﻿using System;
using System.Collections.Generic;
using System.Text;
using PixBlocks_Addition.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IDBEntities
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }


        int SaveChanges();
    }
}