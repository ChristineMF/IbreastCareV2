﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IbreastCare.DAL;
using IbreastCare.Models;
using IbreastCare.ViewModel;

namespace IbreastCare.Controllers
{
    public class AccountController : Controller
    {
        private IbreastDBEntities Db = new IbreastDBEntities(); //新建Db 為IbreastDBEntities
        // GET: Account
        //CaseManager看到的PatientList
        public ActionResult Index()
        {
            List<Member> members = Db.Members.ToList();

            List<PatientViewModel> model = Common.MapToList<Member, PatientViewModel>(members);

            //計算年齡
            foreach (var item in model)
            {
                item.age = CalculateAgeCorrect(item.BirthDate, DateTime.Now);
            }

            return View(model);
        }
        //年齡計算公式
        int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }

        public ActionResult Combine()
        {

            return View();
        }
        public ActionResult Login()
        {

            return View("Combine");
        }
        [HttpPost]
        public ActionResult Login([Bind(Prefix = "Login")] LoginViewModel loginmodel)
        {

            if (ModelState.IsValid)
            {

                Member member = Db.Members.FirstOrDefault(m => m.UserName == loginmodel.UserName && m.Password == loginmodel.Password);
                if (member == null)
                {
                    ViewBag.Msg = "帳號或密碼錯誤";
                    return View("Combine");
                }
                Session["RoleId"] = member.RoleId;
                Session["NickName"] = member.NickName;
                Session["UserId"] = member.UserId;
                if (member.RoleId == 2 && member.Status == "on")
                {
                    return RedirectToAction("Index", "Patient", new { id = member.UserId });

                    //return RedirectToAction("Index", "Patient");

                }
                else
                {
                    if (member.RoleId == 3 && member.Status == "on")
                        //return RedirectToAction("Index", " CaseManager", new { id = member.UserId });
                        return RedirectToAction("Index", "CaseManager");
                    else
                    {
                        if (member.RoleId == 4 && member.Status == "on")
                            //return RedirectToAction("Index", " CaseManager", new { id = member.UserId });
                            return RedirectToAction("Index", "AccountManage", new { id = member.UserId });
                        else
                        {
                            ViewBag.Msg = "權限未開啟";
                            return View("Combine");
                        }

                    }
                }

            }
            else
            {
                return View("Combine");
            }

        }
        //登出
       
        public ActionResult Logout()
        {
            Session["RoleId"] = null;
            Session["NickName"] = null;
            Session["UserId"] = null;
           
            return RedirectToAction("Index", "Home");
        }
        //Guest註冊
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RegisterViewModel member)
        {
            if (ModelState.IsValid)
            {
                //1.存資料庫  RegisterViewModel=>Member
                member.RoleId = 3;
                member.InputDate = DateTime.Now;
                member.Status = "on";
                
               
                Member model = Common.MapTo<RegisterViewModel, Member>(member);
                
                Db.Members.Add(model);
               
                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(member);
            }
     
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Prefix = "Register")] RegisterViewModel member, MydataViewModel mydata)
        {
            if (ModelState.IsValid)
            {
                //1.存資料庫  RegisterViewModel=>Member
                member.RoleId = 2;
                member.InputDate = DateTime.Now;
                member.Status = "on";

                Member model = Common.MapTo<RegisterViewModel, Member>(member);
                Db.Members.Add(model);

                //2.取Member資料庫,Member=>RegisterViewModel
                RegisterViewModel detailmodel = Common.MapTo<Member, RegisterViewModel>(model);

                //3-1.將Member.UserId存入MydataViewModel.UserId
                //3-2.設定系統時間為inputDate的時間
                mydata.UserId = detailmodel.UserId;
                mydata.InputDate = DateTime.Now;

                //4.存資料庫 Personal_Data=>MydataViewModel
                Personal_Datas mydataAll = Common.MapTo<MydataViewModel, Personal_Datas>(mydata);
                Db.Personal_Datas.Add(mydataAll);

                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.data = "資料輸入不完整";
                return View("Combine");
            }
        }

//Patient修改
    public ActionResult Edit(int? id)
        {
            ViewBag.myuserid = (int)Session["UserId"];

            if (id == null)
            {
                return HttpNotFound();
            }
            Member member = Db.Members.FirstOrDefault(m => m.UserId == id);

            if (member==null)
            {
                return HttpNotFound();
            }
            RegisterViewModel editmodel = Common.MapTo<Member, RegisterViewModel>(member);
            return View(editmodel);
        }
        [HttpPost]
        public ActionResult Edit(RegisterViewModel editmodel)
        {
            if (ModelState.IsValid)
            {
                //1.存資料庫  RegisterViewModel=>Member
                
                Member member = Db.Members.FirstOrDefault(m => m.UserId == editmodel.UserId);
                member.UserName = editmodel.UserName;
                member.NickName = editmodel.NickName;
                if(editmodel.Password!="1")
                {
                    member.Password = editmodel.Password;
                   
                }
                
                member.CellPhone = editmodel.CellPhone;
                member.Email = editmodel.Email;           
                Db.SaveChanges();
                return RedirectToAction("Details", "Account", new { id = editmodel.UserId });
            }
            else
            {
                return View(editmodel);
            }

        }

        //Patient看到的帳號明細
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Member member = Db.Members.FirstOrDefault(m => m.UserId == id);

            if (member == null)
            {
                return HttpNotFound();
            }
            RegisterViewModel detailmodel = Common.MapTo<Member, RegisterViewModel>(member);
            return View(detailmodel);

        }

    }
}