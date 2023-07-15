using IbreastCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            CarVm carVm = new CarVm();
            carVm.SelectedCars = new string[] { "1", "2" };
            carVm.AllCars = GetAllCars();
            return View(carVm);
        }
      
        [HttpPost]
        public ActionResult SaveCars(CarVm carVm)
        {
            if (ModelState.IsValid)
            {
                return View(carVm);
            }
            return RedirectToAction("Index", carVm);
        }
        private IEnumerable<SelectListItem> GetAllCars()
        {
            List<SelectListItem> allCars = new List<SelectListItem>();
            allCars.Add(new SelectListItem() { Value = "1", Text = "奔驰" });
            allCars.Add(new SelectListItem() { Value = "2", Text = "宝马" });
            allCars.Add(new SelectListItem() { Value = "3", Text = "奇瑞" });
            allCars.Add(new SelectListItem() { Value = "4", Text = "比亚迪" });
            allCars.Add(new SelectListItem() { Value = "5", Text = "起亚" });
            allCars.Add(new SelectListItem() { Value = "6", Text = "大众" });
            allCars.Add(new SelectListItem() { Value = "7", Text = "斯柯达" });
            allCars.Add(new SelectListItem() { Value = "8", Text = "丰田" });
            allCars.Add(new SelectListItem() { Value = "9", Text = "本田" });

            return allCars.AsEnumerable();
        }
    }
}
