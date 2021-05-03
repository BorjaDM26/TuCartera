using System;
using Microsoft.EntityFrameworkCore;
using TuCartera.DBModel.Contexts.Entities;

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

        public DbSet<SpUserGetLoginResult> SpUserGetLogin { get; set; }
        public DbSet<SpUserPostLoginResult> SpUserPostLogin { get; set; }
        public DbSet<SpUserRegisterResult> SpUserRegister { get; set; }
        public DbSet<SpTransactionItemResult> SpTransactionList { get; set; }
        public DbSet<SpTransactionItemResult> SpTransactionItem { get; set; }
        public DbSet<SpTransactionAddResult> SpTransactionAdd { get; set; }
        public DbSet<SpTransactionEditResult> SpTransactionEdit { get; set; }
        public DbSet<SpTransactionDeleteResult> SpTransactionDelete { get; set; }
        public DbSet<SpPortfolioItemResult> SpPortfolioList { get; set; }
        public DbSet<SpPortfolioItemResult> SpPortfolioItem { get; set; }
        public DbSet<SpPortfolioAddResult> SpPortfolioAdd { get; set; }
        public DbSet<SpPortfolioEditResult> SpPortfolioEdit { get; set; }
        public DbSet<SpPortfolioDeleteResult> SpPortfolioDelete { get; set; }
        public DbSet<SpTickerItemResult> SpTickerList { get; set; }
        public DbSet<SpTickerStateResult> SpTickerStateList { get; set; }
        public DbSet<SpCurrencyItemResult> SpCurrencyList { get; set; }
        public DbSet<SpTransactionTypeItemResult> SpTransactionTypeList { get; set; }

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

            modelBuilder.Entity<SpUserGetLoginResult>().HasNoKey();
            modelBuilder.Entity<SpUserPostLoginResult>().HasNoKey();
            modelBuilder.Entity<SpUserRegisterResult>().HasNoKey();
            modelBuilder.Entity<SpTransactionItemResult>().HasNoKey();
            modelBuilder.Entity<SpTransactionAddResult>().HasNoKey();
            modelBuilder.Entity<SpTransactionEditResult>().HasNoKey();
            modelBuilder.Entity<SpTransactionDeleteResult>().HasNoKey();
            modelBuilder.Entity<SpPortfolioItemResult>().HasNoKey();
            modelBuilder.Entity<SpPortfolioAddResult>().HasNoKey();
            modelBuilder.Entity<SpPortfolioEditResult>().HasNoKey();
            modelBuilder.Entity<SpPortfolioDeleteResult>().HasNoKey();
            modelBuilder.Entity<SpCurrencyItemResult>().HasNoKey();
            modelBuilder.Entity<SpTickerItemResult>().HasNoKey();
            modelBuilder.Entity<SpTickerStateResult>().HasNoKey();
            modelBuilder.Entity<SpTransactionTypeItemResult>().HasNoKey();

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
