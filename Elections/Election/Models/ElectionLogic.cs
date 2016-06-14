using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;

namespace Election.Models
{
    public class ElectionLogic
    {
        public ElectionLogic()
        {
        }

        public void UseToken(string t)
        {
            using (ElectionEntities db = new ElectionEntities())
            {
                string tokenHash = Hash(t);
                var token = db.Tokens.FirstOrDefault(x => x.Token == tokenHash && x.IsUsed == false);

                if (token != null)
                {
                    token.IsUsed = true;
                    token.VoteData = DateTime.Now;

                    db.SaveChanges();
                }
                else
                {
                    //zwrot info że token jest już użyty lub podano nieprawidłowy
                }
            }
        }

        public void SaveVote(int candidateID)
        {
            using (ElectionEntities db = new ElectionEntities())
            {
                if(candidateID >0)
                    db.Vote.Add(new Vote {CandidateID = candidateID});
                db.SaveChanges();
            }
        }

        public int CheckCitizenData(Citizens citizen)
        {
            using (ElectionEntities db = new ElectionEntities())
            {
                var tmp =
                    db.Citizens.Where(
                        x =>
                            x.PESEL == citizen.PESEL && 
                            x.FirstName == citizen.FirstName &&
                            x.LastName == citizen.LastName && 
                            x.Address == citizen.Address && 
                            x.City == citizen.City &&
                            x.IdNumber == citizen.IdNumber).FirstOrDefault();
                

                return tmp != null ? (int)tmp.TokenID : -1;
            }
        }

        public void GenerateTokens()
        {
            List<char> chars = new List<char>();
            Enumerable.Range('A', 'Z' - 'A' + 1).ToList().ForEach(x => chars.Add((char)x));
            Enumerable.Range('a', 'z' - 'a' + 1).ToList().ForEach(x => chars.Add((char)x));
            Enumerable.Range(49, 9).ToList().ForEach(x => chars.Add((char)x));


            using (ElectionEntities db = new ElectionEntities())
            {
                while(db.Citizens.Any(x => x.TokenID == null))
                {
                    string token = CreateToken(chars);
                    SaveToken(token);
                }
            }
        }

        private string CreateToken(List<char> chars)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < 15; i++)
            {
                sb.Append(chars[r.Next(chars.Count)]);
            }

            return Hash(sb.ToString());
            //return sb.ToString();
        }

        private void SaveToken(string token)
        {
            using(ElectionEntities db = new ElectionEntities())
            {
                if (!db.Tokens.Any(x=> x.Token == token))
                {
                    Tokens t = new Tokens();
                    t.Token = token;
                    t.IsUsed = false;

                    db.Tokens.Add(t);
                    db.SaveChanges();

                    var citizen = db.Citizens.First(x => x.TokenID == null);

                    if(citizen != null)
                        citizen.TokenID = t.TokenID;

                    db.SaveChanges();
                }
            }
        }

        private string Hash(string token) //funkcja hashujaca SHA
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(token));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

    }
}