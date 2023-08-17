using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IbreastCare.DAL;
using IbreastCare.ViewModel;
using IbreastCare.Models;

namespace IbreastCareAdmin.Controllers
{
    public class AccountManageController : Controller
    {
        private IbreastDBEntities Db = new IbreastDBEntities(); //新建Db 為IbreastDBEntities
        // GET: Account
        // GET: AccountManage
        public ActionResult AccountList()
        {
            List<Member> members = Db.Members.ToList();
            List<AccountViewModel> model = Common.MapToList<Member, AccountViewModel>(members);
            return View(model);
         
        }
        public ActionResult AccountSet()
        {
            return View();
        }
    }
}