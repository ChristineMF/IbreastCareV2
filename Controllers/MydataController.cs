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
        public ActionResult Index(int? id)
        {
            //ViewBag.userid = Session["UserId"].ToString();//接Session取得的UserID
            //var myuserid = Session["UserId"];
            if (id == null)
            {
                return HttpNotFound();
            }
            //Personal_Data mydataList = Db.Personal_Data.FirstOrDefault(m => m.UserId == id);
            //if (mydataList == null)
            //{
            //    return HttpNotFound();
            //}

            List<Personal_Data> mydata = Db.Personal_Data.ToList();

            List<MydataViewModel> model = Common.MapToList<Personal_Data, MydataViewModel>(mydata);
            List<MydataViewModel> dataList = new List<MydataViewModel>();
            foreach (var item in model)
            {
                if (item.UserId == id)
                {
                    dataList.Add(item);
                }
               
            }
            Session["UserId"]=id;
            Session["InputDate"] = DateTime.Now;
            return View(dataList);

        }

        public ActionResult MydataCreate()
        {
            ViewBag.userid = (int)Session["UserId"];
            ViewBag.inputdate = Session["InputDate"];
            return View();
        }
        [HttpPost]
        public ActionResult MydataCreate(MydataViewModel mydata, int id)
        {
            if (ModelState.IsValid)
            {
                //1.存資料庫  RegisterViewModel=>Member  
                mydata.InputDate = DateTime.Now;
                mydata.UserId = id;

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