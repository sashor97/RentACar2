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
    public class VoziloesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Voziloes
        public ActionResult Index(int? sortNumber=null)
        {
            
            if (User.IsInRole("Owner"))
            {
                string email = User.Identity.GetUserName();

                var Sopstvenici = db.Sopstvenici.Where(k => k.email == email).First();
                var count = db.Sopstvenici.Where(k => k.email == email).Count();
                if (count == 0)
                {
                    return RedirectToAction("Create", "Korisniks");
                }
                if (sortNumber.HasValue)
                {
                    var vozilaSorted = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik).Where(v => v.SopstvenikId == Sopstvenici.SopstvenikId).OrderBy(x=>x.PriceDay);
                    return View(sort(db,sortNumber));
                }
                var vozila = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik).Where(v => v.SopstvenikId == Sopstvenici.SopstvenikId);
                return View(vozila.ToList());
            }
            else
            {
                if (sortNumber.HasValue)
                {
                    //sort(db, sortNumber);
                    //var vozilaSorted = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik).OrderBy(x => x.PriceDay);
                    return View(sort(db,sortNumber));
                }
                var vozila = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik);
                return View(vozila.ToList());
             }
        }

        private List<Vozilo> sort(ApplicationDbContext db, int? sortNumber)
        {
            switch (sortNumber)
            {
                case 1:
                    return db.Vozila
                        .Include(v => v.Kategorija)
                        .Include(v => v.Proizvoditel)
                        .Include(v => v.Sopstvenik)
                        .OrderBy(x => x.PriceDay)
                        .ToList();
                case 2:
                    return db.Vozila
                        .Include(v => v.Kategorija)
                        .Include(v => v.Proizvoditel)
                        .Include(v => v.Sopstvenik)
                        .OrderByDescending(x => x.PriceDay)
                        .ToList();
                case 3:
                    return db.Vozila
                        .Include(v => v.Kategorija)
                        .Include(v => v.Proizvoditel)
                        .Include(v => v.Sopstvenik)
                        .OrderBy(x => x.Proizvoditel.Name)
                        .ToList();
                case 4:
                    return db.Vozila
                        .Include(v => v.Kategorija)
                        .Include(v => v.Proizvoditel)
                        .Include(v => v.Sopstvenik)
                        .OrderBy(x => x.ModelName)
                        .ToList();
                   
                case 5:
                    return db.Vozila
                        .Include(v => v.Kategorija)
                        .Include(v => v.Proizvoditel)
                        .Include(v => v.Sopstvenik)
                        .OrderByDescending(x => x.Location)
                        .ToList();
                default:
                    return db.Vozila
                        .Include(v => v.Kategorija)
                        .Include(v => v.Proizvoditel)
                        .Include(v => v.Sopstvenik)
                        .ToList();
            }
            
        }

        // GET: Voziloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vozilo vozilo = db.Vozila.Include(v => v.Kategorija).Include(v => v.Proizvoditel).Include(v => v.Sopstvenik).Include(v => v.Komentari).Include(v=>v.Komentari.Select(k=>k.Korisnik)).First(v => v.VoziloId == id);
            if (vozilo == null)
            {
                return HttpNotFound();
            }
            double averageRating=vozilo.Komentari.Select(k => k.Rating).ToList().Average();
            ViewBag.averageRating = averageRating;
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
