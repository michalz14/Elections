using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Election.Models
{
    public class ElectionLogic
    {
        public ElectionLogic()
        {
        }

        public int CheckCitizenData(Citizen citizen)
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

            for (int i = 0; i < 100000; i++) //liczba obywateli
            {
                CreateToken(chars);
            }
            
        }

        private void CreateToken(List<char> chars)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < 15; i++)
            {
                sb.Append(chars[r.Next(chars.Count)]);
            }

            SaveToken(sb.ToString());
        }

        private void SaveToken(string token)
        {
            using(ElectionEntities db = new ElectionEntities())
            {
                if (db.Tokens.Any(x=> x.Token1 == token))
                {
                    Token t = new Token();
                    t.Token1 = token;
                    t.IsUsed = false;

                    db.Tokens.Add(t);
                    var citizen = db.Citizens.First(x => x.TokenID == null);
                    citizen.TokenID = t.TokenID;

                    db.SaveChanges();
                }
            }
        }

    }
}