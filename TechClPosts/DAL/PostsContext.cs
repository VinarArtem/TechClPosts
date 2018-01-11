using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TechClPosts.Models.AppModels;

namespace TechClPosts.DAL
{
    public class PostsContext : DbContext
    {
        public PostsContext() : base("PostsContext") { }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserKey)
                .WillCascadeOnDelete(false);
        }
    }
}