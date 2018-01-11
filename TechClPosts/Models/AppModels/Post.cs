using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechClPosts.Models.AppModels
{
    public enum Category
    {
        Technitions
    }
    public class Post
    {
        #region Properties

        [Key]
        public Guid PostKey { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        [Display(Name = "Posted")]
        public DateTime CreationTime { get; set; }
        public long Like { get; set; }
        public Guid UserKey { get; set; }
        public Guid SubjectKey { get; set; }

        public virtual ICollection<Comment> Coments { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual User User{ get; set; }

        #endregion

        public Post(Guid postedUserKey, Guid subjectKey, string description, string content)
        {
            this.PostKey = Guid.NewGuid();
            this.UserKey = postedUserKey;
            this.SubjectKey = subjectKey;
            this.Description = description;
            this.Content = content;
            this.CreationTime = DateTime.Now;
            this.Like = 0;
        }

        public Post() { }
    }
}