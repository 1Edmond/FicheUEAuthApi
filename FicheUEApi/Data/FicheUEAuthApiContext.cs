using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FicheUEAuthApi.Models;

namespace FicheUEAuthApi.Data
{
    public class FicheUEAuthApiContext : DbContext
    {
        public FicheUEAuthApiContext (DbContextOptions<FicheUEAuthApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
    }
}
