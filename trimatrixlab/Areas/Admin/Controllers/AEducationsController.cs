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
    public class AEducationsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/AEducations/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        // GET: Admin/AEducations
        public ActionResult Index()
        {
            return View(db.tbEducations.ToList());
        }

        // GET: Admin/AEducations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEducation tbEducation = db.tbEducations.Find(id);
            if (tbEducation == null)
            {
                return HttpNotFound();
            }
            return View(tbEducation);
        }

        // GET: Admin/AEducations/Create
        public ActionResult Create()
        {
            tbEducation item = new tbEducation();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbEducation tbEdu, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            tbEducation item = new tbEducation();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbEdu.HinhAnh;
                }
                if (file1 != null)
                {
                    item.Icon = UploadImage(file1);
                }
                else
                {
                    item.Icon = tbEdu.Icon;
                }
                item.TieuDe = tbEdu.TieuDe;
                item.TieuDeCon = tbEdu.TieuDeCon;
                item.MoTa = tbEdu.MoTa;
                item.NoiDung = tbEdu.NoiDung;
                item.NgayBatDau = tbEdu.NgayBatDau;
                item.NgayKetThuc = tbEdu.NgayKetThuc;
                item.Hide = false;
                db.tbEducations.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AEducations");
            }
            catch
            {
                return View(tbEdu);
            }
        }

        // GET: Admin/AEducations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEducation tbEducation = db.tbEducations.Find(id);
            if (tbEducation == null)
            {
                return HttpNotFound();
            }
            return View(tbEducation);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbEducation tbEdu, HttpPostedFileBase Editfile, HttpPostedFileBase Editfile1)
        {
            try
            {
                tbEducation item = new tbEducation();

                item = db.tbEducations.Find(tbEdu.IDEducation);
                if (Editfile != null && Editfile1 != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                    item.Icon = UploadImage(Editfile1);
                }
                item.TieuDe = tbEdu.TieuDe;
                item.TieuDeCon = tbEdu.TieuDeCon;
                item.MoTa = tbEdu.MoTa;
                item.NoiDung = tbEdu.NoiDung;
                item.NgayBatDau = tbEdu.NgayBatDau;
                item.NgayKetThuc = tbEdu.NgayKetThuc;
                item.Hide = tbEdu.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AEducations");
            }
            catch (Exception ex)
            {
                return View(tbEdu);
            }
        }

        // GET: Admin/AEducations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEducation tbEducation = db.tbEducations.Find(id);
            if (tbEducation == null)
            {
                return HttpNotFound();
            }
            return View(tbEducation);
        }

        // POST: Admin/AEducations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEducation tbEducation = db.tbEducations.Find(id);
            db.tbEducations.Remove(tbEducation);
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
