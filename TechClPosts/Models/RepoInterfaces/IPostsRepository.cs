using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechClPosts.Models.AppModels;

namespace TechClPosts.Models.RepoInterfaces
{
    interface IPostsRepository
    {
        void AddPost(Post post);

        Post GetPost(Guid postKey);

        IEnumerable<Post> AllPosts();

        void EditPost(Post updatedPost);

        void DeletePost(Post postToDelete);
    }
}
