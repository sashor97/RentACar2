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
    public class ProizvoditelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proizvoditels
        public ActionResult Index()
        {
            return View(db.Proizvoditeli.ToList());
        }

        // GET: Proizvoditels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvoditel proizvoditel = db.Proizvoditeli.Find(id);
            if (proizvoditel == null)
            {
                return HttpNotFound();
            }
            return View(proizvoditel);
        }

        [Authorize(Roles ="Administrator, Owner")]
        // GET: Proizvoditels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proizvoditels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProizvoditelId,Name,Year")] Proizvoditel proizvoditel)
        {
            if (ModelState.IsValid)
            {
                db.Proizvoditeli.Add(proizvoditel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvoditel);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Proizvoditels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvoditel proizvoditel = db.Proizvoditeli.Find(id);
            if (proizvoditel == null)
            {
                return HttpNotFound();
            }
            return View(proizvoditel);
        }


        // POST: Proizvoditels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProizvoditelId,Name,Year")] Proizvoditel proizvoditel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvoditel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvoditel);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Proizvoditels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvoditel proizvoditel = db.Proizvoditeli.Find(id);
            if (proizvoditel == null)
            {
                return HttpNotFound();
            }
            return View(proizvoditel);
        }

        // POST: Proizvoditels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvoditel proizvoditel = db.Proizvoditeli.Find(id);
            db.Proizvoditeli.Remove(proizvoditel);
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
