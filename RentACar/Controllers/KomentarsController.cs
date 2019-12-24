using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class KomentarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Komentars
        public ActionResult Index()
        {
            var komentari = db.Komentari.Include(k => k.Korisnik).Include(k => k.Vozilo);
            return View(komentari.ToList());
        }

        // GET: Komentars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Include(k=>k.Korisnik).Include(k=>k.Vozilo).Where(k=>k.SopstvenikId==id).First();
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }
        public ActionResult DodadiKomentarZaVozilo(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name");
            
            var Vozilo = db.Vozila.Where(k =>k.VoziloId== id).First();
            if(Vozilo==null)
            {
                return HttpNotFound();
            }
            List<int> ids = new List<int>();
            ids.Add(id);


            ViewBag.VoziloId = new SelectList(ids);
            ViewBag.VoziloName = Vozilo.ModelName;



            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodadiKomentarZaVozilo(AddComment model)
        {
            Komentar komentar = new Komentar();
            komentar.VoziloId = model.VoziloId;
            komentar.Rating = model.Rating;
            komentar.Description = model.Description;
            string email = User.Identity.GetUserName();
            var korisnici = db.Korisnici.Where(k => k.email == email).First();
           komentar.KorisnikId = korisnici.KorisnikId;

            db.Komentari.Add(komentar);
            db.SaveChanges();
            return RedirectToAction("Details","Voziloes", new { id = model.VoziloId});
        }

            // GET: Komentars/Create
            public ActionResult Create()
        {
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name");
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName");
            return View();
        }

        // POST: Komentars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SopstvenikId,Description,Rating,VoziloId,KorisnikId")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                db.Komentari.Add(komentar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", komentar.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", komentar.VoziloId);
            return View(komentar);
        }

        // GET: Komentars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", komentar.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", komentar.VoziloId);
            return View(komentar);
        }

        // POST: Komentars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SopstvenikId,Description,Rating,VoziloId,KorisnikId")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komentar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", komentar.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", komentar.VoziloId);
            return View(komentar);
        }

        // GET: Komentars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        // POST: Komentars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Komentar komentar = db.Komentari.Find(id);
            db.Komentari.Remove(komentar);
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
