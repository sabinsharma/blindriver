using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLINDRIVER_TEAM4.Models;

namespace BLINDRIVER_TEAM4.Controllers
{
    public class DoctorAvailableDates1Controller : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: DoctorAvailableDates1
        public ActionResult Index()
        {
            var doctorAvailableDates = db.DoctorAvailableDates.Include(d => d.Doctor).Include(d => d.Member);
            return View(doctorAvailableDates.ToList());
        }

        // GET: DoctorAvailableDates1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAvailableDate doctorAvailableDate = db.DoctorAvailableDates.Find(id);
            if (doctorAvailableDate == null)
            {
                return HttpNotFound();
            }
            return View(doctorAvailableDate);
        }

        // GET: DoctorAvailableDates1/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName");
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username");
            return View();
        }

        // POST: DoctorAvailableDates1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DoctorId,AvailableDate,EnteredDate,EnteredBy,Active")] DoctorAvailableDate doctorAvailableDate)
        {
            if (ModelState.IsValid)
            {
                db.DoctorAvailableDates.Add(doctorAvailableDate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName", doctorAvailableDate.DoctorId);
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", doctorAvailableDate.EnteredBy);
            return View(doctorAvailableDate);
        }

        // GET: DoctorAvailableDates1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAvailableDate doctorAvailableDate = db.DoctorAvailableDates.Find(id);
            if (doctorAvailableDate == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName", doctorAvailableDate.DoctorId);
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", doctorAvailableDate.EnteredBy);
            return View(doctorAvailableDate);
        }

        // POST: DoctorAvailableDates1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DoctorId,AvailableDate,EnteredDate,EnteredBy,Active")] DoctorAvailableDate doctorAvailableDate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorAvailableDate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName", doctorAvailableDate.DoctorId);
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", doctorAvailableDate.EnteredBy);
            return View(doctorAvailableDate);
        }

        // GET: DoctorAvailableDates1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAvailableDate doctorAvailableDate = db.DoctorAvailableDates.Find(id);
            if (doctorAvailableDate == null)
            {
                return HttpNotFound();
            }
            return View(doctorAvailableDate);
        }

        // POST: DoctorAvailableDates1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorAvailableDate doctorAvailableDate = db.DoctorAvailableDates.Find(id);
            db.DoctorAvailableDates.Remove(doctorAvailableDate);
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
