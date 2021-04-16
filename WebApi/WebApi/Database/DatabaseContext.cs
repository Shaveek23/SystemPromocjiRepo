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
        virtual public DbSet<Comment> Comments { get; set; }
        virtual public DbSet<User> Users { get; set; }
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
            modelBuilder.Entity<User>(entity =>
            {
                modelBuilder.Entity<User>().ToTable("User");
                modelBuilder.Entity<User>().HasKey(p => p.UserID);
                modelBuilder.Entity<User>().Property(p => p.UserID).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<User>().Property(p => p.Timestamp).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<User>().Property(p => p.UserEmail).IsRequired().HasMaxLength(30);
                modelBuilder.Entity<User>().Property(p => p.IsAdmin);
                modelBuilder.Entity<User>().Property(p => p.IsEnterprenuer);
                modelBuilder.Entity<User>().Property(p => p.IsVerified);
                modelBuilder.Entity<User>().Property(p => p.Active);

                // seeding example data to database:
                modelBuilder.Entity<User>().HasData
                (
                  new User { UserID = 1, Timestamp = new DateTime(2021, 4, 16, 22, 30, 20), UserEmail = "jaroslaw@kaczyslaw.pl", UserName = "jaroslawpolsezbaw", IsAdmin = false, IsEnterprenuer = true, IsVerified = true, Active = true },
                  new User { UserID = 2, Timestamp = new DateTime(2021, 4, 13, 12, 30, 20), UserEmail = "antoni@kaczyslaw.pl", UserName = "tobrzozawybuchla", IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Active = true }
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

            modelBuilder.Entity<Post>(entity =>
            {
                modelBuilder.Entity<Post>().ToTable("Posts");
                modelBuilder.Entity<Post>().HasKey(p => p.PostID);
                modelBuilder.Entity<Post>().Property(p => p.PostID).IsRequired().ValueGeneratedOnAdd();
                modelBuilder.Entity<Post>().Property(p => p.UserID).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.CategoryID).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.Date).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(50);
                modelBuilder.Entity<Post>().Property(p => p.Content).IsRequired();
                modelBuilder.Entity<Post>().Property(p => p.IsPromoted).IsRequired();

                modelBuilder.Entity<Post>().HasData
                (
                  new Post { PostID = 1, UserID = 1, CategoryID = 1, Title = "tytuł 1", Content = "Oto mój pierwszy post!", Date = new DateTime(2021, 3, 11, 12, 23, 46), IsPromoted = false },
                  new Post { PostID = 2, UserID = 2, CategoryID = 1, Title = "tytuł 2", Content = "Oto mój drugi post!", Date = new DateTime(2021, 6, 21, 11, 2, 44), IsPromoted = false },
                  new Post { PostID = 3, UserID = 3, CategoryID = 1, Title = "tytuł 3", Content = "Oto mój trzeci post!", Date = new DateTime(2021, 4, 11, 1, 21, 4), IsPromoted = false }
                );
            });
        }

    }
}
