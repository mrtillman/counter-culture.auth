using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CounterCulture.Models;

namespace CounterCulture.Repositories
{
    public class SecureDbContext : IdentityDbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options)
            : base(options) { }
    }
}