using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

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
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public string Salt { get; set; } 

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
            this.CreationTime = DateTime.Now;
            this.Salt = this.CreationTime.ToString("yyyyMMddhhmmssffff");
            this.Password = GetHashPass(password);
        }

        public User() { }

        #endregion

        //Check of rights
        public bool IsAdmin { get { return this.Role == UserRole.Admin; } }
        public bool IsContentManager { get { return this.Role == UserRole.ContentManager; } }
        public bool IsCommon { get { return this.Role == UserRole.Common; } }
        public bool IsActice { get { return this.Role != UserRole.Disabled; } }

        #region Methods

        /// <summary>
        /// Crypts the password
        /// </summary>
        /// <param name="password">Raw pasword</param>
        /// <returns></returns>
        private string GetHashPass(string password)
        {
            string res = string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password + this.Salt));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }

                res = sb.ToString();
            }

            return res;
        }

        public bool Authorize(string password)
        {
            return this.Password == GetHashPass(password);
        }

        public void DisableUser(Guid userKey)
        {
            this.Role = UserRole.Disabled;
        }

        #endregion
    }
}