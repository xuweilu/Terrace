using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Terrace.Models;
using MvcContrib.Filters;

namespace Terrace.Controllers
{
    public class PaperController : Controller
    {
        private TerraceContext db = new TerraceContext();
        public ActionResult Index(int page = 1)
        {
            return View(db.Papers.OrderBy(p => p.Id).ToPagedList(page, 10));
        }

        [ModelStateToTempData]
        public ActionResult Create()
        {
            if(TempData["LastPostModel"] == null)
            {
                return View(TempData["LastPostModel"] as Paper);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        public ActionResult Create([Bind(Include = "TrueOrFalseQuestions,SingleQuestions,MultipleQuestions")]Paper paper)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                paper.Author = db.Teachers.Where(t => t.Email == username).FirstOrDefault();
                paper.EditOn = DateTime.Now;

                var tSum = paper.TrueOrFalseQuestions.Count;
                //遍历所有判断题
                for (int i = 0; i < tSum; i++)
                {
                    paper.TrueOrFalseQuestions[i].Type = QuestionType.判断题;
                }

                var sSum = paper.SingleQuestions.Count;
                //遍历所有单选题
                for (int i = 0; i < sSum; i++)
                {
                    var sqItem = paper.SingleQuestions[i];
                    sqItem.Type = QuestionType.单选题;
                    //遍历所有单选题选项
                    for (int soId = 0; soId < 4; soId++)
                    {
                        int opId = soId + 1;        //选项名在枚举中的位置为从1开始
                        var opItem = paper.SingleQuestions[i].SingleOptions[soId];      //取得当前选项
                        opItem.OptionId = (SingleOptionId)opId;     //为当前选项制定选项名
                    }
                }

                var mSum = paper.MultipleQuestions.Count;       //得到多选题数目
                //遍历所有多选题
                for (int i = 0; i < mSum; i++)
                {
                    var mqItem = paper.MultipleQuestions[i];
                    mqItem.Type = QuestionType.多选题;
                    int moSum = mqItem.MultipleOptions.Count;       //取得当前多选题选项总数
                    //遍历当前多选题所有选项
                    for (int moId = 0; moId < moSum; moId++)
                    {
                        int opId = moId + 1;        //选项名在枚举中的位置从1开始
                        var opItem = mqItem.MultipleOptions[moId];      //取得当前选项
                        opItem.OptionId = (MultipleOptionId)opId;       //为当前选项制定选项名
                    }
                }
                db.Papers.Add(paper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["LastPostModel"] = paper;
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Paper paper = db.Papers
                .AsNoTracking()
                .Include("TrueOrFalseQuestions")
                .Include("SingleQuestions")
                .Include("MultipleQuestions")
                .Include("SingleQuestions.SingleOptions")
                .Include("MultipleQuestions.MultipleOptions")
                .FirstOrDefault(p => p.Id == id);
            if (paper == null)
                return HttpNotFound();
            return View(paper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection formCollection)
        {
            //取得要修改的对象
            var newpaper = db.Papers
                .Include("TrueOrFalseQuestions")
                .Include("SingleQuestions")
                .Include("MultipleQuestions")
                .Include("SingleQuestions.SingleOptions")
                .Include("MultipleQuestions.MultipleOptions")
                .FirstOrDefault(p => p.Id == id);

            //通过窗口更新对象中如下字段
            if (TryUpdateModel(newpaper,"", new string[] { "TrueOrFalseQuestions", "SingleQuestions", "MultipleQuestions" }))
            {
                var username = User.Identity.Name;      //得到当前登陆用户
                newpaper.Author = db.Teachers.Where(t => t.Email == username).FirstOrDefault();     //修改作者为当前登录用户
                newpaper.EditOn = DateTime.Now;
                foreach (var nt in newpaper.TrueOrFalseQuestions)
                {
                    nt.Type = QuestionType.判断题;
                }
                foreach (var ns in newpaper.SingleQuestions)
                {
                    ns.Type = QuestionType.单选题;
                    for (int i = 0; i < 4; i++) 
                    {
                        int n = i + 1;
                        ns.SingleOptions[i].OptionId = (SingleOptionId)n;
                    }
                }
                foreach (var nm in newpaper.MultipleQuestions)
                {
                    nm.Type = QuestionType.多选题;
                    int nmoSum = nm.MultipleOptions.Count;
                    for (int i = 0; i < nmoSum; i++)
                    {
                        int n = i + 1;
                        nm.MultipleOptions[i].OptionId =(MultipleOptionId)n;
                    }
                }
                db.Entry(newpaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //public ActionResult Edit([Bind(Include = "TrueOrFalseQuestions,SingleQuestions,MultipleQuestions")]Paper paper)
        //{
        //    //根据路由数值设置属性，目前不用
        //    //if (paper.ObjectState == EntityObjectState.Modified)
        //    //    db.Entry(paper).State = EntityState.Modified;

        //    //修改时间改为此时
        //    paper.EditOn = DateTime.Now;

        //    if (ModelState.IsValid)
        //    {
        //        //遍历所有判断题
        //        foreach (var tq in paper.TrueOrFalseQuestions)
        //        {
        //            db.Entry(tq).State = EntityState.Modified;
        //        }
        //        //遍历所有单选题,设置其状态为已修改
        //        foreach (var sq in paper.SingleQuestions)
        //        {
        //            db.Entry(sq).State = EntityState.Modified;
        //            //遍历该单选题中所有选项，设置其状态为已修改
        //            foreach (var opItem in sq.SingleOptions)
        //            {
        //                db.Entry(opItem).State = EntityState.Modified;
        //            }
        //        }
        //        //遍历所有多选题，设置其状态为已修改
        //        foreach (var mq in paper.MultipleQuestions)
        //        {
        //            db.Entry(mq).State = EntityState.Modified;
        //            //遍历该多选题中所有选项，设置其状态为已修改
        //            foreach (var opItem in mq.MultipleOptions)
        //            {
        //                db.Entry(opItem).State = EntityState.Modified;
        //            }
        //        }
        //        db.Entry(paper).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(paper);
        //}

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ////方式1
            //Paper paper = db.Papers.Find(id);
            //方式2
            Paper paper = db.Papers.Where(p => p.Id == id).FirstOrDefault();
            //方式3
            //var paper = from p in db.Papers
            //              where p.Id == id
            //              select p;
            //db.Entry(paper).State = EntityState.Deleted;
            //foreach (var tq in paper.TrueOrFalseQuestions)
            //{
            //    paper.TrueOrFalseQuestions.Remove(tq);
            //}
            //foreach (var sq in paper.SingleQuestions)
            //{
            //    foreach (var sqop in sq.SingleOptions)
            //    {
            //        sq.SingleOptions.Remove(sqop);
            //    }
            //    paper.SingleQuestions.Remove(sq);
            //}
            //foreach (var mq in paper.MultipleQuestions)
            //{
            //    foreach (var mqop in mq.MultipleOptions)
            //    {
            //        mq.MultipleOptions.Remove(mqop);
            //    }
            //    paper.MultipleQuestions.Remove(mq);
            //}
            db.Papers.Remove(paper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Papers
                .Include("TrueOrFalseQuestions")
                .Include("SingleQuestions")
                .Include("MultipleQuestions")
                .Include("SingleQuestions.SingleOptions")
                .Include("MultipleQuestions.MultipleOptions")
                .FirstOrDefault(p => p.Id == id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            return PartialView("Detail",paper);
        }
    }
}