using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Remote("IsEmailExists", "EmployeeEntry", ErrorMessage = "Email already exists!")]
        public string Email { get; set; }
         [Required]
        public int DesignationId { get; set; }
    }
}