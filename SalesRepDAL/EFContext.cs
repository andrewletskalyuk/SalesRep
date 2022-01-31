using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL.Entities;

namespace SalesRepDAL
{
    public class EFContext : IdentityDbContext<DbUser, DbRole, long, IdentityUserClaim<long>,
                            DbUserRole, IdentityUserLogin<long>,
                            IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        #region DBContext set it for access
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleRep> SaleRep { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TradeCompany> Trades { get; set; }
        public DbSet<TradeCompany_Supplier> Trades_Supplier { get; set; }
        public DbSet<TradeOrder> TradeOrders { get; set; }
        #endregion
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ///user roles
            builder.Entity<DbUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            ///set many to many 
            //TraidCompany_Suppliers
            builder.Entity<TradeCompany_Supplier>()
                .HasKey(tcs => new { tcs.TradeCompanyID, tcs.SupplierID });

            builder.Entity<TradeCompany_Supplier>()
                .HasOne(tc => tc.TradeCompany)
                .WithMany(tc => tc.TradeCompany_Suppliers)
                .HasForeignKey(tc => tc.TradeCompanyID);

            builder.Entity<TradeCompany_Supplier>()
                .HasOne(s => s.Supplier)
                .WithMany(s => s.TradeCompany_Suppliers)
                .HasForeignKey(s => s.SupplierID);
        }
    }
}