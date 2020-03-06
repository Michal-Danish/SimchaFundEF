using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simchas.Data
{
    public class SimchaContext : DbContext
    {
        private string _connectionString;

        public SimchaContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Simcha> Simchas { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


        #region many to many
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contribution>()
                .HasKey(sc => new { sc.SimchaId, sc.ContributorId });
            
            modelBuilder.Entity<Contribution>()
                .HasOne(s => s.Simcha)
                .WithMany(c => c.Contributions)
                .HasForeignKey(s => s.SimchaId);
            
            modelBuilder.Entity<Contribution>()
                .HasOne(c => c.Contributor)
                .WithMany(s => s.Contributions)
                .HasForeignKey(c => c.ContributorId);
        }
        #endregion
    }
}
