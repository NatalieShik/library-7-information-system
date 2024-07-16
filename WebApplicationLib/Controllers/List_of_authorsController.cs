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
    public class List_of_authorsController : Controller
    {
        private LibrarySystemEntities db = new LibrarySystemEntities();

        // GET: List_of_authors
        public ActionResult Index()
        {
            var list_of_authors = db.List_of_authors.Include(l => l.Authors).Include(l => l.Library_catalog);
            return View(list_of_authors.ToList());
        }

        // GET: List_of_authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_of_authors list_of_authors = db.List_of_authors.Find(id);
            if (list_of_authors == null)
            {
                return HttpNotFound();
            }
            return View(list_of_authors);
        }

        // GET: List_of_authors/Create
        public ActionResult Create()
        {
            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Surname");
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title");
            return View();
        }

        // POST: List_of_authors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Library_catalog_Id,Author_Id,Id")] List_of_authors list_of_authors)
        {
            if (ModelState.IsValid)
            {
                db.List_of_authors.Add(list_of_authors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Surname", list_of_authors.Author_Id);
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title", list_of_authors.Library_catalog_Id);
            return View(list_of_authors);
        }

        // GET: List_of_authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_of_authors list_of_authors = db.List_of_authors.Find(id);
            if (list_of_authors == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Surname", list_of_authors.Author_Id);
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title", list_of_authors.Library_catalog_Id);
            return View(list_of_authors);
        }

        // POST: List_of_authors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Library_catalog_Id,Author_Id,Id")] List_of_authors list_of_authors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list_of_authors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_Id = new SelectList(db.Authors, "Id", "Surname", list_of_authors.Author_Id);
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title", list_of_authors.Library_catalog_Id);
            return View(list_of_authors);
        }

        // GET: List_of_authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_of_authors list_of_authors = db.List_of_authors.Find(id);
            if (list_of_authors == null)
            {
                return HttpNotFound();
            }
            return View(list_of_authors);
        }

        // POST: List_of_authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List_of_authors list_of_authors = db.List_of_authors.Find(id);
            db.List_of_authors.Remove(list_of_authors);
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
