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
    public class StudentiPredmetisController : Controller
    {
        private FakultetEntities db = new FakultetEntities();

        // GET: StudentiPredmetis
        public ActionResult Index()
        {
            var studentiPredmetis = db.StudentiPredmetis.Include(s => s.Predmeti).Include(s => s.Studenti);
            return View(studentiPredmetis.ToList());
        }

        public ActionResult OcjeneStudenta(int StudentID)
        {
            var studentiPredmetis = db.StudentiPredmetis.Where(k => k.FKStudentId == StudentID);
            return PartialView(studentiPredmetis.ToList());
        }

        // GET: StudentiPredmetis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentiPredmeti studentiPredmeti = db.StudentiPredmetis.Find(id);
            if (studentiPredmeti == null)
            {
                return HttpNotFound();
            }
            return View(studentiPredmeti);
        }

        // GET: StudentiPredmetis/Create
        public ActionResult Create()
        {
            ViewBag.FKPredmetId = new SelectList(db.Predmetis, "PKPredmetId", "Naziv");
            ViewBag.FKStudentId = new SelectList(db.Studentis, "PKStudentId", "Ime");
            return View();
        }

        // POST: StudentiPredmetis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FKStudentId,FKPredmetId,Ocjena,Datum,PKStudPredId")] StudentiPredmeti studentiPredmeti)
        {
            if (ModelState.IsValid)
            {
                db.StudentiPredmetis.Add(studentiPredmeti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKPredmetId = new SelectList(db.Predmetis, "PKPredmetId", "Naziv", studentiPredmeti.FKPredmetId);
            ViewBag.FKStudentId = new SelectList(db.Studentis, "PKStudentId", "Ime", studentiPredmeti.FKStudentId);
            return View(studentiPredmeti);
        }

        // GET: StudentiPredmetis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentiPredmeti studentiPredmeti = db.StudentiPredmetis.Find(id);
            if (studentiPredmeti == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKPredmetId = new SelectList(db.Predmetis, "PKPredmetId", "Naziv", studentiPredmeti.FKPredmetId);
            ViewBag.FKStudentId = new SelectList(db.Studentis, "PKStudentId", "Ime", studentiPredmeti.FKStudentId);
            return View(studentiPredmeti);
        }

        // POST: StudentiPredmetis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FKStudentId,FKPredmetId,Ocjena,Datum,PKStudPredId")] StudentiPredmeti studentiPredmeti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentiPredmeti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKPredmetId = new SelectList(db.Predmetis, "PKPredmetId", "Naziv", studentiPredmeti.FKPredmetId);
            ViewBag.FKStudentId = new SelectList(db.Studentis, "PKStudentId", "Ime", studentiPredmeti.FKStudentId);
            return View(studentiPredmeti);
        }

        // GET: StudentiPredmetis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentiPredmeti studentiPredmeti = db.StudentiPredmetis.Find(id);
            if (studentiPredmeti == null)
            {
                return HttpNotFound();
            }
            return View(studentiPredmeti);
        }

        // POST: StudentiPredmetis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentiPredmeti studentiPredmeti = db.StudentiPredmetis.Find(id);
            db.StudentiPredmetis.Remove(studentiPredmeti);
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
