﻿using back_end.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace back_end.Infastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

}
