using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    public class SecureDbContext : IdentityDbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options)
            : base(options) { }
    }
}