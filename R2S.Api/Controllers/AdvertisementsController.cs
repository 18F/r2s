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
            var ad1 = new AdvertisementViewModel() { Title = "Advert 1", Description = "Aenean nec ipsum non nulla elementum scelerisque luctus ac libero." };
            var ad2 = new AdvertisementViewModel() { Title = "Advert 2", Description = "Donec sodales metus vel urna semper quis feugiat urna porttitor massa." };
            var ad3 = new AdvertisementViewModel() { Title = "Advert 3", Description = "Praesent blandit eleifend neque vel viverra vestibulum non enim." };
            var ads = new List<AdvertisementViewModel>() { ad1, ad2, ad3 };
            return this.Json(ads, JsonRequestBehavior.AllowGet);
        }
    }
}