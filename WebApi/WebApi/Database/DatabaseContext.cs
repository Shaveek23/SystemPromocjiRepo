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

        }

    }
}
