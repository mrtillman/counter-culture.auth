using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Repositories
{
    public class SecureDbContext : IdentityDbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options)
            : base(options) { }
    }
}