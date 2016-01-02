using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Terrace.Models;

namespace Terrace.Controllers
{
    public class ExamController : Controller
    {
        private TerraceContext db = new TerraceContext();
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }
    }
}