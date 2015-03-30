using R2S.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R2S.Api.Controllers
{
    public class AdvertisementsController : Controller
    {
        // GET: Advertisements
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hello()
        {
            var ad1 = new AdvertisementViewModel() { Title = "Advert 1" };
            var ad2 = new AdvertisementViewModel() { Title = "Advert 2" };
            var ad3 = new AdvertisementViewModel() { Title = "Advert 3" };
            var ads = new List<AdvertisementViewModel>() { ad1, ad2, ad3 };
            return this.Json(ads, JsonRequestBehavior.AllowGet);
        }
    }
}