using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R2S.Api.Models
{


    /// <summary>
    /// Summary description for ApplyBilletsDummyData
    /// </summary>
    public class ApplyBilletsDummyData
    {

        public ApplyBillet[] ApplyBillets { get; set;}

        public ApplyBilletsDummyData()
	    {
		    //
		    // TODO: Add constructor logic here
		    //

            ApplyBillet[] tempApplyBillets = new ApplyBillet[3];
            tempApplyBillets[0] = new ApplyBillet("Unit 1", "Billet 1");
            tempApplyBillets[1] = new ApplyBillet("Unit 1", "Billet 2");
            tempApplyBillets[2] = new ApplyBillet("Unit 2", "Billet 1");

            ApplyBillets = tempApplyBillets;
        }
    }

}