using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Terrace.Models;

namespace Terrace.Controllers
{
    public class AdminController : Controller
    {
        private TerraceContext db = new TerraceContext();
        private PasswordHasher passwordHasher = new PasswordHasher();

        //出于安全原因关闭管理员注册页面
        //public ActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Register([Bind(Include = "Password,Name")]Administrator admin)
        //{
        //    var chk_admin = db.Administrators.Where(a => a.Name == admin.Name).FirstOrDefault();
        //    if (chk_admin != null) { ModelState.AddModelError("Name", "您输入的账号名称已经有人注册过了"); }
        //    if (ModelState.IsValid)
        //    {
        //        admin.Password = passwordHasher.HashPassword(admin.Password);
        //        admin.RegisterOn = DateTime.Now;
        //        db.Administrators.Add(admin);
        //        db.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //    else { return View(); }
        //}

        //显示会员登录界面
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password, string returnUrl)
        {
            var hash_pw = db.Administrators.Where(a => a.Name == name).Select(a => a.Password).FirstOrDefault();
            PasswordVerificationResult ValidataAdmin = passwordHasher.VerifyHashedPassword(hash_pw, password);
            if (ValidataAdmin == PasswordVerificationResult.Success)
            {
                FormsAuthentication.SetAuthCookie(name, false);
                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction(returnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "您输入的账号或者密码错误！");
                return View();
            }
        }

        //运行管理员注销
        public ActionResult Logout()
        {
            //清除窗体验证的Cookies
            FormsAuthentication.SignOut();
            //清除所有session信息
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            return View();
        }
        //管理学生界面
        public ActionResult AlterStudent(int page = 1)
        {
            return View(db.Students.OrderBy(s => s.Id).ToPagedList(page, 10));
        }
        //删除学生
        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            ViewBag.Id = id;
            Student student = db.Students.Where(s => s.Id == id).FirstOrDefault();
            db.Students.Remove(student);
            db.SaveChanges();
            var deletedid = id;
            //return Json(deletedid);
            return new EmptyResult();
        }
        //管理教师界面
        public ActionResult AlterTeacher(int page = 1)
        {
            return View(db.Teachers.OrderBy(t => t.Id).ToPagedList(page, 10));
        }
        //删除老师
        [HttpPost]
        public ActionResult DeleteTeacher(int id)
        {
            ViewBag.Id = id;
            Student student = db.Students.Where(s => s.Id == id).FirstOrDefault();
            db.Students.Remove(student);
            db.SaveChanges();
            return new EmptyResult();
        }
    }
}