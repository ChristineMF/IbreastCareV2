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

            List<Personal_Data> mydata = Db.Personal_Data.Where(p => p.UserId == myuserid).OrderByDescending(p => p.InputDate).ToList();

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
            List<OperationType> opType = Db.OperationTypes.ToList();

            MydataViewModel editmodel = Common.MapTo<Personal_Data, MydataViewModel>(mydata);

            //Her的DropdownList
            List<SelectListItem> HerList = new List<SelectListItem>();
            HerList.Add(new SelectListItem() { Text = "請選擇", Value = "" });
            HerList.Add(new SelectListItem() { Text = "陰性", Value = "陰性" });
            HerList.Add(new SelectListItem() { Text = "陽性1+", Value = "陽性1+" });
            HerList.Add(new SelectListItem() { Text = "陽性2+", Value = "陽性2+" });
            HerList.Add(new SelectListItem() { Text = "陽性3+", Value = "陽性3+" });
            ViewBag.HerList = new SelectList(HerList, "Value", "Text", editmodel.Her);

            //CellType的DropdownList
            List<SelectListItem> CellTypeList = new List<SelectListItem>();
            CellTypeList.Add(new SelectListItem() { Text = "請選擇", Value = "" });
            CellTypeList.Add(new SelectListItem() { Text = "浸潤性腺管癌", Value = "浸潤性腺管癌" });
            CellTypeList.Add(new SelectListItem() { Text = "乳腺管原位癌", Value = "乳腺管原位癌" });
            CellTypeList.Add(new SelectListItem() { Text = "浸潤性小葉癌", Value = "浸潤性小葉癌" });
            CellTypeList.Add(new SelectListItem() { Text = "乳小葉原位癌", Value = "乳小葉原位癌" });
            CellTypeList.Add(new SelectListItem() { Text = "其他", Value = "其他" });
            ViewBag.CellTypeList = new SelectList(CellTypeList, "Value", "Text", editmodel.CellType);


             List<string> singleTreat= editmodel.TreatPlan.Split(',').ToList();
            ViewBag.treats = singleTreat;
            
            //SelectOpTypes
            //List<SelectListItem> myList = new List<SelectListItem>();
            //foreach (var item in Db.OperationTypes)
            //{
            //    myList.Add(new SelectListItem() { Text = item.OpeTypeName, Value = item.OpeTypeId.ToString() });

            //}
            //ViewBag.list = myList;



            return View(editmodel);

        }
        [HttpPost]
        public ActionResult MydataEdit(MydataViewModel mydataView)
        {
            //1.存資料庫  RegisterViewModel => Personal_Data
            Personal_Data editmodel = Db.Personal_Data.FirstOrDefault(m => m.MyId == mydataView.MyId);

            //2.取Personal_Data資料庫,Personal_Data => MydataViewModel
           // MydataViewModel mydataList = Common.MapTo<Personal_Data, MydataViewModel>(editmodel);
            //List<MydataViewModel> myList = new List<MydataViewModel>();
            //myList.Add(mydataList);
            //foreach (var item in myList)
            //{
            editmodel.OperationDate = mydataView.OperationDate;
            editmodel.ER = mydataView.ER;
            editmodel.PR = mydataView.PR;
            editmodel.Menopause = mydataView.Menopause;
            editmodel.Note = mydataView.Note;
            editmodel.Her = mydataView.Her;
            editmodel.Height = mydataView.Height;
            editmodel.InputDate = DateTime.Now;
            editmodel.OperationType = mydataView.OperationType;
            editmodel.TreatPlan = mydataView.TreatPlan;

            if (mydataView == null)//若id取回的資料為空，則秀錯誤畫面
            {
                return RedirectToAction("Index", "Mydata", new { id = editmodel.UserId }); 
            }
           

            Db.SaveChanges();
            return RedirectToAction("Index", "Mydata", new { id = editmodel.UserId });
        }
        private IEnumerable<SelectListItem> AllOpTypes()
        {
            //集合產生各種清單元素
            List<SelectListItem> myList = new List<SelectListItem>();
            foreach (var item in Db.OperationTypes)
            {
                myList.Add(new SelectListItem() { Text = item.OpeTypeName, Value = item.OpeTypeId.ToString() });

            }
            ViewBag.list = myList;
            return myList.AsEnumerable();

        }



            
                
    }
           
}
        //public ActionResult MydataDetails()
        //{
        //    CarVm carVm = new CarVm();
        //    carVm.SelectedCars = new string[] { "1", "2" };
        //    carVm.AllCars = GetAllCars();
        //    return View(carVm);
        //}
        //[HttpPost]
        //public ActionResult MydataDetails(CarVm carVm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View(carVm);
        //    }
        //    return RedirectToAction("Index", carVm);
        //}
        //private IEnumerable<SelectListItem> GetAllCars()
        //{
        //    List<SelectListItem> allCars = new List<SelectListItem>();
        //    allCars.Add(new SelectListItem() { Value = "1", Text = "奔驰" });
        //    allCars.Add(new SelectListItem() { Value = "2", Text = "宝马" });
        //    allCars.Add(new SelectListItem() { Value = "3", Text = "奇瑞" });
        //    allCars.Add(new SelectListItem() { Value = "4", Text = "比亚迪" });
        //    allCars.Add(new SelectListItem() { Value = "5", Text = "起亚" });
        //    allCars.Add(new SelectListItem() { Value = "6", Text = "大众" });
        //    allCars.Add(new SelectListItem() { Value = "7", Text = "斯柯达" });
        //    allCars.Add(new SelectListItem() { Value = "8", Text = "丰田" });
        //    allCars.Add(new SelectListItem() { Value = "9", Text = "本田" });

        //    return allCars.AsEnumerable();
   
    
        
    

        
