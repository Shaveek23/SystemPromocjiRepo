using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;

namespace WebApi.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
             : base(options)
        {

        }
        virtual public DbSet<Person> Persons { get; set; }
        virtual public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
            base.OnModelCreating(modelBuilder);
            // configuring Person in database
            modelBuilder.Entity<Person>(entity =>
            {
                modelBuilder.Entity<Person>().ToTable("People");
                modelBuilder.Entity<Person>().HasKey(p => p.PersonID);
                modelBuilder.Entity<Person>().Property(p => p.PersonID).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired().HasMaxLength(30);
                modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
                modelBuilder.Entity<Person>().Property(p => p.City).IsRequired().HasMaxLength(30);
                modelBuilder.Entity<Person>().Property(p => p.Address).IsRequired().HasMaxLength(30);

                // seeding example data to database:
                modelBuilder.Entity<Person>().HasData
                (
                  new Person { PersonID = 1, FirstName = "Adam", LastName = "Nowak", Address = "ul. Koszykowa 57A/7", City = "Warszawa" }
                );

            });

            modelBuilder.Entity<Post>(entity =>
            {
                modelBuilder.Entity<Post>().ToTable("Posts");
                modelBuilder.Entity<Post>().HasKey(p => p.PostID);
                modelBuilder.Entity<Post>().Property(p => p.PostID).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<Post>().Property(p => p.UserID).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.CategoryID);
                modelBuilder.Entity<Post>().Property(p => p.Date).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(50);
                modelBuilder.Entity<Post>().Property(p => p.Content).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.Localization).IsRequired().HasMaxLength(50);
                modelBuilder.Entity<Post>().Property(p => p.ShopName).HasMaxLength(50);
                modelBuilder.Entity<Post>().Property(p => p.IsPromoted).IsRequired();

                modelBuilder.Entity<Post>().HasData
                (
                  new Post { PostID = 1, UserID = 1, CategoryID = 1, Title = "tytuł 1", Content="Oto mój pierwszy post!", Date=DateTime.Now, IsPromoted=false, Localization = "Warszawa", ShopName = "Sklep1" },
                  new Post { PostID = 2, UserID = 2, CategoryID = 1, Title = "tytuł 2", Content = "Oto mój drugi post!", Date = DateTime.Now, IsPromoted = false, Localization = "Kraków", ShopName = "Sklep2" },
                  new Post { PostID = 3, UserID = 3, CategoryID = 1, Title = "tytuł 3", Content = "Oto mój trzeci post!", Date = DateTime.Now, IsPromoted = false, Localization = "Poznań", ShopName = "Sklep3" }
                );
            });
        }

    }
}
