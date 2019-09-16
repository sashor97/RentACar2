using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class RezervacijasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rezervacijas
        public ActionResult Index()
        {
            var rezervacii = db.Rezervacii.Include(r => r.Korisnik).Include(r => r.Vozilo);
            return View(rezervacii.ToList());
        }

        // GET: Rezervacijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacii.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacijas/Create
        public ActionResult Create()
        {
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name");
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
            Rezervacija rezervacija = db.Rezervacii.Find(id);
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
            Rezervacija rezervacija = db.Rezervacii.Find(id);
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
