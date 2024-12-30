using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.DBContext;

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class AGrPortfoliosController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        // GET: Admin/AGrPortfolios
        public ActionResult Index()
        {
            return View(db.tbGrPortfolios.ToList());
        }

        // GET: Admin/AGrPortfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGrPortfolio tbGrPortfolio = db.tbGrPortfolios.Find(id);
            if (tbGrPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(tbGrPortfolio);
        }

        // GET: Admin/AGrPortfolios/Create
        public ActionResult Create()
        {
            tbGrPortfolio item = new tbGrPortfolio();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbGrPortfolio tbGrPor)
        {
            tbGrPortfolio item = new tbGrPortfolio();
            try
            {
                item.TieuDe = tbGrPor.TieuDe;
                item.Hide = false;
                db.tbGrPortfolios.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AGrPortfolios");
            }
            catch
            {
                return View(tbGrPor);
            }
        }

        // GET: Admin/AGrPortfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGrPortfolio tbGrPortfolio = db.tbGrPortfolios.Find(id);
            if (tbGrPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(tbGrPortfolio);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbGrPortfolio tbGrPor)
        {
            try
            {
                tbGrPortfolio item = new tbGrPortfolio();

                item = db.tbGrPortfolios.Find(tbGrPor.IDGrPortfolios);
                item.TieuDe = tbGrPor.TieuDe;
                item.Hide = tbGrPor.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AGrPortfolios");
            }
            catch (Exception ex)
            {
                return View(tbGrPor);
            }
        }

        // GET: Admin/AGrPortfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGrPortfolio tbGrPortfolio = db.tbGrPortfolios.Find(id);
            if (tbGrPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(tbGrPortfolio);
        }

        // POST: Admin/AGrPortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbGrPortfolio tbGrPortfolio = db.tbGrPortfolios.Find(id);
            db.tbGrPortfolios.Remove(tbGrPortfolio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
