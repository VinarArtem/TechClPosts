using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechClPosts.Models.AppModels
{
    public enum UserRole
    {
        Common,
        ContentManager,
        Admin,
        Disabled
    }

    public class User
    {
        #region Properties

        [Key]
        public Guid UserKey { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        #endregion

        #region Constructors

        public User(string name, string login, string password, UserRole role)
        {
            this.UserKey = Guid.NewGuid();
            this.Role = role;
            this.Name = name;
            this.Login = login;
            this.Password = password;
            this.CreationTime = DateTime.Now;
        }

        public User() { }

        #endregion

        //Check of rights
        public bool IsAdmin { get { return this.Role == UserRole.Admin; } }
        public bool IsContentManager { get { return this.Role == UserRole.ContentManager; } }
        public bool IsCommon { get { return this.Role == UserRole.Common; } }
        public bool IsActice { get { return this.Role != UserRole.Disabled; } }

        #region Methods

        public bool Authorize(string login, string password)
        {
            return this.Login == login && this.Password == password;
        }

        public void DisableUser(Guid userKey)
        {
            this.Role = UserRole.Disabled;
        }

        #endregion
    }
}