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
    public class VoluteerPostsController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: VoluteerPosts
        public ActionResult Index()
        {
            var voluteerPosts = db.VoluteerPosts.Include(v => v.Member);
            return View(voluteerPosts.ToList());
        }

        // GET: VoluteerPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoluteerPost voluteerPost = db.VoluteerPosts.Find(id);
            if (voluteerPost == null)
            {
                return HttpNotFound();
            }
            return View(voluteerPost);
        }

        // GET: VoluteerPosts/Create
        public ActionResult Create()
        {
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username");
            return View();
        }

        // POST: VoluteerPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,LastSubmitDate,PublishDate,Active,Published,EnteredDate,EnteredBy")] VoluteerPost voluteerPost)
        {
            if (ModelState.IsValid)
            {
                db.VoluteerPosts.Add(voluteerPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", voluteerPost.EnteredBy);
            return View(voluteerPost);
        }

        // GET: VoluteerPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoluteerPost voluteerPost = db.VoluteerPosts.Find(id);
            if (voluteerPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", voluteerPost.EnteredBy);
            return View(voluteerPost);
        }

        // POST: VoluteerPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,LastSubmitDate,PublishDate,Active,Published,EnteredDate,EnteredBy")] VoluteerPost voluteerPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voluteerPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", voluteerPost.EnteredBy);
            return View(voluteerPost);
        }

        // GET: VoluteerPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoluteerPost voluteerPost = db.VoluteerPosts.Find(id);
            if (voluteerPost == null)
            {
                return HttpNotFound();
            }
            return View(voluteerPost);
        }

        // POST: VoluteerPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoluteerPost voluteerPost = db.VoluteerPosts.Find(id);
            db.VoluteerPosts.Remove(voluteerPost);
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
