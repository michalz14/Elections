using Election.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
                Tokens model = new Tokens();
                model.TokenID = tokenId;


                // tutaj trzeba jakos przekazac dalej tokenID zeby go mozna bylo pozniej sprawdzic z tym co ktos wpisal
                // nie pisalem nigdy nic w MVC wiec nie wiem jak to sie przesyla. Jakos do querystringa najlepiej pewnie
                // moge to zrobic asynchronicznie przez javasript i ajaxem ale to duzo przerabiania
                return View(model);
            }

            //info ze nie ma danych
            else return HttpNotFound();
        }
        
        public ActionResult VoteView(Tokens model)
        {
            var token = db.Tokens.Where(x => x.TokenID == model.TokenID).FirstOrDefault();
            if (model.Token==token.Token && token.IsUsed!=true)
            {
                var candidates = db.Candidates.Include(x => x.PoliticalGroups).ToList();
                CandidatesDTO candidatesDTO = new CandidatesDTO();
                List<CandidatesDTO> candidatesDTOList = new List<CandidatesDTO>();
                VoteViewDTO voteModel = new VoteViewDTO();
                if (candidates!=null)
                {
                    foreach (var item in candidates)
                    {
                        candidatesDTO.CandidateID = item.CandidateID;
                        candidatesDTO.LastName = item.LastName;
                        candidatesDTO.FirstName = item.FirstName;
                        candidatesDTO.PoliticalGroup = item.PoliticalGroup;


                        candidatesDTOList.Add(candidatesDTO);
                        candidatesDTO = new CandidatesDTO();
                    }
                    voteModel.candidatesDTOlist = candidatesDTOList;
                    voteModel.TokenId = model.TokenID;
                }
               
                return View(voteModel);
            }
            return View("InvalidTokenView");
        }


        public ActionResult ThanksPage(VoteViewDTO model)
        {

            Tokens tokenObject = db.Tokens.Where(x => x.TokenID == model.TokenId).FirstOrDefault();
            tokenObject.IsUsed = true;
            db.SaveChanges();
            logic.SaveVote(model.CandidateId);
            return View();
        }

        public ActionResult InvalidTokenView()
        {
            return View();
        }
    }
}