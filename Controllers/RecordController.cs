using IbreastCare.DAL;
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
            var myuserid = (int)Session["UserId"];
            //List<Personal_Data> mydata = Db.Personal_Data.Where(p => p.UserId == myuserid).OrderByDescending(p => p.MyId).ToList();
        
            return View();
        }
    }
}