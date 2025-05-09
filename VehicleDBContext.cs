using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Vehicle.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CodeFirst.Vehicle.DataAccessLayer
{
    public class VehicleDBContext: DbContext
    {
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<VehicleProduct> VehicleProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configrationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var connectionString = configrationBuilder.GetConnectionString("VehicleDBConnectionString");
                Console.WriteLine("Connection String: " + connectionString);
                optionsBuilder.UseSqlServer(connectionString);
                // optionsBuilder.UseSqlServer("Data Source=RAJAT3644;Initial Catalog= Vehicle;User Id=sa;Password=MySql#3816;TrustServerCertificate=true");
            }
        }
        //the below data anotation can be used when dbfunction name and function name is different
       
       
        [DbFunction("Ufn_GenerateNewVehicleCategoryID", IsBuiltIn = false)]
        public static byte GetCategoryIDfunction(byte categoryId)
        {
            Console.WriteLine("{0} is the category id", categoryId);

            return 0;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<VehicleProduct>(entity =>
            {
                entity.HasAlternateKey(e => e.ProductName)
                .HasName("unique_ProductName");

                entity.HasOne(v => v.VehicleCategory)
                .WithMany(v => v.VehicleProducts)
                .HasForeignKey(v => v.CategoryId)
                .HasConstraintName("FK_VehicleProduct_VehicleCategory");


            });
            // when scalar function data annotation is not usied  is name and schema
        //modelBuilder.HasDefaultSchema("dbo")
                //0is default value
          //  .HasDbFunction(() => VehicleDBContext.GetCategoryIDfunction(0))
            //.HasName("Ufn_GenerateNewVehicleCategoryID");
        }
    }
}

