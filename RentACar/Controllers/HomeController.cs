using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            // da rechime imash Rezervacija
            Rezervacija rezervacija = new Rezervacija(); // iako ova tie entitet od baza

            rezervacija.DateFrom = DateTime.Now;


            return View(rezervacija.Total);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}