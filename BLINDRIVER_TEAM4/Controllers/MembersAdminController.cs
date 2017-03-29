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
    public class MembersAdminController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: MembersAdmin
        public ActionResult Index(string searchString, string sortOrder)
        {
            ViewBag.RoleSortParm = String.IsNullOrEmpty(sortOrder) ? "role_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "first_asc" ? "first_desc" : "first_asc";
            ViewBag.LastNameSortParm = sortOrder == "last_asc" ? "last_desc" : "last_asc";
            ViewBag.UserNameSortParm = sortOrder == "username_asc" ? "username_desc" : "username_asc";
            ViewBag.DateSortParm = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            IQueryable<Member> members = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                members = db.Members.Where(m => m.FirstName.Contains(searchString)
                                       || m.LastName.Contains(searchString)
                                       || m.Phone.Contains(searchString)
                                       || m.Address.Contains(searchString)
                                       || m.Email.Contains(searchString)
                                       || m.Gender.Contains(searchString)
                                       || m.PostalCode.Contains(searchString)
                                       || m.Role.RoleName.Contains(searchString));
            }
            else
            {
                members = db.Members.Include(m => m.Role);
            }
            switch (sortOrder)
            {
                case "role_desc":
                    members = members.OrderByDescending(m => m.Role.RoleName);
                    break;
                case "first_asc":
                    members = members.OrderBy(m => m.FirstName);
                    break;
                case "first_desc":
                    members = members.OrderByDescending(m => m.FirstName);
                    break;
                case "last_asc":
                    members = members.OrderBy(m => m.LastName);
                    break;
                case "last_desc":
                    members = members.OrderByDescending(m => m.LastName);
                    break;
                case "username_asc":
                    members = members.OrderBy(m => m.Username);
                    break;
                case "username_desc":
                    members = members.OrderByDescending(m => m.Username);
                    break;
                case "date_asc":
                    members = members.OrderBy(m => m.JoinedDay);
                    break;
                case "date_desc":
                    members = members.OrderByDescending(m => m.JoinedDay);
                    break;
                default:
                    members = members.OrderBy(m => m.Role.RoleName);
                    break;
            }
            return View(members.ToList());
        }

        // GET: MembersAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        /* Admin can't create user
        // GET: MembersAdmin/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

       
        // POST: MembersAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,RoleId,JoinedDay,FirstName,LastName,Gender,DOB,Email,Phone,Address,PostalCode,MiddleName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", member.RoleId);
            return View(member);
        }
        */

        // GET: MembersAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", member.RoleId);
            return View(member);
        }

        // POST: MembersAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,RoleId,JoinedDay,FirstName,LastName,Gender,DOB,Email,Phone,Address,PostalCode,MiddleName")] Member member)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.Entry(member).Property(x => x.Password).IsModified = false;
                db.Entry(member).Property(x => x.JoinedDay).IsModified = false;
                // set validate false to save possibly
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", member.RoleId);
            return View(member);
        }

        // GET: MembersAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: MembersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
