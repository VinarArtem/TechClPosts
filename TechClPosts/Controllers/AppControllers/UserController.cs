using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechClPosts.Models.AppModels;
using TechClPosts.Models.RepoInterfaces;

namespace TechClPosts.Controllers.AppControllers
{
    public class UserController : Controller
    {
        private IUsersRepository userRepo = new PostsRepository();

        //Id for cookies
        private const string id = "id";

        // GET: User
        public ActionResult Index()
        {
            if (Request.Cookies[id] == null)
            {
                return View(nameof(Authorize));
            }
            else
            {
                try
                {
                    Guid userKey = Guid.Parse(Request.Cookies[id].Value);

                    User user = userRepo.GetUser(userKey);

                    return PartialView(user);
                }
                catch
                {
                    return View(nameof(Authorize));
                }
            }
        }

        public ActionResult Authorize()
        {
            ViewBag.ErrorMessage = string.Empty;

            return View();
        }
    }
}