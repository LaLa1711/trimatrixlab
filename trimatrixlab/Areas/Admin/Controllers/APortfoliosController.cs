using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.App_Start;
using trimatrixlab.Areas.Admin.AModels;
using trimatrixlab.DBContext;

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class APortfoliosController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();


        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/APortfolios/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        // GET: Admin/APortfolios
        public ActionResult Index()
        {
            return View(db.tbPortfolios.ToList());
        }

        // GET: Admin/APortfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPortfolio tbPortfolio = db.tbPortfolios.Find(id);
            if (tbPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(tbPortfolio);
        }

        public string GetPort(int? id)
        {
            string html = "";

            List<AModelGrPor> lst = new List<AModelGrPor>();
            lst = (from grpo in db.tbGrPortfolios
                   where grpo.Hide != true
                   select (new AModelGrPor
                   {
                       Id = grpo.IDGrPortfolios,
                       Name = grpo.TieuDe

                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> -----Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;

        }
        public String GetGrPortName(int id)

        {
            try
            {
                return db.tbGrPortfolios.Find(id).TieuDe;
            }
            catch
            {
                return "";
            }
        }

        // GET: Admin/APortfolios/Create
        public ActionResult Create()
        {
            tbPortfolio item = new tbPortfolio();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbPortfolio tbPort, HttpPostedFileBase file)
        {
            tbPortfolio item = new tbPortfolio();
            try
            {
                if (file != null)
                {
                    item.Image = UploadImage(file);
                }
                else
                {
                    item.Image = tbPort.Image;
                }
                item.Link = tbPort.Link;
                item.TieuDe = tbPort.TieuDe;
                item.IDGrPortfolio = tbPort.IDGrPortfolio;
                item.Portfolio = tbPort.Portfolio;
                item.Hide = false;
                db.tbPortfolios.Add(item);
                db.SaveChanges();
                return Redirect("/admin/APortfolios");
            }
            catch
            {
                return View(tbPort);
            }
        }

        // GET: Admin/APortfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPortfolio tbPortfolio = db.tbPortfolios.Find(id);
            if (tbPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(tbPortfolio);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbPortfolio tbPor, HttpPostedFileBase EditHinhAnh)
        {
            try
            {
                tbPortfolio item = new tbPortfolio();

                item = db.tbPortfolios.Find(tbPor.IDPortfolio);
                if (EditHinhAnh != null)
                {
                    item.Image = UploadImage(EditHinhAnh);
                }
                item.Link = tbPor.Link;
                item.TieuDe = tbPor.TieuDe;
                item.IDGrPortfolio = tbPor.IDGrPortfolio;
                item.Portfolio = tbPor.Portfolio;
                item.Hide = tbPor.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/APortfolios");
            }
            catch (Exception ex)
            {
                return View(tbPor);
            }
        }

        // GET: Admin/APortfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPortfolio tbPortfolio = db.tbPortfolios.Find(id);
            if (tbPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(tbPortfolio);
        }

        // POST: Admin/APortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPortfolio tbPortfolio = db.tbPortfolios.Find(id);
            db.tbPortfolios.Remove(tbPortfolio);
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
