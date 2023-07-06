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
            //ViewBag.userid = Session["UserId"].ToString();//接Session取得的UserID
            var myuserid = (int)Session["UserId"];
            //if (id == null)
            //{
            //    return HttpNotFound();
            //}
            //Personal_Data mydataList = Db.Personal_Data.FirstOrDefault(m => m.UserId == id);
            //if (mydataList == null)
            //{
            //    return HttpNotFound();
            //}

            List<Personal_Data> mydata = Db.Personal_Data.Where(p=>p.UserId==myuserid).ToList();

            List<MydataViewModel> model = Common.MapToList<Personal_Data, MydataViewModel>(mydata);
            //List<MydataViewModel> dataList = new List<MydataViewModel>();
            //foreach (var item in model)
            //{
            //    if (item.UserId == myuserid)
            //    {
            //        dataList.Add(item);
            //    }
            //}
           
            //ViewBag.myid = Session["UserId"]; 
            //Session["InputDate"] = DateTime.Now;
            return View(model);

        }

        public ActionResult MydataCreate()
        {
            ViewBag.userid = (int)Session["UserId"];
            //ViewBag.inputdate = Session["InputDate"];
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

        public ActionResult MydataEdit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Personal_Data mydata = Db.Personal_Data.FirstOrDefault(m => m.MyId == id);

            if (mydata == null)
            {
                return HttpNotFound();
            }
            MydataViewModel editmodel = Common.MapTo<Personal_Data, MydataViewModel>(mydata);
            return View(editmodel);

        }
        //[HttpPost]
        //public ActionResult MydataEdit(MydataViewModel mydataView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //1.存資料庫  RegisterViewModel=>Member

        //        Personal_Data editmodel = Db.Personal_Data.FirstOrDefault(m => m.UserId == mydataView.UserId);



        //        //2.取Personal_Data資料庫,Personal_Data=>MydataViewModel
        //        MydataViewModel mydataList = Common.MapTo<Personal_Data, MydataViewModel>(editmodel);




        //        editmodel.CellPhone = mydataView.CellPhone;
        //        editmodel.Email = mydataView.Email;
        //        Db.SaveChanges();
        //        return RedirectToAction("Details", "Account", new { id = editmodel.UserId });
        //    }
        //    else
        //    {
        //        return View(editmodel);
        //    }
        //    return View();
        //}
        //public ActionResult MydataDetails()
        //{
        //    return View();
        //}

    }
}