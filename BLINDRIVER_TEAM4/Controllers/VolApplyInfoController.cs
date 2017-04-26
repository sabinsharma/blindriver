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
    public class VolApplyInfoController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: VolApplyInfo
        public ActionResult Index()
        {
            var volunteerInfoes = db.VolunteerInfoes.Include(v => v.City).Include(v => v.VoluteerPost);
            return View(volunteerInfoes.ToList());
        }

        // GET: VolApplyInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerInfo volunteerInfo = db.VolunteerInfoes.Find(id);
            if (volunteerInfo == null)
            {
                return HttpNotFound();
            }
            return View(volunteerInfo);
        }

        // GET: VolApplyInfo/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            ViewBag.VolunteerPostId = new SelectList(db.VoluteerPosts, "Id", "Title");
            return View();
        }

        // POST: VolApplyInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplyDate,FirstName,MiddleName,LastName,Email,Phone,CityId,Address,PostalCode,Description,PublishDate,VolunteerPostId")] VolunteerInfo volunteerInfo)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerInfoes.Add(volunteerInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", volunteerInfo.CityId);
            ViewBag.VolunteerPostId = new SelectList(db.VoluteerPosts, "Id", "Title", volunteerInfo.VolunteerPostId);
            return View(volunteerInfo);
        }

        // GET: VolApplyInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerInfo volunteerInfo = db.VolunteerInfoes.Find(id);
            if (volunteerInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", volunteerInfo.CityId);
            ViewBag.VolunteerPostId = new SelectList(db.VoluteerPosts, "Id", "Title", volunteerInfo.VolunteerPostId);
            return View(volunteerInfo);
        }

        // POST: VolApplyInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApplyDate,FirstName,MiddleName,LastName,Email,Phone,CityId,Address,PostalCode,Description,PublishDate,VolunteerPostId")] VolunteerInfo volunteerInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", volunteerInfo.CityId);
            ViewBag.VolunteerPostId = new SelectList(db.VoluteerPosts, "Id", "Title", volunteerInfo.VolunteerPostId);
            return View(volunteerInfo);
        }

        // GET: VolApplyInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerInfo volunteerInfo = db.VolunteerInfoes.Find(id);
            if (volunteerInfo == null)
            {
                return HttpNotFound();
            }
            return View(volunteerInfo);
        }

        // POST: VolApplyInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VolunteerInfo volunteerInfo = db.VolunteerInfoes.Find(id);
            db.VolunteerInfoes.Remove(volunteerInfo);
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
