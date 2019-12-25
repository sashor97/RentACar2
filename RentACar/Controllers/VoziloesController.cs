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
    public class VoziloesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Voziloes
        public ActionResult Index()
        {
            var vozila = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik);
            return View(vozila.ToList());
        }

        // GET: Voziloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vozilo vozilo = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik).Include(v => v.Komentari).First(v => v.VoziloId == id);
            if (vozilo == null)
            {
                return HttpNotFound();
            }
            return View(vozilo);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Voziloes/Create
        public ActionResult Create()
        {
            ViewBag.KategorijaId = new SelectList(db.Kategorii, "KategorijaId", "Name");
            ViewBag.ProizvoditelId = new SelectList(db.Proizvoditeli, "ProizvoditelId", "Name");
            ViewBag.SopstvenikId = new SelectList(db.Sopstvenici, "SopstvenikId", "Name");
            return View();
        }

        // POST: Voziloes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoziloId,ModelName,ImageUrl,Location,PriceDay,KategorijaId,ProizvoditelId,SopstvenikId")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                db.Vozila.Add(vozilo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategorijaId = new SelectList(db.Kategorii, "KategorijaId", "Name", vozilo.KategorijaId);
            ViewBag.ProizvoditelId = new SelectList(db.Proizvoditeli, "ProizvoditelId", "Name", vozilo.ProizvoditelId);
            ViewBag.SopstvenikId = new SelectList(db.Sopstvenici, "SopstvenikId", "Name", vozilo.SopstvenikId);
            return View(vozilo);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Voziloes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vozilo vozilo = db.Vozila.Find(id);
            if (vozilo == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaId = new SelectList(db.Kategorii, "KategorijaId", "Name", vozilo.KategorijaId);
            ViewBag.ProizvoditelId = new SelectList(db.Proizvoditeli, "ProizvoditelId", "Name", vozilo.ProizvoditelId);
            ViewBag.SopstvenikId = new SelectList(db.Sopstvenici, "SopstvenikId", "Name", vozilo.SopstvenikId);
            return View(vozilo);
        }

        // POST: Voziloes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoziloId,ModelName,ImageUrl,Location,PriceDay,KategorijaId,ProizvoditelId,SopstvenikId")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vozilo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategorijaId = new SelectList(db.Kategorii, "KategorijaId", "Name", vozilo.KategorijaId);
            ViewBag.ProizvoditelId = new SelectList(db.Proizvoditeli, "ProizvoditelId", "Name", vozilo.ProizvoditelId);
            ViewBag.SopstvenikId = new SelectList(db.Sopstvenici, "SopstvenikId", "Name", vozilo.SopstvenikId);
            return View(vozilo);
        }

        [Authorize(Roles = "Administrator, Owner")]
        // GET: Voziloes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vozilo vozilo = db.Vozila.Find(id);
            if (vozilo == null)
            {
                return HttpNotFound();
            }
            return View(vozilo);
        }

        // POST: Voziloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vozilo vozilo = db.Vozila.Find(id);
            db.Vozila.Remove(vozilo);
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
