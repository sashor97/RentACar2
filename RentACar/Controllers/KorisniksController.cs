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
    public class KorisniksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator, User")]
        // GET: Korisniks
        // ako e so uloga User prikazi go momentalniot User inaku prikazi gi site sopstvenici
        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                string email = User.Identity.GetUserName();
                var korisnikId = db.Korisnici.Where(s => s.email == email).FirstOrDefault().KorisnikId;

                var korisnik = db.Korisnici.Where(s => s.KorisnikId == korisnikId);

                return View(korisnik.ToList());
            }

            return View(db.Korisnici.ToList());
        }

        // GET: Korisniks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisnici.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }


        /*
        // GET: Korisniks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Korisniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KorisnikId,Name,LastName,Address,Age,email")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Korisnici.Add(korisnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(korisnik);
        }
        */
        // GET: Korisniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisnici.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KorisnikId,Name,LastName,Address,Age,email")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(korisnik);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Korisniks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisnici.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Korisnik korisnik = db.Korisnici.Find(id);
            db.Korisnici.Remove(korisnik);
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
