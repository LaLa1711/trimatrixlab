using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.App_Start;
using trimatrixlab.DBContext;
using static System.Net.WebRequestMethods;

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class ATestimonialsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/ATestimonials/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        private string fileName = "";
        public string UploadImage(HttpPostedFileBase upload)
        {
            fileName = Path.GetFileName(upload.FileName);
            fileName = Processing.UrlImages(fileName);
            bool exsits = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exsits)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
            var path = Path.Combine(Server.MapPath(pathFile), fileName);
            upload.SaveAs(path);
            return pathFile + fileName;
        }
        #endregion

        #endregion

        // GET: Admin/ATestimonials
        public ActionResult Index()
        {
            return View(db.tbTestimonials.ToList());
        }

        // GET: Admin/ATestimonials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTestimonial tbTestimonial = db.tbTestimonials.Find(id);
            if (tbTestimonial == null)
            {
                return HttpNotFound();
            }
            return View(tbTestimonial);
        }

        // GET: Admin/ATestimonials/Create
        public ActionResult Create()
        {
            tbTestimonial item = new tbTestimonial();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbTestimonial tbTes, HttpPostedFileBase file)
        {
            tbTestimonial item = new tbTestimonial();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);                
                }
                else
                {
                    item.HinhAnh = tbTes.HinhAnh;
                }

                item.TieuDe = tbTes.TieuDe;
                item.Ten = tbTes.Ten;
                item.MoTa = tbTes.MoTa;
                item.NoiDung = tbTes.NoiDung;
                item.Hide = false;
                db.tbTestimonials.Add(item);
                db.SaveChanges();
                return Redirect("/admin/ATestimonials");
            }
            catch
            {
                return View(tbTes);
            }
        }

        // GET: Admin/ATestimonials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTestimonial tbTestimonial = db.tbTestimonials.Find(id);
            if (tbTestimonial == null)
            {
                return HttpNotFound();
            }
            return View(tbTestimonial);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbTestimonial tbTes, HttpPostedFileBase Editfile)
        {
            try
            {
                tbTestimonial item = new tbTestimonial();

                item = db.tbTestimonials.Find(tbTes.IDTestimonial);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.TieuDe = tbTes.TieuDe;
                item.Ten = tbTes.Ten;
                item.MoTa = tbTes.MoTa;
                item.NoiDung = tbTes.NoiDung;
                item.Hide = tbTes.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/ATestimonials");
            }
            catch (Exception ex)
            {
                return View(tbTes);
            }
        }

        // GET: Admin/ATestimonials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTestimonial tbTestimonial = db.tbTestimonials.Find(id);
            if (tbTestimonial == null)
            {
                return HttpNotFound();
            }
            return View(tbTestimonial);
        }

        // POST: Admin/ATestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTestimonial tbTestimonial = db.tbTestimonials.Find(id);
            db.tbTestimonials.Remove(tbTestimonial);
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
