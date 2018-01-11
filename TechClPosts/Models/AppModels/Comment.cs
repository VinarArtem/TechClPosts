using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechClPosts.Models.AppModels
{
    public class Comment
    {
        [Key]
        public Guid CommentKey { get; set; }
        public string Content { get; set; }
        [Display(Name = "Commented")]
        public DateTime CreationTime { get; set; }
        public Guid UserKey { get; set; }
        public Guid PostKey { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }

        public Comment(Guid postedUserKey, Guid postKey, string comment)
        {
            this.CommentKey = Guid.NewGuid();
            this.PostKey = postKey;
            this.UserKey = postedUserKey;
            this.CreationTime = DateTime.Now;
            this.Content = comment;
        }

        public Comment() { }
    }
}