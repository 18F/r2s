using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R2S.Api.Models
{


    public class ApplyBillet
    {

        //public int Id { get; set; }
        public string ReserveMobilizationUnitNavyUnitTitle { get; set; }
        public string BilletTitle { get; set; }

        public ApplyBillet(String unitTitle, String billetTitle)
	    {
            ReserveMobilizationUnitNavyUnitTitle = unitTitle;
            BilletTitle = billetTitle;
	    }
    }
}