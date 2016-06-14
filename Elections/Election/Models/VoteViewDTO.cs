using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Election.Models
{
    public class VoteViewDTO
    {
        public List<CandidatesDTO> candidatesDTOlist { get; set; }

        public int TokenId { get; set; }

        public int CandidateId { get; set; }
    }
}