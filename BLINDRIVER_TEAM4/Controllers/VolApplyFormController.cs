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
    public class VolApplyFormController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

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
                return RedirectToAction("Index", "VolunteerView");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", volunteerInfo.CityId);
            ViewBag.VolunteerPostId = new SelectList(db.VoluteerPosts, "Id", "Title", volunteerInfo.VolunteerPostId);
            return View(volunteerInfo);
        }
        [HttpGet]
        public ActionResult Apply(int ? Id)
        {
            ViewBag.postId = Id;
            return RedirectToAction("Create");
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