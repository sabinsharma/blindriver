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
    public class DoctorAppointmentsController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: DoctorAppointments
        public ActionResult Index()
        {
            var doctorAppointments = db.DoctorAppointments.Include(d => d.DoctorAvailableDate);
            return View(doctorAppointments.ToList());
        }

        // GET: DoctorAppointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAppointment doctorAppointment = db.DoctorAppointments.Find(id);
            if (doctorAppointment == null)
            {
                return HttpNotFound();
            }
            return View(doctorAppointment);
        }

        // GET: DoctorAppointments/Create
        public ActionResult Create()
        {
            ViewBag.DoctorAvailableTimeId = new SelectList(db.DoctorAvailableDates, "Id", "Id");
            return View();
        }

        // POST: DoctorAppointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DoctorAvailableTimeId,MemberId,BookingDate")] DoctorAppointment doctorAppointment)
        {
            if (ModelState.IsValid)
            {
                db.DoctorAppointments.Add(doctorAppointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorAvailableTimeId = new SelectList(db.DoctorAvailableDates, "Id", "Id", doctorAppointment.DoctorAvailableTimeId);
            return View(doctorAppointment);
        }

        // GET: DoctorAppointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAppointment doctorAppointment = db.DoctorAppointments.Find(id);
            if (doctorAppointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorAvailableTimeId = new SelectList(db.DoctorAvailableDates, "Id", "Id", doctorAppointment.DoctorAvailableTimeId);
            return View(doctorAppointment);
        }

        // POST: DoctorAppointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DoctorAvailableTimeId,MemberId,BookingDate")] DoctorAppointment doctorAppointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorAppointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorAvailableTimeId = new SelectList(db.DoctorAvailableDates, "Id", "Id", doctorAppointment.DoctorAvailableTimeId);
            return View(doctorAppointment);
        }

        // GET: DoctorAppointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorAppointment doctorAppointment = db.DoctorAppointments.Find(id);
            if (doctorAppointment == null)
            {
                return HttpNotFound();
            }
            return View(doctorAppointment);
        }

        // POST: DoctorAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorAppointment doctorAppointment = db.DoctorAppointments.Find(id);
            db.DoctorAppointments.Remove(doctorAppointment);
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
