using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedisSession.Controllers
{
    [Serializable]
    public class MySessionData
    {
        public string UserName { get; set; }
        public int Age { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["UserName"] = "JohnDoe";
            Session["UserName1"] = "JohnDoe12121";
            Session["MyData"] = new MySessionData { UserName = "Michael", Age = 30 };

            // Đọc dữ liệu từ Session
            var myData = Session["MyData"] as dynamic;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var a = Session["UserName"];
            var b = Session["UserName1"];
            var myData = Session["MyData"];
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}