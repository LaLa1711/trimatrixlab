using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.Areas.Admin.AModels;
using trimatrixlab.DBContext;

namespace trimatrixlab.Areas.Admin.Controllers
{
    public class ASkillsController : Controller
    {
        private trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();

        // GET: Admin/ASkills
        public ActionResult Index()
        {
            return View(db.tbSkills.ToList());
        }

        // GET: Admin/ASkills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSkill tbSkill = db.tbSkills.Find(id);
            if (tbSkill == null)
            {
                return HttpNotFound();
            }
            return View(tbSkill);
        }


        public string GetSkills(int? id)
        {
            string html = "";

            List<AModelGrSkill> lst = new List<AModelGrSkill>();
            lst = (from grsk in db.tbGrSkills
                   where grsk.Hide != true
                   select (new AModelGrSkill
                   {
                       Id = grsk.IDGrSkill,
                       Name = grsk.TieuDe

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
        public String GetGrSkillName(int id)

        {
            try
            {
                return db.tbGrSkills.Find(id).TieuDe;
            }
            catch
            {
                return "";
            }
        }


        // cách 2
        public string GetSkillById(int id)
        {
            var skill = db.tbGrSkills.Find(id);
            if (skill != null)
            {
                return $"<option value='{skill.IDGrSkill}' selected>{skill.TieuDe}</option>";
            }
            return "<option value=''>----- Không có chi nhánh -----</option>";
        }

        public ActionResult GetSkill(int id)
        {
            return Json(GetSkillById(id), JsonRequestBehavior.AllowGet);
        }
        //



        // GET: Admin/ASkills/Create
        public ActionResult Create()
        {
            tbSkill item = new tbSkill();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbSkill tbSki)
        {
            tbSkill item = new tbSkill();
            try
            { 
                item.IDGrSkill = tbSki.IDGrSkill;
                item.Ten = tbSki.Ten;
                item.NoiDung = tbSki.NoiDung;
                item.PhanTram = tbSki.PhanTram;
                item.Hide = false;
                db.tbSkills.Add(item);
                db.SaveChanges();
                return Redirect("/admin/ASkills");
            }
            catch
            {
                return View(tbSki);
            }

        }

        // GET: Admin/ASkills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSkill tbSkill = db.tbSkills.Find(id);
            if (tbSkill == null)
            {
                return HttpNotFound();
            }
            return View(tbSkill);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbSkill tbSki)
        {
            try
            {
                tbSkill item = new tbSkill();

                item = db.tbSkills.Find(tbSki.IDSkill);
                item.IDGrSkill = tbSki.IDGrSkill;
                item.Ten = tbSki.Ten;
                item.NoiDung = tbSki.NoiDung;
                item.PhanTram = tbSki.PhanTram;
                item.Hide = tbSki.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/ASkills");
            }
            catch (Exception ex)
            {
                return View(tbSki);
            }
        }

        // GET: Admin/ASkills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSkill tbSkill = db.tbSkills.Find(id);
            if (tbSkill == null)
            {
                return HttpNotFound();
            }
            return View(tbSkill);
        }

        // POST: Admin/ASkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSkill tbSkill = db.tbSkills.Find(id);
            db.tbSkills.Remove(tbSkill);
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
