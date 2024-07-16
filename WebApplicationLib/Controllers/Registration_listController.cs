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
    public class Registration_listController : Controller
    {
        private LibrarySystemEntities db = new LibrarySystemEntities();

        // GET: Registration_list
        public ActionResult Index()
        {
            return View(db.Registration_list.ToList());
        }

        // GET: Registration_list/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration_list registration_list = db.Registration_list.Find(id);
            if (registration_list == null)
            {
                return HttpNotFound();
            }
            return View(registration_list);
        }

        // GET: Registration_list/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registration_list/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Library_card_number,Surname,Name,Patronymic,Card_issue_date,Reregistration_date")] Registration_list registration_list)
        {
            if (ModelState.IsValid)
            {
                db.Registration_list.Add(registration_list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registration_list);
        }

        // GET: Registration_list/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration_list registration_list = db.Registration_list.Find(id);
            if (registration_list == null)
            {
                return HttpNotFound();
            }
            return View(registration_list);
        }

        // POST: Registration_list/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Library_card_number,Surname,Name,Patronymic,Card_issue_date,Reregistration_date")] Registration_list registration_list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration_list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registration_list);
        }

        // GET: Registration_list/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration_list registration_list = db.Registration_list.Find(id);
            if (registration_list == null)
            {
                return HttpNotFound();
            }
            return View(registration_list);
        }

        // POST: Registration_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration_list registration_list = db.Registration_list.Find(id);
            db.Registration_list.Remove(registration_list);
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
