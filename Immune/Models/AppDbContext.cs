using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Immune.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }

        public DbSet<VaccMembers> vaccMembers { get; set; }
    }
}
