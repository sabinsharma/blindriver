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
    public class VolunteerViewController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();
        // GET: VolunteerView
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