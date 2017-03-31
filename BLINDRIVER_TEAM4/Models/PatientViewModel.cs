using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLINDRIVER_TEAM4.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name", AutoGenerateFilter = true), Required, RegularExpression("^[A-Za-z]*$", ErrorMessage = "Invalid Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name", AutoGenerateFilter = true), RegularExpression("^[A-Za-z]*$", ErrorMessage = "Invalid Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Lase Name", AutoGenerateFilter = true), Required, RegularExpression("^[A-Za-z]*$", ErrorMessage = "Invalid Name")]
        public string LastName { get; set; }

        [Display(Name = "Street Address"), Required]
        public string Address { get; set; }
        [Display(Name = "Postal Code"), Required, RegularExpression("^[A-Za-z]+[1-9]+[A-Za-z]+(/s|-)+[1-9]+[A-Za-z]+[1-9]$", ErrorMessage = "Invalid Format. Format is ")]
        public string PostalCode { get; set; }
        [Display(AutoGenerateField = false)]
        public Nullable<int> EnteredBy { get; set; }
        public bool Active { get; set; }
    }
}