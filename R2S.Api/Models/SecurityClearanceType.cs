//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace R2S.Api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SecurityClearanceType
    {
        public SecurityClearanceType()
        {
            this.CandidateInfoes = new HashSet<CandidateInfo>();
        }
    
        public string Value { get; set; }
        public string Description { get; set; }
        public Nullable<short> OrderBy { get; set; }
    
        public virtual ICollection<CandidateInfo> CandidateInfoes { get; set; }
    }
}
