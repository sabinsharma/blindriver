using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace BLINDRIVER_TEAM4.Models
{
    public class DoctorSearchViewModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("Language")]
        public string Language { get; set; }

        [DisplayName("Year of Experience")]
        public int YearOfExperience { get; set; }

        [DisplayName("Discription text")]
        public string Text { get; set; }

        [DisplayName("Photo")]
        public byte[] Photo { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<DoctorAvailableDate> DoctorAvailableDates { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public List<Doctor> Doctors { get; set; }


    }
}