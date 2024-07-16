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
    public class Library_catalogController : Controller
    {
        private LibrarySystemEntities db = new LibrarySystemEntities();

        // GET: Library_catalog
        public ActionResult Index()
        {
            return View(db.Library_catalog.ToList());
        }

        // GET: Library_catalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library_catalog library_catalog = db.Library_catalog.Find(id);
            if (library_catalog == null)
            {
                return HttpNotFound();
            }
            return View(library_catalog);
        }

        // GET: Library_catalog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Library_catalog/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Photo,Library_cipher,Year_of_publication,Place_of_publication,Publishing_house_name,Total,Number_of_available")] Library_catalog library_catalog)
        {
            if (ModelState.IsValid)
            {
                db.Library_catalog.Add(library_catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(library_catalog);
        }

        // GET: Library_catalog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library_catalog library_catalog = db.Library_catalog.Find(id);
            if (library_catalog == null)
            {
                return HttpNotFound();
            }
            return View(library_catalog);
        }

        // POST: Library_catalog/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Photo,Library_cipher,Year_of_publication,Place_of_publication,Publishing_house_name,Total,Number_of_available")] Library_catalog library_catalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(library_catalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(library_catalog);
        }

        // GET: Library_catalog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library_catalog library_catalog = db.Library_catalog.Find(id);
            if (library_catalog == null)
            {
                return HttpNotFound();
            }
            return View(library_catalog);
        }

        // POST: Library_catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Library_catalog library_catalog = db.Library_catalog.Find(id);
            db.Library_catalog.Remove(library_catalog);
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
