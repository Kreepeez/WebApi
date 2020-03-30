

using Microsoft.EntityFrameworkCore;
using System;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class WebApplication3Context : DbContext
    {
         public WebApplication3Context(DbContextOptions<WebApplication3Context> options)
             : base(options)
         {
         }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<BlogPostTags> BlogPostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BlogPostTags>()
                  .HasKey(pt => new { pt.Slug, pt.TagId });

            modelBuilder.Entity<BlogPostTags>()
                .HasOne(pt => pt.BlogPost)
                .WithMany(p => p.BlogPostTags)
                .HasForeignKey(pt => pt.Slug);

            modelBuilder.Entity<BlogPostTags>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.BlogPostTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Slug = "augmented-reality-ios-application",
                    Title = "Augmented Reality iOS Application",
                    Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    CreatedAt = DateTime.UtcNow ,
                    UpdatedAt = DateTime.UtcNow
                },
                new BlogPost
                {

                    Title = "test test",
                    Slug = "test-test",
                    Body = "very much test",
                    Description = "test is made of test yes",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
                ) ;
                
        }

       


    }
}
