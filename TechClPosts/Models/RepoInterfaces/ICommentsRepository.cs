using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechClPosts.Models.AppModels;

namespace TechClPosts.Models.RepoInterfaces
{
    interface ICommentsRepository
    {
        void AddComment(Comment comment);

        Comment GetComment(Guid commentKey);

        IEnumerable<Comment> AllComments();

        void EditComment(Comment updatedComment);

        void DeleteComment(Comment commentToDelete);
    }
}
