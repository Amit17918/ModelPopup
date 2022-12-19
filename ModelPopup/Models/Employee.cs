using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelPopup.Models
{
    public class Employee
    {
        public String id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public String Name { get; set; }

        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Email is required")]
        public String Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No.")]
        public String Phone_No { get; set; }

        public String Address { get; set; }

        public String state { get; set; }

        public String city { get; set; }

        public List<Employee> empList = new List<Employee>();

        [Display(Name = "Agree?")]
        public bool Agree { get; set; }

        public List<SelectListItem> cityList = new List<SelectListItem>();

        public List<SelectListItem> stateList = new List<SelectListItem>();


        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public class MustBeTrueAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value != null && value is bool && (bool)value;
            }
        }
    }
}