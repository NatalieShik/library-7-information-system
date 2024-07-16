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
    public class Register_of_copiesController : Controller
    {
        private LibrarySystemEntities db = new LibrarySystemEntities();

        // GET: Register_of_copies
        public ActionResult Index()
        {
            var register_of_copies = db.Register_of_copies.Include(r => r.Library_catalog).Include(r => r.Registration_list);
            return View(register_of_copies.ToList());
        }

        // GET: Register_of_copies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register_of_copies register_of_copies = db.Register_of_copies.Find(id);
            if (register_of_copies == null)
            {
                return HttpNotFound();
            }
            return View(register_of_copies);
        }

        // GET: Register_of_copies/Create
        public ActionResult Create()
        {
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title");
            ViewBag.Last_reader = new SelectList(db.Registration_list, "Library_card_number", "Surname");
            return View();
        }

        // POST: Register_of_copies/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Issued_to,When_issued,When_must_be_returned,Is_returned,Last_reader,Fine_paid,Library_catalog_Id")] Register_of_copies register_of_copies)
        {
            if (ModelState.IsValid)
            {
                db.Register_of_copies.Add(register_of_copies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title", register_of_copies.Library_catalog_Id);
            ViewBag.Last_reader = new SelectList(db.Registration_list, "Library_card_number", "Surname", register_of_copies.Last_reader);
            return View(register_of_copies);
        }

        // GET: Register_of_copies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register_of_copies register_of_copies = db.Register_of_copies.Find(id);
            if (register_of_copies == null)
            {
                return HttpNotFound();
            }
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title", register_of_copies.Library_catalog_Id);
            ViewBag.Last_reader = new SelectList(db.Registration_list, "Library_card_number", "Surname", register_of_copies.Last_reader);
            return View(register_of_copies);
        }

        // POST: Register_of_copies/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Issued_to,When_issued,When_must_be_returned,Is_returned,Last_reader,Fine_paid,Library_catalog_Id")] Register_of_copies register_of_copies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register_of_copies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Library_catalog_Id = new SelectList(db.Library_catalog, "Id", "Title", register_of_copies.Library_catalog_Id);
            ViewBag.Last_reader = new SelectList(db.Registration_list, "Library_card_number", "Surname", register_of_copies.Last_reader);
            return View(register_of_copies);
        }

        // GET: Register_of_copies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register_of_copies register_of_copies = db.Register_of_copies.Find(id);
            if (register_of_copies == null)
            {
                return HttpNotFound();
            }
            return View(register_of_copies);
        }

        // POST: Register_of_copies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register_of_copies register_of_copies = db.Register_of_copies.Find(id);
            db.Register_of_copies.Remove(register_of_copies);
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
