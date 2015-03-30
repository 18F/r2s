namespace R2S.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using R2S.Api.Models;
    using R2S.Api.Utilities;

    /// <summary>
    ///  Handles all Identity related actions.
    /// </summary>
    public class IdentityController : Controller
    {
        /// <summary>
        /// Returns the <c ref="IdentityModel" /> representing the user calling this action.
        /// </summary>
        /// <returns>The <c ref="IdentityModel" /> of the current user.</returns>
        public ActionResult Current()
        {
            var identityModel = new IdentityModel() 
            {
                ClientCertificate = new CertificateModel() {
                    EDIPI = DodPkiCertificateParser.GetEdipiFromContext(System.Web.HttpContext.Current),
                    Subject = DodPkiCertificateParser.GetSubjectFromContext(System.Web.HttpContext.Current)
                }
            };

            return this.Json(identityModel, JsonRequestBehavior.AllowGet);
        }
    }
}