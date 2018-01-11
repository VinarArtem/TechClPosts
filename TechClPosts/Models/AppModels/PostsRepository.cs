using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechClPosts.DAL;
using TechClPosts.Models.RepoInterfaces;

namespace TechClPosts.Models.AppModels
{
    public class PostsRepository : IDisposable, IUsersRepository, ISubjectsRepository, IPostsRepository, ICommentsRepository
    {
        PostsContext db = new PostsContext();

        #region Users Repository

        public void AddUser(User user)
        {
            db.Users.Add(user);

            db.SaveChanges();
        }

        public User GetUser(Guid userKey)
        {
            return db.Users.Find(userKey);
        }

        public IEnumerable<User> AllUsers()
        {
            IEnumerable<User> users = db.Users;

            return users.Where(x => x.IsActice);
        }
        
        public void EditUser(User updatedUser)
        {
            User userToUpdate = db.Users.Find(updatedUser.UserKey);
            userToUpdate.Role = updatedUser.Role;
            userToUpdate.Name = updatedUser.Name;

            db.SaveChanges();
        }

        public void DeleteUser(User userToDelete)
        {
            db.Users.Remove(userToDelete);

            db.SaveChanges();
        }

        public User UserLogin (string userLogin, string userPassword)
        {
            return db.Users.FirstOrDefault(x => x.Login == userLogin && x.Password == userPassword);
        }

        #endregion

        #region Subjects Repository

        public void AddSubject(Subject subject)
        {
            db.Subjects.Add(subject);

            db.SaveChanges();
        }

        public Subject GetSubject(Guid subjectKey)
        {
            return db.Subjects.Find(subjectKey);
        }

        public IEnumerable<Subject> AllSubjects()
        {
            return db.Subjects;
        }

        public void EditSubject(Subject updatedSubject)
        {
            Subject subjectToUpdate = db.Subjects.Find(updatedSubject.SubjectKey);
            subjectToUpdate.SubjectName = updatedSubject.SubjectName;

            db.SaveChanges();
        }

        public void DeleteSubject(Subject subjectToDelete)
        {
            db.Subjects.Remove(subjectToDelete);

            db.SaveChanges();
        }

        #endregion

        #region Posts Repository

        public void AddPost(Post post)
        {
            db.Posts.Add(post);

            db.SaveChanges();
        }

        public Post GetPost(Guid postKey)
        {
            return db.Posts.Find(postKey);
        }

        public IEnumerable<Post> AllPosts()
        {
            return db.Posts;
        }

        public void EditPost(Post updatedPost)
        {
            Post postToUpdate = db.Posts.Find(updatedPost.PostKey);
            postToUpdate.Description = updatedPost.Description;
            postToUpdate.Content = updatedPost.Content;

            db.SaveChanges();
        }

        public void DeletePost(Post postToDelete)
        {
            db.Posts.Remove(postToDelete);

            db.SaveChanges();
        }

        #endregion

        #region Comments Repository

        public void AddComment(Comment comment)
        {
            db.Comments.Add(comment);

            db.SaveChanges();
        }

        public Comment GetComment(Guid commentKey)
        {
            return db.Comments.Find(commentKey);
        }

        public IEnumerable<Comment> AllComments()
        {
            return db.Comments;
        }

        public void EditComment(Comment updatedComment)
        {
            Comment commentToUpdate = db.Comments.Find(updatedComment.CommentKey);
            commentToUpdate.Content = updatedComment.Content;

            db.SaveChanges();
        }

        public void DeleteComment(Comment commentToDelete)
        {
            db.Comments.Remove(commentToDelete);

            db.SaveChanges();
        }

        #endregion

        #region Dispose
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}