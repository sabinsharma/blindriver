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
    [Authorize(Roles = "Admin, Staff, Member, Visitor")]
    public class EventsController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: Events
        public ActionResult Index()
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            int id = Convert.ToInt32(ticket.Name.Split('|')[1]);
            var events = db.Events.Include(e => e.Member).Join(db.EventMemberStatus, e=>e.Id, ems=>ems.EventId, (e,ems) => new { e, ems }).Where(j=>j.ems.MemberId == id).Select(e=>e.e).OrderBy(e=>e.DateTime);
            //var events = db.Events.Include(e => e.Member);
            return View(events.ToList());
        }

        // GET: Events/Details/5
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

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.EnteredBy = new SelectList(db.Members.Where(m => m.RoleId > 0), "Id", "Username");
            return View();
        }

        // POST: Events/Create
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

        // GET: Events/Edit/5
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
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", @event.EnteredBy);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,DateTime,NumberInvited,NumberGoing,NumberDeclined,Active,EnteredDate,EnteredBy")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnteredBy = new SelectList(db.Members, "Id", "Username", @event.EnteredBy);
            return View(@event);
        }

        // GET: Events/Delete/5
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

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
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

        public ActionResult Participate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            int userId = Convert.ToInt32(ticket.Name.Split('|')[1]);
            EventMemberStatu @memberStatus = db.EventMemberStatus.Where(e => e.EventId == id && e.MemberId == userId).Select(e=>e).FirstOrDefault();
            ViewBag.MemberStatus = @memberStatus;
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Participate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participate([Bind(Include = "Id,Title,Content,DateTime,NumberInvited,NumberGoing,NumberDeclined,EnteredBy,Place")]Event @event, string MemberStatus, string OldStatus)
        {
            if ((MemberStatus == "Going" || MemberStatus == "Decline") && MemberStatus != OldStatus)
            {
                if (ModelState.IsValid)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    int userId = Convert.ToInt32(ticket.Name.Split('|')[1]);

                    var EventStatus = new EventMemberStatu() { EventId = @event.Id, MemberId = userId, Status = MemberStatus };

                    db.EventMemberStatus.Attach(EventStatus);
                    db.Entry(EventStatus).Property(x => x.Status).IsModified = true;

                    switch (MemberStatus)
                    {
                        case "Going":
                            @event.NumberGoing += 1;
                            if (OldStatus != "Invited")
                                @event.NumberDeclined -= 1;
                            break;
                        case "Decline":
                            @event.NumberDeclined += 1;
                            if (OldStatus != "Invited")
                                @event.NumberGoing -= 1;
                            break;
                        default:
                            break;
                    }

                    db.Entry(@event).State = EntityState.Modified;
                    db.Entry(@event).Property(x => x.EnteredDate).IsModified = false;                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(@event);
            }           
            return View(@event);
        }
    }
}
