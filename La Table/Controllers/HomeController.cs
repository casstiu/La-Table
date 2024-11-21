using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace La_Table.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NoAccountHome()
        {
            return View();
        }
        public ActionResult NoAccountBookings()
        {
            return View();
        }
        public ActionResult NoAccountAbout()
        {
            return View();
        }
        public ActionResult NoAccountPromos()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult CustomerHome()
        {
            return View();
        }
        public ActionResult CustomerPromos()
        {
            return View();
        }
        public ActionResult CustomerAbout()
        {
            return View();
        }
        public ActionResult CustomerBookings()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult AdminAccount()
        {
            return View();
        }
        public ActionResult AdminLogs()
        {
            return View();
        }
        public ActionResult AdminPromos()
        {
            return View();
        }
    }
}