using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakultetAPI.ModelEF;

namespace FakultetAPI.Controllers
{
    public class PredmetisController : Controller
    {
        private FakultetEntities db = new FakultetEntities();

        // GET: Predmetis
        public ActionResult Index()
        {
            var predmetis = db.Predmetis.Include(p => p.Profesori);
            return View(predmetis.ToList());
        }

        // GET: Predmetis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmeti predmeti = db.Predmetis.Find(id);
            if (predmeti == null)
            {
                return HttpNotFound();
            }
            return View(predmeti);
        }

        // GET: Predmetis/Create
        public ActionResult Create()
        {
            ViewBag.FKProfesorId = new SelectList(db.Profesoris, "PKProfesorId", "Ime");
            ViewBag.Test = "AAAAAAAAAAAA";
            return View();
        }

        // POST: Predmetis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKPredmetId,Naziv,Godina,FKProfesorId")] Predmeti predmeti)
        {
            if (ModelState.IsValid)
            {
                db.Predmetis.Add(predmeti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKProfesorId = new SelectList(db.Profesoris, "PKProfesorId", "Ime", predmeti.FKProfesorId);
            return View(predmeti);
        }

        // GET: Predmetis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmeti predmeti = db.Predmetis.Find(id);
            if (predmeti == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKProfesorId = new SelectList(db.Profesoris, "PKProfesorId", "Ime", predmeti.FKProfesorId);
            return View(predmeti);
        }

        // POST: Predmetis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKPredmetId,Naziv,Godina,FKProfesorId")] Predmeti predmeti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predmeti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKProfesorId = new SelectList(db.Profesoris, "PKProfesorId", "Ime", predmeti.FKProfesorId);
            return View(predmeti);
        }

        // GET: Predmetis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmeti predmeti = db.Predmetis.Find(id);
            if (predmeti == null)
            {
                return HttpNotFound();
            }
            return View(predmeti);
        }

        // POST: Predmetis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Predmeti predmeti = db.Predmetis.Find(id);
            db.Predmetis.Remove(predmeti);
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
