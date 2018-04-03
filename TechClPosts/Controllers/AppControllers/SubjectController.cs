using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechClPosts.Models.RepoInterfaces;
using TechClPosts.Models.AppModels;

namespace TechClPosts.Controllers.AppControllers
{
    public class SubjectController : Controller
    {
        //Subject DB Repository
        private ISubjectsRepository subjRepo = new PostsRepository();

        // GET: Subject
        public ActionResult Index()
        {
            var subjects = subjRepo.AllSubjects().OrderBy(x => x.SubjectName);

            return PartialView(subjects.ToList());
        }
    }
}