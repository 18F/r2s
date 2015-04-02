using R2S.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R2S.Api.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            var order1 = new OrderViewModel() { Title = "Orders 1", Description = "Aenean nec ipsum non nulla elementum scelerisque luctus ac libero." };
            var order2 = new OrderViewModel() { Title = "Orders 2", Description = "Donec sodales metus vel urna semper quis feugiat urna porttitor massa." };
            var order3 = new OrderViewModel() { Title = "Orders 3", Description = "Praesent blandit eleifend neque vel viverra vestibulum non enim." };
            var orders = new List<OrderViewModel>() { order1, order2, order3 };
            return this.Json(orders, JsonRequestBehavior.AllowGet);
        }
    }
}