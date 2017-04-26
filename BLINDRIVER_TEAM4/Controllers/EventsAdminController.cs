using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLINDRIVER_TEAM4.Models;
using System.Web.Security;

namespace BLINDRIVER_TEAM4.Controllers
{
    [Authorize(Roles = "Admin, Staff, Member")]
    public class EventsAdminController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: EventsAdmin
        public ActionResult Index()
        {
            // just show all the active Events
            var events = db.Events.Include(e => e.Member).Where(e => e.Active).OrderBy(e=>e.DateTime);
            return View(events.ToList());
        }

        // GET: EventsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }            
            return View(@event);
        }

        // GET: EventsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.EnteredBy = new SelectList(db.Members.OrderByDescending(m => m.RoleId).Select(m => new {
                Id = m.Id,
                Fullname = m.FirstName + " " + m.LastName + " - " + m.Role.RoleName
            }), "Id", "Fullname");
            return View();
        }

        // POST: EventsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,DateTime,Place")] Event @event, int[] Members)
        {
            if (ModelState.IsValid)
            {
                @event.EnteredDate = DateTime.Now.Date;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                @event.EnteredBy = Convert.ToInt32(ticket.Name.Split('|')[1]);
                @event.Active = true;
                @event.NumberInvited = Members.Count();
                db.Events.Add(@event);
                db.SaveChanges();

                int CurrentEventId = db.Events.OrderByDescending(e => e.Id).FirstOrDefault().Id;
                foreach (var m in Members)
                {
                    EventMemberStatu evm = new EventMemberStatu();
                    evm.EventId = CurrentEventId;
                    evm.MemberId = m;
                    evm.Status = "Invited";
                    db.EventMemberStatus.Add(evm);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", @event.EnteredBy);
            return View(@event);
        }

        // GET: EventsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            var existedMembers = db.EventMemberStatus.Where(e => e.EventId == id).Select(e => e.MemberId).ToList();

            ViewBag.NotYetInvited = new SelectList(db.Members.Where(x => !existedMembers.Contains(x.Id)).Select(m => new {
                Id = m.Id,
                Fullname = m.FirstName + " " + m.LastName + " - " + m.Role.RoleName
            }), "Id", "Fullname");

            //ViewBag.NotYetInvited = new SelectList(db.Members.OrderByDescending(m => m.RoleId).Select(m => new {
            //    Id = (int)m.Id,
            //    Fullname = m.FirstName + " " + m.LastName + " - " + m.Role.RoleName
            //}).Where(x => !existedMembers.Contains(x.Id)), "Id", "Fullname");
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", @event.EnteredBy);
            return View(@event);
        }

        // POST: EventsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,DateTime,NumberInvited,Active,Place")] Event @event, int[] Members)
        {
            if (ModelState.IsValid)
            {
                @event.EnteredDate = DateTime.Now.Date;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                @event.EnteredBy = Convert.ToInt32(ticket.Name.Split('|')[1]);
                @event.NumberInvited += Members.Count();
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", @event.EnteredBy);
            return View(@event);
        }

        // GET: EventsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: EventsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            // delete all the member status in the table EventMemberStatus before deleting the Event
            db.EventMemberStatus.RemoveRange(db.EventMemberStatus.Where(e => e.EventId == id));
            db.Events.Remove(@event);
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
