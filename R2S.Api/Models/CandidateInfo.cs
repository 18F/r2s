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
    
    public partial class CandidateInfo
    {
        public int CandidateInfoID { get; set; }
        public string MinimumRank { get; set; }
        public string MaximumRank { get; set; }
        public string Designator { get; set; }
        public string Rating { get; set; }
        public string NOBC { get; set; }
        public string NEC { get; set; }
        public string AQD { get; set; }
        public string Qualifications { get; set; }
        public string Gender { get; set; }
        public Nullable<short> SecurityClearanceRequiredID { get; set; }
        public string SecurityClearanceTypeID { get; set; }
        public Nullable<int> RequirementInfoID { get; set; }
        public string RankSwitch { get; set; }
    
        public virtual RequirementInfo RequirementInfo { get; set; }
        public virtual YesNo YesNo { get; set; }
        public virtual SecurityClearanceType SecurityClearanceType { get; set; }
    }
}