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
    public class AGrSkillsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        // GET: Admin/AGrSkills
        public ActionResult Index()
        {
            return View(db.tbGrSkills.ToList());
        }

        // GET: Admin/AGrSkills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGrSkill tbGrSkill = db.tbGrSkills.Find(id);
            if (tbGrSkill == null)
            {
                return HttpNotFound();
            }
            return View(tbGrSkill);
        }

        // GET: Admin/AGrSkills/Create
        public ActionResult Create()
        {
            tbGrSkill item = new tbGrSkill();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbGrSkill tbGrSki)
        {
            tbGrSkill item = new tbGrSkill();
            try
            {
                item.TieuDe = tbGrSki.TieuDe;
                item.Hide = false;
                db.tbGrSkills.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AGrSkills");
            }
            catch
            {
                return View(tbGrSki);
            }
        }

        // GET: Admin/AGrSkills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGrSkill tbGrSkill = db.tbGrSkills.Find(id);
            if (tbGrSkill == null)
            {
                return HttpNotFound();
            }
            return View(tbGrSkill);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbGrSkill tbGrSki)
        {
            try
            {
                tbGrSkill item = new tbGrSkill();

                item = db.tbGrSkills.Find(tbGrSki.IDGrSkill);
                item.TieuDe = tbGrSki.TieuDe;
                item.Hide = tbGrSki.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AGrSkills");
            }
            catch (Exception ex)
            {
                return View(tbGrSki);
            }
        }

        // GET: Admin/AGrSkills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGrSkill tbGrSkill = db.tbGrSkills.Find(id);
            if (tbGrSkill == null)
            {
                return HttpNotFound();
            }
            return View(tbGrSkill);
        }

        // POST: Admin/AGrSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbGrSkill tbGrSkill = db.tbGrSkills.Find(id);
            db.tbGrSkills.Remove(tbGrSkill);
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
