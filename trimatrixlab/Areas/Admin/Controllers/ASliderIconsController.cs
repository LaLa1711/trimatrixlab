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
    public class ASliderIconsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/ASliderIcons/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        // GET: Admin/ASliderIcons
        public ActionResult Index()
        {
            return View(db.tbSliderIcons.ToList());
        }

        // GET: Admin/ASliderIcons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSliderIcon tbSliderIcon = db.tbSliderIcons.Find(id);
            if (tbSliderIcon == null)
            {
                return HttpNotFound();
            }
            return View(tbSliderIcon);
        }

        // GET: Admin/ASliderIcons/Create
        public ActionResult Create()
        {
            tbSliderIcon item = new tbSliderIcon();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbSliderIcon tbSli, HttpPostedFileBase file)
        {
            tbSliderIcon item = new tbSliderIcon();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbSli.HinhAnh;
                }
                item.TieuDe = tbSli.TieuDe;
                item.Hide = false;
                db.tbSliderIcons.Add(item);
                db.SaveChanges();
                return Redirect("/admin/ASliderIcons");
            }
            catch
            {
                return View(tbSli);
            }
        }

        // GET: Admin/ASliderIcons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSliderIcon tbSliderIcon = db.tbSliderIcons.Find(id);
            if (tbSliderIcon == null)
            {
                return HttpNotFound();
            }
            return View(tbSliderIcon);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbSliderIcon tbSli, HttpPostedFileBase EditHinhAnh)
        {
            try
            {
                tbSliderIcon item = new tbSliderIcon();

                item = db.tbSliderIcons.Find(tbSli.IDSliderIcon);
                if (EditHinhAnh != null)
                {
                    item.HinhAnh = UploadImage(EditHinhAnh);
                }
                item.TieuDe = tbSli.TieuDe;
                item.Hide = tbSli.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/ASliderIcons");
            }
            catch (Exception ex)
            {
                return View(tbSli);
            }
        }

        // GET: Admin/ASliderIcons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSliderIcon tbSliderIcon = db.tbSliderIcons.Find(id);
            if (tbSliderIcon == null)
            {
                return HttpNotFound();
            }
            return View(tbSliderIcon);
        }

        // POST: Admin/ASliderIcons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSliderIcon tbSliderIcon = db.tbSliderIcons.Find(id);
            db.tbSliderIcons.Remove(tbSliderIcon);
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
