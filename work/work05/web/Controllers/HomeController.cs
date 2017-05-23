﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("Your application description page.");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return Json("", "");
        }

        public ViewResult Index()
        {
            throw new NotImplementedException();
        }
    }
}