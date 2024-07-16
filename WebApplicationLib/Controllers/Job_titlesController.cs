using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationLib.Models;

namespace WebApplicationLib.Controllers
{
    public class Job_titlesController : Controller
    {
        private LibrarySystemEntities db = new LibrarySystemEntities();

        // GET: Job_titles
        public ActionResult Index()
        {
            return View(db.Job_titles.ToList());
        }

        // GET: Job_titles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_titles job_titles = db.Job_titles.Find(id);
            if (job_titles == null)
            {
                return HttpNotFound();
            }
            return View(job_titles);
        }

        // GET: Job_titles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Job_titles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] Job_titles job_titles)
        {
            if (ModelState.IsValid)
            {
                db.Job_titles.Add(job_titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job_titles);
        }

        // GET: Job_titles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_titles job_titles = db.Job_titles.Find(id);
            if (job_titles == null)
            {
                return HttpNotFound();
            }
            return View(job_titles);
        }

        // POST: Job_titles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] Job_titles job_titles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_titles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job_titles);
        }

        // GET: Job_titles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_titles job_titles = db.Job_titles.Find(id);
            if (job_titles == null)
            {
                return HttpNotFound();
            }
            return View(job_titles);
        }

        // POST: Job_titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job_titles job_titles = db.Job_titles.Find(id);
            db.Job_titles.Remove(job_titles);
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
