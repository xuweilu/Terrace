using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Terrace.Models;

namespace Terrace.Controllers
{
    public class AccountController : Controller
    {
        private TerraceContext db = new TerraceContext();
        private PasswordHasher passwordHasher = new PasswordHasher();
        //注册界面
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "MemberRole,Email,Password,Name")]Member member)
        {
            if (member.MemberRole == Models.Roles.学生)
            {
                var chk_member = db.Students.Where(s => s.Email == member.Email).FirstOrDefault();
                if (chk_member != null) { ModelState.AddModelError("Email", "您输入的Email已经有人注册过了！"); }
                if (ModelState.IsValid)
                {
                    Student student = new Student();
                    student.MemberRole = Models.Roles.学生;
                    student.Email = member.Email;
                    student.Password = passwordHasher.HashPassword(member.Password);
                    student.Name = member.Name;
                    student.RegisterOn = DateTime.Now;
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                    return View();
            }
            else if (member.MemberRole == Models.Roles.教师)
            {
                var chk_member = db.Teachers.Where(t => t.Email == member.Email).FirstOrDefault();
                if (chk_member != null) { ModelState.AddModelError("Email", "您输入的Email已经有人注册过了！"); }
                if (ModelState.IsValid)
                {
                    Teacher teacher = new Teacher();
                    teacher.MemberRole = Models.Roles.教师;
                    teacher.Email = member.Email;
                    teacher.Password = passwordHasher.HashPassword(member.Password);
                    teacher.Name = member.Name;
                    teacher.RegisterOn = DateTime.Now;
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else return View();
            }
            else
            {
                ModelState.AddModelError("MemberRole", "请选择你的身份");
                return View();
            }
        }

        //显示会员登录界面
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Roles role,string email, string password, string returnUrl)
        {
            if (role == Models.Roles.学生)
            {
                var hash_pw = db.Students.Where(s => s.Email == email).Select(s => s.Password).FirstOrDefault();
                //记录用户名
                var username = db.Students.Where(s => s.Email == email).Select(s => s.Email).FirstOrDefault();
                PasswordVerificationResult ValidateUser = passwordHasher.VerifyHashedPassword(hash_pw, password);
                if (ValidateUser == PasswordVerificationResult.Success)
                {
                    //添加该用户名到验证
                    FormsAuthentication.SetAuthCookie(username, false);
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else {
                    ModelState.AddModelError("", "您输入的账号或者密码错误！");
                    return View();
                }
            }
            else if (role == Models.Roles.教师)
            {
                var hash_pw = db.Teachers.Where(t => t.Email == email).Select(t => t.Password).FirstOrDefault();
                var username = db.Teachers.Where(t => t.Email == email).Select(t => t.Email).FirstOrDefault();
                PasswordVerificationResult ValidateUser = passwordHasher.VerifyHashedPassword(hash_pw, password);
                if (ValidateUser == PasswordVerificationResult.Success)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else {
                    ModelState.AddModelError("", "您输入的账号或者密码错误！");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "必须选择身份！");
                return View();
            }
        }
        
        //运行会员注销
        public ActionResult Logout()
        {
            //清除窗体验证的Cookies
            FormsAuthentication.SignOut();
            //清除所有session信息
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}