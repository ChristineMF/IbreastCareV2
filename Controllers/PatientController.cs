using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index(int? id)
        {
            ViewBag.myuserid = (int)Session["UserId"];
            //ViewBag.userid = Session["UserId"].ToString();
            return View();
        }
    }
}