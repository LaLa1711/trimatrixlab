using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.DBContext;

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class AInterestsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        // GET: Admin/AInterests
        public ActionResult Index()
        {
            return View(db.tbInterests.ToList());
        }

        // GET: Admin/AInterests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInterest tbInterest = db.tbInterests.Find(id);
            if (tbInterest == null)
            {
                return HttpNotFound();
            }
            return View(tbInterest);
        }

        // GET: Admin/AInterests/Create
        public ActionResult Create()
        {
            tbInterest item = new tbInterest();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbInterest tbInt)
        {
            tbInterest item = new tbInterest();
            try
            {
                item.Icon = tbInt.Icon;
                item.TenIcon = tbInt.TenIcon;
                item.Background = tbInt.Background;
                item.Hide = false;
                db.tbInterests.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AInterests");
            }
            catch
            {
                return View(tbInt);
            }
        }

        // GET: Admin/AInterests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInterest tbInterest = db.tbInterests.Find(id);
            if (tbInterest == null)
            {
                return HttpNotFound();
            }
            return View(tbInterest);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbInterest tbInt)
        {
            try
            {
                tbInterest item = new tbInterest();

                item = db.tbInterests.Find(tbInt.IDInterest);
                item.Icon = tbInt.Icon;
                item.TenIcon = tbInt.TenIcon;
                item.Background = tbInt.Background;
                item.Hide = tbInt.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AInterests");
            }
            catch (Exception ex)
            {
                return View(tbInt);
            }
        }

        // GET: Admin/AInterests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInterest tbInterest = db.tbInterests.Find(id);
            if (tbInterest == null)
            {
                return HttpNotFound();
            }
            return View(tbInterest);
        }

        // POST: Admin/AInterests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInterest tbInterest = db.tbInterests.Find(id);
            db.tbInterests.Remove(tbInterest);
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
