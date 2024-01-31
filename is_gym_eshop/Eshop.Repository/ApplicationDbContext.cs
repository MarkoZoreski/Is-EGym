using Eshop.Domain.DomainModels;
using Eshop.Domain.Identity;
using Eshop.Domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;

namespace Eshop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EshopUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCards { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-4QCTE4I;Initial Catalog=GymDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;", b => b.MigrationsAssembly("Eshop.Web"));
        //    // database at home
        //    //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShopD;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("Eshop.Web"));
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProductInShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.CurrnetProduct)
                .WithMany(z => z.ProductInShoppingCarts)
                .HasForeignKey(z => z.ProductId);

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.UserCart)
                .WithMany(z => z.ProductInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ShoppingCart>()
                .HasOne<EshopUser>(z => z.CartOwner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.CartOwnerId);

            builder.Entity<ProductInOrder>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.Product)
                .WithMany(z => z.ProductInOrders)
                .HasForeignKey(z => z.ProductId);

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.ProductInOrders)
                .HasForeignKey(z => z.OrderId);
        }
    }
}
