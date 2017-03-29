//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BLINDRIVER_TEAM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Member
    {
        public Member()
        {
            this.Contacts = new HashSet<Contact>();
            this.Doctors = new HashSet<Doctor>();
            this.DoctorAvailableDates = new HashSet<DoctorAvailableDate>();
            this.Facilities = new HashSet<Facility>();
            this.FAQs = new HashSet<FAQ>();
            this.Locations = new HashSet<Location>();
            this.Services = new HashSet<Service>();
            this.Severities = new HashSet<Severity>();
            this.VoluteerPosts = new HashSet<VoluteerPost>();
            this.Patients = new HashSet<Patient>();
            this.PatientVisitingHours = new HashSet<PatientVisitingHour>();
            this.Reviews = new HashSet<Review>();
            this.ReviewCategories = new HashSet<ReviewCategory>();
            this.VisitingSchedules = new HashSet<VisitingSchedule>();
        }
    
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "Registered Date")]
        [DataType(DataType.Date)]
        public System.DateTime JoinedDay { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "DOB")]
        [Required]
        public System.DateTime DOB { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone format is not valid.")]
        [RegularExpression(@"^([0-9]{3})([0-9]{3})([0-9]{4})$", ErrorMessage = "Phone format is not valid.")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^([A-Z][0-9][A-Z])[-. ]?([0-9][A-Z][0-9])$", ErrorMessage = "Postal Code is not valid.")]
        public string PostalCode { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Profile Image")]
        public string Photo { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<DoctorAvailableDate> DoctorAvailableDates { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
        public virtual ICollection<FAQ> FAQs { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Severity> Severities { get; set; }
        public virtual ICollection<VoluteerPost> VoluteerPosts { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<PatientVisitingHour> PatientVisitingHours { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ReviewCategory> ReviewCategories { get; set; }
        public virtual ICollection<VisitingSchedule> VisitingSchedules { get; set; }
    }
}
