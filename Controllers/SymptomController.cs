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

        private int GetCurrentUserID()
        {
            var myuserid = (int)Session["UserId"];
            return myuserid; // 假设用户ID为123
        }
        
       
        //展示症状列表
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Symptom> symptoms = Db.Symptoms.Where(p => p.UserId == id).ToList();
            if (symptoms == null)
            {
                return HttpNotFound();
            }
            List<SymptomViewModel> model = Common.MapToList<Symptom, SymptomViewModel>(symptoms);
            return View(model);
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

            
            List<SymptomViewModel> symptomDetails = GetSymptomDetailsBySymptomIDAndUserID(symptomID.Value, userID);

            ViewBag.SymptomDetails = symptomDetails;
            return View();
        }

        private List<SymptomViewModel> GetSymptomsByUserID(int userID)
        {
            var userSymptoms = Db.Symptoms.Where(p => p.UserId == userID).ToList();
            List<SymptomViewModel> mySymptom = Common.MapToList<Symptom, SymptomViewModel>(userSymptoms);

            return mySymptom;
        }
        private List<SymptomViewModel> GetSymptomDetailsBySymptomIDAndUserID(int symptomID, int userID)
        {
            
             var symptomDetails = Db.SymptomDetails.Where(sd => sd.SymptomID == symptomID && sd.UserId == userID)
                    .Select(sd => new SymptomViewModel
                    {
                        SymptomID = sd.SymptomID,
                        Category = sd.Symptom.Category,
                        SymptomName = sd.Symptom.SymptomName,
                        SeverityLevelID = sd.SeverityLevelID,
                        SeverityLevel = sd.SeverityLevel.SeverityLevel1,
                        Description = sd.Description,
                        UserID = sd.UserId.Value
                    }).ToList();
              
            return symptomDetails;
        }
    }
}