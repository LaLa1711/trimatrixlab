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
    public class ASocialsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        // GET: Admin/ASocials
        public ActionResult Index()
        {
            return View(db.tbSocials.ToList());
        }

        // GET: Admin/ASocials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSocial tbSocial = db.tbSocials.Find(id);
            if (tbSocial == null)
            {
                return HttpNotFound();
            }
            return View(tbSocial);
        }

        // GET: Admin/ASocials/Create
        public ActionResult Create()
        {
            tbSocial item = new tbSocial();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbSocial tbSoc)
        {
            tbSocial item = new tbSocial();
            try
            {
                item.Image = tbSoc.Image;
                item.LinkSocial = tbSoc.LinkSocial;
                item.Button = tbSoc.Button;
                item.Hide = false;
                db.tbSocials.Add(item);
                db.SaveChanges();
                return Redirect("/admin/aSocials");
            }
            catch
            {
                return View(tbSoc);
            }
        }

        // GET: Admin/ASocials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSocial tbSocial = db.tbSocials.Find(id);
            if (tbSocial == null)
            {
                return HttpNotFound();
            }
            return View(tbSocial);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbSocial tbSoc)
        {
            try
            {
                tbSocial item = new tbSocial();

                item = db.tbSocials.Find(tbSoc.IDSocial);
                item.Image = tbSoc.Image;
                item.LinkSocial = tbSoc.LinkSocial;
                item.Button = tbSoc.Button;
                item.Hide = tbSoc.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/ASocials");
            }
            catch (Exception ex)
            {
                return View(tbSoc);
            }
        }

        // GET: Admin/ASocials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSocial tbSocial = db.tbSocials.Find(id);
            if (tbSocial == null)
            {
                return HttpNotFound();
            }
            return View(tbSocial);
        }

        // POST: Admin/ASocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSocial tbSocial = db.tbSocials.Find(id);
            db.tbSocials.Remove(tbSocial);
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
