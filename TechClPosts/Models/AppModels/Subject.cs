using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechClPosts.Models.AppModels
{
    public class Subject
    {
        [Key]
        public Guid SubjectKey { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Subject(string name)
        {
            this.SubjectKey = Guid.NewGuid();
            this.SubjectName = name;
        }

        public Subject() { }
    }
}