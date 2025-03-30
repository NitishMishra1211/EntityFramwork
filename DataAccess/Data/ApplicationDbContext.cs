using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace DataAccess.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product> ().HasData(
                new Product { Id = 1, Title = "Book1", Description = "Description1", ISBN = "123456", Author = "Author1", ListPrice = 10, Price = 5, Price50 = 4, Price100 = 3, CategoryId = 1, ImageUrl = "" },
                new Product { Id = 2, Title = "Book2", Description = "Description2", ISBN = "123457", Author = "Author2", ListPrice = 20, Price = 10, Price50 = 8, Price100 = 6, CategoryId = 2, ImageUrl = "" },
                new Product { Id = 3, Title = "Book3", Description = "Description3", ISBN = "123458", Author = "Author3", ListPrice = 30, Price = 15, Price50 = 12, Price100 = 9, CategoryId = 3, ImageUrl = "" }
                );
        }
    }
}
