﻿using System;
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
    public class SopstveniksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sopstveniks
        public ActionResult Index()
        {
            if (User.IsInRole("Owner"))
            {
                string email = User.Identity.GetUserName();
                var SopstvenikId = db.Sopstvenici.Where(s => s.email == email).FirstOrDefault().SopstvenikId;
                return RedirectToAction("Details", new { id=SopstvenikId });
                //var sopstvenik = db.sopstvenici.where(s => s.sopstvenikid == sopstvenikid);

                //return view(sopstvenik.tolist());
            }
            return View(db.Sopstvenici.Include(s=>s.Vozila).ToList());
        }

        // GET: Sopstveniks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sopstvenik sopstvenik = db.Sopstvenici.Find(id);
            if (sopstvenik == null)
            {
                return HttpNotFound();
            }
            return View(sopstvenik);
        }

        /*
        [Authorize(Roles = "Administrator, Owner")]
        // GET: Sopstveniks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sopstveniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SopstvenikId,Name,LastName,Address,Age,email")] Sopstvenik sopstvenik)
        {
            if (ModelState.IsValid)
            {
                db.Sopstvenici.Add(sopstvenik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sopstvenik);
        }
        */

        [Authorize(Roles ="User,Administrator")]
        public ActionResult PrikaziVozilaSopstvenik(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sopstvenik sopstvenik = db.Sopstvenici.Include(k => k.Vozila).Where(k => k.SopstvenikId == id).First();
            if (sopstvenik == null)
            {
                return HttpNotFound();
            }

            return View(sopstvenik);
        }




        [Authorize(Roles = "Administrator, Owner")]
        // GET: Sopstveniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sopstvenik sopstvenik = db.Sopstvenici.Find(id);
            if (sopstvenik == null)
            {
                return HttpNotFound();
            }
            return View(sopstvenik);
        }

        // POST: Sopstveniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SopstvenikId,Name,LastName,Address,Age,email")] Sopstvenik sopstvenik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sopstvenik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sopstvenik);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Sopstveniks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sopstvenik sopstvenik = db.Sopstvenici.Find(id);
            if (sopstvenik == null)
            {
                return HttpNotFound();
            }
            return View(sopstvenik);
        }

        // POST: Sopstveniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sopstvenik sopstvenik = db.Sopstvenici.Find(id);
            db.Sopstvenici.Remove(sopstvenik);
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
