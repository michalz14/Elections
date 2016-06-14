using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Election.Models
{
    public class CandidatesDTO
    {

        public int CandidateID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<int> PoliticalGroup { get; set; }

        public virtual PoliticalGroups PoliticalGroups { get; set; }
    }
}