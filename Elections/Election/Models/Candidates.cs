//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Election.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Candidates
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidates()
        {
            this.Vote = new HashSet<Vote>();
        }
    
        public int CandidateID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<int> PoliticalGroup { get; set; }
    
        public virtual PoliticalGroups PoliticalGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vote> Vote { get; set; }
    }
}
