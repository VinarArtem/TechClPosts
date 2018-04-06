using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult Login(string userLogin, string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(userLogin) && !string.IsNullOrWhiteSpace(userPassword))
            {
                User user = userRepo.UserLogin(userLogin, userPassword);

                if (user != null)
                {
                    HttpContext.Response.Cookies[id].Value = user.UserKey.ToString();
                    HttpContext.Response.Cookies[id].Expires = DateTime.Now.AddDays(30);

                    return Content("login successful");
                }
                else
                {
                    return Content("login failed");
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Authorize()
        {
            ViewBag.ErrorMessage = string.Empty;

            return View();
        }
    }
}