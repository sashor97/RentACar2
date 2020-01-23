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
    public class KategorijasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kategorijas
        public ActionResult Index()
        {
            if (User.IsInRole("Owner"))
            {
                string email = User.Identity.GetUserName();
                var sopstvenikId = db.Sopstvenici.Where(s => s.email == email).FirstOrDefault().SopstvenikId;

                ViewBag.SopstvenikId = sopstvenikId;
            }
           
            
            return View(db.Kategorii.Include(k => k.Vozila).ToList());
        }

        // GET: Kategorijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorii.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }
        public ActionResult PrikaziVozilaKategorija(int ?id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (User.IsInRole("Owner"))
            {
                string email = User.Identity.GetUserName();
                var sopstvenikId = db.Sopstvenici.Where(s => s.email == email).FirstOrDefault().SopstvenikId;

                ViewBag.SopstvenikId = sopstvenikId;
            }

            Kategorija kategorija = db.Kategorii.Include(k => k.Vozila).Where(k => k.KategorijaId == id).First();
            if(kategorija==null)
            {
                return HttpNotFound();
            }

            var vozila = kategorija.Vozila;

            return View(kategorija);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Kategorijas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategorijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategorijaId,Name,Type")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Kategorii.Add(kategorija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategorija);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Kategorijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorii.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategorijaId,Name,Type")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorija);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Kategorijas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorii.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategorija kategorija = db.Kategorii.Find(id);
            db.Kategorii.Remove(kategorija);
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
