using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace R2S.Api.Models
{
    public class OpportunityModel
    {
        public string AdvertisementID { get; set; }
        public string RankRange { get; set; } 
        public string QualificationSummary { get; set; } 
        public string MissionName { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }

    }

    /*
    public class OpportunityDBContext : DbContext
    {
        public DbSet<Opportunity> Opportunities { get; set; }
    }
     * 
     * */
}