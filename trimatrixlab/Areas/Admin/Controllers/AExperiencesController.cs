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

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class AExperiencesController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();


        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/AExperiences/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        // GET: Admin/AExperiences
        public ActionResult Index()
        {
            return View(db.tbExperiences.ToList());
        }

        // GET: Admin/AExperiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExperience tbExperience = db.tbExperiences.Find(id);
            if (tbExperience == null)
            {
                return HttpNotFound();
            }
            return View(tbExperience);
        }

        // GET: Admin/AExperiences/Create
        public ActionResult Create()
        {
            tbExperience item = new tbExperience();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbExperience tbExp, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            tbExperience item = new tbExperience();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbExp.HinhAnh;
                }
                if (file1 != null)
                {
                    item.Icon = UploadImage(file1);
                }
                else
                {
                    item.Icon = tbExp.Icon;
                }
                item.TieuDe = tbExp.TieuDe;
                item.TieuDeCon = tbExp.TieuDeCon;
                item.MoTa = tbExp.MoTa;
                item.NoiDung = tbExp.NoiDung;
                item.NgayBatDau = tbExp.NgayBatDau;
                item.NgayKetThuc = tbExp.NgayKetThuc;
                item.Hide = false;
                db.tbExperiences.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AExperiences");
            }
            catch
            {
                return View(tbExp);
            }
        }

        // GET: Admin/AExperiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExperience tbExperience = db.tbExperiences.Find(id);
            if (tbExperience == null)
            {
                return HttpNotFound();
            }
            return View(tbExperience);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbExperience tbExp, HttpPostedFileBase Editfile, HttpPostedFileBase Editfile1)
        {
            try
            {
                tbExperience item = new tbExperience();

                item = db.tbExperiences.Find(tbExp.IDExperience);
                if (Editfile != null && Editfile1 != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                    item.Icon = UploadImage(Editfile1);
                }
                item.TieuDe = tbExp.TieuDe;
                item.TieuDeCon = tbExp.TieuDeCon;
                item.MoTa = tbExp.MoTa;
                item.NoiDung = tbExp.NoiDung;
                item.NgayBatDau = tbExp.NgayBatDau;
                item.NgayKetThuc = tbExp.NgayKetThuc;
                item.Hide = tbExp.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AExperiences");
            }
            catch (Exception ex)
            {
                return View(tbExp);
            }
        }

        // GET: Admin/AExperiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbExperience tbExperience = db.tbExperiences.Find(id);
            if (tbExperience == null)
            {
                return HttpNotFound();
            }
            return View(tbExperience);
        }

        // POST: Admin/AExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbExperience tbExperience = db.tbExperiences.Find(id);
            db.tbExperiences.Remove(tbExperience);
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
