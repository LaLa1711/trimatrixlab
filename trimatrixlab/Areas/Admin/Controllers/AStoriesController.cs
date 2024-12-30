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
    public class AStoriesController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/AStories/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        // GET: Admin/AStories
        public ActionResult Index()
        {
            return View(db.tbStories.ToList());
        }

        // GET: Admin/AStories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbStory tbStory = db.tbStories.Find(id);
            if (tbStory == null)
            {
                return HttpNotFound();
            }
            return View(tbStory);
        }

        // GET: Admin/AStories/Create
        public ActionResult Create()
        {
            tbStory item = new tbStory();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbStory tbSto, HttpPostedFileBase file)
        {
            tbStory item = new tbStory();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbSto.HinhAnh;
                }
                item.Ten = tbSto.Ten;
                item.NgayDang = tbSto.NgayDang;
                item.TieuDe = tbSto.TieuDe;
                item.NoiDung = tbSto.NoiDung;
                item.Link = tbSto.Link;
                item.Hide = false;
                db.tbStories.Add(item);
                db.SaveChanges();
                return Redirect("/admin/AStories");
            }
            catch
            {
                return View(tbSto);
            }
        }

        // GET: Admin/AStories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbStory tbStory = db.tbStories.Find(id);
            if (tbStory == null)
            {
                return HttpNotFound();
            }
            return View(tbStory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbStory tbSto, HttpPostedFileBase Editfile)
        {
            try
            {
                tbStory item = new tbStory();

                item = db.tbStories.Find(tbSto.IDStory);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.TieuDe = tbSto.TieuDe;
                item.NoiDung = tbSto.NoiDung;
                item.Link = tbSto.Link;
                item.Hide = tbSto.Hide;
                item.Ten = tbSto.Ten;
                item.NgayDang = tbSto.NgayDang;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/AStories");
            }
            catch (Exception ex)
            {
                return View(tbSto);
            }
        }

        // GET: Admin/AStories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbStory tbStory = db.tbStories.Find(id);
            if (tbStory == null)
            {
                return HttpNotFound();
            }
            return View(tbStory);
        }

        // POST: Admin/AStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbStory tbStory = db.tbStories.Find(id);
            db.tbStories.Remove(tbStory);
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
