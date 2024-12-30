using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trimatrixlab.DBContext;
using trimatrixlab.Models;

namespace trimatrixlab.Controllers
{
    public class PartialviewController : Controller
    {
        trimatrixlab_CVEntities db = new trimatrixlab_CVEntities();
        // GET: Partialview

        //about
        public ActionResult Card()
        {
            try
            {
                AboutModel Card = new AboutModel();
                Card = (from ab in db.tbAbouts
                        select (new AboutModel
                        {
                            IDAbout = ab.IDAbout,
                            Name = ab.Name,
                            Image = ab.Image,
                            Descriptions = ab.Descriptions,
                            Email = ab.Email,
                            Web = ab.Web,
                            Address = ab.Address,
                            Phone = ab.Phone,
                            DateOfBirth = ab.DateOfBirth,
                            ZipCode = ab.ZipCode,
                            Link = ab.Link,
                            Poster = ab.Poster
                        })).FirstOrDefault();
                return PartialView(Card);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public ActionResult Social()
        {
            try
            {
                List<SocialModel> Social = (from so in db.tbSocials
                                            where so.Hide != true
                                            select (new SocialModel
                                            {
                                                IDSocial = so.IDSocial,
                                                LinkSocial = so.LinkSocial,
                                                Image = so.Image,
                                                Button = so.Button,
                                            })).ToList();
                return PartialView(Social);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public ActionResult About()
        {
            try
            {
                AboutModel About = new AboutModel();
                About = (from ab in db.tbAbouts

                         select (new AboutModel
                         {
                             Contents = ab.Contents,
                             Link = ab.Link
                         })).FirstOrDefault();
                return PartialView(About);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }

        //education
        public ActionResult Education()
        {
            try
            {
                List<EducationModel> education = (from edu in db.tbEducations
                                                  where edu.Hide != true
                                                  select (new EducationModel
                                                  {
                                                      IDEducation = edu.IDEducation,
                                                      TieuDe = edu.TieuDe,
                                                      TieuDeCon = edu.TieuDeCon,
                                                      MoTa = edu.MoTa,
                                                      NgayBatDau = edu.NgayBatDau,
                                                      NgayKetThuc = edu.NgayKetThuc,
                                                  })).ToList();
                return PartialView(education);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public ActionResult ModalsEdu()
        {
            try
            {
                List<EducationModel> modalsedu = (from edu in db.tbEducations
                                                  where edu.Hide != true
                                                  select (new EducationModel
                                                  {
                                                      IDEducation = edu.IDEducation,
                                                      TieuDe = edu.TieuDe,
                                                      NoiDung = edu.NoiDung,
                                                      NgayBatDau = edu.NgayBatDau,
                                                      NgayKetThuc = edu.NgayKetThuc,
                                                      HinhAnh = edu.HinhAnh,
                                                      Icon = edu.Icon,
                                                  })).ToList();
                return PartialView(modalsedu);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }


        //skills

        public ActionResult GrSkill()
        {
            try
            {
                List<tbGrSkill> groups = db.tbGrSkills.Where(g => g.Hide != true).ToList();

                List<Skill_GrSkillModel> skillGroups = new List<Skill_GrSkillModel>();

                foreach (tbGrSkill tbGrSkill in groups)
                {
                    GrSkillModel groupModel = new GrSkillModel
                    {
                        IDGrSkill = tbGrSkill.IDGrSkill,
                        TieuDe = tbGrSkill.TieuDe,
                        Hide = tbGrSkill.Hide
                    };

                    List<SkillModel> skills = (from skill in db.tbSkills
                                               where skill.IDGrSkill == tbGrSkill.IDGrSkill && skill.Hide != true
                                               select new SkillModel
                                               {
                                                   IDSkill = skill.IDSkill,
                                                   IDGrSkill = skill.IDGrSkill,
                                                   Ten = skill.Ten,
                                                   NoiDung = skill.NoiDung,
                                                   PhanTram = skill.PhanTram,
                                                   Hide = skill.Hide
                                               }).ToList();

                    skillGroups.Add(new Skill_GrSkillModel
                    {
                        Group = groupModel,
                        Skills = skills
                    });
                }

                return PartialView(skillGroups);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }

        }






        //experience
        public ActionResult Experience()
        {
            try
            {
                List<ExperienceModel> exper = (from exp in db.tbExperiences
                                               where exp.Hide != true
                                               select (new ExperienceModel
                                               {
                                                   IDExperience = exp.IDExperience,
                                                   TieuDe = exp.TieuDe,
                                                   TieuDeCon = exp.TieuDeCon,
                                                   MoTa = exp.MoTa,
                                                   NgayBatDau = exp.NgayBatDau,
                                                   NgayKetThuc = exp.NgayKetThuc,
                                               })).ToList();
                return PartialView(exper);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public ActionResult ModalsExp()
        {
            try
            {
                List<ExperienceModel> modalsexp = (from exp in db.tbExperiences
                                                   where exp.Hide != true
                                                   select (new ExperienceModel
                                                   {
                                                       IDExperience = exp.IDExperience,
                                                       TieuDe = exp.TieuDe,
                                                       NoiDung = exp.NoiDung,
                                                       NgayBatDau = exp.NgayBatDau,
                                                       NgayKetThuc = exp.NgayKetThuc,
                                                       HinhAnh = exp.HinhAnh,
                                                       Icon = exp.Icon,
                                                   })).ToList();
                return PartialView(modalsexp);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }

        //Portfolios
        public ActionResult Group()
        {
            try
            {
                List<GrPortfolioModel> grport = (from exp in db.tbGrPortfolios
                                                 where exp.Hide != true
                                                 select (new GrPortfolioModel
                                                 {
                                                     IDGrPortfolios = exp.IDGrPortfolios,
                                                     TieuDe = exp.TieuDe,
                                                 })).ToList();
                return PartialView(grport);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }

        public ActionResult Portifolio(int id)
        {
            try
            {
                List<Port_GrPortModel> port = (from sd in db.tbPortfolios
                                              where sd.IDGrPortfolio ==id && sd.Hide != true
                                             select (new Port_GrPortModel
                                             {
                                                 IDGrPortfolio = sd.IDGrPortfolio, 
                                                 IDPortfolio = sd.IDPortfolio,
                                                 TieuDe = sd.TieuDe,
                                                 Portfolio = sd.Portfolio,
                                                 Image = sd.Image,
                                                 Link = sd.Link,
                                             })).ToList();
                return PartialView(port);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }



        public ActionResult GrPort()
        {
            try
            {
                List<GrPortfolioModel> grport = (from exp in db.tbGrPortfolios
                                                 where exp.Hide != true
                                                 select (new GrPortfolioModel
                                                 {
                                                     IDGrPortfolios = exp.IDGrPortfolios,
                                                     TieuDe = exp.TieuDe,
                                                 })).ToList();
                return PartialView(grport);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
            //try
            //{
            //    List<tbGrPortfolio> groups = db.tbGrPortfolios.Where(g => g.Hide != true).ToList();

            //    List<Port_GrPortModel> portGroups = new List<Port_GrPortModel>();

            //    foreach (tbGrPortfolio tbGrPortfolio in groups)
            //    {
            //        GrPortfolioModel groupModel = new GrPortfolioModel
            //        {
            //            IDGrPortfolios = tbGrPortfolio.IDGrPortfolios,
            //            TieuDe = tbGrPortfolio.TieuDe,
            //            Hide = tbGrPortfolio.Hide
            //        };

            //        List<PortfolioModel> port = (from por in db.tbPortfolios
            //                                   where por.IDGrPortfolio == tbGrPortfolio.IDGrPortfolios && por.Hide != true
            //                                   select new PortfolioModel
            //                                   {
            //                                       IDPortfolio = por.IDPortfolio,
            //                                       IDGrPortfolio = por.IDGrPortfolio,
            //                                       TieuDe = por.TieuDe,
            //                                       Portfolio = por.Portfolio,
            //                                       Image = por.Image,
            //                                       Link = por.Link,
            //                                       Hide = por.Hide
            //                                   }).ToList();

            //        portGroups.Add(new Port_GrPortModel
            //        {
            //            Group = groupModel,
            //            Portfolios = port
            //        });
            //    }

            //    return PartialView(portGroups);
            //}
            //catch (Exception ex)
            //{
            //    return Redirect("/not-found");
            //}

        }

        //Interest
        public ActionResult Interest()
        {
            try
            {
                AboutModel Int = new AboutModel();
                Int = (from ab in db.tbAbouts
                       select (new AboutModel
                       {
                           MoTaInterest = ab.MoTaInterest
                       })).FirstOrDefault();
                return PartialView(Int);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public ActionResult InteresIcon()
        {
            try
            {
                List<InterestModel> intIcon = (from inte in db.tbInterests
                                               where inte.Hide != true
                                               select (new InterestModel
                                               {
                                                   Icon = inte.Icon,
                                                   TenIcon = inte.TenIcon,
                                                   Background = inte.Background,
                                               })).ToList();
                return PartialView(intIcon);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }



        //Testimonials
        public ActionResult Testimonials()
        {
            try
            {
                List<TestimonialModel> test = (from tes in db.tbTestimonials
                                               where tes.Hide != true
                                               select (new TestimonialModel
                                               {
                                                   TieuDe = tes.TieuDe,
                                                   Ten = tes.Ten,
                                                   MoTa = tes.MoTa,
                                                   NoiDung = tes.NoiDung,
                                                   HinhAnh = tes.HinhAnh
                                               })).ToList();
                return PartialView(test);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public ActionResult SliderIcons()
        {
            try
            {
                List<SliderIconModel> slider = (from sli in db.tbSliderIcons
                                                where sli.Hide != true
                                                select (new SliderIconModel
                                                {
                                                    TieuDe = sli.TieuDe,
                                                    HinhAnh = sli.HinhAnh
                                                })).ToList();
                return PartialView(slider);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }

        //Story
        public ActionResult Stories()
        {
            try
            {
                List<StoryModel> story = (from sto in db.tbStories
                                          where sto.Hide != true
                                          select (new StoryModel
                                          {
                                              IDStory = sto.IDStory,
                                              Ten = sto.Ten,
                                              NgayDang = sto.NgayDang,
                                              TieuDe = sto.TieuDe,
                                              NoiDung = sto.NoiDung,
                                              HinhAnh = sto.HinhAnh,
                                              Link = sto.Link
                                          })).ToList();
                return PartialView(story);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }





        //contact
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(ContactModel tbCon)
        {
            try
            {
                var contact = new tbContact
                {
                    Ten = tbCon.Ten,
                    Email = tbCon.Email,
                    Phone = tbCon.Phone,
                    NoiDung = tbCon.NoiDung
                };
                db.tbContacts.Add(contact);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            catch
            {
                return PartialView(tbCon);


            }

        }
        [HttpPost]
        public JsonResult GetJson(tbContact obj)
        {
            tbContact user = new tbContact();
            try
            {
                user = new tbContact
                {
                    Ten = obj.Ten,
                    Email = obj.Email,
                    Phone = obj.Phone,
                    NoiDung = obj.NoiDung
                };
                db.tbContacts.Add(user);
                db.SaveChanges();
            }
            catch
            {
                user = new tbContact();
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}