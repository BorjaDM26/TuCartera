using System;
using Microsoft.EntityFrameworkCore;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.DBModel.Contexts.StoreProcedures;

namespace TuCartera.DBModel.Contexts
{
    public class TuCarteraContext : DbContext
    {
        #region Initialization

        private string Connection;

        public TuCarteraContext(string connectionString) {
            this.Connection = connectionString;
        }

        public TuCarteraContext(DbContextOptions<TuCarteraContext> options)
            : base(options) {
        }

        #endregion

        #region Entities

        public DbSet<User> User { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Ticker> Ticker { get; set; }
        public DbSet<PortfolioTicker> PortfolioTicker { get; set; }
        public DbSet<Transactiontype> Transactiontype { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        #endregion

        #region Stored procedures



        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Entities

            modelBuilder.Entity<User>().HasKey(u => u.id);
            modelBuilder.Entity<Currency>().HasKey(d => d.id);
            modelBuilder.Entity<Portfolio>().HasKey(c => c.id);
            modelBuilder.Entity<Ticker>().HasKey(t => t.id);
            modelBuilder.Entity<PortfolioTicker>().HasKey(tc => new { tc.portfolio_id, tc.ticker_id});
            modelBuilder.Entity<Transactiontype>().HasKey(tt => tt.id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.id);

            #endregion

            #region Stored procedures



            #endregion

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !String.IsNullOrEmpty(this.Connection))
            {
                optionsBuilder.UseSqlServer(this.Connection);
            }
        }
    }
}
