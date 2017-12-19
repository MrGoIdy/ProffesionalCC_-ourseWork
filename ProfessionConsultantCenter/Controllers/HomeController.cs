using Microsoft.AspNet.Identity;
using ProfessionConsultantCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProfessionConsultantCenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ProfessionConsultantDB db = new ProfessionConsultantDB())
            {

            }
            return View();
        }

        public ActionResult Consultation(int id)
        {
            ProfessionConsultantDB db = new ProfessionConsultantDB();
            Consultation consultation;
            consultation = db.Consultations.FirstOrDefault(x => x.Id == id);    
                var users = db.Users.Where(x => x.Consultations.FirstOrDefault(y => y.Id == id) != null);
                if (consultation == null)
                {
                    return HttpNotFound();
                }
            
            consultation.Users = users.ToList();
            return View(consultation);

        }

        [Authorize]
        public ActionResult RegistryUserInConsultation(int id)
        {
            using(ProfessionConsultantDB db = new ProfessionConsultantDB())
            {
                try
                {
                    db.Consultations.FirstOrDefault(x=>x.Id == id).Users.Add(db.Users.FirstOrDefault(x => x.Login == User.Identity.Name));
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    return HttpNotFound();
                }
            }
            return RedirectToAction("Consultation", new { id = id });
        }

        [Authorize]
        public ActionResult RemoveUserInConsultation(int id)
        {
            using (ProfessionConsultantDB db = new ProfessionConsultantDB())
            {
                try
                {
                   db.Consultations.FirstOrDefault(x => x.Id == id).Users.Remove(db.Users.FirstOrDefault(x => x.Login == User.Identity.Name));

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            return RedirectToAction("Consultation", new { id = id });
        }

        public ActionResult Admin()
        {
            User user = new ProfessionConsultantCenter.Models.ProfessionConsultantDB().Users.FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user != null)
            {
                if (user.Role.Name == "Admin")
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult UsersTable(int id)
        {
            IEnumerable<User> users;
            using (ProfessionConsultantDB db = new ProfessionConsultantDB())
            {
                users = db.Users.Where(x => x.Consultations.FirstOrDefault(y => y.Id == id) != null).ToList();
            }
            return PartialView(users);

        }

        [Authorize]
        public ActionResult AddConsultation(Consultation consultation)
        {
            using (ProfessionConsultantDB db = new ProfessionConsultantDB())
            {
                db.Consultations.Add(consultation);
                db.SaveChanges();
            }
            return RedirectToAction("Calendar");
        }

        public ActionResult RemoveConsultation(int consultationId)
        {
            using (ProfessionConsultantDB db = new ProfessionConsultantDB())
            {
                try
                {
                    db.Consultations.Remove(db.Consultations.FirstOrDefault(x => x.Id == consultationId));
                    db.SaveChanges();
                }
                catch { }
            }
                return RedirectToAction("Admin");
        }

        public ActionResult RemoveConsultant(int consultantId)
            
        {
            using (ProfessionConsultantDB db = new ProfessionConsultantDB())
            {
                try
                {
                    User user = db.Users.FirstOrDefault(x => x.Id == consultantId);
                    user.Role = db.Roles.FirstOrDefault(x => x.Name == "User");              
                    db.SaveChanges();
                }
                catch { }
            }
            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult Searching()
        {
            return View();
        }


        [Authorize]
        public ActionResult ListOfConsultation(string search)
        {
            if (search != null)
            {
                List<Consultation>  c1= new ProfessionConsultantDB().Consultations.OrderBy(x => x.Date).ToList();
            List<Consultation> c2= c1.FindAll(x => x.Name.IndexOf(search) != -1
      || x.Time.IndexOf(search) != -1
       || x.Adres.IndexOf(search) != -1 ||
        x.Date.ToShortDateString().ToString().IndexOf(search) != -1);
                return PartialView(c2);
            } else
            {
                return PartialView(new ProfessionConsultantDB().Consultations.OrderBy(x => x.Date).ToList());
            }
         
        }


        public ActionResult ConsultationInfo(int id)
        {
            ProfessionConsultantDB db = new ProfessionConsultantDB();
            Consultation consultation;
            consultation = db.Consultations.FirstOrDefault(x => x.Id == id);
            var users = db.Users.Where(x => x.Consultations.FirstOrDefault(y => y.Id == id) != null);
            if (consultation == null)
            {
                return HttpNotFound();
            }

            consultation.Users = users.ToList();
            return PartialView(consultation);
        }

        public ActionResult ConsultationList(int page)
        {
            try {
                return PartialView(new ProfessionConsultantDB().Consultations.Where(x=>x.Date>=DateTime.Now).ToList().GetRange(page * 10, 10).OrderBy(x => x.Date));
            }
            catch
            {
                return PartialView(new ProfessionConsultantDB().Consultations.Where(x => x.Date >= DateTime.Now).ToList().OrderBy(x => x.Date));
            }
        }

        public ActionResult ConsultantList(int page)//////////////////////////////////////////////////////
        {
            try {
                return PartialView(new ProfessionConsultantDB().Users.Where(x => x.Role.Name == "Consultant").ToList().GetRange(page * 10, 10).OrderBy(x => x.Surname));
            
             }
            catch
            {
                return PartialView(new ProfessionConsultantDB().Users.Where(x => x.Role.Name == "Consultant").OrderBy(x => x.Surname));

            }
        }

        [Authorize]
        public ActionResult RegisterConsultation()
        {
            return View(new Consultation());
        }

        [Authorize]
        public ActionResult Calendar(DateTime date)
        {         
            ViewBag.NextDate = date.AddMonths(1);
            ViewBag.PreviousDate = date.AddMonths(-1);
            ViewBag.Date = date;
            ViewBag.Selected = date.Month+"/"+date.Year;
            return View(new ProfessionConsultantDB().Consultations.OrderBy(x=>x.Time).Where(x=>x.Date.Month == date.Month && x.Date.Year == date.Year));
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";      
            return View();
        }



        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

    }
}