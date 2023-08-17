using IbreastCare.DAL;
using IbreastCare.Models;
using IbreastCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.Controllers
{
    public class SymptomController : Controller
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: Symptom

        public int GetCurrentUserID()
        {
            var myuserid = (int)Session["UserId"];
            return myuserid; // 假设用户ID为123
        }


        //展示症状列表
        public ActionResult Index(int? id)
        {
            ViewBag.myuserid = (int)Session["UserId"];
            var myuserid = (int)Session["UserId"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //MySymptoms.UserId==id,SymptomDetailID==SymptomDetails.SymptomDetailID,Description,SymptomID==Symptoms.SymptomID,Category,SeverityLevelID=SymptomDetails.ServityLevelID

            var mySymptomsAll = Db.MySymptoms.
                Join(Db.SymptomDetails, p => p.SymptomDetailID, o => o.SymptomDetailID, (ms, sd) => new { ms.MySymptomsId, ms.UserId, ms.SymptomDetailID, ms.OnsetDate, ms.InputDate, ms.ModificationDate, sd.SymptomID, sd.SeverityLevelID, sd.Description })
                .Join(Db.Symptoms, md => md.SymptomID, s => s.SymptomID, (msd, ss) => new { msd.MySymptomsId, msd.UserId, msd.SymptomID, msd.Description, msd.InputDate, msd.ModificationDate, msd.OnsetDate, msd.SeverityLevelID, msd.SymptomDetailID, ss.Category })
                .Join(Db.SeverityLevels, msds => msds.SeverityLevelID, sl => sl.SeverityLevelID, (msds, sl) => new { msds.MySymptomsId, msds.Category, msds.Description, msds.InputDate, msds.ModificationDate, msds.OnsetDate, msds.SeverityLevelID, msds.SymptomDetailID, msds.SymptomID, msds.UserId, sl.SeverityLevelName })
                .Where(p => p.UserId == myuserid).OrderByDescending(p => p.MySymptomsId).ToList()
                .Select(item => new SymptomViewModel
                {
                    MySymptomsId = item.MySymptomsId,
                    UserId = item.UserId,
                    SymptomID = item.SymptomID,
                    Description = item.Description,
                    InputDate = item.InputDate,
                    ModificationDate = item.ModificationDate,
                    OnsetDate = item.OnsetDate,
                    SeverityLevelID = item.SeverityLevelID,
                    SymptomDetailID = item.SymptomDetailID.Value,
                    Category = item.Category,
                    SeverityLevelName = item.SeverityLevelName
                }).ToList();

            
            if (mySymptomsAll == null)
            {
                return HttpNotFound();
            }
         
            return View(mySymptomsAll);
        }

        public ActionResult SymptomCreate(int? id)
        {
            ViewBag.myuserid = (int)Session["UserId"];
            var myuserid = (int)Session["UserId"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           

            List<Symptom> categories = Db.Symptoms.ToList();
           
            List<SymptomViewModel> model = Common.MapToList<Symptom, SymptomViewModel>(categories);
           
            List<SelectListItem> CategoriesAll = new List<SelectListItem>();
            foreach (var item in model)
            {
                CategoriesAll.Add(new SelectListItem() { Text=item.Category,Value=item.SymptomID.ToString()});
            }
            ViewBag.symptomList = new SelectList(model, "SymptomID", "Category");
           

            return View();
        }

            //處理選擇症狀類別後的細節
            [HttpPost]
        public ActionResult ShowDetails(int? symptomID)
        {
            int userID = GetCurrentUserID();
            if (symptomID == null)
            {
                return RedirectToAction("Index","Symptom",new { id= userID });
            }

            var selectDetails = Db.SymptomDetails.Where(d => d.SymptomID == symptomID);
            List<SelectListItem> showDetailsItem = new List<SelectListItem>();
            foreach (var item in selectDetails)
            {
                showDetailsItem.Add(new SelectListItem() { Text = item.Description, Value = item.SymptomDetailID.ToString() });
            }
            ViewBag.symptomDetail = showDetailsItem;
            return View(showDetailsItem);
        }
        [HttpPost]
        public ActionResult Save(SymptomViewModel mySymviewModel)
        {
            int userID = GetCurrentUserID();
            if (mySymviewModel.SymptomDetailID == null)
            {
                return RedirectToAction("Index", "Symptom", new { id = userID });
            }
            var selectDetail = Db.SymptomDetails.FirstOrDefault(sd => sd.SymptomDetailID == mySymviewModel.SymptomDetailID);
            if (selectDetail != null)
            {
                var mylevel = Db.SeverityLevels.FirstOrDefault(s => s.SeverityLevelID == selectDetail.SeverityLevelID).SeverityLevelName;
                ViewBag.selectlevel = mylevel;
                MySymptomViewModel mySym = new MySymptomViewModel()
                {
                    UserId = mySymviewModel.UserId,
                    SymptomDetailID = mySymviewModel.SymptomDetailID,
                    InputDate = DateTime.Now,
                    OnsetDate = mySymviewModel.OnsetDate
                };
                MySymptom mydataAll = Common.MapTo<MySymptomViewModel, MySymptom>(mySym);
                Db.MySymptoms.Add(mydataAll);
                return RedirectToAction("Index", "Symptom",new { id=userID});
            }
            else
            {
                ViewBag.data = "資料輸入不完整";
                return View(mySymviewModel);
            }
           
        }



        }
}