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
    public class StudentisController : Controller
    {
        private FakultetEntities db = new FakultetEntities();

        // GET: Studentis
        public ActionResult Index()
        {
            return View(db.Studentis.ToList());
        }

        // GET: Studentis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studenti studenti = db.Studentis.Find(id);
            if (studenti == null)
            {
                return HttpNotFound();
            }
            return View(studenti);
        }

        // GET: Studentis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Studentis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKStudentId,Ime,Prezime,BrIndexa")] Studenti studenti)
        {
            if (ModelState.IsValid)
            {
                db.Studentis.Add(studenti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studenti);
        }

        // GET: Studentis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studenti studenti = db.Studentis.Find(id);
            if (studenti == null)
            {
                return HttpNotFound();
            }
            return View(studenti);
        }

        // POST: Studentis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKStudentId,Ime,Prezime,BrIndexa")] Studenti studenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studenti);
        }

        // GET: Studentis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studenti studenti = db.Studentis.Find(id);
            if (studenti == null)
            {
                return HttpNotFound();
            }
            return View(studenti);
        }

        // POST: Studentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Studenti studenti = db.Studentis.Find(id);
            db.Studentis.Remove(studenti);
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
