using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

using IbreastCare.Models;
using IbreastCare.ViewModel;

namespace IbreastCare.Controllers
{
    public class MydataController : Controller
    {
        private IbreastDBEntities Db = new IbreastDBEntities(); //新建Db 為IbreastDBEntities
        // GET: Mydata
        public ActionResult Index()
        {
            List<Personal_Data> mydata = Db.Personal_Data.ToList();
            List<MydataViewModel> model = Common.MapToList<Personal_Data, MydataViewModel>(mydata);
            return View(model);
           
        }
        public ActionResult MydataCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MydataCreate(MydataViewModel mydata)
        {
            if (ModelState.IsValid)
            {
                //1.存資料庫  RegisterViewModel=>Member  
                mydata.InputDate = DateTime.Now;
              
                Personal_Data model = Common.MapTo<MydataViewModel, Personal_Data>(mydata);

                Db.Personal_Data.Add(model);

                Db.SaveChanges();
                return RedirectToAction("Index", "Mydata");
            }
            else
            {
                return View(mydata);
            }
           
        }
        //public ActionResult MydataEdit()
        //{
        //    return View();
        //}
        //public ActionResult MydataDetails()
        //{
        //    return View();
        //}

    }
}