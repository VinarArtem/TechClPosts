﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechClPosts.Models.RepoInterfaces;
using TechClPosts.Models.AppModels;

namespace TechClPosts.Controllers.AppControllers
{
    public class HomeController : Controller
    {
        //Users BD Repository
        private IPostsRepository postRepo = new PostsRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}