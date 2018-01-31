using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechClPosts.Models.AppModels;
using TechClPosts.Models.RepoInterfaces;
using System.Net;

namespace TechClPosts.Controllers.AppControllers
{
    public class AdminController : Controller
    {
        //Repositories
        IUsersRepository userRepo = new PostsRepository();
        IPostsRepository postRepo = new PostsRepository();
        ISubjectsRepository subjectRepo = new PostsRepository();
        //Id for cookies
        private const string id = "id";

        // GET: Admin
        public ActionResult Index()
        {
            User user = CheckAuthentication(Request.Cookies[id]);

            if (user == null)
            {
                return RedirectToAction("Authorize");
            }
            else
            {
                return View(user);
            }
        }

        #region Posts

        public ActionResult ListOfPosts()
        {
            var posts = postRepo.AllPosts()
                .OrderByDescending(x => x.CreationTime);

            return PartialView(posts.ToList());
        }

        public ActionResult PostDetails(string postKey)
        {
            try
            {
                Guid postGuid = Guid.Parse(postKey);

                Post post = postRepo.GetPost(postGuid);

                return PartialView(post);
            }
            catch (Exception ex)
            {
                return new EmptyResult();
            }
        }

        #endregion

        #region PostsCreation

        public ActionResult SubjectsDropDown()
        {
            var subjects = subjectRepo.AllSubjects()
                .OrderBy(x => x.SubjectName);

            return PartialView(subjects.ToList());
        }

        public ActionResult AddPost(string description, string subject, string content)
        {
            User user = CheckAuthentication(Request.Cookies[id]);

            if (user == null)
            {
                return RedirectToAction("Authorize");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(content))
                {
                    try
                    {
                        Guid subjKey = Guid.Parse(subject);
                        Subject subj = subjectRepo.GetSubject(subjKey);
                        content = Uri.UnescapeDataString(content);
                        Post newPost = new Post(user.UserKey, subj.SubjectKey, description, content);

                        postRepo.AddPost(newPost);

                        return new EmptyResult();
                    }
                    catch
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        #endregion

        #region Subject

        public ActionResult AddSubject(string subjectName)
        {
            User user = CheckAuthentication(Request.Cookies[id]);

            if (user == null)
            {
                return RedirectToAction("Authorize");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(subjectName))
                {
                    Subject subj = new Subject(subjectName);

                    subjectRepo.AddSubject(subj);

                    return new EmptyResult();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        public ActionResult SubjectsList()
        {
            var subjects = subjectRepo.AllSubjects()
                .OrderBy(x => x.SubjectName);

            return PartialView(subjects.ToList());
        }

        #endregion

        #region Users

        public ActionResult AddUser(string name, string login, string password, string role)
        {
            User user = CheckAuthentication(Request.Cookies[id]);

            if (user == null)
            {
                return RedirectToAction("Authorize");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(name)
                    && !string.IsNullOrWhiteSpace(login)
                    && !string.IsNullOrWhiteSpace(password)
                    && !string.IsNullOrWhiteSpace(role))
                {
                    try
                    {
                        UserRole userRole = (UserRole)int.Parse(role);

                        User newUser = new User(name, login, password, userRole);

                        userRepo.AddUser(newUser);

                        return new EmptyResult();
                    }
                    catch
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        #endregion

        #region Authentication

        public ActionResult Authorize()
        {
            ViewBag.ErrorMessage = string.Empty;

            return View();
        }

        public ActionResult Login(string userLogin, string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userLogin) || string.IsNullOrWhiteSpace(userPassword))
            {
                ViewBag.ErrorMessage = "Authentication error. Please try again!";

                return View("Authorize");
            }
            else
            {
                User user = userRepo.UserLogin(userLogin, userPassword);

                if (user == null)
                {
                    ViewBag.ErrorMessage = "Wrong username or password!";

                    return View("Authorize");
                }
                else
                {
                    if (user.IsAdmin)
                    {
                        HttpContext.Response.Cookies[id].Value = user.UserKey.ToString();
                        HttpContext.Response.Cookies[id].Expires = DateTime.Now.AddDays(30);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Authentication error. Please try again!";

                        return View("Authorize");
                    }
                }
            }
        }


        #endregion

        #region Help Methods

        //Check User Authentification. If success return user, else return null
        private User CheckAuthentication(HttpCookie cookie)
        {
            if (cookie == null)
            {
                return null;
            }
            else
            {
                try
                {
                    Guid userKey = Guid.Parse(cookie.Value);

                    User user = userRepo.GetUser(userKey);

                    if (user.IsAdmin || user.IsContentManager)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion

    }
}