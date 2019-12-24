using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class RezervacijasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Rezervacijas
        public ActionResult Index()
        {
            
            //string role = Roles.GetRolesForUser()[0];
            
            if (User.IsInRole("Administrator"))
            {
                var rezervacii = db.Rezervacii.Include(r => r.Korisnik).Include(r => r.Vozilo);
                return View(rezervacii.ToList());
                
            }
            else // site koi ne se administrator a treba USER ???
            {
                string email = User.Identity.GetUserName();

                var Korisnici = db.Korisnici.Where(k => k.email == email);
                var count = db.Korisnici.Where(k => k.email == email).Count();
                if (count == 0)
                {
                    return RedirectToAction("Create", "Korisniks");
                }


                var korisnik = db.Korisnici.Where(k => k.email == email).First();
                var rezervacii = db.Rezervacii.Include(r => r.Korisnik).Include(r => r.Vozilo).Where (k => k.KorisnikId == korisnik.KorisnikId);
                return View(rezervacii.ToList());
            } 

            /*
            var rezervacii = db.Rezervacii.Include(r => r.Korisnik).Include(r => r.Vozilo);

            string email = User.Identity.GetUserName();
            var Korisnici = db.Korisnici.Where(k => k.email == email);
            var count = db.Korisnici.Where(k => k.email == email).Count();
            if (count == 0)
            {
                return RedirectToAction("Create", "Korisniks");
            }

            var korisnik = db.Korisnici.Where(k => k.email == email).First();
            ViewBag.KorisnikId = korisnik.KorisnikId;

            return View(rezervacii.ToList());
               */


        }

        // GET: Rezervacijas/CreateForVozilo/1
        public ActionResult CreateForVozilo(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name");
            string email = User.Identity.GetUserName();

            var Korisnici = db.Korisnici.Where(k => k.email == email);
            var count = db.Korisnici.Where(k => k.email == email).Count();
            if (count == 0)
            {
                return RedirectToAction("Create", "Korisniks");
            }

            List<int> ids = new List<int>();
            ids.Add(id);

            
            ViewBag.VoziloId = new SelectList(ids);
            ViewBag.Poraka = "";

            return View();
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForVozilo(AddReservation model)
        {

            Rezervacija rezervacija = new Rezervacija();
           // rezervacija.denoviIznajmuvanje = model.denoviIznajmuvanje;
            rezervacija.DateFrom = Convert.ToDateTime(model.DateFrom);
            rezervacija.DateTo = Convert.ToDateTime(model.DateTo);
            rezervacija.VoziloId = model.VoziloId;
            string dif = (rezervacija.DateTo - rezervacija.DateFrom).TotalDays.ToString();
            rezervacija.denoviIznajmuvanje = Convert.ToInt32(dif);

            var vozilo = db.Vozila.Find(model.VoziloId);
            double cena = vozilo.PriceDay;

            rezervacija.total = cena * rezervacija.denoviIznajmuvanje;


            string email = User.Identity.GetUserName();
            var korisnici = db.Korisnici.Where(k => k.email == email).First();
            rezervacija.KorisnikId = korisnici.KorisnikId;



            var rezervacii = db.Rezervacii.Where(m => m.VoziloId == model.VoziloId);

            foreach(var r in rezervacii)
            {
                DateTime datumOd = r.DateFrom;
                DateTime datumDo = r.DateTo;

                DateTime momentalenDatumOd = rezervacija.DateFrom;
                DateTime momentalenDatumDo = rezervacija.DateTo;

                int result1 = DateTime.Compare(datumOd,momentalenDatumOd);
                int result2 = DateTime.Compare(datumDo, momentalenDatumOd);

                if (result1 <= 0 && result2 >= 0)
                {
                    List<int> ids = new List<int>();
                    ids.Add(model.VoziloId);


                    ViewBag.VoziloId = new SelectList(ids);
                    ViewBag.Poraka = "Veke postoi rezervacija za vneseniot termin";

                    return View();
                }

                int result3 = DateTime.Compare(datumOd, momentalenDatumDo);
                int result4 = DateTime.Compare(datumDo, momentalenDatumDo);

                if (result3 <= 0 && result4 >= 0)
                {
                    List<int> ids = new List<int>();
                    ids.Add(model.VoziloId);


                    ViewBag.VoziloId = new SelectList(ids);
                    ViewBag.Poraka = "Veke postoi rezervacija za vneseniot termin";

                    return View();
                }

                int result5 = DateTime.Compare(datumOd, momentalenDatumOd);
                int result6 = DateTime.Compare(datumDo, momentalenDatumDo);

                if (result5 >= 0 && result6 <= 0)
                {
                    List<int> ids = new List<int>();
                    ids.Add(model.VoziloId);


                    ViewBag.VoziloId = new SelectList(ids);
                    ViewBag.Poraka = "Veke postoi rezervacija za vneseniot termin";

                    return View();
                }
            }






            db.Rezervacii.Add(rezervacija);
            db.SaveChanges();
            return RedirectToAction("Index");


            //   rezervacija.denoviIznajmuvanje = 10;
            //   rezervacija.uspesnost = true;
            //   rezervacija.plateno = true;
            //   rezervacija.total = 1000;

            //   if (ModelState.IsValid)
            //   {
            //       db.Rezervacii.Add(rezervacija);
            //       db.SaveChanges();
            //       return RedirectToAction("Index");
            //   }

            //   ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", rezervacija.KorisnikId);
            //   ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", rezervacija.VoziloId);
            //   return View(rezervacija);
            return Content("Hello");
        }






        // GET: Rezervacijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacii.Include(m => m.Korisnik).Include(m => m.Vozilo).Where(m => m.RezervacijaId == id).First();
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacijas/Create
        public ActionResult Create()
        {

            //ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name");
            string email = User.Identity.GetUserName();

            var Korisnici = db.Korisnici.Where(k => k.email == email);
            var count = db.Korisnici.Where(k => k.email == email).Count();
            if (count == 0)
            {
                return RedirectToAction("Create", "Korisniks");
            }
            ViewBag.KorisnikId = new SelectList(Korisnici, "KorisnikId", "Name");
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName");

            return View();
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RezervacijaId,denoviIznajmuvanje,uspesnost,plateno,total,VoziloId,KorisnikId,DateFrom,DateTo")] Rezervacija rezervacija)
        {

           // rezervacija.denoviIznajmuvanje = 10;
           // rezervacija.uspesnost = true;
           // rezervacija.plateno = true;
           // rezervacija.total = 1000;

            if (ModelState.IsValid)
            {
                db.Rezervacii.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", rezervacija.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", rezervacija.VoziloId);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacii.Include(m => m.Korisnik).Include(m => m.Vozilo).Where(m => m.RezervacijaId == id).First(); ;
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", rezervacija.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", rezervacija.VoziloId);
            return View(rezervacija);
        }

        // POST: Rezervacijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RezervacijaId,denoviIznajmuvanje,uspesnost,plateno,total,VoziloId,KorisnikId,DateFrom,DateTo")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervacija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", rezervacija.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", rezervacija.VoziloId);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacii.Include(m => m.Korisnik).Include(m=> m.Vozilo).Where(m => m.RezervacijaId == id).First();
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervacija rezervacija = db.Rezervacii.Find(id);
            db.Rezervacii.Remove(rezervacija);
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
