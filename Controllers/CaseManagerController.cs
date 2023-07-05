using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCareAdmin.Controllers
{
    public class CaseManagerController : Controller
    {
        // GET: CaseManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PatientList()
        {
            return View();
        }
    }
}