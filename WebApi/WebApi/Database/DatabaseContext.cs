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
        virtual public DbSet<Comment> Comments { get; set; }
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
            modelBuilder.Entity<Comment>(entity =>
            {
                //CO z relacjami ??
                modelBuilder.Entity<Comment>().ToTable("Comment");
                modelBuilder.Entity<Comment>().HasKey(p => p.CommentID);
                modelBuilder.Entity<Comment>().Property(p => p.CommentID).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<Comment>().Property(p => p.DateTime).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<Comment>().Property(p => p.PostID).IsRequired();
                modelBuilder.Entity<Comment>().Property(p => p.UserID).IsRequired();
                modelBuilder.Entity<Comment>().Property(p => p.Content).IsRequired().HasMaxLength(255);

                modelBuilder.Entity<Comment>().HasData
                (
                  new Comment { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Now, Content = "tralalala " },
                    new Comment { CommentID = 2, UserID = 1, PostID = 2, DateTime = DateTime.Now, Content = "tralalala pararara" },
                      new Comment { CommentID = 3, UserID = 2, PostID = 1, DateTime = DateTime.Now, Content = "tu ti tu rum tu tu" }
                );


            });

        }

    }
}
