using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CounterCulture.Models;

namespace CounterCulture.Repositories
{
    public class SecureDbContext : DbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options)
            : base(options) { }
        public DbSet<UserForm> Users { get; set; }

    }
}