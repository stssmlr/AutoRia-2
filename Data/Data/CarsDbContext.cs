using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.Data
{
    public class CarsDbContext : IdentityDbContext<User>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<FuelType> FuelType { get; set; }
        public DbSet<Request> Requests { get; set; }

        public CarsDbContext() { }
        public CarsDbContext(DbContextOptions options) : base(options) { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutoRia;Integrated Security=True;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(new List<Car>()
            {
                new Car() {Id= 1, Mark = "Audi", Model="A8",Year=2018 ,CategoryId=2, Mileage=390000,FuelTypeId=2 ,Discount=5, Price= 19899, Quantity=2, ImageUrl="https://nextcar.ua/images/blog/484/audi-a8-2022__9_.jpg"},
                new Car() {Id= 2, Mark = "Mercedes-Benz", Model="GLS",Year=2019 ,CategoryId=1, Mileage=110000,FuelTypeId=3 ,Discount=0, Price= 29999, Quantity=3, ImageUrl="https://stimg.cardekho.com/images/carexteriorimages/930x620/Mercedes-Benz/GLS/9791/1704772236530/front-left-side-47.jpg"},
                new Car() {Id= 3, Mark = "BMW", Model="X5",Year=2014 ,FuelTypeId=1 ,CategoryId=1, Mileage=220000,Discount=0, Price= 14999, Quantity=1, ImageUrl="https://media.ed.edmunds-media.com/bmw/x5/2025/oem/2025_bmw_x5_4dr-suv_xdrive40i_fq_oem_1_600.jpg"},
                new Car() {Id= 4, Mark = "Volkswagen", Model="Golf",FuelTypeId=5 ,Year=2015 ,Mileage=91000, CategoryId=7, Discount=0, Price= 12999, Quantity=6, ImageUrl="https://images.prismic.io/carwow/2b4b884f-fa2b-40e2-9182-2d2c9450ac5b_37018-ThenewVolkswagenGolfeHybrid.jpg?auto=format&cs=tinysrgb&fit=crop&q=60&w=750"},

            });

            modelBuilder.Entity<Category>().HasData(new List<Category>()
            {
                 new Category() { Id = 1, Name = "Crossover" },
                new Category() { Id = 2, Name = "Sedan" },
                new Category() { Id = 3, Name = "Universal" },
                new Category() { Id = 4, Name = "Miniven" },
                new Category() { Id = 5, Name = "Pickup" },
                new Category() { Id = 6, Name = "Fastback" },
                new Category() { Id = 7, Name = "Hatchback" },

            });

            modelBuilder.Entity<FuelType>().HasData(new List<FuelType>()
            {
                new FuelType() { Id = 1, Name = " Gasoline" },
                new FuelType() { Id = 2, Name = "Diesel" },
                new FuelType() { Id = 3, Name = "Gas" },
                new FuelType() { Id = 4, Name = "Electro" },
                new FuelType() { Id = 5, Name = "Hybrid" }

            });
        }

        
    }
}
