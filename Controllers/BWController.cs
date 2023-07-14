using IbreastCare.DAL;
using IbreastCare.Models;
using IbreastCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.Controllers
{

    public class BWController : Controller
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: BW
        public ActionResult Index(int? id)
        {
            ViewBag.userid = (int)Session["UserId"];
            var myBW =
                Db.BWs.Where(p => p.UserId == id)
                .Select(p => new BWDTO
                {
                    BWId = p.BWId,
                    BW1 = p.BW1,
                    BMI = p.BMI,
                    InputDate = p.InputDate,
                    MeasureDate = p.MeasureDate

                })
                .OrderByDescending(p => p.BWId).ToList()
                .Select(p => new BWDTO
                {
                    BW1 = p.BW1,
                    BMI = p.BMI,
                    InputDate = p.InputDate.Date,
                    MeasureDate = p.MeasureDate
                });
            ViewBag.bwdata = myBW;

            return View(myBW);
        }
        public ActionResult AddBW(int? id)
        {
            ViewBag.userid = (int)Session["UserId"];
            var myBH = Db.Personal_Data.Where(p => p.UserId == id).OrderByDescending(p => p.MyId).FirstOrDefault();
            ViewBag.myHeight = myBH.Height;


            return View();
        }
        public ActionResult ShowLine(int? id)
        {
            //var myuserid = (int)Session["UserId"];
            ViewBag.userid = (int)Session["UserId"];
            
            List<BW> myBW = Db.BWs.Where(p => p.UserId == id).OrderByDescending(p => p.BWId).ToList();
            List<BodyWeightViewModel> model = Common.MapToList<BW, BodyWeightViewModel>(myBW);
            var measureArray = Db.BWs.Where(r => r.MeasureDate != null).Select(r => r.MeasureDate).ToArray();
            var bwArray = Db.BWs.Where(r => r.BW1 != null).Select(r => r.BW1).ToArray();
            var bmiArray = Db.BWs.Where(r => r.BMI != null).Select(r => r.BMI).ToArray();

            var dataMeasure = string.Join(",", measureArray);
            var dataBW = string.Join(",", bwArray);
            var dataBMI = string.Join(",", bmiArray);
            ViewBag.dataMeasure = dataMeasure;
            ViewBag.dataBW = dataBW;
            ViewBag.dataBMI = dataBMI;

            return View(model);
        }
    }
}