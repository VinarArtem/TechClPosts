using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TechClPosts.Models.AppModels;

namespace TechClPosts.DAL
{
    public class PostsInitializer : DropCreateDatabaseIfModelChanges<PostsContext>
    {
        protected override void Seed(PostsContext context)
        {
            context.Users.Add(new User("Administrator", "admin", "leon", UserRole.Admin));

            context.SaveChanges();
        }
    }
}