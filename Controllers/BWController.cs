using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.Controllers
{
    public class BWController : Controller
    {
        // GET: BW
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddBW()
        {
            ViewBag.userid = (int)Session["UserId"];
            return View();
        }
    }
}