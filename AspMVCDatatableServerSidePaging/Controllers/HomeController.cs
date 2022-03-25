﻿using AspMVCDatatableServerSidePaging.Models;
using System.Web.Mvc;

namespace AspMVCDatatableServerSidePaging.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}