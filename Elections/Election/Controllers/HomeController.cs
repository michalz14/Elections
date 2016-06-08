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
        private ElectionEntities db = new ElectionEntities();
        private ElectionLogic logic = new ElectionLogic();
        public ActionResult Index()
        {
            //logic.GenerateTokens();  // metoda odpalająca generowanie i wstrzykiwanie tokenow do bazy
            return View();
        }

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult TokenView(Citizens citizen)
        {
            var tokenId = logic.CheckCitizenData(citizen);
            if (tokenId > 0)
            {
                // tutaj trzeba jakos przekazac dalej tokenID zeby go mozna bylo pozniej sprawdzic z tym co ktos wpisal
                // nie pisalem nigdy nic w MVC wiec nie wiem jak to sie przesyla. Jakos do querystringa najlepiej pewnie
                // moge to zrobic asynchronicznie przez javasript i ajaxem ale to duzo przerabiania
                return View();
            }

            //info ze nie ma danych
            else return HttpNotFound();
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