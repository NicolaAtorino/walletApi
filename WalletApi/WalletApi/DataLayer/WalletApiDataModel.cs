namespace WalletApi.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WalletApiDataModel : DbContext
    {
        public WalletApiDataModel()
            : base("name=WalletApiDataModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Balance)
                .HasPrecision(18, 0);
        }
    }
}
