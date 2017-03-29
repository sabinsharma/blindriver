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
    public class DoctorAvailableTimesController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: DoctorAvailableTimes
        public ActionResult Index()
        {
            var doctorAvailableTimes = db.DoctorAvailableTimes.Include(d => d.DoctorAvailableDate);
            return View(doctorAvailableTimes.ToList());
        }

        // GET: DoctorAvailableTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAvailableTime doctorAvailableTime = db.DoctorAvailableTimes.Find(id);
            if (doctorAvailableTime == null)
            {
                return HttpNotFound();
            }
            return View(doctorAvailableTime);
        }

        // GET: DoctorAvailableTimes/Create
        public ActionResult Create()
        {
            ViewBag.DoctorAvailableDateId = new SelectList(db.DoctorAvailableDates, "Id", "Id");
            return View();
        }

        // POST: DoctorAvailableTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DoctorAvailableDateId,AvailableFrom,AvailableUntil,Active")] DoctorAvailableTime doctorAvailableTime)
        {
            if (ModelState.IsValid)
            {
                db.DoctorAvailableTimes.Add(doctorAvailableTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorAvailableDateId = new SelectList(db.DoctorAvailableDates, "Id", "Id", doctorAvailableTime.DoctorAvailableDateId);
            return View(doctorAvailableTime);
        }

        // GET: DoctorAvailableTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAvailableTime doctorAvailableTime = db.DoctorAvailableTimes.Find(id);
            if (doctorAvailableTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorAvailableDateId = new SelectList(db.DoctorAvailableDates, "Id", "Id", doctorAvailableTime.DoctorAvailableDateId);
            return View(doctorAvailableTime);
        }

        // POST: DoctorAvailableTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DoctorAvailableDateId,AvailableFrom,AvailableUntil,Active")] DoctorAvailableTime doctorAvailableTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorAvailableTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorAvailableDateId = new SelectList(db.DoctorAvailableDates, "Id", "Id", doctorAvailableTime.DoctorAvailableDateId);
            return View(doctorAvailableTime);
        }

        // GET: DoctorAvailableTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAvailableTime doctorAvailableTime = db.DoctorAvailableTimes.Find(id);
            if (doctorAvailableTime == null)
            {
                return HttpNotFound();
            }
            return View(doctorAvailableTime);
        }

        // POST: DoctorAvailableTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorAvailableTime doctorAvailableTime = db.DoctorAvailableTimes.Find(id);
            db.DoctorAvailableTimes.Remove(doctorAvailableTime);
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
