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
    
    public partial class RequirementOwner
    {
        public int RequirementOwnerID { get; set; }
        public string PocName { get; set; }
        public string PocPhone { get; set; }
        public string PocEmail { get; set; }
        public string PocOrganization { get; set; }
        public Nullable<int> RequirementInfoID { get; set; }
    
        public virtual RequirementInfo RequirementInfo { get; set; }
    }
}
