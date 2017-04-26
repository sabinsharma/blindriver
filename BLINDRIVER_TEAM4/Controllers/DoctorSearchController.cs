using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLINDRIVER_TEAM4.Models;

namespace BLINDRIVER_TEAM4.Controllers
{
    public class DoctorSearchController : Controller
    {
        private BlindRiverContext db = new BlindRiverContext();

        // GET: DoctorSearch
        public ActionResult Index()
        {
            this.SetDepartmentItems();

            // Send Enpty ViewModel
            ViewBag.ddlDepartment = new SelectList(db.Departments.Select(d => new { Id = d.Id, DepartmentName = d.DepartmentName }), "Id", "DepartmentName");
            var model = new DoctorSearchViewModel();
            return View(model);
        }
        //POST: DoctorSearch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Gender, DepartmentId")] DoctorSearchViewModel model, string DepartmentName, string FirstName, string LastName, string Gender, string Language)
        {
            if (ModelState.IsValid)
            {
                int dId = Convert.ToInt32(DepartmentName);
                /*var list = db.doctors_tbl.Where(Id =>
                    (string.IsNullOrEmpty(model.gender) || Id.first_name.Contains(model.gender))).ToList();
                model.Doctors = list;
                */
                ViewBag.ddlDepartment = new SelectList(db.Departments.Select(d => new { Id = d.Id, DepartmentName = d.DepartmentName }), "Id", "DepartmentName");

                var list = db.Doctors.Where(item =>
                    (string.IsNullOrEmpty(FirstName) || item.FirstName.Contains(FirstName))
                    && (string.IsNullOrEmpty(LastName) || item.LastName.Contains(LastName))
                    && (string.IsNullOrEmpty(Gender) || item.Gender.Contains(Gender))
                    && (string.IsNullOrEmpty(Language) || item.Language.Contains(Language))
                     && item.DepartmentId == dId).ToList();
                model.Doctors = list;
            }
            this.SetDepartmentItems();
            return View(model);
        }

        private void SetDepartmentItems()
        {
            var list = db.Departments.Select(item => new SelectListItem
            {
                Text = item.Id + ":" + item.DepartmentName,
                Value = item.DepartmentName.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = string.Empty,
                Value = "0"
            });

            ViewBag.Id = list;
        }
    }
}
