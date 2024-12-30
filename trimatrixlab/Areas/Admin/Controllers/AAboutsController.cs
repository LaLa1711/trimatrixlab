using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using trimatrixlab.App_Start;
using trimatrixlab.DBContext;
using static System.Net.WebRequestMethods;

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class AAboutsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/aboutus/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        // GET: Admin/AAbouts
        public ActionResult Index()
        {
            return View(db.tbAbouts.ToList());
        }

        // GET: Admin/AAbouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAbout tbAbout = db.tbAbouts.Find(id);
            if (tbAbout == null)
            {
                return HttpNotFound();
            }
            return View(tbAbout);
        }

        // GET: Admin/AAbouts/Create
        public ActionResult Create()
        {
            tbAbout item = new tbAbout();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbAbout tbAbo, HttpPostedFileBase file, HttpPostedFileBase file1 )
        {
            tbAbout item = new tbAbout();
            try
            {
                if (file != null || file1 != null)
                {
                    item.Image = UploadImage(file);
                    item.Poster = UploadImage(file1);
                }
                else
                {
                    item.Image = tbAbo.Image;
                    item.Poster = tbAbo.Poster;
                }

                item.Name = tbAbo.Name;
                item.Contents = tbAbo.Contents;
                item.Email = tbAbo.Email;
                item.Web = tbAbo.Web;
                item.Address = tbAbo.Address;
                item.Phone = tbAbo.Phone;
                item.DateOfBirth = tbAbo.DateOfBirth;
                item.Descriptions = tbAbo.Descriptions;
                item.ZipCode = tbAbo.ZipCode;
                item.Link = tbAbo.Link;
                item.MoTaInterest = tbAbo.MoTaInterest;
                db.tbAbouts.Add(item);
                db.SaveChanges();
                return Redirect("/admin/aabouts");
            }
            catch
            {
                return View(tbAbo);
            }
        }

        // GET: Admin/AAbouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAbout tbAbout = db.tbAbouts.Find(id);
            if (tbAbout == null)
            {
                return HttpNotFound();
            }
            return View(tbAbout);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbAbout tbAbo, HttpPostedFileBase Editfile, HttpPostedFileBase Editfile1)
        {
            try
            {
                tbAbout item = new tbAbout();

                item = db.tbAbouts.Find(tbAbo.IDAbout);
                if (Editfile != null && Editfile1 != null)
                {
                    item.Image = UploadImage(Editfile);
                    item.Poster = UploadImage(Editfile1);
                }
                item.Name = tbAbo.Name;
                item.Contents = tbAbo.Contents;
                item.Email = tbAbo.Email;
                item.Web = tbAbo.Web;
                item.Address = tbAbo.Address;
                item.Phone = tbAbo.Phone;
                item.DateOfBirth = tbAbo.DateOfBirth;
                item.Descriptions = tbAbo.Descriptions;
                item.ZipCode = tbAbo.ZipCode;
                item.Link = tbAbo.Link;
                item.MoTaInterest = tbAbo.MoTaInterest;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/aabouts");
            }
            catch (Exception ex)
            {
                return View(tbAbo);
            }
        }

        // GET: Admin/AAbouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAbout tbAbout = db.tbAbouts.Find(id);
            if (tbAbout == null)
            {
                return HttpNotFound();
            }
            return View(tbAbout);
        }

        // POST: Admin/AAbouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAbout tbAbout = db.tbAbouts.Find(id);
            db.tbAbouts.Remove(tbAbout);
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
