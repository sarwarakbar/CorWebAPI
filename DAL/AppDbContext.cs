using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Laureate>()
                .HasOne(p => p.Prize)
                .WithMany(b => b.Laureates);
        }

        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Laureate> Laureates { get; set; }
    }
}
