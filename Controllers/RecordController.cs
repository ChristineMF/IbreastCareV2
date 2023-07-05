using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.Controllers
{
    public class RecordController : Controller
    {
        // GET: Record
        public ActionResult Index()
        {
            ViewBag.myuserid = (int)Session["UserId"];
            return View();
        }
    }
}