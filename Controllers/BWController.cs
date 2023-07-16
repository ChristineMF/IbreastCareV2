using IbreastCare.DAL;
using IbreastCare.Models;
using IbreastCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            //var myuserid = (int)Session["UserId"];
            ViewBag.userid = (int)Session["UserId"];

            List<BW>myBW=Db.BWs.Where(p => p.UserId == id).OrderByDescending(p => p.MeasureDate).ToList();
            //List<string> dateStrings = BWList
            //    .Select(p => p.MeasureDate.Date.ToString("yyyy/MM/dd"))
            //    .ToList();
            //List<BW> myBW = BWList
            //     .Select((bw, index) => new BW
            //     {
            //         MeasureDate = DateTime.ParseExact(dateStrings[index], "yyyy/MM/dd", CultureInfo.InvariantCulture),
            //         BMI = bw.BMI,
            //         BWId = bw.BWId,
            //         BW1 = bw.BW1,
            //         InputDate = bw.InputDate,
            //         UserId = bw.UserId
            //     })
            //     .ToList();
            // 將日期字串轉換為 List<DateTime>
            //List<DateTime> MDateString = dateStrings
            //    .Select(dateString => DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture))
            //    .ToList();
            //List<BW> myBW = BWList
            //        .Select(p => new BW
            //        {
            //            MeasureDate = p.MeasureDate.Date,
            //            BMI = p.BMI,
            //            BWId = p.BWId,
            //            BW1 = p.BW1,
            //            InputDate = p.InputDate,
            //            UserId = p.UserId

            //        }).ToList();



            List<BodyWeightViewModel> model = Common.MapToList<BW, BodyWeightViewModel>(myBW);

            //MeasureDate 是可為空的 DateTime? 型別（Nullable DateTime），需要先檢查是否有值，然後才能使用 DateTime.Date 方法來提取日期部分
            //所以要改成Where(r => r.MeasureDate.HasValue)..OrderBy(r => r.MeasureDate).Select(r => r.MeasureDate.Value.Date)
            //最後將欄位改為必填就不用判斷了
            var measureArray = model.Where(r => r.MeasureDate != null).OrderBy(r => r.MeasureDate).Select(r => r.MeasureDate.Date).ToArray();//Labels

            //datas
            var bwArray = model.Where(r => r.BW1 != null).OrderBy(r => r.MeasureDate).Select(r => r.BW1).ToArray();
            var bmiArray = model.Where(r => r.BMI != null).OrderBy(r => r.MeasureDate).Select(r => r.BMI).ToArray();

            // var dataMeasure = string.Join(",", measureArray);轉string時指定字串的日期格式
            var dataMeasure = string.Join(",", measureArray.Select(d => "'" + d.ToString("yyyy/MM/dd") + "'"));
            var dataBW = string.Join(",", bwArray);
            var dataBMI = string.Join(",", bmiArray);
            ViewBag.dataMeasure = dataMeasure;
            ViewBag.dataBW = dataBW;
            ViewBag.dataBMI = dataBMI;
            

            return View(model);
        }
        public ActionResult Index_old(int? id)
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
            var myAge = Db.Members.Where(p => p.UserId == id).FirstOrDefault();
            
            var age = new CalculateAge().CalculateAgeCorrect(myAge.BirthDate.Value.Date, DateTime.Now);
            ViewBag.myAge = age;
            return View();
        }
        public ActionResult EditBW(int? id)
        {
            ViewBag.userid = (int)Session["UserId"];
            var myBH = Db.BWs.Join(Db.Personal_Data, b => b.UserId, p => p.UserId, (bh, ph) => new { bh.UserId, bh.BWId, ph.MyId, ph.Height })
                .Where(e => e.BWId == id).FirstOrDefault();
              
            ViewBag.myHeight = myBH.Height;
            var myAge = Db.Members.Where(p => p.UserId == myBH.UserId).FirstOrDefault();
            var age = new CalculateAge().CalculateAgeCorrect(myAge.BirthDate.Value.Date, DateTime.Now);
            ViewBag.myAge = age;
            var myBW= Db.BWs.Where(p => p.BWId == id).FirstOrDefault();
            //// 取得 myBW.MeasureDate 的日期字串 (yyyy-MM-dd)
            //const measureDateStr = new Date(myBW.MeasureDate).toLocaleDateString("en-CA");

            //// 將 measureDateStr 設定給 item.MeasureDate
            //item.MeasureDate = measureDateStr;

            ViewBag.myWeight = myBW.BW1;
            ViewBag.myBMI = myBW.BMI;
            ViewBag.myMeasure = myBW.MeasureDate.Date;
            ViewBag.myBWId = myBW.BWId;
            return View();
        }
        //[HttpPost]
        public ActionResult DeleteBW(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

           BW mybw = Db.BWs.FirstOrDefault(p => p.BWId == id);
            int myuserid = mybw.UserId;
            if (mybw == null) return HttpNotFound();//404
            Db.BWs.Remove(mybw);
            Db.SaveChanges();
            return RedirectToAction("Index", "BW", new {id = myuserid });
        }
        
        
        public ActionResult ShowLine(int? id)
        {
            //var myuserid = (int)Session["UserId"];
            ViewBag.userid = (int)Session["UserId"];

            List<BW> myBW = Db.BWs.Where(p => p.UserId == id).OrderByDescending(p => p.BWId).ToList();
            List<BodyWeightViewModel> model = Common.MapToList<BW, BodyWeightViewModel>(myBW);
            var measureArray = model.Where(r => r.MeasureDate != null).OrderBy(r => r.BWId).Select(r => r.MeasureDate).ToArray();
            var bwArray = model.Where(r => r.BW1 != null).OrderBy(r => r.BWId).Select(r => r.BW1).ToArray();
            var bmiArray = model.Where(r => r.BMI != null).OrderBy(r => r.BWId).Select(r => r.BMI).ToArray();

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