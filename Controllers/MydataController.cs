using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IbreastCare.Models;
using IbreastCare.ViewModel;
using System.Net;
using System.Threading.Tasks;

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

            //lambda語法,//將join完的新集合再做一次join,每次join都組成新物件做比對
            var y = Db.MyOperations.
                Join(Db.OperationTypes, p => p.OpeTypeId, o => o.OpeTypeId, (mo, po) => new { mo.MyId, mo.MyOpeId, po.OpeTypeName })
                .Join(Db.MyTreats,t=>t.MyId,mt=>mt.MyId,(tm,tp)=>new { tm.MyId,tm.MyOpeId,tm.OpeTypeName,tp.TreatId})
                .Join(Db.TreatPlans,mop=>mop.TreatId,mtp=>mtp.TreatId,(motp,mpt)=>new { motp.MyId,motp.MyOpeId,motp.OpeTypeName,motp.TreatId, mpt.TreatName})
                .Join(Db.Personal_Data, o => new { o.MyId }, p => new { p.MyId }, (o, p) => new { p.UserId,p.MyId, o.OpeTypeName,o.TreatName, p.CellType, p.ER, p.Height, p.Her, p.Menopause, p.InputDate, p.Note, p.OperationDate, p.PR })
                .OrderByDescending(o => o.MyId).ToList();
           

            List<MydataViewModel> model = y.GroupBy(a => a.MyId).Select(g => new MydataViewModel
            {
                MyId = g.Key,
                Height = g.First().Height,
                ER = g.First().ER,
                PR = g.First().PR,
                Her = g.First().Her,
                CellType = g.First().CellType,
                Menopause = g.First().Menopause,
                OperationDate = g.First().OperationDate,
                Note = g.First().Note,
                InputDate = (DateTime)g.First().InputDate,

                OpeTypeName = string.Join(",", g.Select(a => a.OpeTypeName).Distinct()).Trim(),
                TreatName= string.Join(",", g.Select(a => a.TreatName).Distinct()).Trim(),
                UserId = g.First().UserId

            }).ToList();
            

            //var 變數1=new MpperConfiguration(變數2=>變數2.CreateMap<主資料表(顯示)>

            //將List<Personal_Data>轉成List<MydataViewModel>

            //List<MydataViewModel> model = Common.MapToList<Personal_Data, MydataViewModel>(y);
            //MydataViewModel model = mapper.Map<MydataViewModel>(mydata);
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal_Data mydata = Db.Personal_Data.FirstOrDefault(m => m.MyId == id);

            if (mydata == null)
            {
                return HttpNotFound();
            }
            List<OperationType> opType = Db.OperationTypes.ToList();

            MydataViewModel editmodel = Common.MapTo<Personal_Data, MydataViewModel>(mydata);

            //Her的DropdownList,取DB內的值顯示
            List<SelectListItem> HerList = new List<SelectListItem>();
            HerList.Add(new SelectListItem() { Text = "請選擇", Value = "" });
            HerList.Add(new SelectListItem() { Text = "陰性", Value = "陰性" });
            HerList.Add(new SelectListItem() { Text = "陽性1+", Value = "陽性1+" });
            HerList.Add(new SelectListItem() { Text = "陽性2+", Value = "陽性2+" });
            HerList.Add(new SelectListItem() { Text = "陽性3+", Value = "陽性3+" });
            ViewBag.HerList = new SelectList(HerList, "Value", "Text", editmodel.Her);

            //CellType的DropdownList,取DB內的值顯示
            List<SelectListItem> CellTypeList = new List<SelectListItem>();
            CellTypeList.Add(new SelectListItem() { Text = "請選擇", Value = "" });
            CellTypeList.Add(new SelectListItem() { Text = "浸潤性腺管癌", Value = "浸潤性腺管癌" });
            CellTypeList.Add(new SelectListItem() { Text = "乳腺管原位癌", Value = "乳腺管原位癌" });
            CellTypeList.Add(new SelectListItem() { Text = "浸潤性小葉癌", Value = "浸潤性小葉癌" });
            CellTypeList.Add(new SelectListItem() { Text = "乳小葉原位癌", Value = "乳小葉原位癌" });
            CellTypeList.Add(new SelectListItem() { Text = "其他", Value = "其他" });
            ViewBag.CellTypeList = new SelectList(CellTypeList, "Value", "Text", editmodel.CellType);

            //待改在controller先將字串先切成list
            // List<string> singleTreat= editmodel.TreatPlan.Split(',').ToList();
            //ViewBag.treats = singleTreat;

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
        //因為OperationType及TreatPlan接到的是陣列,所以要用string[]去接多選的值
        public ActionResult MydataEdit(MydataViewModel mydataView, string[] OperationType, string[] TreatPlan)
        {
            if (ModelState.IsValid)
            {
                //1.從資料庫取得 Personal_Data
                Personal_Data editmodel = Db.Personal_Data.FirstOrDefault(m => m.MyId == mydataView.MyId);
                if (mydataView == null)//若id取回的資料為空，則秀錯誤畫面
                {
                    return RedirectToAction("Index", "Mydata", new { id = editmodel.UserId });
                }
                //2.取Personal_Data資料庫,Personal_Data => MydataViewModel
                // MydataViewModel mydataList = Common.MapTo<Personal_Data, MydataViewModel>(editmodel);
                //List<MydataViewModel> myList = new List<MydataViewModel>();
                //myList.Add(mydataList);
                //foreach (var item in myList)
                //{

                //2. 更新 Personal_Data 的屬性值
                editmodel.OperationDate = mydataView.OperationDate;

                editmodel.ER = mydataView.ER == "on" ? "陽" : "陰";
                editmodel.PR = mydataView.PR == "on" ? "陽" : "陰";
                editmodel.Menopause = mydataView.Menopause == "on" ? "是" : "否";

                editmodel.Note = mydataView.Note;
                editmodel.Her = mydataView.Her;
                editmodel.Height = mydataView.Height;
                editmodel.InputDate = DateTime.Now;
                editmodel.OperationType = String.Join(",", OperationType);
                editmodel.TreatPlan = String.Join(",", TreatPlan);

                //3. 更新或新增 MyOperation 資料
                List<MyOperation> myop = Db.MyOperations.Where(p => p.MyId == mydataView.MyId).ToList();
                foreach (var item in myop)
                {
                    Db.MyOperations.Remove(item);
                }
                List<string> Optypes = OperationType.ToList();
                foreach (var item in Optypes)
                {

                    if (item != "")
                    {
                        var optypeid = new MyOperation
                        {

                            MyId = mydataView.MyId,
                            OpeTypeId = Convert.ToInt32(item)
                        };
                        Db.MyOperations.Add(optypeid);
                    }

                }
                List<MyTreat> mytreat = Db.MyTreats.Where(p => p.MyId == mydataView.MyId).ToList();
                foreach (var item in mytreat)
                {
                    Db.MyTreats.Remove(item);
                }
                List<string> Treats = TreatPlan.ToList();
                foreach (var item in Treats)
                {

                    if (item != "")
                    {
                        var treatid = new MyTreat
                        {

                            MyId = mydataView.MyId,
                            TreatId = Convert.ToInt32(item)
                        };
                        Db.MyTreats.Add(treatid);
                    }
                }
                Db.SaveChanges();

                //4. 儲存變更並重新導向
                // Db.SaveChanges();
                return RedirectToAction("Index", "Mydata", new { id = mydataView.UserId });

            }
            else
                return View(mydataView);//失敗回到連結畫面
        }
        
        ////myop = mydataView.OperationType;
        //List<MydataViewModel> model = Common.MapToList<MyOperation, MydataViewModel>(myop);



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

        public ActionResult MydataDetails(int? id)
        {
            if (id == null)//若id是空,則秀錯誤畫面
            {
                return HttpNotFound();
            }
            Personal_Data mydata = Db.Personal_Data.FirstOrDefault(p => p.MyId == id);
            //var y = SLIST.Join(ScoreList, o => o.ID, p => p.StudentID,
            //        (c, s) => new { c.ID, c.Classroom, c.Name, s.Class, s.score })

            //MydataViewModel detailmodel = Common.MapTo<Personal_Data, MydataViewModel>(mydata);
            //
            var config = new MapperConfiguration(cfg =>
              cfg.CreateMap<Personal_Data, MydataViewModel>()
              .ForMember(x => x.OperationType, y => y.MapFrom(o => string.Join(",", o.MyOperations.Select(z => z.OperationType.OpeTypeName.Trim()).ToArray())))
              .ForMember(x => x.TreatPlan, y => y.MapFrom(o => string.Join(",", o.MyTreats.Select(z => z.TreatPlan.TreatName.Trim()).ToArray()))));
            var mapper = config.CreateMapper();

            //var config2 = new MapperConfiguration(cfg =>
            //cfg.CreateMap<MyOperation, MydataViewModel>()
            //.ForMember(x => x.MyId, y => y.MapFrom(o => o.OperationType.OpeTypeName.Trim())));
            //  .ForMember(x => x.TreatPlan, y => y.MapFrom(o => string.Join(",", o.MyTreats.Select(z => z.TreatPlan.TreatName.Trim()).ToArray()))));
            //var mapper2= config2.CreateMapper();

            //var configTreat = new MapperConfiguration(cfg =>
            //  cfg.CreateMap<Personal_Data, MydataViewModel>().
            //  ForMember(x => x.TreatPlan, y => y.MapFrom(o => string.Join(",", o.MyOperations.Select(z => z.OperationType.OpeTypeName.Trim()).ToArray()))));
            //var mapper = config.CreateMapper();

            //var 變數1=new MpperConfiguration(變數2=>變數2.CreateMap<主資料表(顯示)>
            MydataViewModel model = mapper.Map<MydataViewModel>(mydata);

            return View(model);

        }
        
        public ActionResult MydataDelete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Personal_Data mydata = Db.Personal_Data.FirstOrDefault(p => p.MyId == id);

            if (mydata == null) return HttpNotFound();//404
            var config = new MapperConfiguration(cfg =>
              cfg.CreateMap<Personal_Data, MydataViewModel>()
              .ForMember(x => x.OperationType, y => y.MapFrom(o => string.Join(",", o.MyOperations.Select(z => z.OperationType.OpeTypeName.Trim()).ToArray())))
              .ForMember(x => x.TreatPlan, y => y.MapFrom(o => string.Join(",", o.MyTreats.Select(z => z.TreatPlan.TreatName.Trim()).ToArray()))));
            var mapper = config.CreateMapper();

            MydataViewModel model = mapper.Map<MydataViewModel>(mydata);
            return View(model);
        }
        [HttpPost]
        [ActionName("MydataDelete")]
        public ActionResult MydataDeleteConfirm(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Personal_Data mydata = Db.Personal_Data.FirstOrDefault(p => p.MyId == id);
            if (mydata == null) return HttpNotFound();//404
            Db.Personal_Data.Remove(mydata);
            List<MyOperation> myop = Db.MyOperations.Where(p => p.MyId == id).ToList();
            foreach (var item in myop)
            {
                Db.MyOperations.Remove(item);
            }

            List<MyTreat> mytreat = Db.MyTreats.Where(p => p.MyId == id).ToList();
            foreach (var item in mytreat)
            {
                Db.MyTreats.Remove(item);
            }

            Db.SaveChanges();
            return RedirectToAction("Index");
        }
        }
    }

    //用Details改成Delete
    //public async Task<ActionResult> Delete(int? id)//回傳的一定會有值，則不加?，若可能會null，才加?
    //{
    //    if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
    //    var opera = await context.Operas.FindAsync(id);
    //    if (opera == null) return HttpNotFound();//404

//    return View(opera);
//}
//[HttpPost]
//[ActionName("Delete")]
//public async Task<ActionResult> DeleteConfirmed(int? id)//有?，不可同名稱,但路徑要一樣，所以設actionName
//{
//    if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
//    var opera = await context.Operas.FindAsync(id);//參數一定要叫id，才能在路由接/後面的資料，若叫其他名稱，則會抓不到
//    if (opera == null) return HttpNotFound();//404

//    context.Operas.Remove(opera);
//    await context.SaveChangesAsync();

//    return RedirectToAction("Index");
//}



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






