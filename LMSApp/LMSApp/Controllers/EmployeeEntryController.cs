using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSApp.Manager;
using LMSApp.Models;

namespace LMSApp.Controllers
{
    public class EmployeeEntryController : Controller
    {
        private EmployeeEntryManager eManager = new EmployeeEntryManager();
        public ActionResult AddEmployee()
        {
            ViewBag.departments = eManager.DesignationList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string Mas = eManager.SaveEmployee(employee);
                ViewBag.Msg = Mas;
                ViewBag.departments = eManager.DesignationList();
            }
            return View();
        }

        public JsonResult IsEmailExists(string email)
        {
            bool isCodeExists = eManager.IsEmailExists(email);

            if (isCodeExists)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }

    }
}