using Election.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Election.Controllers
{
    public class HomeController : Controller
    {
        private masterEntities db = new masterEntities();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult TokenView()
        {
            return View();
        }
        public ActionResult VoteView()
        {
            return View(db.Candidates.ToList());
        }
        public ActionResult ThanksPage()
        {
            return View();
        }
    }
}