using System;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using R2S.Api.Models;
using R2S.Api.Utilities;

namespace R2S.Api.Controllers

{


    public class ApplyController : Controller
    {
        public ApplyController()
        {
        }

        public ActionResult ShowDummyBillets()
        {
            
            ApplyBilletsDummyData dummyData = new ApplyBilletsDummyData();
            ApplyBillet[] billets = dummyData.ApplyBillets;

            return View(billets);
        }

        public PartialViewResult GetMatchingApplyBillets()
        {

            ApplyBilletsDummyData dummyData = new ApplyBilletsDummyData();
            ApplyBillet[] billets = dummyData.ApplyBillets;

            WebRequest request = WebRequest.Create("https://private.navyreserve.navy.mil/apps/rfmtweb/Billet/Search#/search");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();



            return PartialView(billets);
        }

        public ActionResult SearchForBillets ()
        {

            //return View("TEST");

            return RedirectToAction("NotFound");
        }

        public ActionResult Index()
        {
            ApplyBilletsDummyData dummyData = new ApplyBilletsDummyData();
            ApplyBillet[] billets = dummyData.ApplyBillets;

            return View(billets);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }

}