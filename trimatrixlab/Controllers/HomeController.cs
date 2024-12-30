using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.DBContext;

namespace trimatrixlab.Controllers
{
    public class HomeController : Controller
    {

        trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();
        public ActionResult Index()
        {
            Session["FullName"] = db.tbAbouts.FirstOrDefault().Name;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}